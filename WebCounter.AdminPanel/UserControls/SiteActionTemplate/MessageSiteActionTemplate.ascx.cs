using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;
using Page = System.Web.UI.Page;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class MessageSiteActionTemplate : System.Web.UI.UserControl
    {
        public Access access;
        protected DataManager DataManager = new DataManager();
        protected RadAjaxManager radAjaxManager = null;

        public event SelectedTemplateChangedEventHandler SelectedTemplateChanged;
        public delegate void SelectedTemplateChangedEventHandler(object sender, SelectedTemplateChangedEventArgs e);
        public class SelectedTemplateChangedEventArgs : EventArgs
        {
            public Guid SelectedTemplateId { get; set; }
            public string SelectedTemplateTitle { get; set; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SelectedSiteActionTemplateId
        {
            get
            {
                if (ViewState["SelectedSiteActionTemplateId"] == null)                
                    ViewState["SelectedSiteActionTemplateId"] = Guid.Empty;

                return (Guid)ViewState["SelectedSiteActionTemplateId"];
            }
            set
            {
                ViewState["SelectedSiteActionTemplateId"] = value;        
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rtsTabs.OnClientTabSelected = string.Format("{0}_setupEditor", ucHtmlEditor.ClientID);

            access = Access.Check();

            radAjaxManager = RadAjaxManager.GetCurrent(Page);                                                

            gridSiteActionTemplates.SiteID = ((LeadForceBasePage)Page).SiteId;
            gridSiteActionTemplates.Where.Add(new GridWhere() { Column = "SiteActionTemplateCategoryID", Value = ((int)SiteActionTemplateCategory.BaseTemplate).ToString() });
                
            InitForm();
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            radAjaxManager.AjaxSettings.AddAjaxSetting(gridSiteActionTemplates, plSiteActionTemplateInfo);
            radAjaxManager.AjaxSettings.AddAjaxSetting(gridSiteActionTemplates, ucHtmlEditor);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, plSiteActionTemplateInfo);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, ucHtmlEditor);            
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, gridSiteActionTemplates);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, rUserColumnValues);                     

            base.OnPreRender(e);
        }



        /// <summary>
        /// Inits the form.
        /// </summary>
        private void InitForm()
        {
            if (!string.IsNullOrEmpty(Request.Params["__ASYNCPOST"]) && bool.Parse(Request.Params["__ASYNCPOST"]) && Request.Params["__EVENTARGUMENT"] == "ShowForm")
            {
                SelectedSiteActionTemplateId = Guid.Empty;
                ClearSiteActionTemplateForm();
                gridSiteActionTemplates.Rebind();                
                BindData(null);
            }                        
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <param name="siteActionTemplateId">The site action template id.</param>
        private void BindData(Guid? siteActionTemplateId)
        {
            var siteId = ((LeadForceBasePage) Page).SiteId;

            plNotBase.Visible = false;

            dcbParentTemplate.SelectedId = Guid.Empty;
            dcbParentTemplate.SelectedText = string.Empty;

            dcbParentTemplate.SiteID = siteId;
            
            ddlReplaceLinks.Items.Clear();
            
            EnumHelper.EnumToDropDownList<ReplaceLinks>(ref ddlReplaceLinks, false);

            rUserColumnValues.DataSource = DataManager.SiteColumns.SelectAll(siteId);
            rUserColumnValues.DataBind();

            ddlActionType.DataSource = DataManager.ActionTypes.SelectAll();
            ddlActionType.DataTextField = "Title";
            ddlActionType.DataValueField = "ID";
            ddlActionType.DataBind();

            if (siteActionTemplateId.HasValue)
            {
                rtsTabs.Tabs[1].Selected = true;
                RadMultiPage1.PageViews[1].Selected = true;                

                var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(siteId, (Guid) siteActionTemplateId);
                if (siteActionTemplate != null)
                {                    
                    dcbParentTemplate.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn
                    {
                        Name = "ID",
                        DbType = DbType.Guid,
                        Operation = FilterOperation.NotEqual,
                        Value = siteActionTemplate.ID.ToString()
                    });

                    if (siteActionTemplate.UsageID.HasValue && siteActionTemplate.SiteActionTemplateCategoryID.HasValue)
                    {                        
                        switch ((SiteActionTemplateCategory)siteActionTemplate.SiteActionTemplateCategoryID)
                        {
                            case SiteActionTemplateCategory.MassMail:
                                var massMail = DataManager.MassMail.SelectById(siteId, (Guid)siteActionTemplate.UsageID);
                                if (massMail != null)
                                {
                                    hlUsage.NavigateUrl = UrlsData.AP_MassMail(massMail.ID);
                                    hlUsage.Text = massMail.Name;
                                }
                                break;
                            case SiteActionTemplateCategory.Workflow:
                                var workflowTemplate = DataManager.WorkflowTemplate.SelectById(siteId, (Guid)siteActionTemplate.UsageID);
                                if (workflowTemplate != null)
                                {
                                    hlUsage.NavigateUrl = UrlsData.AP_WorkflowTemplateEdit(workflowTemplate.ID);
                                    hlUsage.Text = workflowTemplate.Name;
                                }
                                break;
                            case SiteActionTemplateCategory.Event:
                                var siteEventTemplate = DataManager.SiteEventTemplates.SelectById((Guid)siteActionTemplate.UsageID);
                                if (siteEventTemplate != null)
                                {
                                    hlUsage.NavigateUrl = UrlsData.AP_SiteEventTemplate(siteEventTemplate.ID);
                                    hlUsage.Text = siteEventTemplate.Title;
                                }
                                break;
                        }
                    }

                    if (siteActionTemplate.SiteActionTemplateCategoryID.HasValue &&
                        siteActionTemplate.SiteActionTemplateCategoryID != (int)SiteActionTemplateCategory.BaseTemplate)
                        plNotBase.Visible = true;
                    
                    ddlActionType.SelectedIndex = ddlActionType.FindItemIndexByValue(siteActionTemplate.ActionTypeID.ToString());
                    ddlReplaceLinks.SelectedIndex = ddlReplaceLinks.FindItemIndexByValue(siteActionTemplate.ReplaceLinksID.ToString());
                    txtFromEmail.Text = siteActionTemplate.FromEmail;
                    txtFromName.Text = siteActionTemplate.FromName;
                    txtToEmail.Text = siteActionTemplate.ToEmail;
                    txtReplyEmail.Text = siteActionTemplate.ReplyToEmail;
                    txtReplyName.Text = siteActionTemplate.ReplyToName;                    
                    txtMessageCaption.Text = siteActionTemplate.MessageCaption;
                    ucHtmlEditor.Content = siteActionTemplate.MessageBody;
                }
            }            
        }



        /// <summary>
        /// Clears the site action template form.
        /// </summary>
        private void ClearSiteActionTemplateForm()
        {            
            dcbParentTemplate.SelectedId = Guid.Empty;
            dcbParentTemplate.SelectedText = string.Empty;
            ddlActionType.ClearSelection();
            ddlReplaceLinks.ClearSelection();         
            txtFromEmail.Text = string.Empty;
            txtFromName.Text = string.Empty;
            txtMessageCaption.Text = string.Empty;
            txtReplyEmail.Text = string.Empty;
            txtReplyName.Text = string.Empty;
            txtToEmail.Text = string.Empty;
            ucHtmlEditor.Content = string.Empty;            
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlActionType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlActionType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(((DropDownList)sender).SelectedValue) && (ActionType)int.Parse(((DropDownList)sender).SelectedValue) == ActionType.EmailToFixed)
                pToEmail.Visible = true;
            else
                pToEmail.Visible = false;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridSiteActionTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridSiteActionTemplates_OnItemDataBound(object sender, GridItemEventArgs e)
        {            
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var lbtnSelect = (LinkButton)item.FindControl("lbtnSelect");
                var imgOk = (Image) item.FindControl("imgOk");
                lbtnSelect.CommandArgument = data["ID"].ToString();

                if (SelectedSiteActionTemplateId == Guid.Parse(data["ID"].ToString()))
                {
                    lbtnSelect.Visible = false;
                    imgOk.Visible = true;
                }
                else
                {
                    lbtnSelect.Visible = true;
                    imgOk.Visible = false;
                }

                lbtnSelect.OnClientClick = string.Empty;                
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSelect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSelect_OnClick(object sender, EventArgs e)
        {
            SelectedSiteActionTemplateId = Guid.Parse(((LinkButton) sender).CommandArgument);                                    

            var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(((LeadForceBasePage)Page).SiteId, SelectedSiteActionTemplateId);
            if (siteActionTemplate != null)
            {                
                dcbParentTemplate.SelectedText = siteActionTemplate.Title;                
            }

            BindData(SelectedSiteActionTemplateId);

            gridSiteActionTemplates.Rebind();            
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

            tbl_SiteActionTemplate siteActionTemplate = null;
            
            siteActionTemplate =
                DataManager.SiteActionTemplate.SelectById(((LeadForceBasePage)Page).SiteId, SelectedSiteActionTemplateId);

            if (siteActionTemplate != null)
            {
                siteActionTemplate.SiteID = ((LeadForceBasePage) Page).SiteId;
                siteActionTemplate.ActionTypeID = int.Parse(ddlActionType.SelectedValue);
                siteActionTemplate.ReplaceLinksID = int.Parse(ddlReplaceLinks.SelectedValue);
                siteActionTemplate.ToEmail = txtToEmail.Text;
                siteActionTemplate.FromEmail = txtFromEmail.Text;
                siteActionTemplate.FromName = txtFromName.Text;
                siteActionTemplate.ReplyToEmail = txtReplyEmail.Text;
                siteActionTemplate.ReplyToName = txtReplyName.Text;
                siteActionTemplate.MessageCaption = txtMessageCaption.Text;
                siteActionTemplate.MessageBody = ucHtmlEditor.Content;

                if ((SiteActionTemplateCategory) siteActionTemplate.SiteActionTemplateCategoryID !=
                    SiteActionTemplateCategory.BaseTemplate)
                {
                    if (dcbParentTemplate.SelectedId != Guid.Empty)
                        siteActionTemplate.ParentID = dcbParentTemplate.SelectedId;
                }

                if (SelectedSiteActionTemplateId != Guid.Empty)
                    DataManager.SiteActionTemplate.Update(siteActionTemplate);

                SelectedSiteActionTemplateId = siteActionTemplate.ID;

                gridSiteActionTemplates.Rebind();

                if (SelectedTemplateChanged != null)
                {
                    SelectedTemplateChanged(this,
                                            new SelectedTemplateChangedEventArgs()
                                                {
                                                    SelectedTemplateId = siteActionTemplate.ID,
                                                    SelectedTemplateTitle = siteActionTemplate.Title
                                                });
                }
            }

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_CloseSiteActionTemplateRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_CloseSiteActionTemplateRadWindow", this.ClientID + "_CloseSiteActionTemplateRadWindow();", true);
        }
    }
}