using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.FormCode
{
    public enum PopupAlign
    {
        [Description("Верху слева")]
        TopLeft = 0,
        [Description("Верху по центру")]
        TopCenter = 1,
        [Description("Верху справа")]
        TopRight = 2,
        [Description("Посередине слева")]
        MiddleLeft = 3,
        [Description("По центру")]
        Center = 4,
        [Description("Посередине справа")]
        MiddleRight = 5,
        [Description("Внизу слева")]
        BottomLeft = 6,
        [Description("Внизу по центру")]
        BottomCenter = 7,
        [Description("Внизу справа")]
        BottomRight = 8
    }
}