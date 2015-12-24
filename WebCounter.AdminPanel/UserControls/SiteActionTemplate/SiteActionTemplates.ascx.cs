using System;
using System.ComponentModel;
using Labitec.UI.BaseWorkspace;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SiteActionTemplates : System.Web.UI.UserControl
    {
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

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string SectionTitle
        {
            get
            {
                object o = ViewState["SectionTitle"];
                return (o == null ? null : (string)o);
            }
            set
            {
                ViewState["SectionTitle"] = value;
            }
        }

        private DataManager dataManager = new DataManager();
        public Guid siteID = new Guid();


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            siteID = ((LeadForceBasePage)Page).SiteId;

            gridSiteActionTemplates.AddNavigateUrl = UrlsData.AP_SiteActionTemplateAdd();
            gridSiteActionTemplates.Actions.Add(new GridAction { Text = "Редактировать", NavigateUrl = string.Format("~/{0}/SiteActionTemplates/Edit/{{0}}", Page.RouteData.Values["tab"] as string), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridSiteActionTemplates.Where.Add(new GridWhere { Column = "SiteActionTemplateCategoryID", Value = ((int)SiteActionTemplateCategory.BaseTemplate).ToString() });
            gridSiteActionTemplates.SiteID = siteID;
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            gridSiteActionTemplates.Where.Clear();

            gridSiteActionTemplates.Where.Add(new GridWhere { Column = "SiteActionTemplateCategoryID", Value = ddlFilter.SelectedValue });

            gridSiteActionTemplates.Rebind();
        }
    }
}