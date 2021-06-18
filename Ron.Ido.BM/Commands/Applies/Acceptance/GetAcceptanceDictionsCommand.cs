using MediatR;
using Ron.Ido.BM.Constants;
using Ron.Ido.BM.Models.Applies.Acceptance;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Applies.Acceptance
{
    public class GetAcceptanceDictionsCommand : IRequest<AcceptanceDictions>
    {
    }
    public class GetAcceptanceDictionsCommandHandler : IRequestHandler<GetAcceptanceDictionsCommand, AcceptanceDictions>
    {
        private ODataService _odataService;

        public GetAcceptanceDictionsCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<AcceptanceDictions> Handle(GetAcceptanceDictionsCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var statuses = _odataService.GetOptions<ApplyStatus>("Name", "Id", query => query.Where(s => ApplyAllowedStatuses.Acceptance.Contains(s.Id)));
                var levels = _odataService.GetOptions<LearnLevel>("Name", "Id");
                var entryForms = _odataService.GetOptions<ApplyEntryForm>("Name", "Id");
                var stages = _odataService.GetOptions<ReglamentEtap>("Name", "Id");

                return new AcceptanceDictions
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
