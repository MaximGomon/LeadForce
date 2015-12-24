using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_OrderTypeMetaData))]
    public partial class tbl_OrderType
    {
        public class tbl_OrderTypeMetaData
        {
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(true)]
            public object ID { get; set; }

            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(true)]
            public object SiteID { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Название")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object Name { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Нумератор")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_Numerator", "ListValueField", "ID", "ListTextField", "Title")]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object NumeratorID { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Срок действия")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_ExpirationAction", "ListValueField", "ID", "ListTextField", "Name")]
            [ReadOnly(false)]
            public object ExpirationActionID { get; set; }

            [DataType("Bool")]
            [Display(Name = "Физическая доставка")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]            
            [ReadOnly(false)]
            public object IsPhysicalDelivery { get; set; }            
        }
    }
}
