using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum ContactRoleType
    {
        [Description("Общий адрес")]
        GeneralEmail = 0,
        [Description("Адрес для контакта")]
        ContactRole = 1,
        [Description("Адрес для процесса")]
        WorkflowRole = 2
    }
}