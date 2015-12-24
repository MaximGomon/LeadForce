using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.WebSite;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class WebSitePage : System.Web.UI.Page
    {
        protected DataManager DataManager = new DataManager();
        private Guid _webSitePageId = Guid.Empty;
        private Guid _webSiteId = Guid.Empty;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ucNotificationMessage.Text = string.Empty;

            Header.DataBind();

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                _webSitePageId = Guid.Parse(Request.QueryString["id"]);

            if (!string.IsNullOrEmpty(Request.QueryString["websiteid"]))
                _webSiteId = Guid.Parse(Request.QueryString["websiteid"]);

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var webSitePage = DataManager.WebSitePage.SelectById(CurrentUser.Instance.SiteID, _webSitePageId);

            EnumHelper.EnumToDropDownList<WebSiteElementStatus>(ref ddlStatus, false);

            rlbScriptsAndStyles.DataSource = DataManager.ExternalResource.SelectAll(_webSiteId);
            rlbScriptsAndStyles.DataTextField = "Title";
            rlbScriptsAndStyles.DataValueField = "ID";
            rlbScriptsAndStyles.DataBind();

            if (webSitePage != null)
            {
                txtWebSitePageTitle.Text = webSitePage.Title;
                txtLink.Text = webSitePage.Url;
                chxIsHomePage.Checked = webSitePage.IsHomePage;
                txtMetaTitle.Text = webSitePage.MetaTitle;
                txtMetaKeywords.Text = webSitePage.MetaKeywords;
                txtMetaDescription.Text = webSitePage.MetaDescription;                
                ddlStatus.SelectedIndex = ddlStatus.FindItemIndexByValue(webSitePage.WebSiteElementStatusID.ToString());
                ucHtmlEditor.Content = webSitePage.Body;

                lrlDomain.Text = DataManager.WebSite.GetWebSiteUrl(CurrentUser.Instance.SiteID, webSitePage.WebSiteID, false);
                if (lrlDomain.Text.Contains(BusinessLogicLayer.Configuration.Settings.CMSSiteHost))
                    lrlWebSiteId.Text = string.Concat("/", webSitePage.WebSiteID.ToString());

                foreach (var externalResource in webSitePage.tbl_ExternalResource)                
                    rlbScriptsAndStyles.FindItemByValue(externalResource.ID.ToString()).Checked = true;

                ApplyStyles();
            }
        }


        protected void ApplyStyles()
        {
            var css = string.Empty;
            var externalResources = DataManager.ExternalResource.SelectAll(_webSiteId);

            foreach (RadListBoxItem item in rlbScriptsAndStyles.Items)
            {
                if (item.Checked)
                {
                    var value = Guid.Parse(item.Value);
                    var externalResource = externalResources.SingleOrDefault(o => o.ID == value);

                    if (externalResource.ExternalResourceTypeID == (int)ExternalResourceType.CSS)
                    {                        
                        if (!string.IsNullOrEmpty(externalResource.File))
                        {
                            var webSite = DataManager.WebSite.SelectById(externalResource.DestinationID);
                            var fsp = new FileSystemProvider();
                            var filePath = fsp.GetLink(webSite.SiteID, "ExternalResource", externalResource.File, FileType.Attachment);
                            css += string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}"" />", filePath);
                        }
                        if (!string.IsNullOrEmpty(externalResource.Url))
                            css += string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}"" />", externalResource.Url);
                        if (!string.IsNullOrEmpty(externalResource.Text))
                            css += string.Format(@"<style type=""text/css"">{0}</style>", externalResource.Text);                        
                    }
                }
            }

            ucHtmlEditor.Content = Regex.Replace(ucHtmlEditor.Content, @"<system\>.*\<\/system\>", string.Empty);
            if (!string.IsNullOrEmpty(css))
                ucHtmlEditor.Content += string.Concat("<system>", css, "</system>");
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            var webSitePage = DataManager.WebSitePage.SelectById(CurrentUser.Instance.SiteID, _webSitePageId) ?? new tbl_WebSitePage();

            if (DataManager.WebSitePage.IsExistsPage(_webSiteId, _webSitePageId, txtLink.Text))
            {
                ucNotificationMessage.Text = "Страница с таким адресом уже существует.";
                return;
            }

            webSitePage.Title = txtWebSitePageTitle.Text;
            webSitePage.Url = txtLink.Text;
            webSitePage.IsHomePage = chxIsHomePage.Checked;
            webSitePage.MetaTitle = txtMetaTitle.Text;
            webSitePage.MetaKeywords = txtMetaKeywords.Text;
            webSitePage.MetaDescription = txtMetaDescription.Text;
            webSitePage.WebSiteElementStatusID = int.Parse(ddlStatus.SelectedValue);
            webSitePage.Body = Regex.Replace(ucHtmlEditor.Content, @"<system\>.*\<\/system\>", string.Empty);

            var externalResources = DataManager.ExternalResource.SelectAll(_webSiteId);

            webSitePage.tbl_ExternalResource.Clear();

            foreach (RadListBoxItem item in rlbScriptsAndStyles.Items)
            {
                if (item.Checked)
                {
                    var value = Guid.Parse(item.Value);
                    webSitePage.tbl_ExternalResource.Add(externalResources.SingleOrDefault(o => o.ID == value));   
                }                    
            }

            if (webSitePage.ID == Guid.Empty)
            {
                webSitePage.WebSiteID = _webSiteId;
                DataManager.WebSitePage.Add(webSitePage);
            }
            else
                DataManager.WebSitePage.Update(webSitePage);

            if (!Page.ClientScript.IsStartupScriptRegistered("Close"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Close", "Close();", true);
        }

        protected void rlbScriptsAndStyles_OnItemCheck(object sender, RadListBoxItemEventArgs e)
        {
            ApplyStyles();
        }
    }
}