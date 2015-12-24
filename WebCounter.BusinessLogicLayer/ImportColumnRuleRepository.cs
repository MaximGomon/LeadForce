using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ImportColumnRuleRepository
    {
        private WebCounterEntities _dataContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ImportColumnRuleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ImportColumnRuleRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by import column ID.
        /// </summary>
        /// <param name="importColumnID">The import column ID.</param>
        /// <returns></returns>
        public tbl_ImportColumnRule SelectByImportColumnID(Guid importColumnID)
        {
            return _dataContext.tbl_ImportColumnRule.Where(a => a.ImportColumnID == importColumnID).SingleOrDefault();
        }


        public List<tbl_ImportColumnRule> SelectByImportID(Guid importID)
        {
            return _dataContext.tbl_ImportColumnRule.Where(a => a.ImportID == importID).ToList();
        }



        /// <summary>
        /// Adds the specified import column rule.
        /// </summary>
        /// <param name="importColumnRule">The import column rule.</param>
        /// <returns></returns>
        public tbl_ImportColumnRule Add(tbl_ImportColumnRule importColumnRule)
        {
            if (importColumnRule.ID == Guid.Empty)
                importColumnRule.ID = Guid.NewGuid();
            _dataContext.tbl_ImportColumnRule.AddObject(importColumnRule);
            _dataContext.SaveChanges();

            return importColumnRule;
        }



        public void DeleteByImportID(Guid importID)
        {
            var importColumnRules = SelectByImportID(importID);
            foreach (var importColumnRule in importColumnRules)
                _dataContext.DeleteObject(importColumnRule);
            _dataContext.SaveChanges();
        }
    }
}