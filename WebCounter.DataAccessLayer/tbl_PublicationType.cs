using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_PublicationTypeMetaData))]
    public partial class tbl_PublicationType
    {
        public class tbl_PublicationTypeMetaData
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
            [Display(Name = "Нумератор")]
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_Numerator", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object NumeratorID { get; set; }

            [DataType("Int")]
            [Display(Name = "Порядок")]
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object Order { get; set; }


            [DataType("Image")]
            [Display(Name = "Лого")]
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object Logo { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Текст «Добавить»")]
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object TextAdd { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Текст «Пометка к добавить»")]
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object TextMarkToAdd { get; set; }


            [DataType(DataType.MultilineText)]
            [Display(Name = "Текст «Нравится»")]
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object TextLike { get; set; }

            [DataType(DataType.MultilineText)]
            [Display(Name = "Текст «Нравится для комментария»")]
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object TextLikeComment { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Вид публикации")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_PublicationKind", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object PublicationKindID { get; set; }

            [DataType("Bool")]
            [Display(Name = "Включать в поиск")]
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsSearchable { get; set; }


            [DataType("Dictionary")]
            [Display(Name = "Доступ по умолчанию")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_PublicationAccessRecord", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object PublicationAccessRecordID { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Доступ по умолчанию для комментирования")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_PublicationAccessComment", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object PublicationAccessCommentID { get; set; }

            [DataType("Bool")]
            [Display(Name = "Значение по умолчанию")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsDefault { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Формировать запрос")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_RequestSourceType", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object RequestSourceTypeID { get; set; }            
        }
    }
}
