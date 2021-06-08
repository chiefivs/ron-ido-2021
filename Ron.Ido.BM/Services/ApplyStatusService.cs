using Ron.Ido.Common.DependencyInjection;
using Ron.Ido.EM;
using System.Linq;

namespace Ron.Ido.BM.Services
{
    public class ApplyStatusService : IDependency
    {
        protected AppDbContext _appDbContext;

        public ApplyStatusService(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public bool DenyDelete(long applyStatusId)
        {

            return _appDbContext.Applies.Any(a => a.StatusId == applyStatusId);
        }
    }
}
