using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class InvoiceProductsMap
    {
        public Guid ID { get; set; }
        public Guid InvoiceID { get; set; }
        public Guid ProductID { get; set; }
        public Guid? TaskID { get; set; }
        public string AnyProductName { get; set; }
        public string SerialNumber { get; set; }
        public Guid? PriceListID { get; set; }
        public decimal Quantity { get; set; }
        public Guid UnitID { get; set; }
        public Guid CurrencyID { get; set; }
        public decimal Rate { get; set; }
        public decimal CurrencyPrice { get; set; }
        public decimal CurrencyAmount { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public Guid? SpecialOfferPriceListID { get; set; }
        public decimal? Discount { get; set; }
        public decimal? CurrencyDiscountAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal CurrencyTotalAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
