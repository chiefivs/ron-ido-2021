using Ron.Ido.BM.Models.Account;
using Ron.Ido.EM.Enums;

namespace Ron.Ido.Web.Authorization
{
    public class MainMenu
    {
        public static MenuItem[] Items = new[]
        {
            new MenuItem("Процедура")
            { 
                Submenu = new []
                {
                    new MenuItem("Поиск", "applies/search", PermissionEnum.APPLY_VIEW),
                    new MenuItem("Платежи", "applies/payments", PermissionEnum.PAYMENTS_VIEW),
                }
            },
            new MenuItem("Прием и выдача документов")
            {
                Submenu = new []
                {
                    new MenuItem("Прием заявлений", "applies/acceptance", PermissionEnum.APPLY_VIEW, PermissionEnum.APPLY_CREATE, PermissionEnum.APPLY_EDIT, PermissionEnum.APPLY_DEL),
                    new MenuItem("Дубликаты", "duplicates/duplicates", PermissionEnum.APPLY_VIEW),
                }
            },
            new MenuItem("Доступ")
            {
                Submenu = new[] {
                    new MenuItem("Пользователи", "admin/users", PermissionEnum.USER_VIEW, PermissionEnum.USER_CREATE, PermissionEnum.USER_EDIT, PermissionEnum.USER_DEL),
                    new MenuItem("Роли", "admin/roles", PermissionEnum.ROLE_VIEW, PermissionEnum.ROLE_CREATE, PermissionEnum.ROLE_EDIT, PermissionEnum.ROLE_DEL),
                }
            },
            new MenuItem("Настройки")
            {
                Submenu = new[] {
                    new MenuItem("Статусная модель", "admin/statusModel", PermissionEnum.SETTINGS),
                }
            }
        };
    }
}
