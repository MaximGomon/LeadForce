using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum WorkflowTemplateElementType
    {
        [Description("Сообщение")]
        Message = 0,
        [Description("Задача")]
        Task = 1,
        [Description("Ожидание события")]
        WaitingEvent = 2,
        [Description("Действие")]
        Activity = 3,
        [Description("Подпроцесс")]
        SubProcess = 4,
        [Description("Начало процесса")]
        StartProcess = 5,
        [Description("Окончание процесса")]
        EndProcess = 6,
        [Description("Сегментация")]
        Tag = 7,
        [Description("Скоринг")]
        Scoring = 8,
        [Description("Внешний запрос")]
        ExternalRequest = 9
    }
}