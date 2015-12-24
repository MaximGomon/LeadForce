using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.Invoice
{
    public enum InvoiceStatus
    {        
        [Description("Не выставлен")]
        NotSend = 0,        
        [Description("Ожидание оплаты")]
        PendingPayment = 1,
        [Description("Частично оплачен")]
        PartialPaid = 2,
        [Description("Оплачен")]
        Paid = 3,
        [Description("Отменен")]
        Canceled = 4        
    }
}
