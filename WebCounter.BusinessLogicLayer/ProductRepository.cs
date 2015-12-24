using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ProductRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ProductRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_Product> SelectAll()
        {
            return _dataContext.tbl_Product;
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Product> SelectBySiteId(Guid siteId)
        {
            return _dataContext.tbl_Product.Where(c => c.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by ids.
        /// </summary>
        /// <param name="productIds">The product ids.</param>
        /// <returns></returns>
        public IQueryable<tbl_Product> SelectByIds(List<Guid> productIds)
        {
            return _dataContext.tbl_Product.Where(o => productIds.Contains(o.ID));
        }
        


        /// <summary>
        /// Selects the by  id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public tbl_Product SelectById(Guid siteId, Guid productId)
        {
            return _dataContext.tbl_Product.Where(c => c.SiteID == siteId && c.ID == productId).SingleOrDefault();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public tbl_Product SelectById(Guid productId)
        {
            return _dataContext.tbl_Product.SingleOrDefault(c => c.ID == productId);
        }



        /// <summary>
        /// Selects the by brand id.
        /// </summary>
        /// <param name="brandId">The brand id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Product> SelectByBrandId(Guid brandId)
        {
            return _dataContext.tbl_Product.Where(c => c.BrandID == brandId && c.ProductStatusID == (int)ProductStatus.Current);
        }



        /// <summary>
        /// Selects the name by id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public string SelectNameById(Guid productId)
        {
            return _dataContext.tbl_Product.Where(p => p.ID == productId).Select(p => p.Title).SingleOrDefault();
        }

        

        public tbl_Product Save(Guid SiteID, Guid productId, string title, string SKU, Guid? categoryID, Guid? productTypeID, decimal? price, int? productStatusID,
            Guid? unitID, decimal? wholesalePrice, Guid? supplierID, Guid? manufacturerID, string supplierSKU, Guid? countryID, string description, Guid? ownerId, Guid? BrandID)
        {
            var product = _dataContext.tbl_Product.SingleOrDefault(p => p.ID == productId) ?? new tbl_Product();

            product.Title = title;
            product.SiteID = SiteID;
            product.SKU = SKU;
            product.ProductCategoryID = categoryID;
            product.ProductTypeID = productTypeID;
            product.Price = price;
            product.ProductStatusID = productStatusID;
            product.UnitID = unitID;
            product.WholesalePrice = wholesalePrice;
            product.SupplierID = supplierID;
            product.ManufacturerID = manufacturerID;
            product.SupplierSKU = supplierSKU;
            product.CountryID = countryID;
            product.Description = description;
            product.BrandID = BrandID;

            if (productId == Guid.Empty)
            {
                product.ID = Guid.NewGuid();
                product.CreatedAt = DateTime.Now;
                product.OwnerID = ownerId;
                _dataContext.tbl_Product.AddObject(product);
            }
            else            
                product.ModifiedAt = DateTime.Now;            

            _dataContext.SaveChanges();
            return product;
        }
    }
}