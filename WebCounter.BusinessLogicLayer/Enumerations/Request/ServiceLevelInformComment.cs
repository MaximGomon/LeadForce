using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.Request
{
    public enum ServiceLevelInformComment
    {        
        [Description("Нет")]
        Not = 1,
        [Description("Персональные")]
        Personal = 2,
        [Description("По личным требованиям")]
        PersonalRequirement = 3,
        [Description("По всем требованиям")]
        All = 4
    }
}