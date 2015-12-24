using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum Direction
    {
        [Description("Исходящее")]
        Out = 0,        
        [Description("Входящее")]
        In = 1
    }
}
