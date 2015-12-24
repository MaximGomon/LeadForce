using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.FormCode
{
    public enum FormContactCategory
    {
        [Description("Все")]
        All = 0,
        [Description("Известные")]
        Known = 1,
        [Description("Неизвестные")]
        Unknown = 2
    }
}