using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum Period
    {
        [Description("Визит")]
        Visit = 0,
        [Description("Час")]
        Hour = 1,
        [Description("День")]
        Day = 2,
        [Description("Месяц")]
        Month = 3
    }
}