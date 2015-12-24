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
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls.Task
{
    public partial class TaskHistories : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid TaskId
        {
            get
            {
                object o = ViewState["TaskId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["TaskId"] = value;
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            gridTaskHistories.Where = new List<GridWhere>();
            gridTaskHistories.Where.Add(new GridWhere { Column = "TaskID", Value = TaskId.ToString() });
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridTaskHistories control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridTaskHistories_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var lrlUserFullName = (Literal)item.FindControl("lrlUserFullName");

                ((Literal)item.FindControl("lrlTaskStatus")).Text = EnumHelper.GetEnumDescription((TaskStatus)int.Parse(data["tbl_TaskHistory_TaskStatusID"].ToString()));

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lrlUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), data["tbl_Contact_UserFullName"]);
            }
        }
    }
}