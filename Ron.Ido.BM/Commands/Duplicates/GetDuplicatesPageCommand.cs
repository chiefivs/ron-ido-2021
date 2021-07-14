using MediatR;
using Ron.Ido.BM.Constants;
using Ron.Ido.BM.Models.Duplicates;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Duplicates
{
    public class GetDuplicatesPageCommand : IRequest<ODataPage<DuplicatesPageItemDto>>
    {
        public ODataRequest Request { get; private set; }

        public GetDuplicatesPageCommand(ODataRequest request)
        {
            Request = request;
        }
    }

    public class GetDuplicatesSearchPageCommandHandler : IRequestHandler<GetDuplicatesPageCommand, ODataPage<DuplicatesPageItemDto>>
    {
        private ODataService _odataService;

        public GetDuplicatesSearchPageCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataPage<DuplicatesPageItemDto>> Handle(GetDuplicatesPageCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var request = cmd.Request;
                request.ReplaceOrder(nameof(DuplicatesPageItemDto.CreateDate), nameof(Duplicate.CreateTime));
                request.ReplaceOrder(nameof(DuplicatesPageItemDto.CreatorFullName), nameof(Duplicate.FullName));
                request.ReplaceOrder(nameof(DuplicatesPageItemDto.OwnerFullName), nameof(Duplicate.DocFullName));
                request.ReplaceOrder(nameof(DuplicatesPageItemDto.Status), nameof(Duplicate.StatusId));
                request.ReplaceOrder(nameof(DuplicatesPageItemDto.BarCode), nameof(Duplicate.BarCode));

                var result = _odataService.GetPage(request,
                    new[]
                    {
                        request.CreateCustomFilter<Duplicate>(query => {
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
                        new ODataMapMemberConfig<Duplicate, DuplicatesPageItemDto>(
                            duplicateDto => duplicateDto.Id,
                            expr => expr.MapFrom(duplicate => duplicate.Id)
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesPageItemDto>(
                            duplicateDto => duplicateDto.DossierId,
                            expr => expr.MapFrom(duplicate => duplicate.Dossiers.First().Id)
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesPageItemDto>(
                            duplicateDto => duplicateDto.CreateDate,
                            expr => expr.MapFrom(duplicate => $"{duplicate.CreateTime:dd.MM.yyyy}")
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesPageItemDto>(
                            duplicateDto => duplicateDto.BarCode,
                            expr => expr.MapFrom(dossier => dossier.BarCode)
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesPageItemDto>(
                            duplicateDto => duplicateDto.CertificateNum,
                            expr => expr.MapFrom(duplicate => duplicate.Dossiers.First().Apply != null ? duplicate.Dossiers.First().Apply.BarCode : "")
                            //TODO: здесь должен быть не номер заявления, а номер свидетельства из дела, в котором это заявление рассматривалось
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesPageItemDto>(
                            duplicateDto => duplicateDto.CreatorFullName,
                            expr => expr.MapFrom(duplicate => $"{duplicate.FullName}")
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesPageItemDto>(
                            duplicateDto => duplicateDto.OwnerFullName,
                            expr => expr.MapFrom(duplicate => $"{(duplicate.Dossiers.First().Apply != null ? duplicate.Dossiers.First().Apply.DocFullName : "")}")
                            //TODO: здесь должно быть не ФИО из заявления, а ФИО по свидетельству из дела, в котором это заявление рассматривалось
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesPageItemDto>(
                            duplicateDto => duplicateDto.Storage,
                            expr => expr.MapFrom(duplicate => $"{duplicate.Storage}")
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesPageItemDto>(
                            duplicateDto => duplicateDto.HandoutDate,
                            expr => expr.MapFrom(duplicate => $"{duplicate.HandoutTime:dd.MM.yyyy}")
                        ),
                        new ODataMapMemberConfig<Duplicate, DuplicatesPageItemDto>(
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
