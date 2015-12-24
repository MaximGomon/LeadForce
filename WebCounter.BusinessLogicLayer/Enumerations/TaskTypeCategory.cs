using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum TaskTypeCategory
    {                           
        [Description("Короткая непрерывная задача")]
        TODO = 1,
        [Description("Встреча")]
        Meeting = 2,
        [Description("Длительная задача")]
        LongTermTask = 3,
        [Description("Мероприятие")]
        Event = 4
    }
}