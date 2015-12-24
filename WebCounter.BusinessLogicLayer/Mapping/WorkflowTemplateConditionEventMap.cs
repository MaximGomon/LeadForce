using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class WorkflowTemplateConditionEventMap
    {
        public Guid ID { get; set; }
        public Guid? WorkflowTemplateID { get; set; }
        public Guid? WorkflowTemplateElementEventID { get; set; }
        public int Category { get; set; }
        public int? ActivityType { get; set; }
        public string Code { get; set; }
        public int? ActualPeriod { get; set; }
        public string Requisite { get; set; }
        public int? Formula { get; set; }
        public string Value { get; set; }
    }
}
