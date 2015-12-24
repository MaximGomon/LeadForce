using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum PublicationKind
    {
        [Description("Статья базы знаний")]
        KnowledgeBase = 1,
        [Description("Обсуждение")]
        Discussion = 2,
        [Description("Другая публикация")]
        OtherPublication = 3
    }
}