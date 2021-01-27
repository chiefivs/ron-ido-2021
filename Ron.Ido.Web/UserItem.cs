using System.Collections.Generic;

namespace ForeignDocsRec2020.Web
{

    public class UserInfo
    {
        public string UserName { get; set; }
        public UserClaim[] Claims { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Visitor
    }
}
