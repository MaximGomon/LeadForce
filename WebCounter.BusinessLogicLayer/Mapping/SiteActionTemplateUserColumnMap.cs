using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class SiteActionTemplateUserColumnMap
    {
        public Guid ID { get; set; }
        public Guid SiteID { get; set; }
        public Guid SiteEventTemplateID { get; set; }
        public Guid SiteColumnID { get; set; }
        public string StringValue { get; set; }
        public DateTime? DateValue { get; set; }
        public Guid? SiteColumnValueID { get; set; }
    }
}