using System;
using System.Linq;
using WebCounter.DataAccessLayer;


namespace WebCounter.BusinessLogicLayer
{
    public class ContactFunctionInCompanyRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ContactFunctionInCompanyRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified contact function in company.
        /// </summary>
        /// <param name="contactFunctionInCompany">The contact function in company.</param>
        /// <returns></returns>
        public tbl_ContactFunctionInCompany Add(tbl_ContactFunctionInCompany contactFunctionInCompany)
        {
            if (contactFunctionInCompany.ID == Guid.Empty)
                contactFunctionInCompany.ID = Guid.NewGuid();

            _dataContext.tbl_ContactFunctionInCompany.AddObject(contactFunctionInCompany);

            _dataContext.SaveChanges();

            return contactFunctionInCompany;
        }



        /// <summary>
        /// Selects all.
        /// </summary>        
        /// <returns></returns>
        public IQueryable<tbl_ContactFunctionInCompany> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_ContactFunctionInCompany.Where(cf => cf.SiteID == siteId);
        }



        /// <summary>
        /// Selects the name of the by.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public tbl_ContactFunctionInCompany SelectByName(Guid siteId, string name)
        {
            return _dataContext.tbl_ContactFunctionInCompany.SingleOrDefault(o => o.Name.ToLower() == name.ToLower() && o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by name and create.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public tbl_ContactFunctionInCompany SelectByNameAndCreate(Guid siteId, string name)
        {
            var contactFunctionInCompany = SelectByName(siteId, name);
            if (contactFunctionInCompany == null)
            {
                contactFunctionInCompany = new tbl_ContactFunctionInCompany { SiteID = siteId, Name = name };
                Add(contactFunctionInCompany);
            }
            return contactFunctionInCompany;
        }
    }
}