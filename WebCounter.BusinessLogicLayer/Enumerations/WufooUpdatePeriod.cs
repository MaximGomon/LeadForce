using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum WufooUpdatePeriod
    {
        [Description("В ручную")]
        Manual = 0,
        [Description("Постоянно")]
        Always = 1,
        [Description("Раз в час")]
        Hourly = 2,
        [Description("Раз в сутки")]
        Daily = 3
    }
}
