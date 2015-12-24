using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer
{
    /// <summary>
    /// Action statuses
    /// </summary>
    public enum ActionStatus
    {
        [Description("Запланировано")]
        Scheduled = 1,
        [Description("Выполнено")]
        Done = 2,
        [Description("Отменено")]
        Cancelled = 3,
        [Description("Ошибка")]
        Error = 4,
        [Description("Запрещен")]
        InvalidEmail = 5,
        [Description("Отменено (ограничение тарифа)")]
        CancelledAboveTraffic = 6
    }
}