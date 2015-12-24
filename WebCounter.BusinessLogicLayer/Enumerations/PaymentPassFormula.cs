using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum PaymentPassFormula
    {
        [Description("Остаток")]
        Balance = 0,
        [Description("Процент")]
        Percent = 1,
        [Description("Фиксированная сумма")]
        FixedSum = 2
    }
}