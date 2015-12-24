using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class InvoiceProductsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public InvoiceProductsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="invoiceProductId">The order product id.</param>
        /// <returns></returns>
        public tbl_InvoiceProducts SelectById(Guid invoiceProductId)
        {
            return _dataContext.tbl_InvoiceProducts.Where(o => o.ID == invoiceProductId).SingleOrDefault();
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="invoiceId">The invoice id.</param>
        /// <returns></returns>
        public IQueryable<tbl_InvoiceProducts> SelectAll(Guid invoiceId)
        {
            return _dataContext.tbl_InvoiceProducts.Where(o => o.InvoiceID == invoiceId);
        }



        /// <summary>
        /// Adds the specified order product.
        /// </summary>
        /// <param name="invoiceProduct">The order product.</param>
        /// <returns></returns>
        public tbl_InvoiceProducts Add(tbl_InvoiceProducts invoiceProduct)
        {
            invoiceProduct.ID = Guid.NewGuid();
            _dataContext.tbl_InvoiceProducts.AddObject(invoiceProduct);
            _dataContext.SaveChanges();

            return invoiceProduct;        
        }




        /// <summary>
        /// Updates the specified to updateinvoice products.
        /// </summary>
        /// <param name="toUpdateinvoiceProducts">To updateinvoice products.</param>
        /// <param name="invoiceId">The invoice id.</param>
        public void Update(List<InvoiceProductsMap> toUpdateinvoiceProducts, Guid invoiceId)
        {
            var existsinvoiceProducts = SelectAll(invoiceId).ToList();
            
            foreach (var updateinvoiceProduct in toUpdateinvoiceProducts)
            {
                var existinvoiceProduct = existsinvoiceProducts.Where(op => op.ID == updateinvoiceProduct.ID).SingleOrDefault();

                if (existinvoiceProduct == null)
                {
                    _dataContext.tbl_InvoiceProducts.AddObject(new tbl_InvoiceProducts()
                    {
                        ID = updateinvoiceProduct.ID,
                        InvoiceID = invoiceId,
                        ProductID = updateinvoiceProduct.ProductID,
                        AnyProductName = updateinvoiceProduct.AnyProductName,
                        SerialNumber = updateinvoiceProduct.SerialNumber,
                        PriceListID = updateinvoiceProduct.PriceListID,
                        Quantity = updateinvoiceProduct.Quantity,
                        UnitID = updateinvoiceProduct.UnitID,
                        CurrencyID = updateinvoiceProduct.CurrencyID,
                        Rate = updateinvoiceProduct.Rate,
                        CurrencyPrice = updateinvoiceProduct.CurrencyPrice,
                        CurrencyAmount = updateinvoiceProduct.CurrencyAmount,
                        Price = updateinvoiceProduct.Price,
                        Amount = updateinvoiceProduct.Amount,
                        SpecialOfferPriceListID = updateinvoiceProduct.SpecialOfferPriceListID,
                        Discount = updateinvoiceProduct.Discount,
                        CurrencyDiscountAmount = updateinvoiceProduct.CurrencyDiscountAmount,
                        DiscountAmount = updateinvoiceProduct.DiscountAmount,
                        CurrencyTotalAmount = updateinvoiceProduct.CurrencyTotalAmount,
                        TotalAmount = updateinvoiceProduct.TotalAmount
                    }); 
                }
                else
                {        
                    existinvoiceProduct.ProductID = updateinvoiceProduct.ProductID;
                    existinvoiceProduct.AnyProductName = updateinvoiceProduct.AnyProductName;
                    existinvoiceProduct.SerialNumber = updateinvoiceProduct.SerialNumber;
                    existinvoiceProduct.PriceListID = updateinvoiceProduct.PriceListID;
                    existinvoiceProduct.Quantity = updateinvoiceProduct.Quantity;
                    existinvoiceProduct.UnitID = updateinvoiceProduct.UnitID;
                    existinvoiceProduct.CurrencyID = updateinvoiceProduct.CurrencyID;
                    existinvoiceProduct.Rate = updateinvoiceProduct.Rate;
                    existinvoiceProduct.CurrencyPrice = updateinvoiceProduct.CurrencyPrice;
                    existinvoiceProduct.CurrencyAmount = updateinvoiceProduct.CurrencyAmount;
                    existinvoiceProduct.Price = updateinvoiceProduct.Price;
                    existinvoiceProduct.Amount = updateinvoiceProduct.Amount;
                    existinvoiceProduct.SpecialOfferPriceListID = updateinvoiceProduct.SpecialOfferPriceListID;
                    existinvoiceProduct.Discount = updateinvoiceProduct.Discount;
                    existinvoiceProduct.CurrencyDiscountAmount = updateinvoiceProduct.CurrencyDiscountAmount;
                    existinvoiceProduct.DiscountAmount = updateinvoiceProduct.DiscountAmount;
                    existinvoiceProduct.CurrencyTotalAmount = updateinvoiceProduct.CurrencyTotalAmount;
                    existinvoiceProduct.TotalAmount = updateinvoiceProduct.TotalAmount;
                }
            }

            foreach (var existsinvoiceProduct in existsinvoiceProducts)
            {
                if (toUpdateinvoiceProducts.Where(op => op.ID == existsinvoiceProduct.ID).SingleOrDefault() == null)
                    _dataContext.tbl_InvoiceProducts.DeleteObject(existsinvoiceProduct);
            }

            _dataContext.SaveChanges();
        }
    }
}