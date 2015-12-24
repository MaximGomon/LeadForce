using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum ProcessingOfReturns
    {        
        [Description("Менять статус Email")]
        ChangeEmailStatus = 0,        
        [Description("Пропускать")]
        Skip = 1,
        [Description("Загружать")]
        Download = 2
    }
}
