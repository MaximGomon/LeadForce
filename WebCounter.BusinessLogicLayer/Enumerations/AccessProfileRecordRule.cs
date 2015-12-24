using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum AccessProfileRecordRule
    {
        [Description("Не используется")]
        NotUsed = 0,
        [Description("Собственное значение")]
        SelfValue = 1,
        [Description("Конкретная запись")]
        SpecificValue = 2
    }
}