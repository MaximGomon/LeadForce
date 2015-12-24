using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AccessProfileRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessProfileRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AccessProfileRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_AccessProfile> SelectAll()
        {
            return _dataContext.tbl_AccessProfile.OrderBy(a => a.Title).ToList();
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public List<tbl_AccessProfile> SelectAll(Guid? siteId)
        {
            if (siteId == null)
                return _dataContext.tbl_AccessProfile.Where(a => a.SiteID.Equals(siteId)).OrderBy(a => a.Title).ToList();

            return _dataContext.tbl_AccessProfile.Where(a => a.SiteID == siteId).OrderBy(a => a.Title).ToList();
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="accessProfileID">The access profile ID.</param>
        /// <returns></returns>
        public tbl_AccessProfile SelectById(Guid accessProfileID)
        {
            return _dataContext.tbl_AccessProfile.FirstOrDefault(a => a.ID == accessProfileID);
        }



        /// <summary>
        /// Selects the by product id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public tbl_AccessProfile SelectByProductId(Guid productId)
        {
            return _dataContext.tbl_AccessProfile.FirstOrDefault(o => o.ProductID == productId);
        }



        /// <summary>
        /// Adds the specified access profile.
        /// </summary>
        /// <param name="accessProfile">The access profile.</param>
        /// <returns></returns>
        public tbl_AccessProfile Add(tbl_AccessProfile accessProfile)
        {
            if (accessProfile.ID == Guid.Empty)
                accessProfile.ID = Guid.NewGuid();

            _dataContext.tbl_AccessProfile.AddObject(accessProfile);
            _dataContext.SaveChanges();

            return accessProfile;
        }



        /// <summary>
        /// Updates the specified access profile.
        /// </summary>
        /// <param name="accessProfile">The access profile.</param>
        public void Update(tbl_AccessProfile accessProfile)
        {
            _dataContext.SaveChanges();
        }
    }
}