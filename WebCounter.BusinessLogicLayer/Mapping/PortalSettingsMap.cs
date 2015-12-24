using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    public class PortalSettingsMap
    {
        public Guid ID { get; set; }
        public Guid SiteID { get; set; }
        public string Title { get; set; }
        public string WelcomeMessage { get; set; }
        public string Domain { get; set; }
        public string Logo { get; set; }
        public string CompanyMessage { get; set; }
        public string HeaderTemplate { get; set; }
        public string MainMenuBackground { get; set; }
        public string BlockTitleBackground { get; set; }
    }
}
