using System;
using System.Collections.Generic;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class SiteActionTemplateRecipientMap
    {
        public Guid ID { get; set; }
        public Guid SiteActionTemplateID { get; set; }
        public Guid? ContactID { get; set; }
        public Guid? ContactRoleID { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Key { get; set; }
    }
}
