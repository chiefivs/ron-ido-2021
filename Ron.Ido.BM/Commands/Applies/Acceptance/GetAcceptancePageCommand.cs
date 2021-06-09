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
                request.ReplaceOrder(nameof(AcceptancePageItemDto.CreateDate), nameof(Apply.CreateTime));
                request.ReplaceOrder(nameof(AcceptancePageItemDto.CreatorFullName), nameof(Apply.CreatorSurname), nameof(Apply.CreatorFirstName), nameof(Apply.CreatorLastName));
                request.ReplaceOrder(nameof(AcceptancePageItemDto.OwnerFullName), nameof(Apply.OwnerSurname), nameof(Apply.OwnerFirstName), nameof(Apply.OwnerLastName));
                request.ReplaceOrder(nameof(AcceptancePageItemDto.Status), nameof(Apply.StatusId));

                var result = _odataService.GetPage(request,
                    new[]
                    {
                        request.CreateCustomFilter<Apply>(query => {
                            var statusFilter = request.GetFilter("statuses");
                            var allowedStatuses = statusFilter != null
                                ? ApplyAllowedStatuses.Acceptance.Intersect(statusFilter.GetIds()).ToArray()
                                : ApplyAllowedStatuses.Acceptance;
                            query = query.Where(a => allowedStatuses.Contains(a.StatusId));

                            var levelFilter = request.GetFilter("learnLevels");
                            if(levelFilter != null)
                            {
                                var ids = levelFilter.GetIds();
                                query = query.Where(a => a.DocTypeId != null && ids.Contains(a.DocType.LearnLevelId));
                            }

                            var entryFormFilter = request.GetFilter("entryForms");
                            if(entryFormFilter != null)
                            {
                                var ids = entryFormFilter.GetIds();
                                query = query.Where(a => ids.Contains(a.Id));
                            }

                            var stagesFilter = request.GetFilter("stages");
                            if(stagesFilter != null)
                            {
                                var stagesIds = stagesFilter.GetIds();
                                query = query.Where(a => stagesIds.Contains(a.Status.EtapId));
                            }


                            return query;
                        })
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
