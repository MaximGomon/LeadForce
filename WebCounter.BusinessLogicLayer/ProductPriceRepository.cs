using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ProductPriceRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductPriceRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ProductPriceRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_ProductPrice> SelectAll()
        {
            return _dataContext.tbl_ProductPrice.ToList();
        }



        /// <summary>
        /// Selects the list of product complectation by product id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public List<tbl_ProductPrice> SelectByProductId(Guid productId)
        {
            return _dataContext.tbl_ProductPrice.Where(pc => pc.ProductID == productId).ToList();
        }



        /// <summary>
        /// Selects the by product and price list id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <param name="priceListId">The price list id.</param>
        /// <returns></returns>
        public tbl_ProductPrice SelectByProductAndPriceListId(Guid productId, Guid priceListId)
        {
            return _dataContext.tbl_ProductPrice.Where(pc => pc.ProductID == productId && pc.PriceListID == priceListId).FirstOrDefault();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_ProductPrice SelectById(Guid id)
        {
            return _dataContext.tbl_ProductPrice.SingleOrDefault(o => o.ID == id);
        }




        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="ProductID">The product ID.</param>
        public void DeleteAll(Guid ProductID)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_ProductPrice WHERE ProductID = @ProductID",
                                             new SqlParameter { ParameterName = "ProductID", Value = ProductID });
        }



        /// <summary>
        /// Adds the specified source product complectations.
        /// </summary>
        /// <param name="productPrices">The product prices.</param>
        public void Add(List<tbl_ProductPrice> productPrices)
        {
            foreach (var productPrice in productPrices)
                _dataContext.tbl_ProductPrice.AddObject(new tbl_ProductPrice()
                                                            {
                                                                ID =  productPrice.ID,
                                                                DateFrom = productPrice.DateFrom,
                                                                DateTo = productPrice.DateTo,
                                                                Discount = productPrice.Discount,
                                                                Price = productPrice.Price,
                                                                PriceListID = productPrice.PriceListID,
                                                                ProductID = productPrice.ProductID,
                                                                QuantityFrom = productPrice.QuantityFrom,
                                                                QuantityTo=productPrice.QuantityTo,
                                                                SupplierID = productPrice.SupplierID
                                                            });

            _dataContext.SaveChanges();
        }


    }
}