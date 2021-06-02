using MediatR;
using Ron.Ido.BM.Models.Admin.Access;
using Ron.Ido.BM.Models.OData;
using Ron.Ido.BM.Services;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Admin.Access
{
    public class SaveUserCommand : IRequest
    {
        public UserDto User { get; private set; }

        public SaveUserCommand(UserDto user)
        {
            User = user;
        }
    }
    public class SaveUserCommandHandler : UserHandlerBase, IRequestHandler<SaveUserCommand>
    {

        public SaveUserCommandHandler(ODataService service) : base(service)
        {
        }

        public Task<Unit> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidateUser(request.User);
            if (errors.Any())
                throw new ODataValidationException(errors);

            Service.SaveDto(request.User,
                new[] {
                    new ODataMapMemberConfig<UserDto, User>(
                            user => user.UserRoles,
                            expr => expr.Ignore()
                        ),
                    new ODataMapMemberConfig<UserDto, User>(
                        user => user.PasswordHash,
                        expr => expr.Ignore()
                        )
                },
                (userDto, user, context) =>
                {
                    user.UserRoles.Clear();
                    var roles = userDto.Roles
                    .Select(r => new UserRole { RoleId = r, User = user });

                    user.UserRoles.AddRange(roles);

                    if (!string.IsNullOrEmpty(userDto.Password))
                    {
                        user.PasswordHash = userDto.Password.GetHashString();
                    }
                });

            return Task.FromResult(Unit.Value);
        }
    }

}
