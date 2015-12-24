using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum TaskStatus
    {        
        [Description("Поручена")]
        Charged = 0,
        [Description("В планах")]
        Planned = 1,
        [Description("В работе")]
        InWork = 2,
        [Description("Выполнена")]
        Completed = 3,
        [Description("Отклонена")]
        Rejected = 4,
        [Description("Отменена")]
        Canceled = 5,
        [Description("Отложена")]
        HoldOver = 6
    }
}
