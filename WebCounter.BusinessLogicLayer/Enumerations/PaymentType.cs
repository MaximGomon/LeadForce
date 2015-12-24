using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum PaymentType
    {
        [Description("Приход")]
        Income = 0,
        [Description("Расход")]
        Outgo = 1,
        [Description("Трансфер")]
        Transfer = 2,
        [Description("Акт приход")]
        ActIncome = 3,
        [Description("Акт расход")]
        ActOutgo = 4
    }
}