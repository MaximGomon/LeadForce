using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum Status
    {        
        [Description("В планах")]
        InPlans = 0,
        [Description("Активен")]
        Active = 1,
        [Description("Приостановлен")]
        Suspended = 2,
        [Description("Архив")]
        Archive = 3
    }
}
