using MediatR;
using Ron.Ido.BM.Models.Applies.Acceptance;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.EM.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Applies.Acceptance
{
    public class GetAppliesAcceptancePageCommand: IRequest<ODataPage<AppliesAcceptancePageItemDto>>
    {
        public ODataRequest Request { get; private set; }

        public GetAppliesAcceptancePageCommand(ODataRequest request)
        {
            Request = request;
        }
    }

    public class GetAppliesAcceptancePageCommandHandler : IRequestHandler<GetAppliesAcceptancePageCommand, ODataPage<AppliesAcceptancePageItemDto>>
    {
        private ODataService _odataService;

        public GetAppliesAcceptancePageCommandHandler(ODataService service)
        {
            _odataService = service;
        }

        public Task<ODataPage<AppliesAcceptancePageItemDto>> Handle(GetAppliesAcceptancePageCommand cmd, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var request = cmd.Request;
                var result = _odataService.GetPage<Apply, AppliesAcceptancePageItemDto>(request,
                    new[] {
                        new ODataMapMemberConfig<Apply, AppliesAcceptancePageItemDto>(
                            userDto => userDto.CreatorFullName,
                            expr => expr.MapFrom(apply => $"{apply.CreatorSurname} {apply.CreatorFirstName} {apply.CreatorLastName}")
                        ),
                        new ODataMapMemberConfig<Apply, AppliesAcceptancePageItemDto>(
                            userDto => userDto.OwnerFullName,
                            expr => expr.MapFrom(apply => $"{apply.OwnerSurname} {apply.OwnerFirstName} {apply.OwnerLastName}")
                        ),
                    });

                return result;
            });
        }
    }
}
