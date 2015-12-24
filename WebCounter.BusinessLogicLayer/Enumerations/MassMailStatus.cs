using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer
{
    /// <summary>
    /// Mail mail statuses
    /// </summary>
    public enum MassMailStatus
    {
        [Description("Запланирована")]
        Scheduled = 0,
        [Description("Проведена")]
        Done = 1,
        [Description("Отменена")]
        Cancel = 2
    }
}