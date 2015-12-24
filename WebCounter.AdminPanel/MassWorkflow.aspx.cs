using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Contact;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class MassWorkflow : LeadForceBasePage
    {
        private Guid _massWorkflowId;
        public Access access;
        protected RadAjaxManager radAjaxManager = null;


        public List<Guid> SelectedContactList
        {
            get
            {
                if (ViewState["SelectedContactList"] == null)
                    ViewState["SelectedContactList"] = new List<Guid>();
                return (List<Guid>)ViewState["SelectedContactList"];
            }
            set
            {
                ViewState["SelectedContactList"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Мероприятие - LeadForce";

            access = Access.Check();
            if (!access.Write)
                lbtnSave.Visible = false;

            if (Page.RouteData.Values["id"] != null)
                _massWorkflowId = Guid.Parse(Page.RouteData.Values["id"] as string);

            hlCancel.NavigateUrl = UrlsData.AP_MassWorkflows();

            gridContacts.SiteID = SiteId;

            if (!Page.IsPostBack)
                BindData();

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(ucSelectContacts, gridContacts);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, ucChart1);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, ucChart2);
            radAjaxManager.AjaxRequest += radAjaxManager_AjaxRequest;
        }



        /// <summary>
        /// Handles the AjaxRequest event of the radAjaxManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void radAjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "BuildCharts")
            {
                if (!string.IsNullOrEmpty(hfWidth.Value))
                {
                    ucChart1.AnalyticReportId = Guid.Parse("67566bb9-5dec-422a-bc0f-eaff5dc40a3c");
                    ucChart2.AnalyticReportId = Guid.Parse("a00fd583-24de-4f75-8f79-f5de236576e8");
                    ucChart1.ColumnSystemNameValueToBuildChart = "Conversion";
                    ucChart2.ColumnSystemNameValueToBuildChart = "Conversion";

                    ucChart1.ReportFilters.Clear();
                    ucChart2.ReportFilters.Clear();

                    var filter = new AnalyticReportFilter
                                     {
                                         FilterId = Guid.NewGuid(),
                                         ColumnName = "TBLMassWorkflow.ID",
                                         Operator = FilterOperator.Equal,
                                         Value = _massWorkflowId.ToString()
                                     };

                    ucChart1.ReportFilters.Add(filter);
                    ucChart2.ReportFilters.Add(filter);

                    ucChart1.Visible = true;
                    ucChart2.Visible = true;
                    ucChart1.Width = int.Parse(hfWidth.Value) - 295;
                    ucChart2.Width = int.Parse(hfWidth.Value) - 295;

                    ucChart1.BindData();
                    ucChart2.BindData();
                }
            }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            EnumHelper.EnumToDropDownList<MassWorkflowType>(ref ddlMassWorkflowType);

            if (_massWorkflowId != Guid.Empty)
            {
                var massWorkflow = DataManager.MassWorkflow.SelectById(SiteId, _massWorkflowId);
                txtName.Text = massWorkflow.Name;
                txtDescription.Text = massWorkflow.Description;
                ucSelectWorkflowTemplate.SelectedWorkflowTemplateId = massWorkflow.WorkflowTemplateID;
                litStatus.Text = EnumHelper.GetEnumDescription((MassWorkflowStatus)massWorkflow.Status);
                hdnStatus.Value = massWorkflow.Status.ToString();
                switch ((MassWorkflowStatus)massWorkflow.Status)
                {
                    case MassWorkflowStatus.Active:
                        btnRun.Visible = false;
                        ucSelectContacts.Visible = false;
                        break;
                    case MassWorkflowStatus.Done:
                        btnRun.Visible = false;
                        btnCancel.Visible = false;
                        ucSelectContacts.Visible = false;
                        break;
                    case MassWorkflowStatus.Cancelled:
                        btnRun.Visible = false;
                        btnCancel.Visible = false;
                        ucSelectContacts.Visible = false;
                        break;
                }

                SelectedContactList = DataManager.MassWorkflowContact.SelectByMassWorkflowId(_massWorkflowId).Select(a => a.ContactID).ToList();

                if ((MassWorkflowStatus)massWorkflow.Status != MassWorkflowStatus.InPlans)
                {
                    ucSelectContacts.Visible = false;
                    gridContacts.Columns[7].Visible = false;
                }
                    

                if (massWorkflow.StartDate.HasValue)
                {
                    txtStartDate.Text = ((DateTime)massWorkflow.StartDate).ToString("dd.MM.yyyy HH:mm:ss");
                    pnlStartDate.Visible = true;
                }

                if (massWorkflow.MassWorkflowTypeID.HasValue)
                {
                    ddlMassWorkflowType.SelectedIndex = ddlMassWorkflowType.FindItemIndexByValue(massWorkflow.MassWorkflowTypeID.Value.ToString());
                }
                    
            }
            else
            {
                litStatus.Text = EnumHelper.GetEnumDescription(MassWorkflowStatus.InPlans);
                hdnStatus.Value = ((int)MassWorkflowStatus.InPlans).ToString();
            }

            BindGridContacts();
        }



        /// <summary>
        /// Binds the grid contacts.
        /// </summary>
        protected void BindGridContacts()
        {
            var selectedItem = new List<Guid>();
            selectedItem = SelectedContactList;
            if (selectedItem.Count == 0)
                selectedItem.Add(Guid.Empty);

            var query = new StringBuilder();
            foreach (var item in selectedItem)
                query.AppendFormat("'{0}',", item);

            
            gridContacts.Where = new List<GridWhere>();
            gridContacts.Where.Add(new GridWhere { CustomQuery = string.Format("tbl_Contact.ID IN ({0})", query.ToString().TrimEnd(new[] { ',' })) });

            ucSelectContacts.SelectedItems = SelectedContactList;
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

            var massWorkflow = DataManager.MassWorkflow.SelectById(SiteId, _massWorkflowId) ?? new tbl_MassWorkflow();

            massWorkflow.SiteID = SiteId;
            massWorkflow.Name = txtName.Text;
            massWorkflow.Description = txtDescription.Text;
            massWorkflow.WorkflowTemplateID = ucSelectWorkflowTemplate.SelectedWorkflowTemplateId;
            massWorkflow.Status = hdnStatus.Value.ToInt();

            if (!string.IsNullOrEmpty(ddlMassWorkflowType.SelectedValue))
                massWorkflow.MassWorkflowTypeID = int.Parse(ddlMassWorkflowType.SelectedValue);
            else
                massWorkflow.MassWorkflowTypeID = null;

            if (_massWorkflowId == Guid.Empty)
            {
                massWorkflow.ID = Guid.NewGuid();
                DataManager.MassWorkflow.Add(massWorkflow);
            }
            else
                DataManager.MassWorkflow.Update(massWorkflow);

            DataManager.MassWorkflowContact.Save(SelectedContactList, massWorkflow.ID);

            if ((MassWorkflowStatus)massWorkflow.Status == MassWorkflowStatus.Active && massWorkflow.StartDate == null)
                DataManager.MassWorkflow.MassWorkflowInit(massWorkflow.ID);

            if ((MassWorkflowStatus)massWorkflow.Status == MassWorkflowStatus.Cancelled)
                DataManager.MassWorkflow.MassWorkflowCancel(massWorkflow.ID);

            Response.Redirect(UrlsData.AP_MassWorkflows());
        }



        /// <summary>
        /// Handles the OnClick event of the btnRun control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnRun_OnClick(object sender, EventArgs e)
        {
            litStatus.Text = EnumHelper.GetEnumDescription(MassWorkflowStatus.Active);
            hdnStatus.Value = ((int)MassWorkflowStatus.Active).ToString();
            btnRun.Visible = false;
        }



        /// <summary>
        /// Handles the OnClick event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            litStatus.Text = EnumHelper.GetEnumDescription(MassWorkflowStatus.Cancelled);
            hdnStatus.Value = ((int)MassWorkflowStatus.Cancelled).ToString();
            btnRun.Visible = false;
            btnCancel.Visible = false;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridContacts_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var ibtnDelete = (ImageButton)item.FindControl("ibtnDelete");
                ibtnDelete.CommandArgument = data["ID"].ToString();
            }
        }



        /// <summary>
        /// Handles the OnSelectedChanged event of the ucSelectContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.Contact.SelectContacts.SelectedChangedEventArgs"/> instance containing the event data.</param>
        protected void ucSelectContacts_OnSelectedChanged(object sender, SelectContacts.SelectedChangedEventArgs e)
        {
            SelectedContactList = e.ContactList;

            BindGridContacts();
            gridContacts.Rebind();
        }



        /// <summary>
        /// Handles the OnCommand event of the ibtnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void ibtnDelete_OnCommand(object sender, CommandEventArgs e)
        {
            SelectedContactList.Remove(e.CommandArgument.ToString().ToGuid());

            BindGridContacts();
            gridContacts.Rebind();
        }        
    }
}