using Ron.Ido.BM.Models.Account;
using Ron.Ido.Common.DependencyInjection;

namespace Ron.Ido.BM.Interfaces
{
    public interface IIdentityService : IDependency
    {
        Identity Identity { get; }
    }

}
