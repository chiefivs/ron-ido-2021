using MediatR;
using Ron.Ido.BM.Constants;
using Ron.Ido.BM.Models.Applies.Acceptance;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Applies.Acceptance
{
    public class GetAcceptancePageCommand: IRequest<ODataPage<AcceptancePageItemDto>>
    {
        public ODataRequest Request { get; private set; }

        public GetAcceptancePageCommand(ODataRequest request)
        {
            Request = request;
        }
    }

    public class GetAppliesAcceptancePageCommandHandler : IRequestHandler<GetAcceptancePageCommand, ODataPage<AcceptancePageItemDto>>
    {
        private ODataService _odataService;

        public GetAppliesAcceptancePageCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataPage<AcceptancePageItemDto>> Handle(GetAcceptancePageCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var request = cmd.Request;
                //request.ReplaceOrder(nameof(AcceptancePageItemDto.CreateDate), nameof(Apply.CreateTime));
                //request.ReplaceOrder(nameof(AcceptancePageItemDto.CreatorFullName), nameof(Apply.CreatorSurname), nameof(Apply.CreatorFirstName), nameof(Apply.CreatorLastName));
                //request.ReplaceOrder(nameof(AcceptancePageItemDto.OwnerFullName), nameof(Apply.OwnerSurname), nameof(Apply.OwnerFirstName), nameof(Apply.OwnerLastName));
                //request.ReplaceOrder(nameof(AcceptancePageItemDto.Status), nameof(Apply.StatusId));

                var result = _odataService.GetPage(request,
                    new[]
                    {
                        new ODataCustomFilter<Apply>(AcceptancePageItemDto.StatusesFilterField, (query, values) => {
                            var allowedStatuses = values != null
                                ? ApplyAllowedStatuses.Acceptance.Intersect(values.Parse<long>(0)).ToArray()
                                : ApplyAllowedStatuses.Acceptance;
                            return query.Where(a => allowedStatuses.Contains(a.StatusId));
                        }, true),
                        new ODataCustomFilter<Apply>(AcceptancePageItemDto.LearnLevelsFilterField, (query, values) => {
                                var ids = values.Parse<long>(0);
                                return query.Where(a => a.DocTypeId != null && ids.Contains(a.DocType.LearnLevelId));
                        }),
                        new ODataCustomFilter<Apply>(AcceptancePageItemDto.EntryFormsFilterField, (query, values) => {
                                var ids = values.Parse<long>(0);
                                return query.Where(a => a.EntryFormId != null && ids.Contains(a.EntryFormId.Value));
                        }),
                        new ODataCustomFilter<Apply>(AcceptancePageItemDto.StagesFilterField, (query, values) => {
                                var ids = values.Parse<long>(0);
                                return query.Where(a => a.Status.EtapId != null && ids.Contains(a.Status.EtapId.Value));
                        }),
                    },
                    new[]
                    {
                        new ODataCustomOrder<Apply>(nameof(AcceptancePageItemDto.CreateDate).ToCamel(),
                            query => query.OrderBy(a => a.CreateTime),
                            query => query.OrderByDescending(a => a.CreateTime)),
                        new ODataCustomOrder<Apply>(nameof(AcceptancePageItemDto.CreatorFullName).ToCamel(),
                            query => query.OrderBy(a => a.CreatorSurname).ThenBy(a => a.CreatorFirstName).ThenBy(a => a.CreatorLastName),
                            query => query.OrderByDescending(a => a.CreatorSurname).ThenByDescending(a => a.CreatorFirstName).ThenByDescending(a => a.CreatorLastName)),
                        new ODataCustomOrder<Apply>(nameof(AcceptancePageItemDto.OwnerFullName).ToCamel(),
                            query => query.OrderBy(a => a.OwnerSurname).ThenBy(a => a.OwnerFirstName).ThenBy(a => a.OwnerLastName),
                            query => query.OrderByDescending(a => a.OwnerSurname).ThenByDescending(a => a.OwnerFirstName).ThenByDescending(a => a.OwnerLastName)),
                        new ODataCustomOrder<Apply>(nameof(AcceptancePageItemDto.Status).ToCamel(),
                            query => query.OrderBy(a => a.StatusId),
                            query => query.OrderByDescending(a => a.StatusId)),
                    },
                    new[] {
                        new ODataMapMemberConfig<Apply, AcceptancePageItemDto>(
                            applyDto => applyDto.DossierId,
                            expr => expr.MapFrom(apply => apply.Dossiers.First().Id)
                        ),
                        new ODataMapMemberConfig<Apply, AcceptancePageItemDto>(
                            applyDto => applyDto.CreateDate,
                            expr => expr.MapFrom(apply => $"{apply.CreateTime:dd.MM.yyyy}")
                        ),
                        new ODataMapMemberConfig<Apply, AcceptancePageItemDto>(
                            applyDto => applyDto.EntryFormId,
                            expr => expr.MapFrom(apply => apply.EntryFormId.HasValue ? (ApplyEntryFormEnum)apply.EntryFormId : ApplyEntryFormEnum.SELF)
                        ),
                        new ODataMapMemberConfig<Apply, AcceptancePageItemDto>(
                            applyDto => applyDto.CreatorFullName,
                            expr => expr.MapFrom(apply => $"{apply.CreatorSurname} {apply.CreatorFirstName} {apply.CreatorLastName}")
                        ),
                        new ODataMapMemberConfig<Apply, AcceptancePageItemDto>(
                            applyDto => applyDto.OwnerFullName,
                            expr => expr.MapFrom(apply => $"{apply.OwnerSurname} {apply.OwnerFirstName} {apply.OwnerLastName}")
                        ),
                        new ODataMapMemberConfig<Apply, AcceptancePageItemDto>(
                            applyDto => applyDto.Status,
                            expr => expr.MapFrom(apply => apply.Status.Name)
                        )
                    });

                return result;
            });
        }
    }
}
