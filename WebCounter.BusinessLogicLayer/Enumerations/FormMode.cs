using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum FormMode
    {
        [Description("Встроенная форма")]
        Inner = 0,        
        [Description("Всплывающая форма")]
        Popup = 1,
        [Description("Автовызов")]
        AutoCall = 2,
        [Description("Плавающая кнопка")]
        FloatingButton = 3,
        [Description("Вызов на закрытие")]
        CallOnClosing = 4
    }
}
