using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Common;
using Page = System.Web.UI.Page;

namespace WebCounter.AdminPanel.UserControls.WorkflowTemplate
{
    public partial class SelectWorkflowTemplate : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();
        protected RadAjaxManager radAjaxManager = null;
        public Access access;


        /// <summary>
        /// Gets or sets the current workflow template id.
        /// </summary>
        /// <value>
        /// The current workflow template id.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        protected Guid? CurrentWorkflowTemplateId
        {
            get
            {
                return (Guid?)ViewState["CurrentWorkflowTemplateId"];
            }
            set
            {
                ViewState["CurrentWorkflowTemplateId"] = value;
            }
        }


        /// <summary>
        /// Gets or sets the selected workflow template id.
        /// </summary>
        /// <value>
        /// The selected workflow template id.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SelectedWorkflowTemplateId
        {
            get
            {
                if (ViewState["SelectedWorkflowTemplateId"] == null)
                    ViewState["SelectedWorkflowTemplateId"] = Guid.Empty;

                return (Guid)ViewState["SelectedWorkflowTemplateId"];
            }
            set
            {
                ViewState["SelectedWorkflowTemplateId"] = value;
                txtWorkflowTemplateId.Text = value.ToString();
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
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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

                BindData();
            }

            gridWorkflowTemplates.SiteID = CurrentUser.Instance.SiteID;
            gridWorkflowTemplates.Where.Add(new GridWhere { CustomQuery = string.Format("(DataBaseStatusID = {0})", (int)DataBaseStatus.Active) });

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnSave, pnlWorkflowTemplate, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbWorkflowTemplate, gridWorkflowTemplates);
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            if (SelectedWorkflowTemplateId == Guid.Empty)
            {
                lbWorkflowTemplate.Text = "Выберите шаблон процесса";
                CurrentWorkflowTemplateId = null;
            }
            else
            {
                var workflowTemplate = DataManager.WorkflowTemplate.SelectByIdWithDeleted(CurrentUser.Instance.SiteID, SelectedWorkflowTemplateId);
                txtWorkflowTemplateId.Text = workflowTemplate.ID.ToString();
                lbWorkflowTemplate.Text = workflowTemplate.Name;
                CurrentWorkflowTemplateId = SelectedWorkflowTemplateId;
            }

            gridWorkflowTemplates.Rebind();
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridWorkflowTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridWorkflowTemplates_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((Literal)item.FindControl("lrlWorkflowTemplateStatus")).Text = EnumHelper.GetEnumDescription((WorkflowTemplateStatus)int.Parse(data["tbl_WorkflowTemplate_Status"].ToString()));

                var lbtnSelect = (LinkButton)item.FindControl("lbtnSelect");
                var imgOk = (Image)item.FindControl("imgOk");
                lbtnSelect.CommandArgument = data["ID"].ToString();

                if (CurrentWorkflowTemplateId == Guid.Parse(data["ID"].ToString()))
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
            CurrentWorkflowTemplateId = Guid.Parse(((LinkButton)sender).CommandArgument);
            gridWorkflowTemplates.Rebind();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (CurrentWorkflowTemplateId != null)
            {
                SelectedWorkflowTemplateId = (Guid)CurrentWorkflowTemplateId;
                BindData();   
            }

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseWorkflowTemplateRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseWorkflowTemplateRadWindow", "CloseWorkflowTemplateRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnClick event of the lbWorkflowTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbWorkflowTemplate_OnClick(object sender, EventArgs e)
        {
            BindData();

            if (!Page.ClientScript.IsStartupScriptRegistered("ShowWorkflowTemplateRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowWorkflowTemplateRadWindow", "ShowWorkflowTemplateRadWindow();", true);
        }
    }
}