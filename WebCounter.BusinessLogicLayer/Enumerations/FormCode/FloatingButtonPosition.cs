using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.FormCode
{
    public enum FloatingButtonPosition
    {
        [Description("Справа")]
        Right = 0,
        [Description("Слева")]
        Left = 1,
        [Description("Сверху")]
        Top = 2,
        [Description("Снизу")]
        Bottom = 3
    }
}