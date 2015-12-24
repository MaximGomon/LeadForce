using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.FormCode
{
    public enum ShowTextBlockInMaster
    {
        [Description("Нет")]
        One = 1,
        [Description("Текст")]
        Text = 2,
        [Description("HTML")]
        HTML = 3
    }
}