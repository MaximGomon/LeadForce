using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum TaskMembersCount
    {                           
        [Description("Один")]
        One = 1,
        [Description("Основной+список")]
        MainPlusList = 2,
        [Description("Список")]
        List = 3        
    }
}