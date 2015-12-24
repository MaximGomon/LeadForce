using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum MassWorkflowType
    {
        [Description("Вебинар (online мероприятие)")]
        Webinar = 1,
        [Description("Семинар (offline мероприятие)")]
        Seminar = 2,
        [Description("Телемаркетинг")]
        Telemarketing = 3,
        [Description("Серия email сообщений")]
        EmailMessages = 4,
        [Description("Другое мероприятие")]
        Other = 5
    }
}