using MediatR;
using Ron.Ido.BM.Models.Account;
using Ron.Ido.BM.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Commands.Account
{
    public class LoginCommand: IRequest<Identity>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public LoginCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Identity>
    {
        private AccountService _accountService;

        public LoginCommandHandler(AccountService accountService)
        {
            _accountService = accountService;
        }

        public Task<Identity> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
                return _accountService.Login(request.Login, request.Password);
            });
        }
    }

}
