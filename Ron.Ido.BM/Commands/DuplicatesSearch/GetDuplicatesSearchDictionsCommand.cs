using MediatR;
using Ron.Ido.BM.Constants;
using Ron.Ido.BM.Models.DuplicatesSearch;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Duplicates.DuplicatesSearch
{
    public class GetDuplicatesSearchDictionsCommand : IRequest<DuplicatesSearchDictions>
    {
    }
    public class GetDuplicatesSearchDictinsCommandHandler : IRequestHandler<GetDuplicatesSearchDictionsCommand, DuplicatesSearchDictions>
    {
        private ODataService _odataService;

        public GetDuplicatesSearchDictinsCommandHandler(ODataService service)
        {
            _odataService = service;
        }
        public Task<DuplicatesSearchDictions> Handle(GetDuplicatesSearchDictionsCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var statuses = _odataService.GetOptions<DuplicateStatus>("Name", "Id", query => query.Where(s => DuplicateAllowedStatuses.Search.Contains(s.Id)));

                return new DuplicatesSearchDictions
                {
                    Statuses = statuses,
                };
            });
        }

    }
}
