using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ImportKeyRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportKeyRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ImportKeyRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public tbl_ImportKey SelectByKey(string key, string table)
        {
            return _dataContext.tbl_ImportKey.SingleOrDefault(o => o.Key == key && o.TableName == table);
        }



        /// <summary>
        /// Adds the specified import key.
        /// </summary>
        /// <param name="importKey">The import key.</param>
        public void Add(tbl_ImportKey importKey)
        {
            importKey.ID = Guid.NewGuid();
            _dataContext.tbl_ImportKey.AddObject(importKey);
            _dataContext.SaveChanges();
        }
    }
}