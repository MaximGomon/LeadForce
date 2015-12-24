using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class SiteEventTemplateActivityMap
    {
        public Guid ID { get; set; }
        public Guid SiteID { get; set; }
        public Guid SiteEventTemplateID { get; set; }
        public int? EventCategoryID { get; set; }
        public int? ActivityTypeID { get; set; }
        public string ActivityCode { get; set; }
        public int? ActualPeriod { get; set; }
        public string Option { get; set; }
        public int? FormulaID { get; set; }
        public string Value { get; set; }
    }
}