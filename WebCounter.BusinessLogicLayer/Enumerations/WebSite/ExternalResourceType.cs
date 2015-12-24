using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.WebSite
{
    public enum ExternalResourceType
    {
        [Description("JavaScript")]
        JavaScript = 0,
        [Description("CSS")]
        CSS = 1
    }
}