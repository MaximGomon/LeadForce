using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_PriceListMetaData))]
    public partial class tbl_PriceList
    {
        public class tbl_PriceListMetaData
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

            [DataType("Dictionary")]
            [Display(Name = "Тип")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_PriceListType", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object PriceListTypeID { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Статус")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_PriceListStatus", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object PriceListStatusID { get; set; }


            [DataType(DataType.Text)]
            [Display(Name = "Комментарий")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object Comment { get; set; }

            

        }
    }
}
