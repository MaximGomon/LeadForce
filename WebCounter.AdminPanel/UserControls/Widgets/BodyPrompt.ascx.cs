using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.UserControls.Widgets
{
    public partial class BodyPrompt : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Code
        {
            get { return (string) ViewState["Code"]; }
            set
            {
                ViewState["Code"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var dataManager = new DataManager();
            var term = dataManager.Term.SelectByCode(CurrentUser.Instance.SiteID, Code);
            if (term != null)
                lrlDescription.Text = term.Description;
            else
                this.Visible = false;
        }
    }
}