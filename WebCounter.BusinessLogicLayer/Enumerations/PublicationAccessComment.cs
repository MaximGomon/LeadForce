using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum PublicationAccessComment
    {
        [Description("Публичное")]
        Public = 1,
        [Description("Компания")]
        Company = 2,
        [Description("Личное")]
        Personal = 3
    }
}