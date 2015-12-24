using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.Request
{
    public enum ServiceLevelInform
    {        
        [Description("Нет")]
        Not = 1,
        [Description("Еженедельно")]
        EveryWeek = 2,
        [Description("Ежедневно")]
        EveryDay = 3
    }
}