using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum ExpirationAction
    {
        [Description("Нет")]
        None = 1,
        [Description("Возврат денег по окончанию")]
        MoneyBack = 2,
        [Description("Закрытие по окончанию")]
        Close = 3
    }
}
