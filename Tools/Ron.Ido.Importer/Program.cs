using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.Common;
using Ron.Ido.Common.Extensions;
using Ron.Ido.Common.Interfaces;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using Ron.Ido.FileStorage;
using Ron.Ido.Importer.NDB.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ron.Ido.Importer
{
    class Program
    {
        private static AppDbContext _appContext;
        private static IFileStorageService _appStorage; 
        private static NostrificationRONContext _nostrContext;
        private static NostrificationStorage _nostrStorage;
        static void Main(string[] args)
        {
            Console.WriteLine("Importer");

            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _appContext = serviceProvider.GetService<AppDbContext>();
            _appStorage = serviceProvider.GetService<IFileStorageService>();
            _nostrContext = serviceProvider.GetService<NostrificationRONContext>();
            _nostrStorage = serviceProvider.GetService<NostrificationStorage>();

            ImportApplyStatuses();
            ImportRoles();
            ImportUsers();
            ImportCountries();
            ImportApplies();
            ImportDuplicates();
            //ImportFiles();
        }

        private static void ImportFiles()
        {
            var maxOldId = _nostrContext.UploadedFiles.Max(f => f.Id);
            var lastConvertedId = _appContext.FileInfos.Any() ? _appContext.FileInfos.Max(f => f.OldId) : 0;

            UploadedFile nextUploadedFile;
            while((nextUploadedFile = _nostrContext.UploadedFiles.FirstOrDefault(f => f.Id > lastConvertedId)) != null){
                var bytes = _nostrStorage.GetFileBytes(nextUploadedFile);
                Guid uid = Guid.Empty;
                if (bytes != null)
                {
                    uid = _appStorage.CreateFile(bytes);
                }

                var login = nextUploadedFile?.User?.Login;
                var userId = login != null ? _appContext.Users.FirstOrDefault(u => u.Login == login)?.Id : null;

                var newFileInfo = new EM.Entities.FileInfo
                {
                    Name = Path.GetFileName(nextUploadedFile.FileName),
                    ContentType = nextUploadedFile.ContentType,
                    CreateTime = nextUploadedFile.UploadTime ?? DateTime.Now,
                    Uid = uid,
                    Size = bytes?.Length ?? 0,
                    Source = nextUploadedFile.Source,
                    OldId = nextUploadedFile.Id,
                    CreatorEmail = nextUploadedFile.CreatorEmail,
                    CreatedById = userId
                };
                _appContext.Add(newFileInfo);
                _appContext.SaveChanges();

                lastConvertedId = nextUploadedFile.Id;
                Console.WriteLine($"{nextUploadedFile.FileName}->{lastConvertedId}:{uid}");
            }
        }

        private static void ImportApplyStatuses()
        {
            foreach (var nStatus in _nostrContext.ApplyStatuses)
            {
                var statusVal = (EM.Enums.ApplyStatusEnum)nStatus.Id;
                string statusValString = Enum.IsDefined(statusVal) ? statusVal.ToString() : null;

                var status = AddEntityIfNotExists(new EM.Entities.ApplyStatus
                {
                    StatusEnumValue = statusValString,
                    Name = nStatus.Name,
                    NameForButton = nStatus.NameForButton,
                    NameForApplier = nStatus.NameForApplier,
                    NameForApplierEng = nStatus.NameForApplierEng,
                    DescriptionForApplier = nStatus.DescriptionForApplier,
                    DescriptionForApplierEng = nStatus.DescriptionForApplierEng,
                    EtapId = nStatus.EtapId,
                    VisibleForApplier = nStatus.VisibleForApplier,
                    OldId = nStatus.Id
                }, item => item.OldId == nStatus.Id);
            }

            var newStatuses = _appContext.ApplyStatuses.ToArray();
            foreach (var status in newStatuses)
            {
                var oldStatus = _nostrContext.ApplyStatuses.FirstOrDefault(s => s.Id == status.OldId);
                if (oldStatus == null)
                    continue;

                var oldStepsStr = oldStatus.AllowStepToStatuses;
                var oldSteps = string.IsNullOrEmpty(oldStepsStr)
                    ? new int[] { }
                    : oldStepsStr.Split(";").Select(v => v.Parse(0));

                var newSteps = oldSteps
                    .Select(v => newStatuses.FirstOrDefault(s => s.OldId == v)?.Id ?? 0)
                    .Where(v => v > 0);

                status.AllowStepToStatuses = newSteps
                    .Select(v => v.ToString())
                    .Join(";");

                _appContext.SaveChanges();
            }
        }

        private static void ImportRoles()
        {
            var getStatusIds = new Func<string, string>(oldIdsString =>
            {
                if (string.IsNullOrEmpty(oldIdsString))
                    return "";

                var oldIds = oldIdsString.Split(";")
                .Select(i => i.Parse(0))
                .ToArray();

                var newIds = oldIds
                .Select(id => _appContext.ApplyStatuses.FirstOrDefault(s => s.OldId == id)?.Id ?? 0)
                .Where(v => v > 0)
                .ToArray();

                return newIds.Select(id => id.ToString()).Join(";");
            });

            var grants = GrantsUtility.AllGrants(typeof(GRANT)).Where(g => g.Permission != EM.Enums.PermissionEnum.NULL);
            foreach(var nRole in _nostrContext.Roles)
            {
                var role = AddEntityIfNotExists(new EM.Entities.Role
                {
                    Name = nRole.Name,
                    ViewApplyStatusesString = getStatusIds(nRole.ViewApplyStatusesString),
                    StepApplyStatusesString = getStatusIds(nRole.StepApplyStatusesString)
                },
                (r => r.Name == nRole.Name));

                var perms = grants
                    .Where(g => GrantsUtility.AnyFlag(g.Value, nRole.Grants))
                    .Select(g => new RolePermission
                    {
                        PermissionId = (long)g.Permission,
                        RoleId = role.Id
                    });

                foreach(var perm in perms)
                {
                    AddEntityIfNotExists(perm, p => p.RoleId == role.Id && p.PermissionId == perm.PermissionId);
                }
            }
        }

        private static void ImportUsers()
        {
            foreach(var nUser in _nostrContext.Users)
            {
                var nameParts = nUser.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var user = AddEntityIfNotExists(new EM.Entities.User
                {
                    SurName = nameParts.Length > 0 ? nameParts[0] : "",
                    FirstName = nameParts.Length > 1 ? nameParts[1] : "",
                    LastName = nameParts.Length > 2 ? nameParts[2] : "",
                    Email = nUser.Email ?? "",
                    Snils = nUser.Snils ?? "",
                    Login = nUser.Login,
                    PasswordHash = (nUser.Password ?? "").GetHashString(),
                    IsBlocked = nUser.Blocked,
                    IsDeleted = nUser.Removed,
                    Remark = nUser.Description
                },
                u => u.Login == nUser.Login);

                foreach(var nUserRole in _nostrContext.UserRoles.Where(ur => ur.UsersId == nUser.Id))
                {
                    var role = _appContext.Roles.FirstOrDefault(r => r.Name == nUserRole.Roles.Name);
                    if (role == null)
                        continue;
                    
                    AddEntityIfNotExists(new EM.Entities.UserRole
                    {
                        RoleId = role.Id,
                        UserId = user.Id
                    },
                    ur => ur.RoleId == role.Id && ur.UserId == user.Id);
                }
            }
        }

        private static void ImportCountries()
        {
            foreach(var region in _nostrContext.Regions)
            {
                AddEntityIfNotExists(
                    new EM.Entities.Region
                    {
                        Name = region.Name,
                        OrderNum = region.OrderNum,
                        OldId = region.Id
                    },
                    item => item.OldId == region.Id);
            }

            foreach(var country in _nostrContext.Countries)
            {
                AddEntityIfNotExists(
                    new EM.Entities.Country
                    {
                        Name = country.Name,
                        NameEng = country.NameEng,
                        FullName = country.FullName,
                        LegalizationComment = country.LegalizationComment,
                        LegalizationId = country.LegalizationId,
                        LegalizationNeeded = country.LegalizationNeeded,
                        RegionId = _appContext.Regions.FirstOrDefault(r => r.OldId == country.RegionId)?.Id,
                        OrderNum = country.OrderNum,
                        A2code = country.A2code,
                        A3code = country.A3code,
                        EiisCode = country.EiisCode,
                        IsgaCode = country.IsgaCode,
                        OksmCode = country.OksmCode,
                        CoordX = country.CoordX,
                        CoordY = country.CoordY,
                        OldId = country.Id
                    },
                    item => item.OldId == country.Id);
            }
        }

        private static void ImportApplies()
        {
            var nApplies = _nostrContext.Applies
                .Include(a => a.ApplyStatusHistories)
                .Include(a => a.ApplyDocuments)
                .Include(a => a.ApplyComments)
                .OrderByDescending(a => a.CreateDate).Take(100)
                .ToArray();

            foreach(var nApply in nApplies)
            {
                CreateApply(nApply);
            }
        }

        private static void ImportDuplicates()
        {
            var nDuplicates = _nostrContext.Duplicates
                .Include(d => d.DuplicateStatusHistories)
                .OrderByDescending(d => d.CreateDate)
                .ToArray();

            foreach(var nD in nDuplicates)
            {
                if (_appContext.Duplicates.Any(d => d.BarCode == nD.BarCode))
                    continue;

                var duplicate = new EM.Entities.Duplicate
                {
                    BarCode = nD.BarCode,
                    Address = nD.Address,
                    Block = nD.Block,
                    Building = nD.Building,
                    CityName = nD.CityName,
                    Corpus = nD.Corpus,
                    CreateTime = nD.CreateDate,
                    CreateUserId = GetUserId(nD.CreatedById),
                    CreatorCountryId = GetCountryId(nD.CreatorCountryId),
                    DocCountryId = GetCountryId(nD.DocCountryId),
                    DocFullName = nD.DocFullName,
                    DocumentDate = nD.DocumentDate,
                    DocumentTypeId = nD.DocumentTypeId,
                    Email = nD.Email,
                    Flat = nD.Flat,
                    FullName = nD.FullName,
                    HandoutTime = nD.HandoutDate,
                    HandoutUserId = GetUserId(nD.HandoutById),
                    IsEnglish = nD.IsEnglish,
                    MailIndex = nD.MailIndex,
                    Note = nD.Note,
                    Phones = nD.Phones,
                    ReturnOriginalsFormId = nD.ReturnOriginalsFormId,
                    ReturnOriginalsPostAddress = nD.ReturnOriginalsPostAddress,
                    SchoolName = nD.SchoolName,
                    StatusId = nD.StatusId,
                    Storage = nD.Storage,
                    Street = nD.Street
                };

                _appContext.Duplicates.Add(duplicate);
                _appContext.SaveChanges();

                if (nD.DigSvidDeliveryByEmail)
                    duplicate.CertificateDeliveryForms.Add(new DuplicateCertificateDeliveryForm { DuplicateId = duplicate.Id, DeliveryFormId = (long)CertificateDeliveryFormEnum.EMAIL });
                if (nD.DigSvidDeliveryByPortal)
                    duplicate.CertificateDeliveryForms.Add(new DuplicateCertificateDeliveryForm { DuplicateId = duplicate.Id, DeliveryFormId = (long)CertificateDeliveryFormEnum.PORTAL });
                if (nD.DigSvidDeliveryByEpgu)
                    duplicate.CertificateDeliveryForms.Add(new DuplicateCertificateDeliveryForm { DuplicateId = duplicate.Id, DeliveryFormId = (long)CertificateDeliveryFormEnum.EPGU });

                var histories = nD.DuplicateStatusHistories.OrderBy(h => h.ChangeTime).ToArray();
                for (int n = 0; n < histories.Length; n++)
                {
                    var nsHistory = histories[n];
                    var end = n < histories.Length - 1 ? (DateTime?)histories[n + 1].ChangeTime : null;
                    duplicate.StatusHistories.Add(new EM.Entities.DuplicateStatusHistory
                    {
                        DuplicateId = duplicate.Id,
                        ChangeTime = nsHistory.ChangeTime,
                        EndTime = end,
                        PrevStatusId = nsHistory.PrevStatusId,
                        StatusId = nsHistory.StatusId,
                        UserId = GetUserId(nsHistory.UserId)
                    });
                }
                _appContext.SaveChanges();

                var dossier = new Dossier { DuplicateId = duplicate.Id };
                if (!string.IsNullOrEmpty(nD.ApplyBarCode))
                {
                    var apply = _appContext.Applies.FirstOrDefault(a => a.PrimaryBarCode == nD.ApplyBarCode);
                    if (apply == null)
                        apply = CreateApply(_nostrContext.Applies.First(a => a.BarCode == nD.ApplyBarCode));

                    dossier.ApplyId = apply?.Id;
                }

                _appContext.Dossiers.Add(dossier);
                _appContext.SaveChanges();
            }
        }

        private static EM.Entities.Apply CreateApply(Apply nApply)
        {
            if (_appContext.Applies.Any(a => a.PrimaryBarCode == nApply.BarCode))
                return null;

            var createTime = nApply.ApplyStatusHistories.OrderBy(h => h.ChangeDate).FirstOrDefault(h => h.StatusId == 1)?.ChangeTime ?? nApply.CreateDate.Value;
            var acceptTime = nApply.ApplyStatusHistories.OrderBy(h => h.ChangeDate).FirstOrDefault(h => h.StatusId == 5)?.ChangeTime;

            var apply = new EM.Entities.Apply
            {
                PrimaryBarCode = nApply.BarCode,
                BarCode = nApply.CurrentBarCode,

                CreateTime = createTime,
                AcceptTime = acceptTime,
                AimId = nApply.AimId,
                BaseLearnDateBegin = nApply.BaseLearnDateBegin,
                BaseLearnDateEnd = nApply.BaseLearnDateEnd,
                ByWarrant = nApply.ByWarrant,
                CertificateDeliveryForms = new List<ApplyCertificateDeliveryForm>(),
                CreatorBirthDate = nApply.CreatorBirthDate,
                CreatorBlock = nApply.CreatorBlock,
                CreatorBuilding = nApply.CreatorBuilding,
                CreatorCitizenshipId = GetCountryId(nApply.CreatorCitizenshipId),
                CreatorCityName = nApply.CreatorCityName,
                CreatorCorpus = nApply.CreatorCorpus,
                CreatorCountryId = GetCountryId(nApply.CreatorCountryId),
                CreatorEmail = nApply.CreatorEmail,
                CreatorFirstName = nApply.CreatorFirstName,
                CreatorFlat = nApply.CreatorFlat,
                CreatorLastName = nApply.CreatorLastName,
                CreatorMailIndex = nApply.CreatorMailIndex,
                CreatorPassportReq = nApply.CreatorPassportReq,
                CreatorPassportTypeId = nApply.CreatorPassportKindTypeId,
                CreatorPhone = nApply.CreatorPhone,
                CreatorStreet = nApply.CreatorStreet,
                CreatorSurname = nApply.CreatorSurname,
                Deleted = nApply.Deleted,
                DeliveryFormId = nApply.DeliveryFormId,
                DocAttachmentsCount = nApply.DocAttachment,
                DocBlankNum = nApply.DocBlankNum,
                DocCountryId = GetCountryId(nApply.DocCountryId),
                DocDate = nApply.DocDate,
                DocDateYear = nApply.DocDateYear,
                DocFullName = nApply.DocFullName,
                DocRegNum = nApply.DocRegNum,
                DocsWillSendByPost = nApply.DocsWillSendByPost,
                DocTypeId = nApply.DocTypeId,
                EntryFormId = nApply.EntryFormId,
                EpguCode = nApply.EpguCode,
                EpguStatus = nApply.EpguStatus,
                FixedLearnSpecialityName = nApply.FixedLearnSpecialityName,
                ForInfoLetter = nApply.ForInfoLetter,
                ForOferta = nApply.ForOferta,
                IsEnglish = nApply.IsEnglish,
                IsNovorossia = nApply.IsNovorossia,
                IsRostovFilial = nApply.IsRostovFilial,
                OrgCreator = nApply.OrgCreator,
                Other = nApply.Other,
                OwnerBirthDate = nApply.OwnerBirthDate,
                OwnerBlock = nApply.OwnerBlock,
                OwnerBuilding = nApply.OwnerBuilding,
                OwnerCitizenshipId = GetCountryId(nApply.OwnerCitizenshipId),
                OwnerCityName = nApply.OwnerCityName,
                OwnerCorpus = nApply.OwnerCorpus,
                OwnerCountryId = GetCountryId(nApply.OwnerCountryId),
                OwnerFirstName = nApply.OwnerFirstName,
                OwnerFlat = nApply.OwnerFlat,
                OwnerLastName = nApply.OwnerLastName,
                OwnerMailIndex = nApply.OwnerMailIndex,
                OwnerPassportReq = nApply.OwnerPassportReq,
                OwnerPassportTypeId = nApply.OwnerPassportKindTypeId,
                OwnerPhone = nApply.OwnerPhone,
                OwnerStreet = nApply.OwnerStreet,
                OwnerSurname = nApply.OwnerSurname,
                ReturnOriginalsFormId = nApply.ReturnOriginalsFormId,
                WarrantReq = nApply.WarrantReq,
                WarrantDate = nApply.WarrantDate,
                WarrantTerm = nApply.WarrantTerm,
                SchoolAddress = nApply.SchoolAddress,
                SchoolCityName = nApply.SchoolCityName,
                SchoolCountryId = GetCountryId(nApply.SchoolCountryId),
                SchoolEmail = nApply.SchoolEmail,
                SchoolFax = nApply.SchoolFax,
                SchoolName = nApply.SchoolName,
                SchoolPhone = nApply.SchoolPhone,
                SchoolPostIndex = nApply.SchoolPostIndex,
                SchoolTypeId = nApply.SchoolTypeId,
                SpecialLearnDateBegin = nApply.SpecialLearnDateBegin,
                SpecialLearnDateEnd = nApply.SpecialLearnDateEnd,
                SpecialLearnFormId = nApply.SpecialLearnFormId,
                StatusId = _appContext.ApplyStatuses.FirstOrDefault(s => s.OldId == nApply.StatusId)?.Id ?? (long)ApplyStatusEnum.NO_VALIDATED,
                StatusChangeTime = nApply.StatusChangeTime,
                StatusHistories = new List<EM.Entities.ApplyStatusHistory>(),
                Storage = nApply.Storage,
                TransmitOpenChannels = nApply.TransmitOpenChannels
            };

            _appContext.Applies.Add(apply);
            _appContext.SaveChanges();

            if (nApply.DigSvidDeliveryByEmail)
                apply.CertificateDeliveryForms.Add(new ApplyCertificateDeliveryForm { ApplyId = apply.Id, DeliveryFormId = (long)CertificateDeliveryFormEnum.EMAIL });
            if (nApply.DigSvidDeliveryByPortal)
                apply.CertificateDeliveryForms.Add(new ApplyCertificateDeliveryForm { ApplyId = apply.Id, DeliveryFormId = (long)CertificateDeliveryFormEnum.PORTAL });
            if (nApply.DigSvidDeliveryByEpgu)
                apply.CertificateDeliveryForms.Add(new ApplyCertificateDeliveryForm { ApplyId = apply.Id, DeliveryFormId = (long)CertificateDeliveryFormEnum.EPGU });

            foreach (var nsHistory in nApply.ApplyStatusHistories)
            {
                var prevStatus = nsHistory.PrevStatusId != null ? _appContext.ApplyStatuses.FirstOrDefault(s => s.OldId == nsHistory.PrevStatusId)?.Id : null;
                apply.StatusHistories.Add(new EM.Entities.ApplyStatusHistory
                {
                    ApplyId = apply.Id,
                    ChangeTime = nsHistory.ChangeTime,
                    EndTime = nsHistory.EndTime,
                    PrevStatusId = prevStatus,
                    StatusId = _appContext.ApplyStatuses.FirstOrDefault(s => s.OldId == nsHistory.StatusId).Id,
                    UserId = GetUserId(nsHistory.UserId)
                });
            }

            _appContext.SaveChanges();

            foreach (var nAttach in nApply.ApplyDocuments)
            {
                Guid uid = Guid.Empty;

                if (nAttach.DocumentFileId != null)
                {
                    var nDocFile = _nostrContext.UploadedFiles.First(f => f.Id == nAttach.DocumentFileId);
                    var bytes = _nostrStorage.GetFileBytes(nDocFile);
                    if (bytes != null)
                    {
                        uid = _appStorage.CreateFile(bytes);
                        var nUser = nDocFile.UserId != null ? _nostrContext.Users.First(u => u.Id == nDocFile.UserId) : null;
                        var login = nUser?.Login;
                        var userId = login != null ? _appContext.Users.FirstOrDefault(u => u.Login == login)?.Id : null;

                        var newFileInfo = new EM.Entities.FileInfo
                        {
                            Name = Path.GetFileName(nDocFile.FileName),
                            ContentType = nDocFile.ContentType,
                            CreateTime = nDocFile.UploadTime ?? DateTime.Now,
                            Uid = uid,
                            Size = bytes?.Length ?? 0,
                            Source = nDocFile.Source,
                            OldId = nDocFile.Id,
                            CreatorEmail = nDocFile.CreatorEmail,
                            CreatedById = userId
                        };
                        _appContext.Add(newFileInfo);
                        _appContext.SaveChanges();
                    }
                }

                apply.Attachments.Add(new ApplyAttachment
                {
                    ApplyId = apply.Id,
                    AttachmentTypeId = nAttach.DocumentTypeId,
                    Description = nAttach.Description,
                    Error = nAttach.Error,
                    FileInfoUid = uid != Guid.Empty ? uid : null,
                    Given = nAttach.Given,
                    Required = nAttach.Required
                });

                _appContext.SaveChanges();
            }

            var dossier = new Dossier { ApplyId = apply.Id };
            _appContext.Dossiers.Add(dossier);
            _appContext.SaveChanges();

            foreach (var nComment in nApply.ApplyComments)
            {
                var nUser = _nostrContext.Users.FirstOrDefault(u => u.Id == nComment.UserId);
                var comment = new DossierComment
                {
                    DossierId = dossier.Id,
                    CreateTime = nComment.CommentDate,
                    Title = nComment.Title,
                    Text = nComment.Body,
                    UserId = nUser != null ? _appContext.Users.First(u => u.Login == nUser.Login)?.Id : null
                };
                dossier.Comments.Add(comment);

                var nAttachs = _nostrContext.ApplyCommentUploadedFiles.Where(uf => uf.ApplyCommentUploadedFileUploadedFileId == nComment.Id);
                foreach (var nAttach in nAttachs)
                {
                    Guid uid = Guid.Empty;
                    var nFile = _nostrContext.UploadedFiles.First(f => f.Id == nAttach.FilesId);
                    var bytes = _nostrStorage.GetFileBytes(nFile);
                    if (bytes != null)
                    {
                        uid = _appStorage.CreateFile(bytes);
                        var nFileUser = nFile.UserId != null ? _nostrContext.Users.First(u => u.Id == nFile.UserId) : null;
                        var login = nFileUser?.Login;
                        var userId = login != null ? _appContext.Users.FirstOrDefault(u => u.Login == login)?.Id : null;

                        var newFileInfo = new EM.Entities.FileInfo
                        {
                            Name = Path.GetFileName(nFile.FileName),
                            ContentType = nFile.ContentType,
                            CreateTime = nFile.UploadTime ?? DateTime.Now,
                            Uid = uid,
                            Size = bytes?.Length ?? 0,
                            Source = nFile.Source,
                            OldId = nFile.Id,
                            CreatorEmail = nFile.CreatorEmail,
                            CreatedById = userId
                        };
                        _appContext.Add(newFileInfo);
                        _appContext.SaveChanges();
                    }
                }
            }

            var fieldsMap = new Dictionary<string, string>
                {
                    { "AcceptDate", "AcceptTime" },
                    { "CreatorCitizenship", "CreatorCitizenshipId" },
                    { "CreatorPassportType", "CreatorPassportTypeId" },
                    { "CreatorCountry", "CreatorCountryId" },
                    { "OwnerCitizenship", "OwnerCitizenshipId" },
                    { "OwnerCountry", "OwnerCountryId" },
                    { "OwnerPassportType", "OwnerPassportTypeId" },
                    { "DocCountry", "DocCountryId" },
                    { "DocType", "DocTypeId" },
                    { "DocAttachment", "DocAttachmentsCount" },
                    { "SchoolCountry", "SchoolCountryId" },
                    { "SchoolType", "SchoolTypeId" },
                    { "SpecialLearnForm", "SpecialLearnFormId" },
                    { "Aim", "AimId" },
                    { "EntryForm", "EntryFormId" }
                };
            foreach (var nError in _nostrContext.ApplyErrors.Include(e => e.Field).Where(e => e.ApplyApplyErrorApplyErrorBarCode == nApply.BarCode))
            {
                var fieldName = nError.Field.Name;
                if (fieldsMap.ContainsKey(fieldName))
                    fieldName = fieldsMap[fieldName];

                _appContext.ApplyMessages.Add(new ApplyMessage
                {
                    ApplyId = apply.Id,
                    FieldName = fieldName,
                    Text = nError.Text
                });
            }
            _appContext.SaveChanges();

            return apply;
        }

        private static long? GetUserId(int? nUserId)
        {
            var nUser = _nostrContext.Users.FirstOrDefault(u => u.Id == nUserId);
            return nUser != null ? _appContext.Users.First(u => u.Login == nUser.Login)?.Id : null;
        }

        private static long? GetCountryId(int? nCountryId)
        {
            return _appContext.Countries.FirstOrDefault(c => c.OldId == nCountryId)?.Id;
        }

        private static T AddEntityIfNotExists<T>(T entity, Func<T, bool> isAdded) where T: class
        {
            var exists = _appContext.Set<T>().FirstOrDefault(isAdded);
            if (exists != null)
                return exists;

            _appContext.Add(entity);
            _appContext.SaveChanges();
            return entity;
        }
    }
}
