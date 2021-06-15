using Ron.Ido.BM.Models.Account;
using Ron.Ido.Common.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Interfaces
{
    public interface IIdentityService : IDependency
    {
        Identity Identity { get; }
    }

}
