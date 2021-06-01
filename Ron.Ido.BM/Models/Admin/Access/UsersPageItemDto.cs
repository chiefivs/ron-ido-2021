namespace Ron.Ido.BM.Models.Admin.Access
{
    public class UsersPageItemDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public bool IsBlocked { get; set; }
    }
}
