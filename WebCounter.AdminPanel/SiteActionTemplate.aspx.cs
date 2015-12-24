using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel
{
    public partial class SiteActionTemplate : LeadForceBasePage
    {
        public Access access;
        private Guid _siteActionTemplateId;


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Шаблоны сообщений - LeadForce";

            access = Access.Check();
            if (!access.Write)
                BtnUpdate.Visible = false;

            var siteActionTemplateId = Page.RouteData.Values["ID"] as string;

            hlCancel.NavigateUrl = UrlsData.AP_SiteActionTemplates();

            if (!string.IsNullOrEmpty(siteActionTemplateId))
                Guid.TryParse(siteActionTemplateId, out _siteActionTemplateId);

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(SiteId, _siteActionTemplateId);
            if (siteActionTemplate != null)
            {
                if ((SiteActionTemplateCategory)siteActionTemplate.SiteActionTemplateCategoryID == SiteActionTemplateCategory.BaseTemplate)
                {
                    txtTitle.Text = siteActionTemplate.Title;
                    ucEditorSiteActionTemplate.SiteActionTemplateId = _siteActionTemplateId;
                    ucEditorSiteActionTemplate.BindData();
                }

                if ((SiteActionTemplateCategory)siteActionTemplate.SiteActionTemplateCategoryID == SiteActionTemplateCategory.System)
                {
                    ucSettingsSiteActionTemplate.SiteActionTemplateId = _siteActionTemplateId;
                    ucSettingsSiteActionTemplate.BindData();

                    ucEditorSiteActionTemplate.SiteActionTemplateId = _siteActionTemplateId;
                    ucEditorSiteActionTemplate.BindData();

                    rtsTabs.FindTabByValue("Settings").Visible = true;
                    rtsTabs.FindTabByValue("Stats").Visible = true;

                    return;
                }
            }

            ucSettingsSiteActionTemplate.Visible = false;

            ucEditorSiteActionTemplate.ShowMessageCaption = false;

            pnlTitle.Visible = true;

            rtsTabs.FindTabByValue("Settings").Visible = false;
            rtsTabs.FindTabByValue("Stats").Visible = false;

            rtsTabs.FindTabByValue("Template").Selected = true;
            rpvTemplate.Selected = true;
        }



        /// <summary>
        /// Handles the Click event of the BtnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(_siteActionTemplateId);

            if (siteActionTemplate == null)
                siteActionTemplate = new tbl_SiteActionTemplate();

            siteActionTemplate.SiteID = SiteId;

            siteActionTemplate.FromEmail = string.Empty;
            siteActionTemplate.ActionTypeID = 1;
            siteActionTemplate.ReplaceLinksID = 0;
            

            if (_siteActionTemplateId != Guid.Empty)
            {
                if ((SiteActionTemplateCategory)siteActionTemplate.SiteActionTemplateCategoryID == SiteActionTemplateCategory.BaseTemplate)
                {
                    siteActionTemplate.Title = txtTitle.Text;
                    ucEditorSiteActionTemplate.Save();
                    DataManager.SiteActionTemplate.Update(siteActionTemplate);
                }

                if ((SiteActionTemplateCategory)siteActionTemplate.SiteActionTemplateCategoryID == SiteActionTemplateCategory.System)
                {
                    //siteActionTemplate.MessageCaption = ((TextBox)ucEditorSiteActionTemplate.FindControl("txtMessageCaption")).Text;
                    ucEditorSiteActionTemplate.Save();
                    ucSettingsSiteActionTemplate.Save();
                }
            }
            else
            {
                siteActionTemplate.Title = txtTitle.Text;
                siteActionTemplate.SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.BaseTemplate;
                siteActionTemplate.OwnerID = CurrentUser.Instance.ContactID;
                siteActionTemplate = DataManager.SiteActionTemplate.Add(siteActionTemplate);

                ucEditorSiteActionTemplate.SiteActionTemplateId = siteActionTemplate.ID;
                ucEditorSiteActionTemplate.Save();
            }

            var thumbnail = GetWebSiteThumb.ClassWSThumb.GetWebSiteThumbnail(string.Format("{0}/Handlers/SiteActionTemplateThumbnail.aspx?siteid={1}&id={2}",
                                                                                BusinessLogicLayer.Configuration.Settings.LeadForceSiteUrl,
                                                                                siteActionTemplate.SiteID, siteActionTemplate.ID), 800, 800, 120, 120);
            using (var stream = new System.IO.MemoryStream())
            {
                var fileProvider = new FileSystemProvider();
                thumbnail.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0;
                fileProvider.Upload(SiteId, "SiteActionTemplates", siteActionTemplate.ID.ToString() + ".png", stream, FileType.Thumbnail, false);
            }

            Response.Redirect(UrlsData.AP_SiteActionTemplates());
        }
    }
}