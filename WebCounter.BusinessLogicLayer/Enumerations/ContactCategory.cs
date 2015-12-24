using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer
{
    /// <summary>
    /// Contact category
    /// </summary>
    public enum ContactCategory
    {
        [Description("Известный контакт")]
        Known = 0,
        [Description("Активный контакт")]
        Active = 1,
        [Description("Активный сверх тарифа")]
        ActiveAboveTariff = 2,
        [Description("Удаленный контакт")]
        Deleted = 3,
        [Description("Анонимный контакт")]
        Anonymous = 4
    }
}