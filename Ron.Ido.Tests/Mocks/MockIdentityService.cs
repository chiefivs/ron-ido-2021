using Ron.Ido.BM.Interfaces;
using Ron.Ido.BM.Models.Account;

namespace Ron.Ido.Tests.Mocks
{
    public class MockIdentityService : IIdentityService
    {
        public Identity Identity => new() { Id = 1, Login = "test", Name = "Test User" };
    }
}
