using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum WorkflowTemplateStatus
    {
        [Description("В планах")]
        InPlans = 0,
        [Description("Действующий")]
        Active = 1,
        [Description("Архив")]
        Archive = 2
    }
}