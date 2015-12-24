using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class SiteEventActionTemplateMap
    {
        public Guid ID { get; set; }
        public Guid SiteID { get; set; }
        public Guid SiteEventTemplateID { get; set; }
        public Guid SiteActionTemplateID { get; set; }
        public int StartAfter { get; set; }
        public int StartAfterTypeID { get; set; }
        public string MessageText { get; set; }
    }
}