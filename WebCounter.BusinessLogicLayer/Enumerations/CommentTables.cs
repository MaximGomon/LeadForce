using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum CommentTables
    {
        [Description("RequirementComments")]
        tbl_RequirementComment = 0,
        [Description("RequestComments")]
        tbl_RequestComment = 1,
        [Description("InvoiceComments")]
        tbl_InvoiceComment = 2,
        [Description("ShipmentComments")]
        tbl_ShipmentComment = 3
    }
}
