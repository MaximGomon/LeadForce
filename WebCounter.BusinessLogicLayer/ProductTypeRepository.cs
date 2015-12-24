using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ProductTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ProductTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public List<tbl_ProductType> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_ProductType.Where(o => o.SiteID == siteId).ToList();
        }



        /// <summary>
        /// Selects the type by id.
        /// </summary>
        /// <param name="typeId">The type id.</param>
        /// <returns></returns>
        public tbl_ProductType SelectById(Guid typeId)
        {
            return _dataContext.tbl_ProductType.Single(pc => pc.ID == typeId);
        }

    }
}