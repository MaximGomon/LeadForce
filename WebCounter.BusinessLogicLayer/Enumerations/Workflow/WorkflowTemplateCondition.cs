using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum WorkflowTemplateCondition
    {
        [Description("Формула И")]
        And = 0,
        [Description("Формула ИЛИ")]
        Or = 1,
        [Description("N событий")]
        NEvent = 2,
        [Description("Произвольное событие")]
        AnyEvent = 3
    }
}