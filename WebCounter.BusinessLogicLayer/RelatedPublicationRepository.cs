using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RelatedPublicationRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedPublicationRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RelatedPublicationRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_RelatedPublication> SelectAll()
        {
            return _dataContext.tbl_RelatedPublication;
        }


        /// <summary>
        /// Selects the name by id.
        /// </summary>
        /// <param name="publicationID">The publication ID.</param>
        /// <returns></returns>
        public IQueryable<tbl_RelatedPublication>  SelectByPublicationId(Guid publicationID)
        {
            return _dataContext.tbl_RelatedPublication.Where(c => c.PublicationID == publicationID);
        }


        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="publicationID">The publication ID.</param>
        public void DeleteAll(Guid publicationID)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_RelatedPublication WHERE PublicationID = @PublicationID",
                                             new SqlParameter { ParameterName = "PublicationID", Value = publicationID });
        }



        /// <summary>
        /// Adds the specified source product complectations.
        /// </summary>
        /// <param name="relatedPublications">The related publications.</param>
        public void Add(List<tbl_RelatedPublication> relatedPublications)
        {
            foreach (var relatedPublication in relatedPublications)
                _dataContext.tbl_RelatedPublication.AddObject(new tbl_RelatedPublication()
                {
                    PublicationID = relatedPublication.PublicationID,
                    ModuleID = relatedPublication.ModuleID,
                    RecordID = relatedPublication.RecordID,
                    ID = relatedPublication.ID
                });

            _dataContext.SaveChanges();
        }


        public string GetRecordTitle(string tableName, Guid? recordId)
        {
            string columnNme;
            switch (tableName)
            {
                case "tbl_Order":
                    columnNme = "Number";
                    break;
                case "tbl_Company":
                    columnNme = "Name";
                    break;
                case "tbl_Contact":
                    columnNme = "UserFullName";
                    break;
                case "tbl_SourceMonitoring":
                    columnNme = "Name";
                    break;
                case "tbl_MassMail":
                    columnNme = "Name";
                    break;
                default:
                    columnNme = "Title";
                    break;
            }

            string sql = String.Format("SELECT [{0}] FROM {1} WHERE ID='{2}'", columnNme, tableName, recordId.ToString());
            return _dataContext.ExecuteStoreQuery<string>(sql).FirstOrDefault();
        }

    }
}