using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.CronJob
{
    public enum LastRunStatus
    {
        [Description("Успешно")]
        Success = 0,
        [Description("Ошибка")]
        Error = 1
    }
}
