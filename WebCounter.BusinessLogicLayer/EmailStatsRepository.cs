using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class EmailStatsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailStatsRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public EmailStatsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public tbl_EmailStats SelectByEmail(string email)
        {
            return _dataContext.tbl_EmailStats.SingleOrDefault(o => o.Email == email);
        }



        /// <summary>
        /// Updates the specified email stats.
        /// </summary>
        /// <param name="emailStats">The email stats.</param>
        public void Update(tbl_EmailStats emailStats)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified email stats.
        /// </summary>
        /// <param name="emailStats">The email stats.</param>
        public void Add(tbl_EmailStats emailStats)
        {
            emailStats.ID = Guid.NewGuid();
            _dataContext.tbl_EmailStats.AddObject(emailStats);
            _dataContext.SaveChanges();
        }
    }
}