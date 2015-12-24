using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.WebSite
{
    public enum ResourcePlace
    {
        [Description("В Header")]
        Header = 0,
        [Description("После Body")]
        Body = 1,
        [Description("Где указано")]
        InPlace = 2
    }
}