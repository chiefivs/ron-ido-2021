using MediatR;
using Ron.Ido.BM.Models.Account;
using Ron.Ido.EM.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Account
{
    public class GetMenuCommand : IRequest<IEnumerable<MenuItem>>
    {
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public IEnumerable<PermissionEnum> Permissions { get; set; }

        public GetMenuCommand(IEnumerable<MenuItem> items, IEnumerable<PermissionEnum> permissions)
        {
            MenuItems = items;
            Permissions = permissions;
        }

        public class GetMenuCommandHandler : IRequestHandler<GetMenuCommand, IEnumerable<MenuItem>>
        {
            public Task<IEnumerable<MenuItem>> Handle(GetMenuCommand request, CancellationToken cancellationToken)
            {
                return Task.Run(() =>
                {
                    return request.MenuItems
                        .Select(i => i.CreateFor(request.Permissions))
                        .Where(i => i != null);
                });
            }
        }
    }
}
