using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ron.Ido.Common;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM;
using Ron.Ido.EM.Entities;
using Ron.Ido.Importer.NDB.Classes;
using System;
using System.Linq;

namespace Ron.Ido.Importer
{
    class Program
    {
        private static AppDbContext _appContext;
        private static NostrificationRONContext _nostrContext;
        static void Main(string[] args)
        {
            Console.WriteLine("Importer");

            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _appContext = serviceProvider.GetService<AppDbContext>();
            _nostrContext = serviceProvider.GetService<NostrificationRONContext>();

            ImportRoles();
            ImportUsers();
        }

        private static void ImportRoles()
        {
            var grants = GrantsUtility.AllGrants(typeof(GRANT)).Where(g => g.Permission != EM.Enums.PermissionEnum.NULL);
            foreach(var nRole in _nostrContext.Roles)
            {
                var role = AddEntityIfNotExists(new EM.Entities.Role
                {
                    Name = nRole.Name,
                },
                (r => r.Name == nRole.Name));

                var perms = grants
                    .Where(g => GrantsUtility.AnyFlag(g.Value, nRole.Grants))
                    .Select(g => new RolePermission
                    {
                        PermissionId = (long)g.Permission,
                        RoleId = role.Id
                    });

                foreach(var perm in perms)
                {
                    AddEntityIfNotExists(perm, p => p.RoleId == role.Id && p.PermissionId == perm.PermissionId);
                }
            }
        }

        private static void ImportUsers()
        {
            foreach(var nUser in _nostrContext.Users)
            {
                var nameParts = nUser.FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var user = AddEntityIfNotExists(new EM.Entities.User
                {
                    SurName = nameParts.Length > 0 ? nameParts[0] : "",
                    FirstName = nameParts.Length > 1 ? nameParts[1] : "",
                    LastName = nameParts.Length > 2 ? nameParts[2] : "",
                    Email = nUser.Email ?? "",
                    Snils = nUser.Snils ?? "",
                    Login = nUser.Login,
                    PasswordHash = (nUser.Password ?? "").GetHashString(),
                    IsBlocked = nUser.Blocked,
                    IsDeleted = nUser.Removed,
                    Remark = nUser.Description
                },
                u => u.Login == nUser.Login);

                foreach(var nUserRole in _nostrContext.UserRoles.Where(ur => ur.UsersId == nUser.Id))
                {
                    var role = _appContext.Roles.FirstOrDefault(r => r.Name == nUserRole.Roles.Name);
                    if (role == null)
                        continue;
                    
                    AddEntityIfNotExists(new EM.Entities.UserRole
                    {
                        RoleId = role.Id,
                        UserId = user.Id
                    },
                    ur => ur.RoleId == role.Id && ur.UserId == user.Id);
                }
            }
        }

        private static T AddEntityIfNotExists<T>(T entity, Func<T, bool> isAdded) where T: class
        {
            var exists = _appContext.Set<T>().FirstOrDefault(isAdded);
            if (exists != null)
                return exists;

            _appContext.Add(entity);
            _appContext.SaveChanges();
            return entity;
        }
    }
}
