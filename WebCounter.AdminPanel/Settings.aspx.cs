using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class Settings : LeadForceBasePage
    {        
        public Guid _contactID;
        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Title = "Системные настройки - LeadForce";            

            if (!Page.IsPostBack)            
                BindData();            
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            ucPortal.SiteId = SiteId;
            ucPortal.Module = "Settings";

            ucSiteDomains.SiteId = SiteId;
            ucSiteDomains.Module = "Settings";

            EnumHelper.EnumToDropDownList<HtmlEditorMode>(ref ddlHtmlEditorMode, false);
            var site = DataManager.Sites.SelectById(SiteId);
            if (site.AccessProfileID.HasValue)
            {
                var accessProfile = DataManager.AccessProfile.SelectById(site.AccessProfileID.Value);
                if (accessProfile != null && accessProfile.DomainsCount > 0)
                {
                    chxIsBlockAccessFromDomainsOutsideOfList.Checked = true;
                    plAccessBlock.Visible = false;
                }
            }
            else
                chxIsBlockAccessFromDomainsOutsideOfList.Checked = site.IsBlockAccessFromDomainsOutsideOfList;

            txtName.Text = site.Name;
            txtLinkProcessingURL.Text = site.LinkProcessingURL;
            lrlCounterCode.Text = HttpUtility.HtmlEncode(ScriptTemplates.Counter(SiteId));
            lrlScriptCode.Text = HttpUtility.HtmlEncode(ScriptTemplates.Script(true));
            chxShowHiddenMessages.Checked = site.ShowHiddenMessages;
            ddlHtmlEditorMode.SelectedIndex = ddlHtmlEditorMode.FindItemIndexByValue(site.HtmlEditorModeID.ToString());
            List<tbl_Sites> fakeList = new List<tbl_Sites>();
            fakeList.Add(site);
            fvSmtp.DataSource = fakeList;
            fvSmtp.DataBind();
        }



        /// <summary>
        /// Handles the Click event of the BtnUpdateSmtp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnUpdateSmtp_Click(object sender, EventArgs e)
        {
            var site = DataManager.Sites.SelectById(SiteId);
            site.Name = txtName.Text;
            site.LinkProcessingURL = txtLinkProcessingURL.Text;
            site.SmtpHost = ((TextBox)fvSmtp.FindControl("txtSmtpHost")).Text;
            site.SmtpUsername = ((TextBox)fvSmtp.FindControl("txtSmtpUsername")).Text;
            site.SmtpPassword = ((TextBox)fvSmtp.FindControl("txtSmtpPassword")).Text;
            int port;
            if (int.TryParse(((TextBox)fvSmtp.FindControl("txtSmtpPort")).Text, out port))
                site.SmtpPort = port;
            else
                site.SmtpPort = null;

            site.SystemEmail = ((TextBox)fvSmtp.FindControl("txtSystemEmail")).Text;
            site.ShowHiddenMessages = chxShowHiddenMessages.Checked;
            site.HtmlEditorModeID = int.Parse(ddlHtmlEditorMode.SelectedValue);
            site.IsBlockAccessFromDomainsOutsideOfList = chxIsBlockAccessFromDomainsOutsideOfList.Checked;

            DataManager.Sites.Update(site);            
        }



        /// <summary>
        /// Handles the PreRender event of the txtSmtpPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void txtSmtpPassword_PreRender(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(((TextBox)fvSmtp.FindControl("txtSmtpPassword")).Text))
                ((TextBox)fvSmtp.FindControl("txtSmtpPassword")).Attributes["value"] = ((TextBox)fvSmtp.FindControl("txtSmtpPassword")).Text;
        }
    }
}