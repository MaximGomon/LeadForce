using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum WorkflowTemplateAutomaticMethod
    {
        [Description("Нет")]
        None = 0,
        [Description("По изменению записи")]
        EditRecord = 1,
        [Description("По действию пользователя")]
        ActivityContact = 2,
        /*[Description("По таймеру")]
        Timer = 2*/
    }
}