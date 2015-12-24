using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer
{
    /// <summary>
    /// Formula type
    /// </summary>
    public enum FormulaType
    {
        [Description("не пусто")]
        NotEmpty = 1,
        [Description("пусто")]
        Empty = 2,
        [Description("выбор из списка")]
        SelectFromList = 3,
        [Description("с начала")]
        StartWith = 4,
        [Description("по маске")]
        Mask = 5,
        [Description("больше")]
        Greater = 6,
        [Description("меньше")]
        Less = 7,
        [Description("больше или равно")]
        GreaterOrEqual = 8,
        [Description("меньше или равно")]
        LessOrEqual = 9
    }
}
