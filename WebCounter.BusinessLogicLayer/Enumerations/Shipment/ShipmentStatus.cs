using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.Shipment
{
    public enum ShipmentStatus
    {                      
        [Description("Подготовлен")]
        Prepared = 1,
        [Description("Подписан")]
        Signed = 2,
        [Description("Отменен")]
        Canceled = 3        
    }
}
