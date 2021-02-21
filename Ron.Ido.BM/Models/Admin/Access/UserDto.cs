namespace Ron.Ido.BM.Models.Admin.Access
{
    public class UserDto
    {
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string Login { get; set; }
        public string[] Roles { get; set; }
        public string FullName { get; set; }
    }
}
