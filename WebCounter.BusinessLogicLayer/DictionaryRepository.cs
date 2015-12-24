using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class DictionaryRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public DictionaryRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_Dictionary> SelectAll(int accessLevelId)
        {
            if ((AccessLevel)accessLevelId == AccessLevel.SystemAdministrator)
                return _dataContext.tbl_Dictionary;

            if ((AccessLevel)accessLevelId == AccessLevel.Administrator)
                return _dataContext.tbl_Dictionary.Where(d => d.AccessLevelID == (int)AccessLevel.User || d.AccessLevelID == (int)AccessLevel.Administrator);
            
            return _dataContext.tbl_Dictionary.Where(d => d.AccessLevelID == accessLevelId);
        }

    }
}