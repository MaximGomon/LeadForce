using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.WebSite
{
    public enum WebSiteElementStatus
    {
        [Description("Активна")]
        Active = 0,
        [Description("Не активна")]
        NoActive = 1
    }
}