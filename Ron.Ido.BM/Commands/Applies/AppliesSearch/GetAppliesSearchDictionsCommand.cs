using MediatR;
using Ron.Ido.BM.Constants;
using Ron.Ido.BM.Models.Applies.AppliesSearch;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Applies.AppliesSearch
{
    public class GetAppliesSearchDictionsCommand : IRequest<AppliesSearchDictions>
    {
    }
    public class GetAppliesSearchDictionsCommandHandler : IRequestHandler<GetAppliesSearchDictionsCommand, AppliesSearchDictions>
    {
        private ODataService _odataService;

        public GetAppliesSearchDictionsCommandHandler(ODataService service)
        {
            _odataService = service;
        }
        public Task<AppliesSearchDictions> Handle(GetAppliesSearchDictionsCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var statuses = _odataService.GetOptions<ApplyStatus>("Name", "Id", query => query.Where(s => ApplyAllowedStatuses.Search.Contains(s.Id)));
                var levels = _odataService.GetOptions<LearnLevel>("Name", "Id");
                var entryForms = _odataService.GetOptions<ApplyEntryForm>("Name", "Id");
                var stages = _odataService.GetOptions<ReglamentEtap>("Name", "Id");

                return new AppliesSearchDictions
                {
                    Statuses = statuses,
                    LearnLevels = levels,
                    EntryForms = entryForms,
                    Stages = stages
                };
            });
        }

    }
}
