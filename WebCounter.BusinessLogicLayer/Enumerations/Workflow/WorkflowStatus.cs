using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum WorkflowStatus
    {
        [Description("Исполняется")]
        Active = 0,
        [Description("Завершено")]
        Done = 1,
        [Description("Отменено")]
        Cancelled = 2,
        [Description("Ошибка")]
        Error = 3
    }
}