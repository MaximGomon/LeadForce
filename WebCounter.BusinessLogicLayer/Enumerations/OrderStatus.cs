using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum OrderStatus
    {
        [Description("В планах")]
        InPlans = 1,        
        [Description("В работе")]
        InProcessing = 2,
        [Description("Выполнен")]
        Done = 3,
        [Description("Выполнен частично")]
        DoneInPart = 4,
        [Description("Отменен")]
        Canceled = 5
    }
}
