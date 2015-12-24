using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.WebSite
{
    public enum WebSiteStructureType
    {        
        [Description("Статическая страница")]
        StaticPage = 0,
        [Description("Страница формы")]
        FormPage = 1
    }
}