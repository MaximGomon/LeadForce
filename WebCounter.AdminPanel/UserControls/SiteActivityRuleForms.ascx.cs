using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using System.Data;
using WebCounter.BusinessLogicLayer.Services;
using WebCounter.DataAccessLayer;
using WufooSharp;
using Page = System.Web.UI.Page;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SiteActivityRuleForms : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();
        public Guid siteID = new Guid();
        protected RadAjaxManager radAjaxManager = null;


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            siteID = ((LeadForceBasePage)Page).SiteId;
            Page.Title = "Формы - LeadForce";

            //            gridSiteActivityRules.AddNavigateUrl = UrlsData.AP_SiteActivityRuleAdd(_ruleTypeId);

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            

            //RadPanelBar1.Items[0].Visible = false;
            //gridSiteActivityRules.TagsControlID = "";            

            gridSiteActivityRules.SiteID = siteID;
            gridSiteActivityRules.Where = new List<GridWhere>();
            
            var optionTagPanel =
                ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(
                    a => a.SystemName == "TagPanel");
            if (optionTagPanel == null && !((LeadForceBasePage)Page).IsDefaultEdition)
            {
                gridSiteActivityRules.ShowSelectCheckboxes = false;
                RadPanelBar1.Visible = false;
            }
            
            var query = string.Empty;

            var optionExternalForm = ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "AllowExternalForm");
            if (optionExternalForm == null && !((LeadForceBasePage)Page).IsDefaultEdition)
            {
                ((RadToolBarSplitButton)rtbFormButtons.Items[0]).Buttons.Remove(rtbFormButtons.FindItemByValue("External"));                                       
                query = string.Format("RuleTypeID={0}", ((int)RuleType.Form).ToString());
            }
            else
                query = string.Format("RuleTypeID={0} OR RuleTypeID={1}", ((int)RuleType.Form).ToString(), ((int)RuleType.ExternalForm).ToString());

            var allowWufooForm = ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "AllowWufooForm");
            if (allowWufooForm == null && !((LeadForceBasePage)Page).IsDefaultEdition)
            {
                ((RadToolBarSplitButton)rtbFormButtons.Items[0]).Buttons.Remove(rtbFormButtons.FindItemByValue("Wufoo"));                
                rwWufooForm.Visible = false;
            }
            else
                query += string.Format(" OR RuleTypeID={0}", (int)RuleType.WufooForm);

            query += string.Format(" OR RuleTypeID={0}", (int)RuleType.LPgenerator);

            if (!string.IsNullOrEmpty(query))
                gridSiteActivityRules.Where.Add(new GridWhere() { CustomQuery = string.Format("({0})", query) });

            var allowOwnForm = ((LeadForceBasePage) Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "AllowOwnForm");
            if (allowOwnForm == null && !((LeadForceBasePage) Page).IsDefaultEdition)
            {
                ((RadToolBarSplitButton)rtbFormButtons.Items[0]).Buttons.Remove(rtbFormButtons.FindItemByValue("LeadForce"));   
            }

            radAjaxManager.AjaxRequest += radAjaxManager_AjaxRequest;
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(radAjaxManager, gridSiteActivityRules, null, UpdatePanelRenderMode.Inline);            
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(gridSiteActivityRules, rdtpLoadDataDate, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(gridSiteActivityRules, lbtnLoadData, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnLoadData, ucLoadDataMessage, null, UpdatePanelRenderMode.Inline);            
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridSiteActivityRules control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridSiteActivityRules_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((HyperLink)item.FindControl("spanName")).Text = data["tbl_SiteActivityRules_Name"].ToString();                

                ((Literal)item.FindControl("lType")).Text = data["tbl_RuleTypes_Title"].ToString();
                
                ((HyperLink)item.FindControl("urlLink")).Text = data["tbl_SiteActivityRules_URL"].ToString();
                if (!string.IsNullOrEmpty(((HyperLink)item.FindControl("urlLink")).Text))
                    ((HyperLink)item.FindControl("urlLink")).NavigateUrl = ((HyperLink)item.FindControl("urlLink")).Text;
                else
                    item.FindControl("spanUrl").Visible = false;
                
                ((Literal)item.FindControl("lDescription")).Text = data["tbl_SiteActivityRules_Description"].ToString();

                var lbCopy = (LinkButton)e.Item.FindControl("lbCopy");
                lbCopy.CommandArgument = data["ID"].ToString();
                lbCopy.Command += new CommandEventHandler(lbCopy_OnCommand);


                var lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                lbDelete.CommandArgument = data["ID"].ToString();
                lbDelete.Command += new CommandEventHandler(lbDelete_OnCommand);

                if ((RuleType)int.Parse(data["tbl_SiteActivityRules_RuleTypeID"].ToString()) == RuleType.Form)
                {
                    ((LinkButton) item.FindControl("lbGetScript")).OnClientClick =
                        string.Format("openRadWindow('{0}'); return false;", data["tbl_SiteActivityRules_Code"].ToString());
                    ((LinkButton) item.FindControl("lbGetScript")).Visible = true;
                }

                if ((RuleType)int.Parse(data["tbl_SiteActivityRules_RuleTypeID"].ToString()) == RuleType.WufooForm)
                {
                    ((LinkButton)item.FindControl("lbtnLoadData")).CommandArgument = string.Format("{0}${1}", data["ID"].ToString(), 
                        !string.IsNullOrEmpty(data["tbl_SiteActivityRules_WufooRevisionDate"].ToString()) ? 
                        DateTime.Parse((data["tbl_SiteActivityRules_WufooRevisionDate"].ToString())).ToString("yyyy-MM-dd HH:mm:ss") : "");
                    item.FindControl("lbtnLoadData").Visible = true;
                }                                

                var allowOwnForm = ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "AllowOwnForm");
                if (int.Parse(data["tbl_SiteActivityRules_RuleTypeID"].ToString()) != (int)RuleType.ExternalForm && allowOwnForm == null && !((LeadForceBasePage)Page).IsDefaultEdition)
                {
                    ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = UrlsData.AP_FormWizard(Guid.Parse(data["ID"].ToString()));
                    ((HyperLink)item.FindControl("spanName")).NavigateUrl = UrlsData.AP_FormWizard(Guid.Parse(data["ID"].ToString()));
                }                
                else
                {
                    ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = UrlsData.AP_SiteActivityRule(Guid.Parse(data["ID"].ToString()), int.Parse(data["tbl_SiteActivityRules_RuleTypeID"].ToString()));
                    ((HyperLink)item.FindControl("spanName")).NavigateUrl = UrlsData.AP_SiteActivityRule(Guid.Parse(data["ID"].ToString()), int.Parse(data["tbl_SiteActivityRules_RuleTypeID"].ToString()));

                    if (int.Parse(data["tbl_SiteActivityRules_RuleTypeID"].ToString()) == (int)RuleType.WufooForm)
                        lbCopy.Visible = false;
                }

            }
        }



        /// <summary>
        /// Handles the OnCommand event of the lbCopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbCopy_OnCommand(object sender, CommandEventArgs e)
        {            
            var newSiteActivityRule = new tbl_SiteActivityRules();
            var _id = Guid.Parse(e.CommandArgument.ToString());
            newSiteActivityRule = _dataManager.SiteActivityRules.CopyByID(_id);
            
            Response.Redirect(UrlsData.AP_SiteActivityRules((int) RuleType.Form));
            //gridSiteActivityRules.Rebind();
        }



	/// <summary>
        /// Handles the OnCommand event of the lbDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {            
            if (_dataManager.ContactColumnValues.SelectBySiteActivityRuleId(Guid.Parse(e.CommandArgument.ToString())).Any())
            {
                radAjaxManager.ResponseScripts.Add(
                    string.Format(
                        "if (confirm('Данная форма уже заполнялась. Удалить пользовательские данные?')) ForceDelete('{0}')",
                        e.CommandArgument));                
                return;
            }

            _dataManager.SiteActivityRules.DeleteByID(siteID, Guid.Parse(e.CommandArgument.ToString()));

            gridSiteActivityRules.Rebind();
        }



        /// <summary>
        /// Handles the AjaxRequest event of the radAjaxManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void radAjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument.Contains("ForceDelete#"))
            {
                var id = Guid.Parse(e.Argument.Replace("ForceDelete#", string.Empty));
                _dataManager.SiteActivityRules.DeleteByID(siteID, id);
                gridSiteActivityRules.Rebind();
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbntAddWufooForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbntAddWufooForm_OnClick(object sender, EventArgs e)
        {
            ucNotificationMessage.Text = string.Empty;

            try
            {
                var client = new WufooClient(txtWufooName.Text, txtWufooAPIKey.Text);
                var wufooForm = client.GetAllForms().FirstOrDefault(o => o.Hash == txtCode.Text);

                var form = _dataManager.SiteActivityRules.SelectFormByCode(siteID, wufooForm.Hash);

                if (form == null)
                {
                    form = new tbl_SiteActivityRules
                               {
                                   SiteID = siteID,
                                   Name = wufooForm.Name,
                                   RuleTypeID = (int) RuleType.WufooForm,
                                   Code = wufooForm.Hash,
                                   Description = wufooForm.Description,
                                   WufooAPIKey = txtWufooAPIKey.Text,
                                   WufooName = txtWufooName.Text
                               };

                    _dataManager.SiteActivityRules.Add(form);

                    Response.Redirect(UrlsData.AP_SiteActivityRule(form.ID, (int)RuleType.WufooForm), false);
                }
                else
                {
                    ucNotificationMessage.Text = "Данная форма уже добавлена в систему.";
                }
            }
            catch (Exception ex)
            {
                Log.Error("Ошибка добавления формы Wufoo", ex);
                ucNotificationMessage.Text = "Ошибка загрузки формы Wufoo. Проверьте введенные данные.";

                if (!Page.ClientScript.IsStartupScriptRegistered("AutoHeight"))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "AutoHeight", "AutoHeight();", true);
            }                                    
        }



        /// <summary>
        /// Handles the OnCommand event of the lbtnLoadData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbtnLoadData_OnCommand(object sender, CommandEventArgs e)
        {
            var commandArguments = ((LinkButton) sender).CommandArgument.Split('$');

            lbtnLoadData.CommandArgument = commandArguments[0];
            if (!string.IsNullOrEmpty(commandArguments[1]))
                rdtpLoadDataDate.SelectedDate = DateTime.Parse(commandArguments[1]);

            if (!Page.ClientScript.IsStartupScriptRegistered("ShowLoadDataRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowLoadDataRadWindow", "ShowLoadDataRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnLoadData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnLoadData_OnClick(object sender, EventArgs e)
        {
            try
            {
                var result = ExternalFormService.WufooLoadData(siteID, Guid.Parse(lbtnLoadData.CommandArgument), rdtpLoadDataDate.SelectedDate.Value);

                if (string.IsNullOrEmpty(result.Message))
                {                    
                    ucLoadDataMessage.MessageType = NotificationMessageType.Success;
                    ucLoadDataMessage.Text = "Данные успешно загружены.";
                }
                else
                {
                    ucLoadDataMessage.MessageType = NotificationMessageType.Warning;
                    ucLoadDataMessage.Text = result.Message;
                }
            }
            catch (Exception ex)
            {
                ucLoadDataMessage.MessageType = NotificationMessageType.Warning;
                ucLoadDataMessage.Text = "Ошибка загрузки данных.";

                Log.Error("Ошибка загрузки данных.", ex);
            }

            if (!Page.ClientScript.IsStartupScriptRegistered("HideLoadDataRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "HideLoadDataRadWindow", "HideLoadDataRadWindow();", true);
        }

        protected void rtbFormButtons_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "LeadForce":
                    Response.Redirect(UrlsData.AP_SiteActivityRuleAdd((int)RuleType.Form));
                    break;
                case "Wizard":
                    Response.Redirect(UrlsData.AP_FormWizard());
                    break;
                case "External":
                    Response.Redirect(UrlsData.AP_SiteActivityRuleAdd((int)RuleType.ExternalForm));
                    break;
                case "Wufoo":
                    if (!Page.ClientScript.IsStartupScriptRegistered("AddWufooForm"))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "AddWufooForm", "AddWufooForm();", true);
                    break;
                case "LPgenerator":
                    Response.Redirect(UrlsData.AP_SiteActivityRuleAdd((int)RuleType.LPgenerator));
                    break;
            }          
        }
    }

}