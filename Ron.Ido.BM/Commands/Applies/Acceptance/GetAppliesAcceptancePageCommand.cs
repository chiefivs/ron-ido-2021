﻿using MediatR;
using Ron.Ido.BM.Models.Applies.Acceptance;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Applies.Acceptance
{
    public class GetAppliesAcceptancePageCommand: IRequest<ODataPage<AppliesAcceptancePageItemDto>>
    {
        public ODataRequest Request { get; private set; }

        public GetAppliesAcceptancePageCommand(ODataRequest request)
        {
            Request = request;
        }
    }

    public class GetAppliesAcceptancePageCommandHandler : IRequestHandler<GetAppliesAcceptancePageCommand, ODataPage<AppliesAcceptancePageItemDto>>
    {
        private ODataService _odataService;

        public GetAppliesAcceptancePageCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataPage<AppliesAcceptancePageItemDto>> Handle(GetAppliesAcceptancePageCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var request = cmd.Request;
                request.ReplaceOrder(nameof(AppliesAcceptancePageItemDto.CreateDate), nameof(Apply.CreateTime));
                request.ReplaceOrder(nameof(AppliesAcceptancePageItemDto.CreatorFullName), nameof(Apply.CreatorSurname), nameof(Apply.CreatorFirstName), nameof(Apply.CreatorLastName));
                request.ReplaceOrder(nameof(AppliesAcceptancePageItemDto.OwnerFullName), nameof(Apply.OwnerSurname), nameof(Apply.OwnerFirstName), nameof(Apply.OwnerLastName));
                request.ReplaceOrder(nameof(AppliesAcceptancePageItemDto.Status), nameof(Apply.StatusId));


                var result = _odataService.GetPage(request,
                    new[] {
                        new ODataMapMemberConfig<Apply, AppliesAcceptancePageItemDto>(
                            applyDto => applyDto.CreateDate,
                            expr => expr.MapFrom(apply => $"{apply.CreateTime:dd.MM.yyyy}")
                        ),
                        new ODataMapMemberConfig<Apply, AppliesAcceptancePageItemDto>(
                            applyDto => applyDto.EntryFormId,
                            expr => expr.MapFrom(apply => apply.EntryFormId.HasValue ? (ApplyEntryFormEnum)apply.EntryFormId : ApplyEntryFormEnum.SELF)
                        ),
                        new ODataMapMemberConfig<Apply, AppliesAcceptancePageItemDto>(
                            applyDto => applyDto.CreatorFullName,
                            expr => expr.MapFrom(apply => $"{apply.CreatorSurname} {apply.CreatorFirstName} {apply.CreatorLastName}")
                        ),
                        new ODataMapMemberConfig<Apply, AppliesAcceptancePageItemDto>(
                            applyDto => applyDto.OwnerFullName,
                            expr => expr.MapFrom(apply => $"{apply.OwnerSurname} {apply.OwnerFirstName} {apply.OwnerLastName}")
                        ),
                        new ODataMapMemberConfig<Apply, AppliesAcceptancePageItemDto>(
                            applyDto => applyDto.Status,
                            expr => expr.MapFrom(apply => apply.Status.Name)
                        )
                    });

                return result;
            });
        }
    }
}