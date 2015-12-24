using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class SourceMonitoring : LeadForceBasePage
    {
        private Guid _sourceMonitoringId;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Мониторинг внешних источников - LeadForce";

            if (Page.RouteData.Values["id"] != null)
                _sourceMonitoringId = Guid.Parse(Page.RouteData.Values["id"] as string);

            hlCancel.NavigateUrl = UrlsData.AP_SourceMonitorings();

            rgSourceMonitoringFilters.Culture = new CultureInfo("ru-RU");

            tagsSourceMonitoring.ObjectID = _sourceMonitoringId;

            if (!Page.IsPostBack)
            {
                BindData();                                
            }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            dcbRequestSourceType.SiteID = SiteId;

            BindSourceTypes();
            BindStatuses();            
            BindSenderProcessings();
            BindProcessingOfAutoReplies();
            BindProcessingOfReturns();

            ViewState["SourceMonitoringFilters"] = DataManager.SourceMonitoringFilter.SelectAll(SiteId, _sourceMonitoringId).ToList();

            var sourceMonitoring = DataManager.SourceMonitoring.SelectById(SiteId, _sourceMonitoringId);
            if (sourceMonitoring != null)
            {
                txtName.Text = sourceMonitoring.Name;
                ddlSourceType.SelectedIndex = ddlSourceType.Items.IndexOf(ddlSourceType.Items.FindByValue(sourceMonitoring.SourceTypeID.ToString()));
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(sourceMonitoring.StatusID.ToString()));
                txtComment.Text = sourceMonitoring.Comment;
                txtPOPHost.Text = sourceMonitoring.PopHost;
                txtPOPUserName.Text = sourceMonitoring.PopUserName;
                txtPOPPassword.Attributes["value"] = sourceMonitoring.PopPassword;
                rntxtPopPort.Text = sourceMonitoring.PopPort.ToString();
                chxIsSsl.Checked = sourceMonitoring.IsSsl;
                chxIsLeaveOnServer.Checked = sourceMonitoring.IsLeaveOnServer;
                lrlLastReceivedAt.Text = sourceMonitoring.LastReceivedAt.HasValue ? sourceMonitoring.LastReceivedAt + " UTC" : string.Empty;                
                rntxtDaysToDelete.Text = sourceMonitoring.DaysToDelete.ToString();
                ddlSenderProcessing.SelectedIndex = ddlSenderProcessing.Items.IndexOf(ddlSenderProcessing.Items.FindByValue(sourceMonitoring.SenderProcessingID.ToString()));
                ddlProcessingOfAutoReplies.SelectedIndex = ddlProcessingOfAutoReplies.Items.IndexOf(ddlProcessingOfAutoReplies.Items.FindByValue(sourceMonitoring.ProcessingOfAutoRepliesID.ToString()));
                chxRemoveAutoReplies.Checked = sourceMonitoring.IsRemoveAutoReplies;
                ddlProcessingOfReturns.SelectedIndex = ddlProcessingOfReturns.Items.IndexOf(ddlProcessingOfReturns.Items.FindByValue(sourceMonitoring.ProcessingOfReturnsID.ToString()));
                chxRemoveReturns.Checked = sourceMonitoring.IsRemoveReturns;
                dcbRequestSourceType.SelectedIdNullable = sourceMonitoring.RequestSourceTypeID;
                ucContact.SelectedValue = sourceMonitoring.ReceiverContactID;
                rdpStartDate.SelectedDate = sourceMonitoring.StartDate;
            }
            else
            {
                rdpStartDate.SelectedDate = DateTime.Now;
            }
        }



        /// <summary>
        /// Binds the source types.
        /// </summary>
        private void BindSourceTypes()
        {
            foreach (var sourceType in EnumHelper.EnumToList<SourceType>())
                ddlSourceType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(sourceType), ((int)sourceType).ToString()));
        }



        /// <summary>
        /// Binds the sender processings.
        /// </summary>
        private void BindSenderProcessings()
        {
            foreach (var senderProcessing in EnumHelper.EnumToList<SenderProcessing>())
                ddlSenderProcessing.Items.Add(new ListItem(EnumHelper.GetEnumDescription(senderProcessing), ((int)senderProcessing).ToString()));
        }



        /// <summary>
        /// Binds the statuses.
        /// </summary>
        private void BindStatuses()
        {
            foreach (var status in EnumHelper.EnumToList<Status>())
                ddlStatus.Items.Add(new ListItem(EnumHelper.GetEnumDescription(status), ((int)status).ToString()));
        }



        /// <summary>
        /// Binds the processing of returns.
        /// </summary>
        private void BindProcessingOfReturns()
        {
            foreach (var processingOfReturns in EnumHelper.EnumToList<ProcessingOfReturns>())
                ddlProcessingOfReturns.Items.Add(new ListItem(EnumHelper.GetEnumDescription(processingOfReturns), ((int)processingOfReturns).ToString()));
        }



        /// <summary>
        /// Binds the processing of auto replies.
        /// </summary>
        private void BindProcessingOfAutoReplies()
        {
            foreach (var processingOfAutoReplies in EnumHelper.EnumToList<ProcessingOfAutoReplies>())
                ddlProcessingOfAutoReplies.Items.Add(new ListItem(EnumHelper.GetEnumDescription(processingOfAutoReplies), ((int)processingOfAutoReplies).ToString()));
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgSourceMonitoringFilters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgSourceMonitoringFilters_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
            {                
                var gridEditFormItem = (GridEditFormItem)e.Item;
                
                var ddlSourceFilterProperty = (DropDownList)gridEditFormItem["SourceProperty"].FindControl("ddlSourceProperty");
                foreach (var sourceEmailProperty in EnumHelper.EnumToList<SourceEmailProperty>())
                    ddlSourceFilterProperty.Items.Add(new ListItem(EnumHelper.GetEnumDescription(sourceEmailProperty), ((int)sourceEmailProperty).ToString()));

                var ddlMonitoringAction = (DropDownList)gridEditFormItem["MonitoringAction"].FindControl("ddlMonitoringAction");
                foreach (var monitoringAction in EnumHelper.EnumToList<MonitoringAction>())
                    ddlMonitoringAction.Items.Add(new ListItem(EnumHelper.GetEnumDescription(monitoringAction), ((int)monitoringAction).ToString()));

                if (!(gridEditFormItem.DataItem is GridInsertionObject))
                {
                    var sourceMonitoringFilter = (tbl_SourceMonitoringFilter) gridEditFormItem.DataItem;
                    if (sourceMonitoringFilter != null)
                    {
                        ddlSourceFilterProperty.SelectedIndex = ddlSourceFilterProperty.Items.IndexOf(ddlSourceFilterProperty.Items.FindByValue(sourceMonitoringFilter.SourcePropertyID.ToString()));
                        ddlMonitoringAction.SelectedIndex = ddlMonitoringAction.Items.IndexOf(ddlMonitoringAction.Items.FindByValue(sourceMonitoringFilter.MonitoringActionID.ToString()));
                    }
                }
            }  
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgSourceMonitoringFilters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgSourceMonitoringFilters_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgSourceMonitoringFilters.DataSource = ViewState["SourceMonitoringFilters"];
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgSourceMonitoringFilters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgSourceMonitoringFilters_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;            
            var id = Guid.Parse(item.GetDataKeyValue("ID").ToString());
            var sourceMonitoringFilter = ((List<tbl_SourceMonitoringFilter>) ViewState["SourceMonitoringFilters"]).Where(s => s.ID == id).FirstOrDefault();
            sourceMonitoringFilter.SourcePropertyID = int.Parse(((DropDownList)item.FindControl("ddlSourceProperty")).SelectedValue);
            sourceMonitoringFilter.Mask = ((TextBox)item.FindControl("txtMask")).Text;            
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgSourceMonitoringFilters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgSourceMonitoringFilters_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var sourceMonitoringFilter = new tbl_SourceMonitoringFilter();
            sourceMonitoringFilter.ID = Guid.NewGuid();
            sourceMonitoringFilter.SiteID = SiteId;
            sourceMonitoringFilter.SourceMonitoringID = _sourceMonitoringId;
            sourceMonitoringFilter.SourcePropertyID = int.Parse(((DropDownList)item.FindControl("ddlSourceProperty")).SelectedValue);
            sourceMonitoringFilter.Mask = ((TextBox) item.FindControl("txtMask")).Text;
            ((List<tbl_SourceMonitoringFilter>) ViewState["SourceMonitoringFilters"]).Add(sourceMonitoringFilter);            
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgSourceMonitoringFilters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgSourceMonitoringFilters_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<tbl_SourceMonitoringFilter>) ViewState["SourceMonitoringFilters"]).Remove(
                ((List<tbl_SourceMonitoringFilter>) ViewState["SourceMonitoringFilters"]).Where(s => s.ID == id).FirstOrDefault());            
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            var sourceMonitoring = DataManager.SourceMonitoring.SelectById(SiteId, _sourceMonitoringId) ?? new tbl_SourceMonitoring();

            sourceMonitoring.Name = txtName.Text;
            sourceMonitoring.SourceTypeID = int.Parse(ddlSourceType.SelectedValue);
            sourceMonitoring.StatusID = int.Parse(ddlStatus.SelectedValue);
            sourceMonitoring.Comment = txtComment.Text;
            sourceMonitoring.PopHost = txtPOPHost.Text;
            sourceMonitoring.PopUserName = txtPOPUserName.Text;
            sourceMonitoring.PopPassword = txtPOPPassword.Text;
            sourceMonitoring.PopPort = int.Parse(rntxtPopPort.Text);
            sourceMonitoring.IsSsl = chxIsSsl.Checked;
            sourceMonitoring.IsLeaveOnServer = chxIsLeaveOnServer.Checked;            
            sourceMonitoring.DaysToDelete = !string.IsNullOrEmpty(rntxtDaysToDelete.Text) ? (int?)int.Parse(rntxtDaysToDelete.Text) : null;
            sourceMonitoring.SenderProcessingID = int.Parse(ddlSenderProcessing.SelectedValue);
            sourceMonitoring.ProcessingOfAutoRepliesID = int.Parse(ddlProcessingOfAutoReplies.SelectedValue);
            sourceMonitoring.IsRemoveAutoReplies = chxRemoveAutoReplies.Checked;
            sourceMonitoring.ProcessingOfReturnsID = int.Parse(ddlProcessingOfReturns.SelectedValue);
            sourceMonitoring.IsRemoveReturns = chxRemoveReturns.Checked;
            sourceMonitoring.RequestSourceTypeID = dcbRequestSourceType.SelectedIdNullable;

            sourceMonitoring.ReceiverContactID = ucContact.SelectedValue;            

            sourceMonitoring.StartDate = rdpStartDate.SelectedDate;

            if (sourceMonitoring.ID == Guid.Empty)
            {                
                sourceMonitoring.SiteID = SiteId;                
                DataManager.SourceMonitoring.Add(sourceMonitoring);
            }
            else
                DataManager.SourceMonitoring.Update(sourceMonitoring);

            DataManager.SourceMonitoringFilter.DeleteAll(SiteId, sourceMonitoring.ID);
            DataManager.SourceMonitoringFilter.Add((List<tbl_SourceMonitoringFilter>)ViewState["SourceMonitoringFilters"]);

            tagsSourceMonitoring.SaveTags(sourceMonitoring.ID);

            Response.Redirect(UrlsData.AP_SourceMonitorings());
        }        
    }
}