using AutoMapper;
using Ron.Ido.BM.Interfaces;
using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Models.FileStorage;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.Common.Interfaces;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ron.Ido.BM.Services
{
    public class ApplyService: DossierService
    {
        private IFileStorageService _storage;
        private FileStorageHelper _helper;

        public ApplyService(AppDbContext context, IFileStorageService storage, FileStorageHelper helper): base(context)
        {
            _storage = storage;
            _helper = helper;
        }

        public ApplyDto GetApplyDto(long id)
        {
            var apply = AppDbContext.Applies.Find(id) ?? new Apply();
            var applyMapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EM.Entities.Apply, ApplyDto>()
                    .ForMember(dto => dto.CertificateDeliveryForms, expr => expr.MapFrom(apply => apply.CertificateDeliveryForms.Select(f => f.DeliveryFormId)))
                    .ForMember(dto => dto.Attachments, expr => expr.Ignore());
            }));

            var applyDto = applyMapper.Map<ApplyDto>(apply);

            var attachmentsArr = apply.Attachments.AsEnumerable();
            var attachmentTypes = AppDbContext.ApplyAttachmentTypes.OrderBy(type => type.OrderNum).ToArray();
            var attachmentDtosList = new List<ApplyAttachmentDto>();

            var attachmentMapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EM.Entities.ApplyAttachment, ApplyAttachmentDto>()
                .ForMember(dto => dto.FileInfo, expr => expr.MapFrom(att => att.FileInfo != null
                    ? new []
                        {
                            new FileInfoDto
                            {
                                Uid = att.FileInfo.Uid,
                                Name = att.FileInfo.Name,
                                Size = att.FileInfo.Size,
                                ContentType = att.FileInfo.ContentType
                            }
                        }
                    : new FileInfoDto[] { }));
            }));
            foreach(var type in attachmentTypes)
            {
                var attachment = attachmentsArr.FirstOrDefault(a => a.AttachmentTypeId == type.Id) ?? new EM.Entities.ApplyAttachment();
                var attachmentDto = attachmentMapper.Map<ApplyAttachmentDto>(attachment);
                attachmentDto.AttachmentTypeId = type.Id;
                attachmentDto.AttachmentTypeName = type.Name;
                attachmentDtosList.Add(attachmentDto);
            }
            foreach(var attachment in attachmentsArr.Where(a => a.AttachmentTypeId == null))
            {
                var attachmentDto = attachmentMapper.Map<ApplyAttachmentDto>(attachment);
                attachmentDtosList.Add(attachmentDto);
            }

            applyDto.Attachments = attachmentDtosList;
            return applyDto;
        }

        public DossierDataDto SaveApplyDto(ApplyDto applyDto)
        {
            var applyMapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplyDto, Apply>()
                    .ForMember(apply => apply.Id, expr => expr.Ignore())
                    .ForMember(apply => apply.CertificateDeliveryForms, expr => expr.Ignore())
                    .ForMember(apply => apply.Attachments, expr => expr.Ignore());
            }));

            var apply = AppDbContext.Applies.Find(applyDto.Id) ?? new Apply();
            applyMapper.Map(applyDto, apply);

            if (apply.Id == 0)
            {
                apply.PrimaryBarCode = apply.BarCode = CreateBarCode(3);
                apply.CreateTime = DateTime.Now;
                //TODO: присвоение статуса должно происходить через ApplyStatusService и сопровождаться событием изменения статуса
                apply.StatusId = AppDbContext.ApplyStatuses.First(a => a.StatusEnumValue == ApplyStatusEnum.NO_VALIDATED.ToString()).Id;
                apply.StatusChangeTime = apply.CreateTime = DateTime.Now;
                AppDbContext.Applies.Add(apply);
            }

            AppDbContext.SaveChanges();

            apply.CertificateDeliveryForms.Clear();
            foreach(var form in applyDto.CertificateDeliveryForms)
            {
                apply.CertificateDeliveryForms.Add(new ApplyCertificateDeliveryForm { ApplyId = apply.Id, DeliveryFormId = form });
            }

            AppDbContext.SaveChanges();

            var notEmptyList = applyDto.Attachments.Where(a => a.IsNotEmpty());
            var emptyList = applyDto.Attachments.Where(a => !a.IsNotEmpty());
            var ids = applyDto.Attachments.Select(a => a.Id).Where(id => id > 0);
            var deletedList = apply.Attachments.Where(a => !ids.Contains(a.Id)).ToArray();

            var uidsForRemove = new List<Guid>();
            var filesForSave = new List<FileInfoDto>();

            foreach(var attachment in deletedList)
            {
                apply.Attachments.Remove(attachment);
            }
            AppDbContext.SaveChanges();

            foreach (var attachDto in emptyList)
            {
                var attach = AppDbContext.ApplyAttachments.Find(attachDto.Id);
                if (attach == null)
                    continue;

                if(attach.FileInfo?.Uid != null)
                {
                    uidsForRemove.Add(attach.FileInfo.Uid);
                    //_removeFile(attach.FileInfo.Uid);
                }

                AppDbContext.ApplyAttachments.Remove(attach);
            }
            AppDbContext.SaveChanges();

            foreach(var attachDto in notEmptyList)
            {
                var attach = AppDbContext.ApplyAttachments.Find(attachDto.Id);
                if (attach == null)
                {
                    attach = new ApplyAttachment
                    {
                        ApplyId = apply.Id,
                        AttachmentTypeId = attachDto.AttachmentTypeId,
                        Description = attachDto.Description,
                        Error = attachDto.Error,
                        Given = attachDto.Given,
                        Required = attachDto.Required
                    };

                    AppDbContext.ApplyAttachments.Add(attach);
                }

                attach.Description = attachDto.Description;
                attach.Error = attachDto.Error;
                attach.Given = attachDto.Given;
                attach.Required = attachDto.Required;
                AppDbContext.SaveChanges();


                var fileInfoDto = attachDto.FileInfo.FirstOrDefault();
                var fileInfo = attach.FileInfo;
                if (fileInfoDto?.Uid != fileInfo?.Uid || fileInfoDto?.BytesBase64 != null)
                {
                    var fileinfo = _helper.CreateFileInfo(fileInfoDto);
                    attach.FileInfoUid = fileinfo?.Uid;

                    //  старый файл удаляем только после обновления приложенного документа
                    if (fileInfo != null)
                    {
                        uidsForRemove.Add(fileInfo.Uid);
                        AppDbContext.FileInfos.Remove(fileInfo);
                        AppDbContext.SaveChanges();
                    }

                    AppDbContext.SaveChanges();
                }
            }

            //TODO: здесь должно быть событие изменения статуса, если заявление новое

            //  файлы в хранилище модифицируем только после успешного обновления БД
            foreach (var uid in uidsForRemove)
                _storage.DeleteFile(uid);

            var dossier = apply.Dossiers.FirstOrDefault(d => d.DuplicateId == null);
            if(dossier == null)
            {
                dossier = new Dossier
                {
                    ApplyId = apply.Id
                };
            }

            return GetDossierByApplyId(apply.Id);
        }


        private string CreateBarCode(int diapason = 3)
        {
            var last = AppDbContext.Applies
                .Where(a => !string.IsNullOrEmpty(a.BarCode))
                .Select(a => new { barcode = a.BarCode, time = a.AcceptTime != null ? a.AcceptTime.Value : a.CreateTime })
                .OrderByDescending(a => a.time)
                .FirstOrDefault();

            var now = DateTime.Now;

            if (last == null || last.time.Year < now.Year)
                return $"{diapason}{now:yyMMdd}00001";

            var num = int.Parse(last.barcode.Substring(7)) + 1;

            return $"{diapason}{now:yyMMdd}{num:00000}";
        }
    }
}
