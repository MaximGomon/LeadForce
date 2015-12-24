using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Task
{
    public partial class MainTaskMember : System.Web.UI.UserControl
    {
        #region Prperties

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public TaskMap Task
        {
            get
            {
                if (ViewState["Task"] == null)
                    ViewState["Task"] = new TaskMap();
                return (TaskMap)ViewState["Task"];
            }
            set
            {
                ViewState["Task"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public TaskTypePaymentScheme? TypePaymentScheme
        {
            get
            {
                return (TaskTypePaymentScheme?)ViewState["TypePaymentScheme"];
            }
            set
            {
                ViewState["TypePaymentScheme"] = value;
            }
        }

        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        #region Methods

        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            dcbOrder.SiteID = CurrentUser.Instance.SiteID;
            plCompanyPay.Visible = TypePaymentScheme == TaskTypePaymentScheme.CompanyPayment;
            BindTaskMemberRoles();

            if (Task != null)
            {
                var dataManager = new DataManager();
                if (Task.MainMemberContactID.HasValue)
                {
                    var taskMember = dataManager.TaskMember.SelectByContactId(Task.ID, (Guid) Task.MainMemberContactID);
                    if (taskMember != null)
                    {
                        if (taskMember.ContractorID.HasValue)
                            dcbContractor.SelectedId = (Guid)taskMember.ContractorID;

                        ddlTaskMemberRole.SelectedIndex = ddlTaskMemberRole.Items.IndexOf(ddlTaskMemberRole.Items.FindByValue(taskMember.TaskMemberRoleID.ToString()));
                        ucContact.SelectedValue = taskMember.ContactID;
                        lrlTaskMemberStatus.Text = EnumHelper.GetEnumDescription((TaskMemberStatus)taskMember.TaskMemberStatusID);

                        if (taskMember.OrderID.HasValue)
                            dcbOrder.SelectedId = (Guid)taskMember.OrderID;

                        ucOrderProduct.SelectedValue = taskMember.OrderProductsID;
                        txtComment.Text = taskMember.Comment;
                        txtUserComment.Text = taskMember.UserComment;
                        chxIsInformed.Checked = taskMember.IsInformed;

                        RefreshOrders();
                    }
                }
            }
        }



        /// <summary>
        /// Binds the task member roles.
        /// </summary>        
        private void BindTaskMemberRoles()
        {
            ddlTaskMemberRole.Items.Clear();
            foreach (var taskMemberRole in EnumHelper.EnumToList<TaskMemberRole>())
                ddlTaskMemberRole.Items.Add(new ListItem(EnumHelper.GetEnumDescription(taskMemberRole), ((int)taskMemberRole).ToString()));
            ddlTaskMemberRole.Items.Insert(0, new ListItem("Выберите значение", "-1") { Selected = true });
        }



        /// <summary>
        /// Saves the specified task id.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        public void Save(Guid taskId)
        {
            if (!ucContact.SelectedValue.HasValue || ddlTaskMemberRole.SelectedValue == "-1")
                return;            

            var dataManager = new DataManager();

            tbl_TaskMember taskMember = null;

            var task = dataManager.Task.SelectById(CurrentUser.Instance.SiteID, taskId);

            if (task.MainMemberContactID.HasValue)            
                taskMember = dataManager.TaskMember.SelectByContactId(task.ID, (Guid) task.MainMemberContactID);
            
            if (taskMember == null)
                taskMember = new tbl_TaskMember();

            if (dcbContractor.SelectedId != Guid.Empty)
                taskMember.ContractorID = dcbContractor.SelectedId;
            else
                taskMember.ContractorID = null;

            taskMember.ContactID = ucContact.SelectedValue;
            taskMember.TaskMemberRoleID = int.Parse(ddlTaskMemberRole.SelectedValue);
            taskMember.TaskMemberStatusID = (int) TaskMemberStatus.Plan;
            taskMember.UserComment = txtUserComment.Text;
            taskMember.Comment = txtComment.Text;
            taskMember.IsInformed = chxIsInformed.Checked;

            if (dcbOrder.SelectedId != Guid.Empty)
                taskMember.OrderID = dcbOrder.SelectedId;
            else
                taskMember.OrderID = null;

            taskMember.OrderProductsID = ucOrderProduct.SelectedValue;

            if (taskMember.ID == Guid.Empty)
            {
                taskMember.ID = Guid.Empty;
                taskMember.TaskID = task.ID;
                dataManager.TaskMember.Add(taskMember);
            }
            else
                dataManager.TaskMember.Update(taskMember);

            task.MainMemberContactID = taskMember.ContactID;
            task.MainMemberCompanyID = taskMember.ContractorID;

            dataManager.Task.Update(task);
        }


        #endregion


        #region Events



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbOrder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbOrder_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {            
            ucOrderProduct.OrderId = dcbOrder.SelectedId;
            ucOrderProduct.SelectedValue = null;            
        }

        #endregion



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbContractor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbContractor_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RefreshOrders();
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ucContact control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucContact_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RefreshOrders();
        }




        /// <summary>
        /// Refreshes the orders.
        /// </summary>
        protected void RefreshOrders()
        {
            dcbOrder.Filters.Clear();
            
            if (ucContact.SelectedValue.HasValue && ucContact.SelectedValue.Value != Guid.Empty)
            {
                dcbOrder.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn()
                {
                    Name = "BuyerContactID",
                    DbType = DbType.Guid,
                    Value = ucContact.SelectedValue.Value.ToString()
                });
            }

            if (dcbContractor.SelectedIdNullable.HasValue && dcbContractor.SelectedIdNullable.Value != Guid.Empty)
            {
                dcbOrder.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn()
                {
                    Name = "BuyerCompanyID",
                    DbType = DbType.Guid,
                    Value = dcbContractor.SelectedIdNullable.Value.ToString()
                });
            }

            dcbOrder.BindData();
        }
    }
}