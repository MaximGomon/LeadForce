using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls.Task
{
    public partial class TaskList : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? OrderId
        {
            get
            {
                object o = ViewState["OrderId"];
                return (o == null ? null : (Guid?)o);
            }
            set
            {
                ViewState["OrderId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? ContactId
        {
            get
            {
                object o = ViewState["ContactId"];
                return (o == null ? null : (Guid?)o);
            }
            set
            {
                ViewState["ContactId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? CompanyId
        {
            get
            {
                object o = ViewState["CompanyId"];
                return (o == null ? null : (Guid?)o);
            }
            set
            {
                ViewState["CompanyId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteId
        {
            get
            {
                object o = ViewState["SiteId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["SiteId"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            gridTasks.SiteID = SiteId;
            gridTasks.Actions.Add(new GridAction { Text = "Карточка задачи", NavigateUrl = string.Format("~/{0}/Tasks/Edit/{{0}}", Page.RouteData.Values["tab"] as string), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridTasks.Where = new List<GridWhere>();
            if (OrderId.HasValue)
            {
                gridTasks.Toolbar = false;
                gridTasks.ClassName = "WebCounter.AdminPanel.TaskOnOrders";
                gridTasks.Where.Add(new GridWhere { CustomQuery = string.Format("OrderID=N'{0}' OR tbl_Task.ID IN (SELECT TaskID FROM tbl_TaskMember WHERE OrderID=N'{0}')", OrderId) });
            }
            if (ContactId.HasValue)
            {
                gridTasks.ClassName = "WebCounter.AdminPanel.TaskOnContact";
                gridTasks.Where.Add(new GridWhere { CustomQuery = string.Format("tbl_Task.ID IN (SELECT TaskID FROM tbl_TaskMember WHERE ContactID=N'{0}')", ContactId) });
                if (ContactId != Guid.Empty)                
                    gridTasks.AddNavigateUrl = UrlsData.AP_TaskAdd() + "?ctid=" + ContactId;                
            }
            if (CompanyId.HasValue)
            {
                gridTasks.ClassName = "WebCounter.AdminPanel.TaskOnCompany";
                gridTasks.Where.Add(new GridWhere { CustomQuery = string.Format("tbl_Task.ID IN (SELECT TaskID FROM tbl_TaskMember WHERE ContractorID=N'{0}')", CompanyId) });
                if (CompanyId != Guid.Empty)                
                    gridTasks.AddNavigateUrl = UrlsData.AP_TaskAdd() + "?cyid=" + CompanyId;               
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridTasks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridTasks_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var lrlUserFullName = (Literal)item.FindControl("lrlUserFullName");

                ((Literal)item.FindControl("lrlTaskStatus")).Text = EnumHelper.GetEnumDescription((TaskStatus)int.Parse(data["tbl_Task_TaskStatusID"].ToString()));

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lrlUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), data["tbl_Contact_UserFullName"]);
            }
        }
    }
}