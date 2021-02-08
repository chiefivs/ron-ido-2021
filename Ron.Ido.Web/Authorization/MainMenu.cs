using Ron.Ido.BM.Models.Account;
using Ron.Ido.EM.Enums;

namespace Ron.Ido.Web.Authorization
{
    public class MainMenu
    {
        public static MenuItem[] Items = new[] {
            new MenuItem("Настройки", "settings", PermissionEnum.SETTINGS),
            new MenuItem("Пользователи", "users", PermissionEnum.USER_VIEW, PermissionEnum.USER_CREATE, PermissionEnum.USER_EDIT, PermissionEnum.USER_DEL)
        };
    }
}
