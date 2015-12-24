using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_PublicationCategoryMetaData))]
    public partial class tbl_PublicationCategory
    {
        public class tbl_PublicationCategoryMetaData
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
            [Display(Name = "Родительская категория")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_PublicationCategory", "ListValueField", "ID", "ListTextField", "Title")]            
            [ReadOnly(false)]
            public object ParentID { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Название")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object Title { get; set; }

            [DataType("Bool")]
            [Display(Name = "Значение по умолчанию")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]            
            [ReadOnly(false)]
            public object IsDefault { get; set; }

            [DataType("Int")]
            [Display(Name = "Порядок")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object Order { get; set; }
        }
    }
}
