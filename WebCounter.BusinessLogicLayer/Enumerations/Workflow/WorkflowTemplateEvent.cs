using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum WorkflowTemplateEvent
    {
        [Description("Создание")]
        Create = 0,
        [Description("Изменение")]
        Edit = 1
    }
}