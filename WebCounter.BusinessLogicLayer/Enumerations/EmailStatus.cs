using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum EmailStatus
    {
        [Description("Разрешен")]
        Allow = 0,   
        [Description("Не подтвержден")]
        NotConfirmed = 1,
        [Description("Запрещен")]
        Banned = 2,
        [Description("Не работает")]
        DoesNotWork = 3
    }
}
