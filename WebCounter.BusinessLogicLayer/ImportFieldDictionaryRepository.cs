using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ImportFieldDictionaryRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportFieldDictionaryRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ImportFieldDictionaryRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_ImportFieldDictionary> SelectByImportFieldID(Guid importFieldID)
        {
            return _dataContext.tbl_ImportFieldDictionary.Where(a => a.ImportFieldID == importFieldID).OrderBy(a => a.Order).ToList();
        }
    }
}