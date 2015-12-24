using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum ProcessingOfAutoReplies
    {                
        [Description("Пропускать")]
        Skip = 0,
        [Description("Загружать")]
        Download = 1
    }
}