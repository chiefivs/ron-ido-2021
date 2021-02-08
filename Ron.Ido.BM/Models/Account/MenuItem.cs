using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ron.Ido.BM.Models.Account
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public object Params { get; set; }
        public MenuItem[] Submenu { get; set; }

        public MenuItem()
        {
            Permissions = new PermissionEnum[] { };
            Submenu = new MenuItem[] { };
        }

        public MenuItem(string title, string path = null, params PermissionEnum[] permissions)
        {
            Title = title;
            Path = path;
            Permissions = permissions;
            Submenu = new MenuItem[] { };
        }

        internal PermissionEnum[] Permissions;

        public bool AuthorizedFor(IEnumerable<PermissionEnum> permissions)
        {
            var all = _getAllPermissions();
            return all == null || !all.Any() || all.Intersect(permissions).Any();
        }

        private PermissionEnum[] _getAllPermissions()
        {
            return Permissions
                .Union(Submenu.SelectMany(i => i._getAllPermissions()))
                .Distinct()
                .ToArray();
        }

        public MenuItem CreateFor(IEnumerable<PermissionEnum> permissions)
        {
            var allowItem = AuthorizedFor(permissions) && !string.IsNullOrEmpty(Path);
            var hasChilds = Submenu.Any(i => i.AuthorizedFor(permissions));
            if (!allowItem && !hasChilds)
                return null;

            var item = new MenuItem
            {
                Title = Title,
                Path = allowItem ? Path : "",
                Submenu = Submenu.Select(i => i.CreateFor(permissions)).Where(i => i != null).ToArray(),
                Params = Params
            };

            if (!allowItem && !item.Submenu.Any())
                return null;

            return item;
        }
    }
}
