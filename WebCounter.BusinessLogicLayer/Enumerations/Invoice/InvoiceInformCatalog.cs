using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.Invoice
{
    public enum InvoiceInformCatalog
    {
        [Description("Нет")]
        None = 1,
        [Description("Еженедельно")]
        Weekly = 2,
        [Description("Раз в две недели")]
        Biweekly = 3,
        [Description("Ежемесячно")]
        Monthly = 4
    }
}
