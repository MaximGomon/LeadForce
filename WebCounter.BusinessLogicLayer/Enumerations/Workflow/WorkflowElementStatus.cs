using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum WorkflowElementStatus
    {
        [Description("В ожидании")]
        Pending = 0,
        [Description("Выполнено")]
        Done = 1,
        [Description("Ошибка")]
        Error = 2,
        [Description("Просрочен")]
        Expired = 3
    }
}