using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class BankRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public BankRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="bankId">The bank id.</param>
        /// <returns></returns>
        public tbl_Bank SelectById(Guid bankId)
        {
            return _dataContext.tbl_Bank.SingleOrDefault(b => b.ID == bankId);
        }
    }
}