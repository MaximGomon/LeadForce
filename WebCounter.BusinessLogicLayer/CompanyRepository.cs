using System;
using System.Linq;
using WebCounter.DataAccessLayer;


namespace WebCounter.BusinessLogicLayer
{
    public class CompanyRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public CompanyRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Adds the specified company.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns></returns>
        public tbl_Company Add(tbl_Company company)
        {
            company.ID = Guid.NewGuid();
            company.CreatedAt = DateTime.Now;
            _dataContext.tbl_Company.AddObject(company);
            _dataContext.SaveChanges();            
            return company;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Company> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_Company.Where(c => c.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        public tbl_Company SelectById(Guid siteId, Guid companyId)
        {
            return _dataContext.tbl_Company.SingleOrDefault(a => a.SiteID == siteId && a.ID == companyId);
        }


        /// <summary>
        /// Selects the by title.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public tbl_Company SelectByName(Guid siteId, string name)
        {
            return _dataContext.tbl_Company.SingleOrDefault(o => o.Name.ToLower() == name.ToLower() && o.SiteID == siteId);
        }



        /// <summary>
        /// Searches the by domain.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="domain">The domain.</param>
        /// <returns></returns>
        public tbl_Company SearchByDomain(Guid siteId, string domain)
        {
            return _dataContext.tbl_Company.FirstOrDefault(a => a.SiteID == siteId && a.Web.Contains(domain));
        }



        /// <summary>
        /// Updates the specified company.
        /// </summary>
        /// <param name="company">The company.</param>
        public void Update(tbl_Company company)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Selects the by name and create.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public tbl_Company SelectByNameAndCreate(Guid siteId, string name)
        {
            var company = SelectByName(siteId, name);
            if (company == null)
            {
                company = new tbl_Company { SiteID = siteId, Name = name };
                var status = _dataContext.tbl_Status.FirstOrDefault(a => a.SiteID == siteId && a.IsDefault) ??
                             _dataContext.tbl_Status.FirstOrDefault(a => a.SiteID == siteId);
                company.StatusID = status.ID;
                Add(company);
            }
            return company;
        }
    }
}