using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum TaskMemberStatus
    {        
        [Description("План")]
        Plan = 0,        
        [Description("Приглашен")]
        Invited = 2,
        [Description("Подтвердил участник")]
        MemberConfirmed = 3,
        [Description("Подтвердил организатор")]
        OrganizerConfirmed = 4,
        [Description("Отказ (не интересно)")]
        RefusedNotInterest = 5,
        [Description("Отказ (нет возможности)")]
        RefusedFailureNoWay = 6,
        [Description("Участвовал")]
        Participated = 7,
        [Description("Не участвовал")]
        NotParticipated = 8,
        [Description("Отменено участие")]
        ParticipatedCanceled = 9,
        [Description("Подана заявка")]
        BidGiven = 10,
        [Description("В работе")]
        InWork = 11
    }
}
