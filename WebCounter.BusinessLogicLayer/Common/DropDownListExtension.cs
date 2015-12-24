using System.Web.UI.WebControls;

namespace WebCounter.BusinessLogicLayer.Common
{
    public static class DropDownListExtension
    {
        public static int FindItemIndexByValue(this DropDownList dropDownList, string value)
        {
            return dropDownList.Items.IndexOf(dropDownList.Items.FindByValue(value));
        }
    }
}
