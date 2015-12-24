using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_PaymentPassRuleMetaData))]
    public partial class tbl_PaymentPassRule
    {
        public class tbl_PaymentPassRuleMetaData
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
            [Display(Name = "Наименование")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object Title { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Тип")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_PaymentType", "ListValueField", "ID", "ListTextField", "Title")]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object PaymentTypeID { get; set; }


            [DataType("Bool")]
            [Display(Name = "Активное")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsActive { get; set; }

            [DataType("Bool")]
            [Display(Name = "Проводить автоматически")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsAutomatic { get; set; }
        }
    }
}
