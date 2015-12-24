using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_PublicationStatusToPublicationTypeMetaData))]
    public partial class tbl_PublicationStatusToPublicationType
    {
        public class tbl_PublicationStatusToPublicationTypeMetaData
        {
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(true)]
            public object ID { get; set; }

            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(true)]
            public object SiteID { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Статус публикации")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_PublicationStatus", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object PublicationStatusID { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Тип публикации")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_PublicationType", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object PublicationTypeID { get; set; }
        }
    }
}
