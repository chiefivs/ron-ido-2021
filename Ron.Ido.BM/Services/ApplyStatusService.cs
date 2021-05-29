using Ron.Ido.Common.DependencyInjection;
using Ron.Ido.EM;
using System.Linq;

namespace Ron.Ido.BM.Services
{
	public class ApplyStatusService : ODataService
    {
		public ApplyStatusService( AppDbContext appDbContext ) : base( appDbContext )
		{
		}

		public bool DenyDelete(long applyStatusId)
		{

			return _appDbContext.Applies.Any( a => a.StatusId == applyStatusId );
		}
    }
}
