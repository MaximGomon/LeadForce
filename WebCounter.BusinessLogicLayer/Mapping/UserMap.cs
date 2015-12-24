using System;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    public class UserMap
    {
        public Guid ID { get; set; }
        public Guid SiteID { get; set; }
        public Guid? ContactID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int AccessLevelID { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public Guid? AccessProfileID { get; set; }
        public Guid? SiteAccessProfileID { get; set; }
        public string UserFullName { get; set; }
        public Guid? CompanyID { get; set; }
        public bool IsTemplateSite { get; set; }
    }
}
