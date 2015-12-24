using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.BusinessLogicLayer
{
    public class AddContactParams
    {
        public Guid ID { get; set; }
        public Guid SiteID { get; set; }
        public string RefferURL { get; set; }
        public string UserIP { get; set; }
        public string UserAgent { get; set; }
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public string OperatingSystemName { get; set; }
        public string OperatingSystemVersion { get; set; }
        public string Resolution { get; set; }
        public string MobileDevice { get; set; }
        public ActivityType ActivityTypeID { get; set; }
        public string ActivityCode { get; set; }
        public SessionSource SessionSource { get; set; }
        public Guid? RefferID { get; set; }
    }
}