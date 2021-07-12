using MediatR;
using Microsoft.EntityFrameworkCore;
using Ron.Ido.BM.Constants;
using Ron.Ido.BM.Models.Duplicate;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using E = Ron.Ido.EM.Entities;

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
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.CreateDate), nameof(E.Duplicate.CreateTime));
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.CreatorFullName),  nameof(Duplicate.FullName));
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.OwnerFullName),  nameof(Duplicate.DocFullName));
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.Status),  nameof(E.Duplicate.StatusId));
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.BarCode),  nameof(E.Duplicate.BarCode));

                var result = _odataService.GetPage<Duplicate, DuplicatesSearchPageItemDto>(request,
                    new[]
                    {
                        request.CreateCustomFilter<E.Duplicate>(query => {
                            var statusFilter = request.GetFilter("statuses");
                            var allowedStatuses = statusFilter != null
                                ? DuplicateAllowedStatuses.Search.Intersect(statusFilter.GetIds()).ToArray()
                                : DuplicateAllowedStatuses.Search;
                            query = query.Where(a => allowedStatuses.Contains(a.StatusId));

                            var barcodeFilter = request.GetFilter("barCode");
                            if(barcodeFilter != null && !string.IsNullOrEmpty(barcodeFilter.Values?.FirstOrDefault()))
                            {
                                query = query.Where(duplicate => duplicate.BarCode.Contains( barcodeFilter.Values.First()) );
                            }
                            /*
                            var createTimeFilter = request.GetFilter("createTime");
                            if(createTimeFilter != null && createTimeFilter.Values.Length == 2)
                                    query = query
                                .WhereGreaterThanOrEqual(nameof(Duplicate.CreateTime), createTimeFilter.Values[0].Parse(typeof(DateTime)))
                                .WhereLessThan(nameof(Duplicate.CreateTime), createTimeFilter.Values[1].Parse(typeof(DateTime)));
                            */
                            var fullname = request.GetFilter("creatorFullName");
                            if(fullname != null && !string.IsNullOrEmpty(fullname.Values?.FirstOrDefault()))
                            {
                                query = query.Where(duplicate => duplicate.FullName.Contains( fullname.Values.First()) );
                            }
                            var docFullName = request.GetFilter("ownerFullName");
                            if(docFullName != null && !string.IsNullOrEmpty(docFullName.Values?.FirstOrDefault()))
                            {
                                query = query.Where(duplicate => duplicate.DocFullName.Contains(docFullName.Values.First() ) );
                            }
                            /*
                            var entryFormFilter = request.GetFilter("entryForms");
                            if(entryFormFilter != null)
                            {
                                var ids = entryFormFilter.GetIds();
                                query = query.Where(a => ids.Contains(a.Id));
                            }
                            */
                            return query;
                        })
                    },
                    new[] {
                        new ODataMapMemberConfig<Duplicate, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.DossierId,
                            expr => expr.MapFrom(dossier => dossier.Dossiers.First().Id)
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.CreateDate,
                            expr => expr.MapFrom(dossier => $"{dossier.CreateTime:dd.MM.yyyy}")
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.CreatorFullName,
                            expr => expr.MapFrom(dossier => $"{dossier.FullName}")
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.OwnerFullName,
                            expr => expr.MapFrom(dossier => $"{dossier.DocFullName}")
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.BarCode,
                            expr => expr.MapFrom(dossier => dossier.BarCode)
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.Status,
                            expr => expr.MapFrom(
                                dossier => dossier.Status.Name
                                )
                            )
                    });

                return result;
            });
        }

    }

}
