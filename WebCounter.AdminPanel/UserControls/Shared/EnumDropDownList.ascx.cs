using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel.UserControls.Shared
{
    public partial class EnumDropDownList : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public int? SelectedValue
        {
            get { return !string.IsNullOrEmpty(ddlEnum.SelectedValue) ? (int?)int.Parse(ddlEnum.SelectedValue) : null; }
            set { ddlEnum.SelectedIndex = ddlEnum.Items.IndexOf(ddlEnum.Items.FindByValue(value.ToString())); }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Enumeration { get; set; }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
                //EnumHelper.EnumToDropDownList(Type.GetType(Enumeration), ref ddlEnum);
        }
    }
}