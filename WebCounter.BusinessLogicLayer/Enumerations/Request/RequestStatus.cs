using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.Request
{
    public enum RequestStatus
    {
        [Description("Новый запрос")]
        New = 0,
        [Description("В работе")]
        InWork = 1,
        [Description("Закрыт")]
        Closed = 2,
        [Description("Дубль")]
        Duplicate = 3
    }
}