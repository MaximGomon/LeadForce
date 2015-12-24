using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum TaskMemberRole
    {        
        [Description("Заказчик")]
        Client = 0,        
        [Description("Исполнитель")]
        Executor = 1,
        [Description("Партнер")]
        Partner = 2,
        [Description("Подрядчик")]
        Contractor = 3,
        [Description("Участник")]
        Member = 4,
        [Description("Ответственный")]
        Responsible = 5
    }
}
