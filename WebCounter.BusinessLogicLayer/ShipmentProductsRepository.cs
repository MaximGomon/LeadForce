using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ShipmentProductsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ShipmentProductsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="shipmentProductId">The order product id.</param>
        /// <returns></returns>
        public tbl_ShipmentProducts SelectById(Guid shipmentProductId)
        {
            return _dataContext.tbl_ShipmentProducts.Where(o => o.ID == shipmentProductId).SingleOrDefault();
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ShipmentProducts> SelectAll(Guid shipmentId)
        {
            return _dataContext.tbl_ShipmentProducts.Where(o => o.ShipmentID == shipmentId);
        }



        /// <summary>
        /// Adds the specified order product.
        /// </summary>
        /// <param name="shipmentProduct">The order product.</param>
        /// <returns></returns>
        public tbl_ShipmentProducts Add(tbl_ShipmentProducts shipmentProduct)
        {
            shipmentProduct.ID = Guid.NewGuid();
            _dataContext.tbl_ShipmentProducts.AddObject(shipmentProduct);
            _dataContext.SaveChanges();

            return shipmentProduct;        
        }




        /// <summary>
        /// Updates the specified to updateshipment products.
        /// </summary>
        /// <param name="toUpdateshipmentProducts">To updateshipment products.</param>
        /// <param name="shipmentId">The shipment id.</param>
        public void Update(List<ShipmentProductsMap> toUpdateshipmentProducts, Guid shipmentId)
        {
            var existsshipmentProducts = SelectAll(shipmentId).ToList();
            
            foreach (var updateshipmentProduct in toUpdateshipmentProducts)
            {
                var existshipmentProduct = existsshipmentProducts.Where(op => op.ID == updateshipmentProduct.ID).SingleOrDefault();

                if (existshipmentProduct == null)
                {
                    _dataContext.tbl_ShipmentProducts.AddObject(new tbl_ShipmentProducts()
                    {
                        ID = updateshipmentProduct.ID,
                        ShipmentID = shipmentId,
                        ProductID = updateshipmentProduct.ProductID,
                        AnyProductName = updateshipmentProduct.AnyProductName,
                        SerialNumber = updateshipmentProduct.SerialNumber,
                        PriceListID = updateshipmentProduct.PriceListID,
                        Quantity = updateshipmentProduct.Quantity,
                        UnitID = updateshipmentProduct.UnitID,
                        CurrencyID = updateshipmentProduct.CurrencyID,
                        Rate = updateshipmentProduct.Rate,
                        CurrencyPrice = updateshipmentProduct.CurrencyPrice,
                        CurrencyAmount = updateshipmentProduct.CurrencyAmount,
                        Price = updateshipmentProduct.Price,
                        Amount = updateshipmentProduct.Amount,
                        SpecialOfferPriceListID = updateshipmentProduct.SpecialOfferPriceListID,
                        Discount = updateshipmentProduct.Discount,
                        CurrencyDiscountAmount = updateshipmentProduct.CurrencyDiscountAmount,
                        DiscountAmount = updateshipmentProduct.DiscountAmount,
                        CurrencyTotalAmount = updateshipmentProduct.CurrencyTotalAmount,
                        TotalAmount = updateshipmentProduct.TotalAmount
                    }); 
                }
                else
                {        
                    existshipmentProduct.ProductID = updateshipmentProduct.ProductID;
                    existshipmentProduct.AnyProductName = updateshipmentProduct.AnyProductName;
                    existshipmentProduct.SerialNumber = updateshipmentProduct.SerialNumber;
                    existshipmentProduct.PriceListID = updateshipmentProduct.PriceListID;
                    existshipmentProduct.Quantity = updateshipmentProduct.Quantity;
                    existshipmentProduct.UnitID = updateshipmentProduct.UnitID;
                    existshipmentProduct.CurrencyID = updateshipmentProduct.CurrencyID;
                    existshipmentProduct.Rate = updateshipmentProduct.Rate;
                    existshipmentProduct.CurrencyPrice = updateshipmentProduct.CurrencyPrice;
                    existshipmentProduct.CurrencyAmount = updateshipmentProduct.CurrencyAmount;
                    existshipmentProduct.Price = updateshipmentProduct.Price;
                    existshipmentProduct.Amount = updateshipmentProduct.Amount;
                    existshipmentProduct.SpecialOfferPriceListID = updateshipmentProduct.SpecialOfferPriceListID;
                    existshipmentProduct.Discount = updateshipmentProduct.Discount;
                    existshipmentProduct.CurrencyDiscountAmount = updateshipmentProduct.CurrencyDiscountAmount;
                    existshipmentProduct.DiscountAmount = updateshipmentProduct.DiscountAmount;
                    existshipmentProduct.CurrencyTotalAmount = updateshipmentProduct.CurrencyTotalAmount;
                    existshipmentProduct.TotalAmount = updateshipmentProduct.TotalAmount;
                }
            }

            foreach (var existsshipmentProduct in existsshipmentProducts)
            {
                if (toUpdateshipmentProducts.Where(op => op.ID == existsshipmentProduct.ID).SingleOrDefault() == null)
                    _dataContext.tbl_ShipmentProducts.DeleteObject(existsshipmentProduct);
            }

            _dataContext.SaveChanges();
        }
    }
}