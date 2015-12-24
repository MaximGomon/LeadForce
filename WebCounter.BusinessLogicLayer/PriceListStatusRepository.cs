using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PriceListStatusRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceListStatusRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PriceListStatusRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_PriceListStatus> SelectAll()
        {
            return _dataContext.tbl_PriceListStatus.ToList();
        }



        /// <summary>
        /// Selects the status by id.
        /// </summary>
        /// <param name="priceListStatusId">The price list status id.</param>
        /// <returns></returns>
        public tbl_PriceListStatus SelectById(int priceListStatusId)
        {
            return _dataContext.tbl_PriceListStatus.Single(pc => pc.ID == priceListStatusId);
        }

    }
}