using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    public class WebSiteStructureMap
    {
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }
        public Guid WebSiteID { get; set; }
        public int WebSiteStructureTypeID { get; set; }
        public int WebSiteElementStatusID { get; set; }
        public string Title { get; set; }
    }
}
