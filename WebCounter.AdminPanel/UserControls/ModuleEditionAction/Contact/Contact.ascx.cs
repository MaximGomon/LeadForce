using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.ModuleEditionAction.Contact
{
    public partial class Contact : System.Web.UI.UserControl
    {
        public Access access;
        public Guid _contactID;
        public Guid SiteId = new Guid();
        private DataManager dataManager = new DataManager();



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

            ((Site)Page.Master).HideInaccessibleTabs(ref RadTabStrip1, ref RadMultiPage1);

            string contactId = Page.RouteData.Values["id"] as string;

            var category = Request.QueryString["c"];
            if (!string.IsNullOrEmpty(category))
                hlCancel.NavigateUrl = UrlsData.AP_Contacts() + "?c=" + category;
            else
                hlCancel.NavigateUrl = UrlsData.AP_Contacts();

            if (!Guid.TryParse(contactId, out _contactID))
            {
                Response.Redirect(UrlsData.AP_Contacts());
            }


            if (!Page.IsPostBack)
            {
                ucContactEdit.ContactId = _contactID;
                ucContactEdit.SiteId = SiteId;
                ucContactEdit.Section = Section;
                ucContactEdit.BindData();

                tagsContact.ObjectID = ucContactEdit.ContactId;
            }

            access = Access.Check(ucContactEdit.OwnerId, ucContactEdit.CompanyId);
            if (!access.Write)
            {
                lbtnSave.Visible = false;
            }

        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var saveResult = ucContactEdit.Save();
            tagsContact.SaveTags(ucContactEdit.ContactId);

            if (saveResult)
            {
                var category = Request.QueryString["c"];
                if (!string.IsNullOrEmpty(category))
                    Response.Redirect(UrlsData.AP_Contacts() + "?c=" + category);
                else
                    Response.Redirect(UrlsData.AP_Contacts());
            }
        }  
    }
}