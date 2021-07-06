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
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.CreateDate), "Duplicate." + nameof(E.Duplicate.CreateTime));
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.CreatorFullName), "Apply." + nameof(Apply.CreatorSurname), "Apply." + nameof(Apply.CreatorFirstName), "Apply." + nameof(Apply.CreatorLastName));
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.OwnerFullName), "Apply." + nameof(Apply.OwnerSurname), "Apply." + nameof(Apply.OwnerFirstName), "Apply." + nameof(Apply.OwnerLastName));
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.Status), "Duplicate." + nameof(E.Duplicate.StatusId));
                request.ReplaceOrder(nameof(DuplicatesSearchPageItemDto.BarCode), "Duplicate." + nameof(E.Duplicate.BarCode));

                var result = _odataService.GetPage<E.Dossier, DuplicatesSearchPageItemDto>(request,
                    new[]
                    {
                        request.CreateCustomFilter<E.Dossier>(query => {
                            var statusFilter = request.GetFilter("statuses");
                            var allowedStatuses = statusFilter != null
                                ? DuplicateAllowedStatuses.Search.Intersect(statusFilter.GetIds()).ToArray()
                                : DuplicateAllowedStatuses.Search;
                            query = query.Include(dossier=>dossier.Duplicate).Where(d=>d.Duplicate != null);
                            query = query.Where(a => allowedStatuses.Contains(a.Duplicate.StatusId));

                            var barcodeFilter = request.GetFilter("barCode");
                            if(barcodeFilter != null && !string.IsNullOrEmpty(barcodeFilter.Values?.FirstOrDefault()))
                            {
                                query = query.Where(dossier => dossier.Duplicate.BarCode.Contains( barcodeFilter.Values.First()) );
                            }

                            var createTimeFilter = request.GetFilter("createTime");
                            if(createTimeFilter != null && createTimeFilter.Values.Length == 2)
                                    query = query
                                .WhereGreaterThanOrEqual("Duplicate."+nameof(Duplicate.CreateTime), createTimeFilter.Values[0].Parse(typeof(DateTime)))
                                .WhereLessThan("Duplicate."+nameof(Duplicate.CreateTime), createTimeFilter.Values[1].Parse(typeof(DateTime)));

                            var creatorSurnameFilter = request.GetFilter("creatorSurname");
                            if(creatorSurnameFilter != null && !string.IsNullOrEmpty(creatorSurnameFilter.Values?.FirstOrDefault()))
                            {
                                query = query.Where(dossier => dossier.Apply != null && dossier.Apply.CreatorSurname.Contains( creatorSurnameFilter.Values.First()) );
                            }
                            var ownerSurnameFilter = request.GetFilter("ownerSurname");
                            if(ownerSurnameFilter != null && !string.IsNullOrEmpty(ownerSurnameFilter.Values?.FirstOrDefault()))
                            {
                                query = query.Where(dossier => dossier.Apply != null && dossier.Apply.OwnerSurname.Contains(ownerSurnameFilter.Values.First() ) );
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
                        new ODataMapMemberConfig<E.Dossier, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.DossierId,
                            expr => expr.MapFrom(dossier => dossier.Id)
                        ),
                        new ODataMapMemberConfig<E.Dossier, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.CreateDate,
                            expr => expr.MapFrom(dossier => $"{dossier.Duplicate.CreateTime:dd.MM.yyyy}")
                        ),
                        new ODataMapMemberConfig<E.Dossier, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.CreatorFullName,
                            expr => expr.MapFrom(dossier => $"{dossier.Duplicate.DocFullName}")
                        ),
                        new ODataMapMemberConfig<E.Dossier, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.OwnerFullName,
                            expr => expr.MapFrom(dossier => $"{dossier.Duplicate.FullName}")
                        ),
                        new ODataMapMemberConfig<E.Dossier, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.BarCode,
                            expr => expr.MapFrom(dossier => dossier.Duplicate.BarCode)
                        ),
                        new ODataMapMemberConfig<E.Dossier, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.Status,
                            expr => expr.MapFrom(
                                dossier => dossier.Duplicate.Status.Name
                                )
                            ),
                        new ODataMapMemberConfig<E.Dossier, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.DossierId,
                            expr => expr.MapFrom(
                                dossier => dossier.Id
                                )),
                        new ODataMapMemberConfig<E.Dossier, DuplicatesSearchPageItemDto>(
                            duplicateDto => duplicateDto.Id,
                            expr => expr.MapFrom(
                                dossier => dossier.Duplicate.Id
                                )

                        )
                    });

                return result;
            });
        }

    }

}
