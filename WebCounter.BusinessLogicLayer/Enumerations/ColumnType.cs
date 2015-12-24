using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer
{
    /// <summary>
    /// Types of columns
    /// </summary>
    public enum ColumnType
    {
        [Description("Строка")]
        String = 1,
        [Description("Дата")]
        Date = 2,
        [Description("Справочник")]
        Enum = 3,
        [Description("Число")]
        Number = 4,
        [Description("Текст")]
        Text = 5,
        [Description("Логическое")]
        Logical = 6
    }
}