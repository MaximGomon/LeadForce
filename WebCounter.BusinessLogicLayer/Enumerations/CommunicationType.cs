using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum CommunicationType
    {       
        [Description("Телефон")]
        Phone = 0,
        [Description("Email")]
        Email = 1,
        [Description("Сотовый")]
        Cellular = 2,
        [Description("Facebook")]
        Facebook = 3,
        [Description("VKontakte")]
        VKontakte = 4,
        [Description("Twitter")]
        Twitter = 5,
        [Description("Web")]
        Web = 6

    }
}