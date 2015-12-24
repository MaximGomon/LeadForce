using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PublicationTermsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedPublicationRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PublicationTermsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_PublicationTerms> SelectAll()
        {
            return _dataContext.tbl_PublicationTerms;
        }


        /// <summary>
        /// Selects the name by id.
        /// </summary>
        /// <param name="publicationID">The publication ID.</param>
        /// <returns></returns>
        public IQueryable<tbl_PublicationTerms> SelectByPublicationId(Guid publicationID)
        {
            return _dataContext.tbl_PublicationTerms.Where(c => c.PublicationID == publicationID);
        }


        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="publicationID">The publication ID.</param>
        public void DeleteAll(Guid publicationID)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_PublicationTerms WHERE PublicationID = @PublicationID",
                                             new SqlParameter { ParameterName = "PublicationID", Value = publicationID });
        }



        /// <summary>
        /// Adds the specified source product complectations.
        /// </summary>
        /// <param name="publicationTerms">The publication terms.</param>
        public void Add(List<tbl_PublicationTerms> publicationTerms)
        {
            foreach (var publicationTerm in publicationTerms)
                _dataContext.tbl_PublicationTerms.AddObject(new tbl_PublicationTerms()
                {
                    ID = publicationTerm.ID,
                    PublicationID = publicationTerm.PublicationID,
                    PublicationCode = publicationTerm.PublicationCode,
                    ElementCode = publicationTerm.ElementCode,
                    Term = publicationTerm.Term,
                    Description = publicationTerm.Description
                });

            _dataContext.SaveChanges();
        }

    }
}