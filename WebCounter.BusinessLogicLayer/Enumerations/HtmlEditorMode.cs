using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum HtmlEditorMode
    {
        [Description("Простой")]
        Simple = 0,
        [Description("Сложный")]
        Extended = 1
    }
}
