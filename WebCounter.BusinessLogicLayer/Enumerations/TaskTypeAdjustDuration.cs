using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum TaskTypeAdjustDuration
    {                
        [Description("Нет")]
        None = 1,
        [Description("Только автор")]
        OnlyCreator = 2,
        [Description("Автор или ответственный")]
        CreatorOrResponsible = 3
    }
}