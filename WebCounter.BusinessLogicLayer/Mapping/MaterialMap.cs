using System;
using System.Collections.Generic;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class MaterialMap
    {
        public Guid ID { get; set; }
        public Guid SiteID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string OldValue { get; set; }
        public Guid? WorkflowTemplateID { get; set; }
    }
}
