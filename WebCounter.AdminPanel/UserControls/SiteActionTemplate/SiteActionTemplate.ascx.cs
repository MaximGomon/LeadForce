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
    public partial class SiteActionTemplate : System.Web.UI.UserControl
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



        /// <summary>
        /// Gets or sets the current site action template id.
        /// </summary>
        /// <value>
        /// The current site action template id.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        protected Guid? CurrentSiteActionTemplateId
        {
            get
            {
                return (Guid?)ViewState["CurrentSiteActionTemplateId"];
            }
            set
            {
                ViewState["CurrentSiteActionTemplateId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        protected Guid? BaseOnSiteActionTemplateId
        {
            get
            {
                return (Guid?)ViewState["BaseOnSiteActionTemplateId"];
            }
            set
            {
                ViewState["BaseOnSiteActionTemplateId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public SiteActionTemplateCategory CurrentSiteActionTemplateCategory
        {
            get
            {
                if (ViewState["CurrentSiteActionTemplateCategory"] == null)
                    ViewState["CurrentSiteActionTemplateCategory"] = SiteActionTemplateCategory.BaseTemplate;

                return (SiteActionTemplateCategory)ViewState["CurrentSiteActionTemplateCategory"];
            }
            set
            {
                ViewState["CurrentSiteActionTemplateCategory"] = value;
            }
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
                txtSiteActionTemplateId.Text = value.ToString();
            }
        }


        /// <summary>
        /// Gets or sets the validation group.
        /// </summary>
        /// <value>
        /// The validation group.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string ValidationGroup
        {
            get
            {
                return (string)ViewState["ValidationGroup"];
            }
            set
            {
                ViewState["ValidationGroup"] = value;
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [show label].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show label]; otherwise, <c>false</c>.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool ShowLabel
        {
            get
            {
                if (ViewState["ShowLabel"] == null)
                    ViewState["ShowLabel"] = true;

                return (bool)ViewState["ShowLabel"];
            }
            set
            {
                ViewState["ShowLabel"] = value;
            }
        }


        public bool fromSilverlight { get; set; }


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
                                    
            rlbNewTemplate.OnClientSelectedIndexChanging = this.ClientID + "_rblNewTemplateOnClientSelectedIndexChanging";            

            gridSiteActionTemplates.SiteID = ((LeadForceBasePage)Page).SiteId;
            gridSiteActionTemplates.Where.Add(new GridWhere() { Column = "SiteActionTemplateCategoryID", Value = ((int)SiteActionTemplateCategory.BaseTemplate).ToString() });

            if (!string.IsNullOrEmpty(ValidationGroup))
            {
                rfvTemplateValidator.ValidationGroup = ValidationGroup;
                rfvTemplateValidator.Enabled = true;
            }
            else
            {
                rfvTemplateValidator.ValidationGroup = string.Empty;
                rfvTemplateValidator.Enabled = false;
            }

            if (ShowLabel)
                lrlLabel.Text = "<label style=\"float:left;margin-right: 3px\">Шаблон:</label>";            
                
            InitForm();

            if (fromSilverlight)
                pnlSiteActionTemplate.Visible = false;

            rwSiteActionTemplate.OnClientClose = string.Format("{0}_OnClientClose", this.ClientID);
        }


        protected override void OnPreRender(EventArgs e)
        {
            radAjaxManager.AjaxSettings.AddAjaxSetting(gridSiteActionTemplates, plSiteActionTemplateInfo);
            radAjaxManager.AjaxSettings.AddAjaxSetting(gridSiteActionTemplates, ucHtmlEditor);
            radAjaxManager.AjaxSettings.AddAjaxSetting(gridSiteActionTemplates, rlbNewTemplate);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rlbNewTemplate, gridSiteActionTemplates);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rlbNewTemplate, plSiteActionTemplateInfo);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rlbNewTemplate, ucHtmlEditor);

            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, plSiteActionTemplateInfo);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, ucHtmlEditor);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, rlbNewTemplate);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, gridSiteActionTemplates);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, rUserColumnValues);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, pToEmail);
            radAjaxManager.AjaxSettings.AddAjaxSetting(ddlActionType, pToEmail);

            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnSave, plLinks, null, UpdatePanelRenderMode.Inline);

            BindLinks();

            base.OnPreRender(e);
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            dcbParentTemplate.InitDataSource(); //AB: HACK
        }



        /// <summary>
        /// Binds the links.
        /// </summary>
        private void BindLinks()
        {
            var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(((LeadForceBasePage) Page).SiteId, SelectedSiteActionTemplateId);
            UpdateUI(siteActionTemplate);         
        }



        /// <summary>
        /// Inits the form.
        /// </summary>
        private void InitForm()
        {
            Guid templateId;
            

            if (!string.IsNullOrEmpty(Request.Params["__ASYNCPOST"]) && bool.Parse(Request.Params["__ASYNCPOST"]) && Guid.TryParse(Request.Params["__EVENTARGUMENT"], out templateId) )
            {
                ClearSiteActionTemplateForm();                
                rlbNewTemplate.ClearSelection();
                rlbNewTemplate.ClearChecked();
                gridSiteActionTemplates.Rebind();
                gridSiteActionTemplates.Visible = false;

                if (templateId != Guid.Empty)
                {
                    CurrentSiteActionTemplateId = templateId;
                    BindData(templateId);
                }
                else
                    BindData(null);                            
            }                        
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <param name="siteActionTemplateId">The site action template id.</param>
        public void BindData(Guid? siteActionTemplateId)
        {
            var siteId = ((LeadForceBasePage) Page).SiteId;

            dcbParentTemplate.SiteID = siteId;

            plNotBase.Visible = false;

            dcbParentTemplate.SelectedId = Guid.Empty;
            dcbParentTemplate.SelectedText = string.Empty;
                                    

            ddlSiteTemplateCategory.Items.Clear();
            ddlReplaceLinks.Items.Clear();

            EnumHelper.EnumToDropDownList<SiteActionTemplateCategory>(ref ddlSiteTemplateCategory, false);
            EnumHelper.EnumToDropDownList<ReplaceLinks>(ref ddlReplaceLinks, false);

            ddlSiteTemplateCategory.SelectedIndex = ddlSiteTemplateCategory.FindItemIndexByValue(((int)CurrentSiteActionTemplateCategory).ToString());
            ProceedTemplateCategory();

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

                    if (siteActionTemplate.ParentID.HasValue)
                    {
                        dcbParentTemplate.SelectedIdNullable = siteActionTemplate.ParentID;
                        dcbParentTemplate.SelectedText = DataManager.SiteActionTemplate.SelectById((Guid)siteActionTemplate.ParentID).Title;
                    }                    

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

                    pToEmail.Visible = (ActionType)siteActionTemplate.ActionTypeID == ActionType.EmailToFixed;

                    ddlReplaceLinks.SelectedIndex = ddlReplaceLinks.FindItemIndexByValue(siteActionTemplate.ReplaceLinksID.ToString());
                    txtFromEmail.Text = siteActionTemplate.FromEmail;
                    txtFromName.Text = siteActionTemplate.FromName;
                    txtToEmail.Text = siteActionTemplate.ToEmail;
                    txtReplyEmail.Text = siteActionTemplate.ReplyToEmail;
                    txtReplyName.Text = siteActionTemplate.ReplyToName;
                    txtTitle.Text = siteActionTemplate.Title;
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
            txtTitle.Text = string.Empty;
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

                if (CurrentSiteActionTemplateId == Guid.Parse(data["ID"].ToString()))
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

                if (rlbNewTemplate.SelectedValue == "Copy")
                {
                    lbtnSelect.OnClientClick = "if (!" + this.ClientID + "_IsEmpty()) return confirm('Заменить шаблон?');";
                }

                if (rlbNewTemplate.SelectedValue == "BaseOn")
                {
                    var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(((LeadForceBasePage)Page).SiteId, Guid.Parse(data["ID"].ToString()));
                    if (siteActionTemplate != null)
                    {
                        if (!siteActionTemplate.MessageBody.Contains("#Text#"))                        
                            lbtnSelect.OnClientClick = "return confirm('В шаблоне отсутствует тег #Text#. Скопировать шаблон?');";                        
                    }                    
                }
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSelect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSelect_OnClick(object sender, EventArgs e)
        {
            CurrentSiteActionTemplateId = Guid.Parse(((LinkButton) sender).CommandArgument);                                    

            var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(((LeadForceBasePage)Page).SiteId, (Guid) CurrentSiteActionTemplateId);
            if (siteActionTemplate != null)
            {                
                if (rlbNewTemplate.SelectedValue == "BaseOn" && !siteActionTemplate.MessageBody.Contains("#Text#"))
                {
                    rlbNewTemplate.ClearChecked();
                    rlbNewTemplate.ClearSelection();
                    rlbNewTemplate.SelectedIndex = rlbNewTemplate.FindItemIndexByValue("Copy");
                }


                BindData(CurrentSiteActionTemplateId);

                gridSiteActionTemplates.Rebind();

                dcbParentTemplate.SelectedId = (Guid)CurrentSiteActionTemplateId;
                dcbParentTemplate.SelectedText = siteActionTemplate.Title;

                if (rlbNewTemplate.SelectedValue == "BaseOn")
                {
                    BaseOnSiteActionTemplateId = CurrentSiteActionTemplateId;
                    ucHtmlEditor.Content = string.Empty;
                }
                else
                    BaseOnSiteActionTemplateId = null;
            }

            CurrentSiteActionTemplateId = null;
        }



        /// <summary>
        /// Proceeds the template category.
        /// </summary>
        protected void ProceedTemplateCategory()
        {
            switch (CurrentSiteActionTemplateCategory)
            {
                case SiteActionTemplateCategory.BaseTemplate:
                    plNotBase.Visible = false;                    
                    break;
                case SiteActionTemplateCategory.MassMail:
                case SiteActionTemplateCategory.Event:
                case SiteActionTemplateCategory.Workflow:
                    plNotBase.Visible = true;                    
                    //if (BaseOnSiteActionTemplateId.HasValue)
                    //{
                    //    var siteActionTemplate = DataManager.SiteActionTemplate.SelectById((Guid) BaseOnSiteActionTemplateId);
                    //    if (siteActionTemplate != null)
                    //    {
                    //        dcbParentTemplate.SelectedId = siteActionTemplate.ID;
                    //        dcbParentTemplate.SelectedText = siteActionTemplate.Title;
                    //    }
                    //}
                    //else
                    //{
                    //    dcbParentTemplate.SelectedId = Guid.Empty;
                    //    dcbParentTemplate.SelectedText = string.Empty;
                    //}
                    break;                    
            }            
        }



        /// <summary>
        /// Determines whether [is template empty].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is template empty]; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsTemplateEmpty()
        {            
            if (!string.IsNullOrEmpty(txtTitle.Text))
                return false;

            if (!string.IsNullOrEmpty(txtMessageCaption.Text))
                return false;

            if (!string.IsNullOrEmpty(ucHtmlEditor.Content))
                return false;

            if (!string.IsNullOrEmpty(txtFromEmail.Text))
                return false;

            if (!string.IsNullOrEmpty(txtFromName.Text))
                return false;

            if (!string.IsNullOrEmpty(txtReplyEmail.Text))
                return false;

            if (!string.IsNullOrEmpty(txtReplyName.Text))
                return false;

            if (!string.IsNullOrEmpty(txtToEmail.Text))
                return false;

            return true;
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

            Guid? parentId = null;

            switch (rlbNewTemplate.SelectedValue)
            {                                    
                case "BaseOn":
                    parentId = BaseOnSiteActionTemplateId;
                    break;                
            }

            tbl_SiteActionTemplate siteActionTemplate = null;
            
            siteActionTemplate =
                DataManager.SiteActionTemplate.SelectById(((LeadForceBasePage)Page).SiteId, CurrentSiteActionTemplateId.HasValue
                                                                ? (Guid) CurrentSiteActionTemplateId
                                                                : Guid.Empty);

            if (siteActionTemplate == null)
                siteActionTemplate = new tbl_SiteActionTemplate();

            siteActionTemplate.SiteID = ((LeadForceBasePage) Page).SiteId;
            siteActionTemplate.ParentID = parentId;
            siteActionTemplate.Title = txtTitle.Text;
            siteActionTemplate.ActionTypeID = int.Parse(ddlActionType.SelectedValue);
            siteActionTemplate.ReplaceLinksID = int.Parse(ddlReplaceLinks.SelectedValue);
            siteActionTemplate.ToEmail = txtToEmail.Text;
            siteActionTemplate.FromEmail = txtFromEmail.Text;
            siteActionTemplate.FromName = txtFromName.Text;
            siteActionTemplate.ReplyToEmail = txtReplyEmail.Text;
            siteActionTemplate.ReplyToName = txtReplyName.Text;
            siteActionTemplate.MessageCaption = txtMessageCaption.Text;
            siteActionTemplate.MessageBody = ucHtmlEditor.Content;

            siteActionTemplate.SiteActionTemplateCategoryID = (int)CurrentSiteActionTemplateCategory;

            
            if ((SiteActionTemplateCategory)siteActionTemplate.SiteActionTemplateCategoryID != SiteActionTemplateCategory.BaseTemplate)
            {
                if (dcbParentTemplate.SelectedId != Guid.Empty)
                    siteActionTemplate.ParentID = dcbParentTemplate.SelectedId;
            }

            if (CurrentSiteActionTemplateId.HasValue && CurrentSiteActionTemplateId != Guid.Empty)
                DataManager.SiteActionTemplate.Update(siteActionTemplate);
            else
            {                    
                siteActionTemplate.OwnerID = CurrentUser.Instance.ContactID;
                DataManager.SiteActionTemplate.Add(siteActionTemplate);
            }

            SelectedSiteActionTemplateId = siteActionTemplate.ID;            

            rlbNewTemplate.ClearSelection();
            rlbNewTemplate.ClearChecked();
            CurrentSiteActionTemplateId = null;
            BaseOnSiteActionTemplateId = null;

            gridSiteActionTemplates.Rebind();

            CurrentSiteActionTemplateId = siteActionTemplate.ID;

            BindData(CurrentSiteActionTemplateId);

            rtsTabs.Tabs[1].Selected = true;
            RadMultiPage1.PageViews[1].Selected = true;

            UpdateUI(siteActionTemplate);

            if (SelectedTemplateChanged != null)
            {
                SelectedTemplateChanged(this,
                                        new SelectedTemplateChangedEventArgs()
                                            {
                                                SelectedTemplateId = siteActionTemplate.ID,
                                                SelectedTemplateTitle = siteActionTemplate.Title
                                            });
            }

            if (fromSilverlight && !Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_WorkflowCallback"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_WorkflowCallback", string.Format("{0}_WorkflowCallback('{1}', '{2}');", this.ClientID, siteActionTemplate.ID, siteActionTemplate.Title), true);
                return;
            }

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_CloseSiteActionTemplateRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_CloseSiteActionTemplateRadWindow", this.ClientID + "_CloseSiteActionTemplateRadWindow();", true);
        }



        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="siteActionTemplate">The site action template.</param>
        private void UpdateUI(tbl_SiteActionTemplate siteActionTemplate)
        {
            if (siteActionTemplate != null)
            {
                txtSiteActionTemplateId.Text = siteActionTemplate.ID.ToString();
                lbtnSiteActionTemplate.Text = siteActionTemplate.Title;
                lbtnSiteActionTemplate.OnClientClick =
                    string.Format(this.ClientID + "_ShowSiteActionTemplateRadWindow('{0}'); return false;",
                                  siteActionTemplate.ID.ToString());
                hlGoToTemplate.NavigateUrl = UrlsData.AP_SiteActionTemplate(siteActionTemplate.ID);
                hlGoToTemplate.Visible = true;
            }
            else
            {
                lbtnSiteActionTemplate.Text = "Выберите шаблон";
                lbtnSiteActionTemplate.OnClientClick = string.Format(this.ClientID + "_ShowSiteActionTemplateRadWindow('{0}'); return false;", Guid.Empty.ToString());
                hlGoToTemplate.Visible = false;
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the rlbNewTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rlbNewTemplate_OnSelectedIndexChanged(object sender, EventArgs e)
        {            
            switch (rlbNewTemplate.SelectedValue)
            {
                case "Empty":
                    gridSiteActionTemplates.Visible = false;                    
                    BindData(null);
                    break;
                case "Copy":
                case "Template":
                case "BaseOn":
                    gridSiteActionTemplates.Visible = true;                                        
                    break;                
            }

            BaseOnSiteActionTemplateId = null;
            CurrentSiteActionTemplateId = null;
            gridSiteActionTemplates.Rebind();
        }




        /// <summary>
        /// Updates the site action template.
        /// </summary>
        /// <param name="contentId">The content id.</param>
        /// <param name="siteActionTemplateId">The site action template id.</param>
        public void UpdateSiteActionTemplate(Guid contentId, Guid siteActionTemplateId)
        {
            var dataManager = new DataManager();
            var siteActionTemplate = dataManager.SiteActionTemplate.SelectById(((LeadForceBasePage)Page).SiteId, siteActionTemplateId);
            if (siteActionTemplate != null && siteActionTemplate.SiteActionTemplateCategoryID != (int)SiteActionTemplateCategory.BaseTemplate)
            {
                siteActionTemplate.UsageID = contentId;
                dataManager.SiteActionTemplate.Update(siteActionTemplate);                
            }
        }
    }
}