using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum ProductStatus
    {
        [Description("В планах")]
        Plan = 0,
        [Description("Текущий")]
        Current = 1,
        [Description("Архив")]
        Archive = 2
    }
}