using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer
{
    /// <summary>
    /// Site domain status
    /// </summary>
    public enum SiteDomainStatus
    {
        [Description("Добавлен")]
        Added = 1,
        [Description("Проверка не прошла")]
        CheckingFailed = 2,
        [Description("Проверяется")]
        Checking = 3,
        [Description("Внешний домен")]
        Attached = 4,
        [Description("Домен LeadForce")]
        LeadForceDomain = 5
    }
}