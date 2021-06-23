using AutoMapper;
using Ron.Ido.BM.Models.Dossier;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Models.Storage;
using Ron.Ido.EM;
using System.Collections.Generic;
using System.Linq;

namespace Ron.Ido.BM.Services
{
    public class ApplyService: ODataService
    {
        public ApplyService(AppDbContext context): base(context)
        {
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
            var attachmentTypes = AppDbContext.ApplyAttachmentTypes.OrderBy(type => type.OrderNum).AsEnumerable();
            var attachmentDtosList = new List<ApplyAttachmentDto>();

            var attachmentMapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EM.Entities.ApplyAttachment, ApplyAttachmentDto>()
                .ForMember(dto => dto.FileInfo, expr => expr.MapFrom(att => att.FileInfo != null
                    ? new FileInfoDto
                    {
                        Uid = att.FileInfo.Uid,
                        Name = att.FileInfo.Name,
                        Size = att.FileInfo.Size
                    }
                    : null));
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
    }
}
