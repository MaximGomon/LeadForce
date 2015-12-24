using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PriceListRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceListRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PriceListRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_PriceList> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_PriceList.Where(pl => pl.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public tbl_PriceList SelectById(Guid priceListID)
        {
            return _dataContext.tbl_PriceList.Where(c => c.ID == priceListID).SingleOrDefault();
        }


    }
}