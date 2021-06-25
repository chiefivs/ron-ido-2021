using MediatR;
using Ron.Ido.BM.Constants;
using Ron.Ido.BM.Models.DuplicatesSearch;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.DuplicatesSearch
{
    public class GetDuplicatesSearchPageCommand : IRequest<ODataPage<DuplicatesSearchPageItemDto>>
    {
        public ODataRequest Request { get; private set; }

        public GetDuplicatesSearchPageCommand(ODataRequest request)
        {
            Request = request;
        }
    }

    public class GetDuplicatesSearchPageCommandHandler : IRequestHandler<GetDuplicatesSearchPageCommand, ODataPage<DuplicatesSearchPageItemDto>>
    {
        private ODataService _odataService;

        public GetDuplicatesSearchPageCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataPage<DuplicatesSearchPageItemDto>> Handle(GetDuplicatesSearchPageCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var request = cmd.Request;
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.CreateDate), nameof(Duplicate.CreateTime));
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.CreatorFullName), nameof(Apply.CreatorSurname), nameof(Apply.CreatorFirstName), nameof(Apply.CreatorLastName));
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.OwnerFullName), nameof(Apply.OwnerSurname), nameof(Apply.OwnerFirstName), nameof(Apply.OwnerLastName));
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.Status), nameof(Duplicate.StatusId));

                var result = _odataService.GetPage(request,
                    new[]
                    {
                        request.CreateCustomFilter<Duplicate>(query => {
                            var statusFilter = request.GetFilter("statuses");
                            var allowedStatuses = statusFilter != null
                                ? DuplicateAllowedStatuses.Search.Intersect(statusFilter.GetIds()).ToArray()
                                : DuplicateAllowedStatuses.Search;
                            query = query.Where(a => allowedStatuses.Contains(a.StatusId));

                            //var entryFormFilter = request.GetFilter("entryForms");
                            //if(entryFormFilter != null)
                            //{
                            //    var ids = entryFormFilter.GetIds();
                            //    query = query.Where(a => ids.Contains(a.Id));
                            //}

                            return query;
                        })
                    },
                    new[] {
                        new ODataMapMemberConfig<Duplicate, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.DossierId,
                            expr => expr.MapFrom(duplicate => duplicate.Dossiers.First().Id)
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.CreateDate,
                            expr => expr.MapFrom(duplicate => $"{duplicate.CreateTime:dd.MM.yyyy}")
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.CreatorFullName,
                            expr => expr.MapFrom(duplicate => $"{duplicate.Dossiers.First().Apply.CreatorSurname} {duplicate.Dossiers.First().Apply.CreatorFirstName} {duplicate.Dossiers.First().Apply.CreatorLastName}")
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.OwnerFullName,
                            expr => expr.MapFrom(duplicate => $"{duplicate.Dossiers.First().Apply.OwnerSurname} {duplicate.Dossiers.First().Apply.OwnerFirstName} {duplicate.Dossiers.First().Apply.OwnerLastName}")
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.Status,
                            expr => expr.MapFrom(
                                duplicate => duplicate.Status.Name
                                )

                        )
                    });

                return result;
            });
        }

    }

}
