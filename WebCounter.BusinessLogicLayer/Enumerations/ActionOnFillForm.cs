using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer
{
    /// <summary>
    /// 
    /// </summary>
    public enum ActionOnFillForm
    {
        [Description("Переход на URL")]
        Redirect = 0,
        [Description("Сообщение в Popup окне")]
        PopupMessage = 1,
        [Description("Сообщение вместо формы")]
        Message = 2
    }
}