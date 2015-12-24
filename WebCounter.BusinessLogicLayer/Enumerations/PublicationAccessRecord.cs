using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum PublicationAccessRecord
    {
        [Description("Публичное")]
        Public = 1,
        [Description("Компания")]
        Company = 2,
        [Description("Личное")]
        Personal = 3,
        [Description("Анонимный доступ")]
        Anonymous = 4
    }
}