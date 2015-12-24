using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.Request
{
    public enum RequestSourceCategory
    {
        [Description("Обращение")]
        Request = 1,        
        [Description("Сообщение")]
        Message = 2,
        [Description("Звонок")]
        Call = 3,
        [Description("Документ")]
        Document = 4
    }
}