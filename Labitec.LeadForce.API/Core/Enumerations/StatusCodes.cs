using System.ComponentModel;

namespace Labitec.LeadForce.API.Core.Enumerations
{
    public enum StatusCodes
    {
        [Description("OK")]
        Ok = 0,
        [Description("AccessDenied")]
        AccessDenied = 1,
        [Description("InvalidParameters")]
        InvalidParameters = 2,
        [Description("InternalError")]
        InternalError = 3
    }
}