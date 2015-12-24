using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum PriceListType
    {
        [Description("Прайс-лист")]
        PriceList = 1,
        [Description("Прайс-лист поставщика")]
        SupplierPriceList = 2,
        [Description("Скидка/акция")]
        Discount = 3
    }
}