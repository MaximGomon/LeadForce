using System;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class ReminderExtended
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateTime ReminderDate { get; set; }
        public Guid ContactID { get; set; }
        public Guid ModuleID { get; set; }
        public Guid ObjectID { get; set; }
        public string ModuleTitle { get; set; }
    }
}
