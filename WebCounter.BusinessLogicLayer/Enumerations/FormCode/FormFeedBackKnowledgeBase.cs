using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.FormCode
{
    public enum FormFeedBackKnowledgeBase
    {
        [Description("Не отражать")]
        None = 1,
        [Description("Отдельным шагом")]
        SeparateStep = 2,
        [Description("Вместе с вводом запроса")]
        WithRequest = 3
    }
}