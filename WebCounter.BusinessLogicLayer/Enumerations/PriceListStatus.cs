using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum PriceListStatus
    {
        [Description("В планах")]
        Planned = 1,
        [Description("Текущий")]
        Current = 2,
        [Description("Архив")]
        Archive = 3
    }
}