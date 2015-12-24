using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum SenderProcessing
    {        
        [Description("Загружать по известным контактам")]
        LoadOfKnownContacts = 0,
        [Description("Создавать новый контакт")]
        CreateNewContact = 1,
        [Description("Загружать по справочнику уровни обслуживания")]
        ServiceLevelContacts = 2
    }
}
