using System;
using System.Collections.Generic;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class WorkflowTemplateElementMap
    {
        public Guid ID { get; set; }
        public Guid WorkflowTemplateID { get; set; }
        public string Name { get; set; }
        public int ElementType { get; set; }
        public bool Optional { get; set; }
        public string ResultName { get; set; }
        public bool AllowOptionalTransfer { get; set; }
        public bool ShowCurrentUser { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int StartAfter { get; set; }
        public int StartPeriod { get; set; }
        public int? DurationHours { get; set; }
        public int? DurationMinutes { get; set; }
        public int? ControlAfter { get; set; }
        public int? ControlPeriod { get; set; }
        public bool ControlFromBeginProccess { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public int? Condition { get; set; }
        public int? ActivityCount { get; set; }
        public List<WorkflowTemplateConditionEventMap> ConditionEvent { get; set; }
        public List<WorkflowTemplateElementResultMap> ElementResult { get; set; }
        public List<WorkflowTemplateElementTagMap> Tag { get; set; }
        public List<WorkflowTemplateElementPeriodMap> Period { get; set; }
        public List<WorkflowTemplateElementExternalRequestMap> ExternalRequest { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}