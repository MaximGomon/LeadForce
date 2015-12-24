using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum AccessLevel
    {
        [Description("Пользователь")]
        User = 0,
        [Description("Администратор")]
        Administrator = 1,
        [Description("Системный администратор")]
        SystemAdministrator = 2,
        [Description("Портал")]
        Portal = 3
    }
}