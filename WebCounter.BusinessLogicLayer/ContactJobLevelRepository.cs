using System;
using System.Linq;
using WebCounter.DataAccessLayer;


namespace WebCounter.BusinessLogicLayer
{
    public class ContactJobLevelRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ContactJobLevelRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified contact job level.
        /// </summary>
        /// <param name="contactJobLevel">The contact job level.</param>
        /// <returns></returns>
        public tbl_ContactJobLevel Add(tbl_ContactJobLevel contactJobLevel)
        {
            if (contactJobLevel.ID == Guid.Empty)
                contactJobLevel.ID = Guid.NewGuid();

            _dataContext.tbl_ContactJobLevel.AddObject(contactJobLevel);

            _dataContext.SaveChanges();

            return contactJobLevel;
        }



        /// <summary>
        /// Selects all.
        /// </summary>        
        /// <returns></returns>
        public IQueryable<tbl_ContactJobLevel> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_ContactJobLevel.Where(cjl => cjl.SiteID == siteId);
        }


        /// <summary>
        /// Selects the name of the by.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public tbl_ContactJobLevel SelectByName(Guid siteId, string name)
        {
            return _dataContext.tbl_ContactJobLevel.SingleOrDefault(o => o.Name.ToLower() == name.ToLower() && o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by name and create.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public tbl_ContactJobLevel SelectByNameAndCreate(Guid siteId, string name)
        {
            var contactJobLevel = SelectByName(siteId, name);
            if (contactJobLevel == null)
            {
                contactJobLevel = new tbl_ContactJobLevel { SiteID = siteId, Name = name };
                Add(contactJobLevel);
            }
            return contactJobLevel;
        }
    }
}