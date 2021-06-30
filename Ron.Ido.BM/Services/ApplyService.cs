using AutoMapper;
using Ron.Ido.BM.Interfaces;
using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Models.FileStorage;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.Common.Interfaces;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ron.Ido.BM.Services
{
    public class ApplyService: ODataService
    {
        private IFileStorageService _storage;
        private IIdentityService _identity;

        public ApplyService(AppDbContext context, IFileStorageService storage, IIdentityService identity): base(context)
        {
            _storage = storage;
            _identity = identity;
        }

        public ApplyDto GetApplyDto(long id)
        {
            var apply = AppDbContext.Applies.Find(id) ?? new EM.Entities.Apply();
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
                                Size = att.FileInfo.Size
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

        public long SaveApplyDto(ApplyDto applyDto)
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
                AppDbContext.Applies.Add(apply);

            AppDbContext.SaveChanges();

            apply.CertificateDeliveryForms.Clear();
            foreach(var form in applyDto.CertificateDeliveryForms)
            {
                apply.CertificateDeliveryForms.Add(new ApplyCertificateDeliveryForm { ApplyId = apply.Id, DeliveryFormId = form });
            }

            AppDbContext.SaveChanges();

            var notEmptyList = applyDto.Attachments.Where(a => a.IsNotEmpty());
            var emptyList = applyDto.Attachments.Where(a => !a.IsNotEmpty());

            foreach (var attachDto in emptyList)
            {
                var attach = AppDbContext.ApplyAttachments.Find(attachDto.Id);
                if (attach == null)
                    continue;

                if(attach.FileInfo?.Uid != null)
                {
                    _removeFile(attach.FileInfo.Uid);
                }

                AppDbContext.ApplyAttachments.Remove(attach);
            }

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


                var fileInfoDto = attachDto.FileInfo.FirstOrDefault();
                var fileInfo = attach.FileInfo;
                if (fileInfoDto?.Uid != fileInfo?.Uid)
                {
                    if (fileInfo != null)
                        _removeFile(fileInfo.Uid);

                    if(fileInfoDto != null)
                    {
                        var fi = _storage.SaveFile(fileInfoDto.Uid.Value);
                        var fileinfo = new FileInfo
                        {
                            Uid = fi.Uid,
                            ContentType = fi.ContentType,
                            Name = fi.Name,
                            CreateTime = DateTime.Now,
                            Size = fi.Size,
                            Source = "N",
                            CreatedById = _identity?.Identity?.Id
                        };

                        AppDbContext.FileInfos.Add(fileinfo);
                        AppDbContext.SaveChanges();

                        attach.FileInfoUid = fi.Uid;
                    }
                }
            }

            AppDbContext.SaveChanges();
            return apply.Id;
        }

        private void _removeFile(Guid uid)
        {
            _storage.DeleteFile(uid);
            var fileinfo = AppDbContext.FileInfos.Find(uid);
            if (fileinfo != null)
                AppDbContext.FileInfos.Remove(fileinfo);

        }
    }
}
