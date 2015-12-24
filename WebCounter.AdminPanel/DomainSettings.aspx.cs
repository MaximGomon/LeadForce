using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using Page = System.Web.UI.Page;

namespace WebCounter.AdminPanel
{
    public partial class DomainSettings : LeadForceBasePage
    {
        public Access access;



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Настройки доменов - LeadForce";

            access = Access.Check();

            var radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnSave, ucNotificationMessage, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnSave, txtDomain, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnSave, txtAliases, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnSave, gridSiteDomains, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnSave, ucCheckSite, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(ucCheckSite, gridSiteDomains, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(gridSiteDomains, lbtnSave, null, UpdatePanelRenderMode.Inline);
            
            ucCheckSite.DomainChecked += ucCheckSite_DomainChecked;
            
            gridSiteDomains.SiteID = SiteId;

            if (!string.IsNullOrEmpty(Request.QueryString["check"]) && bool.Parse(Request.QueryString["check"]))
                gridSiteDomains.Where.Add(new GridWhere() { Column = "PageCountWithCounter", Value = "0"});            
        }

       

        /// <summary>
        /// Ucs the check site_ domain checked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void ucCheckSite_DomainChecked(object sender)
        {
            gridSiteDomains.Rebind();         
        }


        
        /// <summary>
        /// Handles the OnItemDataBound event of the gridSiteDomains control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridSiteDomains_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (access == null)
                    access = Access.Check();

                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((Literal)item.FindControl("lrlSiteDomain")).Text = data["tbl_SiteDomain_Domain"].ToString();                
                ((Literal)item.FindControl("lrlSiteDomainStatus")).Text = EnumHelper.GetEnumDescription((SiteDomainStatus)int.Parse(data["tbl_SiteDomain_StatusID"].ToString()));

                if (!string.IsNullOrEmpty((string)data["tbl_SiteDomain_Aliases"]))
                    ((Literal)item.FindControl("lrlAliases")).Text = string.Format("Псевдонимы: {0}", data["tbl_SiteDomain_Aliases"]);
                if (!string.IsNullOrEmpty((string)data["tbl_SiteDomain_Note"]))
                    ((Literal)item.FindControl("lrlNote")).Text = string.Format("Примечание: {0}", data["tbl_SiteDomain_Note"]);

                ((LinkButton)e.Item.FindControl("lbEdit")).CommandArgument = data["ID"].ToString();                                
                ((LinkButton)e.Item.FindControl("lbDelete")).CommandArgument = data["ID"].ToString();
                ((LinkButton)e.Item.FindControl("lbCheck")).CommandArgument = data["ID"].ToString();
                RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(e.Item.FindControl("lbCheck"), ucCheckSite);                

                ((TextBox)item.FindControl("txtDomain")).Text = data["tbl_SiteDomain_Domain"].ToString();
                ((TextBox)item.FindControl("txtAliases")).Text = data["tbl_SiteDomain_Aliases"].ToString();
                ((RadButton)e.Item.FindControl("lbtnUpdate")).CommandArgument = data["ID"].ToString();

                e.Item.FindControl("lbDelete").Visible = access.Delete;
            }
        }



        /// <summary>
        /// Handles the OnCommand event of the lbEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbEdit_OnCommand(object sender, CommandEventArgs e)
        {
            ((LinkButton)sender).Parent.FindControl("plEdit").Visible = true;
        }



        /// <summary>
        /// Handles the OnCommand event of the lbDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            DataManager.SiteDomain.Delete(DataManager.SiteDomain.SelectById(CurrentUser.Instance.SiteID, (Guid.Parse(e.CommandArgument.ToString()))));
            gridSiteDomains.Rebind();            
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {            
            ucNotificationMessage.Text = string.Empty;

            var siteDomainId = Guid.Empty;

            var result = DataManager.SiteDomain.Save(SiteId, txtDomain.Text, txtAliases.Text, false, ref siteDomainId);

            if (!string.IsNullOrEmpty(result))
            {
                ucNotificationMessage.Text = result;
                return;
            }

            ucCheckSite.SiteDomainId = siteDomainId;            

            txtDomain.Text = string.Empty;
            txtAliases.Text = string.Empty;            

            gridSiteDomains.Rebind();               
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnUpdate_OnClick(object sender, EventArgs e)
        {
            var lbtnUpdate = (RadButton) sender;            
            var txtUpdateDomain = (TextBox)lbtnUpdate.Parent.FindControl("txtDomain");
            var txtUpdateAliases = (TextBox)lbtnUpdate.Parent.FindControl("txtAliases");
            var ucUpdateNotificationMessage = (NotificationMessage)lbtnUpdate.Parent.FindControl("ucNotificationMessage");            

            ucUpdateNotificationMessage.Text = string.Empty;

            var siteDomainId = Guid.Parse(lbtnUpdate.CommandArgument);

            var result = DataManager.SiteDomain.Save(SiteId, txtUpdateDomain.Text, txtUpdateAliases.Text, false, ref siteDomainId);

            if (!string.IsNullOrEmpty(result))
            {
                ucUpdateNotificationMessage.Text = result;
                return;
            }
            
            gridSiteDomains.Rebind();            
        }



        /// <summary>
        /// Handles the OnCommand event of the lbCheck control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbCheck_OnCommand(object sender, CommandEventArgs e)
        {
            var lbCheck = (LinkButton)sender;
            var siteDomainId = Guid.Parse(lbCheck.CommandArgument);
            ucCheckSite.SiteDomainId = siteDomainId;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "CheckDomains", string.Format("ShowCheckDomainRadWindow();{0}_CheckDomains();", ucCheckSite.ClientID), true);                        
        }



        /// <summary>
        /// Handles the OnAjaxSettingCreated event of the rapStatsContainer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxSettingCreatedEventArgs"/> instance containing the event data.</param>
        protected void rapStatsContainer_OnAjaxSettingCreated(object sender, AjaxSettingCreatedEventArgs e)
        {
            e.UpdatePanel.UpdateMode = UpdatePanelUpdateMode.Always;            
        }
    }
}
