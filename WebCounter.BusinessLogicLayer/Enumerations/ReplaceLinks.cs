using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum ReplaceLinks
    {                           
        [Description("Нет")]
        None = 0,
        [Description("Ссылки Google Analytics")]
        GoogleLinks = 1,
        [Description("Ссылки LeadForce")]
        ThroughService = 2
    }
}