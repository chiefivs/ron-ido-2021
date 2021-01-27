namespace Ron.Ido.EM.Entities
{
    public class RolePermission
    {
        public long RoleId { get; set; }

        public long PermissionId { get; set; }

        public virtual Role Role { get; set; }
    }
}
