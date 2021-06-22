using MediatR;
using Ron.Ido.BM.Constants;
using Ron.Ido.BM.Models.Applies.Acceptance;
using Ron.Ido.BM.Models.Applies.AppliesSearch;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Applies.AppliesSearch
{
    public class GetAppliesSearchPageCommand : IRequest<ODataPage<AppliesSearchPageItemDto>>
    {
        public ODataRequest Request { get; private set; }

        public GetAppliesSearchPageCommand(ODataRequest request)
        {
            Request = request;
        }
    }

    public class GetAppliesSearchPageCommandHandler : IRequestHandler<GetAppliesSearchPageCommand, ODataPage<AppliesSearchPageItemDto>>
    {
        private ODataService _odataService;

        public GetAppliesSearchPageCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataPage<AppliesSearchPageItemDto>> Handle(GetAppliesSearchPageCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var request = cmd.Request;
                request.ReplaceOrder(nameof(AppliesSearchPageItemDto.CreateDate), nameof(Apply.CreateTime));
                request.ReplaceOrder(nameof(AppliesSearchPageItemDto.CreatorFullName), nameof(Apply.CreatorSurname), nameof(Apply.CreatorFirstName), nameof(Apply.CreatorLastName));
                request.ReplaceOrder(nameof(AppliesSearchPageItemDto.OwnerFullName), nameof(Apply.OwnerSurname), nameof(Apply.OwnerFirstName), nameof(Apply.OwnerLastName));
                request.ReplaceOrder(nameof(AppliesSearchPageItemDto.Status), nameof(Apply.StatusId));

                var result = _odataService.GetPage<Apply, AppliesSearchPageItemDto>(request,
                    new[]
                    {
                        request.CreateCustomFilter<Apply>(query => {
                            var statusFilter = request.GetFilter("statuses");
                            var allowedStatuses = statusFilter != null
                                ? ApplyAllowedStatuses.Search.Intersect(statusFilter.GetIds()).ToArray()
                                : ApplyAllowedStatuses.Search;
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
                                var ids = stagesFilter.GetIds();
                                query = query.Where(a => a.Status.EtapId != null && ids.Contains(a.Status.EtapId.Value));
                            }


                            return query;
                        })
                    },
                    new[] {
                        new ODataMapMemberConfig<Apply, AppliesSearchPageItemDto>(
                            applyDto => applyDto.DossierId,
                            expr => expr.MapFrom(apply => apply.Dossiers.First().Id)
                        ),
                        new ODataMapMemberConfig<Apply, AppliesSearchPageItemDto>(
                            applyDto => applyDto.CreateDate,
                            expr => expr.MapFrom(apply => $"{apply.CreateTime:dd.MM.yyyy}")
                        ),
                        new ODataMapMemberConfig<Apply, AppliesSearchPageItemDto>(
                            applyDto => applyDto.EntryFormId,
                            expr => expr.MapFrom(apply => apply.EntryFormId.HasValue ? (ApplyEntryFormEnum)apply.EntryFormId : ApplyEntryFormEnum.SELF)
                        ),
                        new ODataMapMemberConfig<Apply, AppliesSearchPageItemDto>(
                            applyDto => applyDto.CreatorFullName,
                            expr => expr.MapFrom(apply => $"{apply.CreatorSurname} {apply.CreatorFirstName} {apply.CreatorLastName}")
                        ),
                        new ODataMapMemberConfig<Apply, AppliesSearchPageItemDto>(
                            applyDto => applyDto.OwnerFullName,
                            expr => expr.MapFrom(apply => $"{apply.OwnerSurname} {apply.OwnerFirstName} {apply.OwnerLastName}")
                        ),
                        new ODataMapMemberConfig<Apply, AppliesSearchPageItemDto>(
                            applyDto => applyDto.Status,
                            expr => expr.MapFrom(apply => apply.Status.Name)
                        )
                    });

                return result;
            });
        }

    }

}
