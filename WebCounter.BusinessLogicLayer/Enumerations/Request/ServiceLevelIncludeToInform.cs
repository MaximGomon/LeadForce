using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.Request
{
    public enum ServiceLevelIncludeToInform
    {
        [Description("Все требования")]
        All = 1,
        [Description("Личные требования")]
        Personal = 2
    }
}