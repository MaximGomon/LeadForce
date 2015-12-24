using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ProductComplectationRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductComplectationRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ProductComplectationRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_ProductComplectation> SelectAll()
        {
            return _dataContext.tbl_ProductComplectation.ToList();
        }



        /// <summary>
        /// Selects the list of product complectation by product id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public List<tbl_ProductComplectation> SelectByProductId(Guid productId)
        {
            return _dataContext.tbl_ProductComplectation.Where(pc => pc.ProductID == productId).ToList();
        }



        /// <summary>
        /// Selects the list of product complectation by base product id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public List<tbl_ProductComplectation> SelectByBaseProductId(Guid productId)
        {
            return _dataContext.tbl_ProductComplectation.Where(pc => pc.BaseProductID == productId).ToList();
        }



        /// <summary>
        /// Selects the by base products.
        /// </summary>
        /// <param name="productIds">The product ids.</param>
        /// <returns></returns>
        public List<tbl_ProductComplectation> SelectByBaseProducts(List<Guid> productIds)
        {
            return _dataContext.tbl_ProductComplectation.Where(pc => productIds.Contains(pc.BaseProductID)).ToList();
        }



        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="BaseProductID">The base product ID.</param>
        public void DeleteAll(Guid BaseProductID)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_ProductComplectation WHERE BaseProductID = @BaseProductID",
                                             new SqlParameter { ParameterName = "BaseProductID", Value = BaseProductID });
        }



        /// <summary>
        /// Adds the specified source product complectations.
        /// </summary>
        /// <param name="productComplectations">The product complectations.</param>
        public void Add(List<tbl_ProductComplectation> productComplectations)
        {
            foreach (var productComplectation in productComplectations)
                _dataContext.tbl_ProductComplectation.AddObject(new tbl_ProductComplectation()
                                                                    {
                                                                        BaseProductID = productComplectation.BaseProductID,
                                                                        ProductID = productComplectation.ProductID,
                                                                        ID = productComplectation.ID,
                                                                        Quantity = productComplectation.Quantity,
                                                                        Price = productComplectation.Price
                                                                    });

            _dataContext.SaveChanges();
        }


    }
}