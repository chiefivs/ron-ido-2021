using Ron.Ido.Common;
using Ron.Ido.Common.Extensions;

namespace Ron.Ido.Web.Authorization
{
    [SectionName("Authorization")]
    public class AuthOptionSettings
    {
        public string SymmetricKey { get; set; }

        public int TokenLifeTimeHours { get; set; }
    }
}
