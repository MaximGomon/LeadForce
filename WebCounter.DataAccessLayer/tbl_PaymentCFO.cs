using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_PaymentCFOMetaData))]
    public partial class tbl_PaymentCFO
    {
        public class tbl_PaymentCFOMetaData
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
            [Display(Name = "Категория проводки")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_PaymentPassCategory", "ListValueField", "ID", "ListTextField", "Title")]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object PaymentPassCategoryID { get; set; }


            [DataType(DataType.MultilineText)]
            [Display(Name = "Примечание")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = true)]
            [ReadOnly(false)]
            public object Note { get; set; }

        }
    }
}
