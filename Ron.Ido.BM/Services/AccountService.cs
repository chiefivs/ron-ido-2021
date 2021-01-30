using Microsoft.Extensions.Configuration;
using Ron.Ido.BM.Models.Account;
using Ron.Ido.Common;
using Ron.Ido.Common.DependencyInjection;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM;
using System.Linq;

namespace Ron.Ido.BM.Services
{
    public class AccountService: IDependency
    {
        private AppDbContext _appDbContext;

        public AccountService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Identity Login(string login, string password)
        {
            login = login ?? "";
            password = password ?? "";

            var user = _appDbContext.Users.SingleOrDefault(u => u.Login == login
            && u.PasswordHash == password.GetHashString()
            && !u.IsBlocked
            && !u.IsDeleted);

            if (user != null)
                return new Identity(user);

            return null;
        }
    }
}
