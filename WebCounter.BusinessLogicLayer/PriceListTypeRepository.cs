using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PriceListTypeRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceListTypeRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PriceListTypeRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_PriceListType> SelectAll()
        {
            return _dataContext.tbl_PriceListType.ToList();
        }



        /// <summary>
        /// Selects the type by id.
        /// </summary>
        /// <param name="priceListTypeId">The price list type id.</param>
        /// <returns></returns>
        public tbl_PriceListType SelectById(int priceListTypeId)
        {
            return _dataContext.tbl_PriceListType.Single(pc => pc.ID == priceListTypeId);
        }

    }
}