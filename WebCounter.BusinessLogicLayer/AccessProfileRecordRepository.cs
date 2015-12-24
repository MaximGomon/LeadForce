using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AccessProfileRecordRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessProfileRecordRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AccessProfileRecordRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by access profile ID.
        /// </summary>
        /// <param name="accessProfileID">The access profile ID.</param>
        /// <returns></returns>
        public List<tbl_AccessProfileRecord> SelectByAccessProfileID(Guid accessProfileID)
        {
            return _dataContext.tbl_AccessProfileRecord.Where(a => a.AccessProfileID == accessProfileID).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="accessProfileRecordID">The access profile record ID.</param>
        /// <returns></returns>
        public tbl_AccessProfileRecord SelectById(Guid accessProfileRecordID)
        {
            return _dataContext.tbl_AccessProfileRecord.Where(a => a.ID == accessProfileRecordID).FirstOrDefault();
        }



        /// <summary>
        /// Adds the specified access profile record.
        /// </summary>
        /// <param name="accessProfileRecord">The access profile record.</param>
        /// <returns></returns>
        public tbl_AccessProfileRecord Add(tbl_AccessProfileRecord accessProfileRecord)
        {
            if (accessProfileRecord.ID == Guid.Empty)
                accessProfileRecord.ID = Guid.NewGuid();

            _dataContext.tbl_AccessProfileRecord.AddObject(accessProfileRecord);
            _dataContext.SaveChanges();

            return accessProfileRecord;
        }



        /// <summary>
        /// Updates the specified access profile record.
        /// </summary>
        /// <param name="accessProfileRecord">The access profile record.</param>
        public void Update(tbl_AccessProfileRecord accessProfileRecord)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified access profile record ID.
        /// </summary>
        /// <param name="accessProfileRecordID">The access profile record ID.</param>
        public void Delete(Guid accessProfileRecordID)
        {
            var accessProfileRecord = SelectById(accessProfileRecordID);
            if (accessProfileRecord != null)
            {
                _dataContext.DeleteObject(accessProfileRecord);
                _dataContext.SaveChanges();
            }
        }
    }
}