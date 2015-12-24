using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_PaymentPassCategoryMetaData))]
    public partial class tbl_PaymentPassCategory
    {
        public class tbl_PaymentPassCategoryMetaData
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
            public object Title { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Примечание")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object Note { get; set; }

        }
    }
}
