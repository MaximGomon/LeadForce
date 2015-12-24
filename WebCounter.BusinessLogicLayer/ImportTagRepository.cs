using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ImportTagRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTagRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ImportTagRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ImportTag> SelectAll(Guid importId)
        {
            return _dataContext.tbl_ImportTag.Where(a => a.ImportID == importId);
        }



        ///// <summary>
        ///// Selects the by id.
        ///// </summary>
        ///// <param name="importTagId">The import tag id.</param>
        ///// <returns></returns>
        public tbl_ImportTag SelectById(Guid importTagId)
        {
            return _dataContext.tbl_ImportTag.SingleOrDefault(a => a.ID == importTagId);
        }



        ///// <summary>
        ///// Adds the specified import tag.
        ///// </summary>
        ///// <param name="importTag">The import tag.</param>
        ///// <returns></returns>
        public tbl_ImportTag Add(tbl_ImportTag importTag)
        {
            if (importTag.ID == Guid.Empty)
                importTag.ID = Guid.NewGuid();
            _dataContext.tbl_ImportTag.AddObject(importTag);
            _dataContext.SaveChanges();

            return importTag;
        }



        ///// <summary>
        ///// Updates the specified import tag.
        ///// </summary>
        ///// <param name="importTag">The import tag.</param>
        public void Update(tbl_ImportTag importTag)
        {
            _dataContext.SaveChanges();
        }



        ///// <summary>
        ///// Saves the specified import tag list.
        ///// </summary>
        ///// <param name="importTagList">The import tag list.</param>
        ///// <param name="importId">The import id.</param>
        public void Save(List<ImportTagMap> importTagList, Guid importId)
        {
            var existsImportTags = SelectAll(importId).ToList();

            foreach (var importTag in importTagList)
            {
                var existImportTag = existsImportTags.SingleOrDefault(a => a.ID == importTag.ID);

                if (existImportTag == null)
                {
                    _dataContext.tbl_ImportTag.AddObject(new tbl_ImportTag
                                                             {
                                                                 ID = importTag.ID,
                                                                 ImportID = importId,
                                                                 SiteTagID = importTag.SiteTagID,
                                                                 Operation = importTag.Operation
                                                             });
                }
                else
                {
                    existImportTag.ID = importTag.ID;
                    existImportTag.ImportID = importId;
                    existImportTag.SiteTagID = importTag.SiteTagID;
                    existImportTag.Operation = importTag.Operation;
                }
            }

            foreach (var existsImportTag in existsImportTags)
            {
                if (importTagList.SingleOrDefault(op => op.ID == existsImportTag.ID) == null)
                    _dataContext.tbl_ImportTag.DeleteObject(existsImportTag);
            }

            _dataContext.SaveChanges();
        }
    }
}