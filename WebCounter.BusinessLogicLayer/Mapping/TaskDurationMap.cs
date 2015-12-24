using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class TaskDurationMap
    {
        public Guid ID { get; set; }
        public DateTime? SectionDateStart { get; set; }
        public DateTime? SectionDateEnd { get; set; }
        public int? ActualDurationHours { get; set; }
        public int? ActualDurationMinutes { get; set; }
        public Guid? ResponsibleID { get; set; }
        public string Comment { get; set; }
        public bool IsTask { get; set; }
    }
}
