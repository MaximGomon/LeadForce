using System;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequirementHistoryRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementHistoryRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequirementHistoryRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified requirement history.
        /// </summary>
        /// <param name="requirementHistory">The requirement history.</param>
        /// <returns></returns>
        public tbl_RequirementHistory Add(tbl_RequirementHistory requirementHistory)
        {
            requirementHistory.ID = Guid.NewGuid();
            requirementHistory.CreatedAt = DateTime.Now;

            _dataContext.tbl_RequirementHistory.AddObject(requirementHistory);
            _dataContext.SaveChanges();

            return requirementHistory;
        }
    }
}