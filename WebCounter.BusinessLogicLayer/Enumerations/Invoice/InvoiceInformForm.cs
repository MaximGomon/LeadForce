using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.Invoice
{
    public enum InvoiceInformForm
    {
        [Description("Всегда вложение")]
        Always = 1,
        [Description("Вложение для отдельных счетов")]
        Separate = 2,
        [Description("Без вложения")]
        None = 3
    }
}
