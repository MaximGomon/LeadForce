using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.FormCode
{
    public enum PopupEffectAppear
    {
        [Description("Без эффектов")]
        None = 0,
        [Description("Выпадение сверху")]
        SlideUp = 1,
        [Description("Выдвижение справа")]
        SlideRight = 2,
        [Description("Выдвижение снизу")]
        SlideDown = 3,
        [Description("Выдвижение слева")]
        SlideLeft = 4,
        [Description("Увеличение")]
        Zoom = 5
    }
}