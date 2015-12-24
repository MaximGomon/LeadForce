using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class WorkflowTemplateElementPeriodMap
    {
        public Guid ID { get; set; }
        public Guid WorkflowTemplateElementID { get; set; }
        public int DayWeek { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
    }
}