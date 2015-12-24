using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum SiteActionTemplateCategory
    {        
        [Description("Базовый")]
        BaseTemplate = 0,
        [Description("Рассылка")]
        MassMail = 1,
        [Description("Событие")]
        Event = 2,
        [Description("Бизнес процесс")]
        Workflow = 3,        
        [Description("Системный")]
        System = 4,
        [Description("Персональный")]
        Personal = 5
    }
}