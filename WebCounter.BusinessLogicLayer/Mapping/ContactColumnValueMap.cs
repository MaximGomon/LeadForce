using System;
using System.Collections.Generic;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class ContactColumnValueMap
    {
        public Guid ID { get; set; }
        public Guid ContactID { get; set; }
        public Guid SiteColumnID { get; set; }
        public string StringValue { get; set; }
        public DateTime? DateValue { get; set; }
        public Guid? SiteColumnValueID { get; set; }
        public bool? LogicalValue { get; set; }
        public int TypeID { get; set; }
    }
}
