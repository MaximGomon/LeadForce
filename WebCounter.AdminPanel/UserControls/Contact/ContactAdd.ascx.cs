using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls.Contact
{
    public partial class ContactAdd : System.Web.UI.UserControl
    {
        public Guid SiteId = new Guid();



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Section Section
        {
            get
            {
                object o = ViewState["Section"];
                return (o == null ? Section.Monitoring : (Section)o);
            }
            set
            {
                ViewState["Section"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteId = ((LeadForceBasePage)Page).SiteId;

            hlCancel.NavigateUrl = UrlsData.AP_Contacts();

            tagsContact.ObjectID = ucContactEdit.ContactId;

            if (!Page.IsPostBack)
            {
                ucContactEdit.SiteId = SiteId;
                ucContactEdit.BindData();
            }
        }



        /// <summary>
        /// Handles the Click event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var saveResult = ucContactEdit.Save();
            tagsContact.SaveTags(ucContactEdit.ContactId);

            if (saveResult)
                Response.Redirect(UrlsData.AP_Contacts());
        }
    }
}