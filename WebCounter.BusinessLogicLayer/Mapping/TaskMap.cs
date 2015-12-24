using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class TaskMap
    {
        public Guid ID { get; set; }
        public Guid CreatorID { get; set; }
        public string Title { get; set; }
        public int PlanDurationHours { get; set; }
        public int PlanDurationMinutes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? MainMemberContactID { get; set; }
        public Guid? MainMemberCompanyID { get; set; }
    }
}