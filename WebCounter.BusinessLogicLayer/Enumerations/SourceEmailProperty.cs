using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum SourceEmailProperty
    {
        [Description("Заголовок")]
        Title = 0,
        [Description("Отправитель")]
        FromName = 1,
        [Description("Email отправителя")]
        FromEmail = 2,
        [Description("Текст")]
        Text = 3
    }
}
