using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Order;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;


namespace WebCounter.AdminPanel.UserControls.Task
{
    public partial class TaskMembers : System.Web.UI.UserControl
    {
        private DataManager _dataManager;
        
        #region Properties

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public int? CurrentStatus
        {
            get
            {
                return (int?)ViewState["CurrentStatus"];
            }
            set
            {
                ViewState["CurrentStatus"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? ProductId
        {
            get { return (Guid?)ViewState["ProductId"]; }
            set { ViewState["ProductId"] = value; }
        }

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
        public TaskTypeCategory? TypeCategory
        {
            get
            {
                return (TaskTypeCategory?)ViewState["TypeCategory"];
            }
            set
            {
                ViewState["TypeCategory"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public TaskMembersCount? MembersCount
        {
            get
            {
                return (TaskMembersCount?)ViewState["MembersCount"];
            }
            set
            {
                ViewState["MembersCount"] = value;
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


        public List<TaskMemberMap> TaskMembersList
        {
            get { return (List<TaskMemberMap>)ViewState["TaskMembers"]; }
        }

        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataManager = ((LeadForceBasePage)Page).DataManager;
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rbtnSelectEventTime, ucSelectEventTime);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rbtnSelectMeetingTime, ucSelectMeetingTime);

            rgTaskMembers.Culture = new CultureInfo("ru-RU");

            if (!Page.IsPostBack)
                BindData();
        }


        #region Methods

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            UpdateActions();            

            if (Task.ID != Guid.Empty)
                ViewState["TaskMembers"] = _dataManager.TaskMember.SelectAll(Task.ID).Select(tm => new TaskMemberMap()
                                                                                                            {
                                                                                                                ID = tm.ID,
                                                                                                                TaskID = tm.TaskID,
                                                                                                                ContractorID = tm.ContractorID,
                                                                                                                ContactID = tm.ContactID,
                                                                                                                TaskMemberRoleID = tm.TaskMemberRoleID,
                                                                                                                TaskMemberStatusID = tm.TaskMemberStatusID,
                                                                                                                Comment = tm.Comment,
                                                                                                                OrderID = tm.OrderID,
                                                                                                                OrderProductsID = tm.OrderProductsID,
                                                                                                                UserComment = tm.UserComment,
                                                                                                                IsInformed = tm.IsInformed
                                                                                                            }).ToList();
            else
                ViewState["TaskMembers"] = new List<TaskMemberMap>();
        }



        /// <summary>
        /// Updates the actions.
        /// </summary>
        public void UpdateActions()
        {
            rbtnSelectEventTime.Visible = false;
            rbtnSelectMeetingTime.Visible = false;
            rbtnInviteMembers.Visible = false;
            rbtnInformMembers.Visible = false;
            rbtnMemberConfirmed.Visible = false;
            rbtnOrginizerConfirmed.Visible = false;
            rbtnParticipated.Visible = false;
            rbtnRefusedNotInterest.Visible = false;
            rbtnRefusedFailureNoWay.Visible = false;
            rbtnParticipatedCanceled.Visible = false;


            if (TypeCategory == TaskTypeCategory.LongTermTask || TypeCategory == TaskTypeCategory.TODO)
            {
                rbtnInviteMembers.Visible = true;
                rbtnInformMembers.Visible = true;
                rbtnMemberConfirmed.Visible = true;
                rbtnParticipatedCanceled.Visible = true;
                rbtnRefusedFailureNoWay.Visible = true;
                rbtnRefusedNotInterest.Visible = true;
                rbtnOrginizerConfirmed.Visible = true;
                rbtnParticipated.Visible = true;
            }

            if (TypeCategory == TaskTypeCategory.Meeting)
            {
                //rbtnSelectMeetingTime.Visible = true;
                rbtnInviteMembers.Visible = true;
                rbtnInformMembers.Visible = true;
                rbtnMemberConfirmed.Visible = true;
                rbtnParticipatedCanceled.Visible = true;
                rbtnRefusedFailureNoWay.Visible = true;
                rbtnRefusedNotInterest.Visible = true;
            }

            if (TypeCategory == TaskTypeCategory.Event)
            {
                //rbtnSelectEventTime.Visible = true;
                rbtnInviteMembers.Visible = true;
                rbtnInformMembers.Visible = true;
                rbtnMemberConfirmed.Visible = true;
                rbtnParticipatedCanceled.Visible = true;
            }
        }



        /// <summary>
        /// Updates the responsible.
        /// </summary>
        /// <param name="responsibleId">The responsible id.</param>
        public void UpdateResponsible(Guid responsibleId)
        {
            if (MembersCount == TaskMembersCount.One)
                return;

            var dataManager = new DataManager();
            var contact = dataManager.Contact.SelectById(CurrentUser.Instance.SiteID, responsibleId);

            if (contact == null)
                return;

            var taskMember = ((List<TaskMemberMap>)ViewState["TaskMembers"]).Where(s => s.ContactID == responsibleId || s.TaskMemberRoleID == (int)TaskMemberRole.Responsible).FirstOrDefault() ?? new TaskMemberMap();

            taskMember.ContractorID = contact.CompanyID;
            taskMember.ContactID = contact.ID;
            taskMember.TaskMemberRoleID = (int) TaskMemberRole.Responsible;
            
            if (taskMember.ID == Guid.Empty)
            {
                taskMember.ID = Guid.NewGuid();
                taskMember.TaskMemberStatusID = (int) TaskMemberStatus.Plan;
                taskMember.TaskMemberStatusID = (int)TaskMemberStatus.Plan;
                ((List<TaskMemberMap>)ViewState["TaskMembers"]).Add(taskMember);
            }

            rgTaskMembers.Rebind();
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="taskMemberId">The taskMemberId.</param>
        /// <param name="item">The item.</param>
        private void SaveToViewState(Guid taskMemberId, GridEditableItem item)
        {
            var taskMember = ((List<TaskMemberMap>)ViewState["TaskMembers"]).Where(s => s.ID == taskMemberId).FirstOrDefault() ?? new TaskMemberMap();
            if (((DictionaryComboBox)item.FindControl("dcbContractor")).SelectedId != Guid.Empty)
                taskMember.ContractorID = ((DictionaryComboBox)item.FindControl("dcbContractor")).SelectedId;
            else
                taskMember.ContractorID = null;

            taskMember.ContactID = ((ContactComboBox)item.FindControl("ucContact")).SelectedValue;

            if (((DictionaryComboBox)item.FindControl("dcbOrder")).SelectedId != Guid.Empty)
                taskMember.OrderID = ((DictionaryComboBox)item.FindControl("dcbOrder")).SelectedId;

            taskMember.OrderProductsID = ((OrderProductsComboBox) item.FindControl("ucOrderProduct")).SelectedValue;

            taskMember.TaskMemberRoleID = int.Parse(((DropDownList)item.FindControl("ddlTaskMemberRole")).SelectedValue);            
            taskMember.Comment = ((TextBox) item.FindControl("txtComment")).Text;
            taskMember.UserComment = ((TextBox)item.FindControl("txtUserComment")).Text;
            taskMember.IsInformed = ((CheckBox)item.FindControl("chxIsInformed")).Checked;

            if (taskMember.ID == Guid.Empty)
            {
                taskMember.ID = Guid.NewGuid();
                taskMember.TaskMemberStatusID = (int)TaskMemberStatus.Plan;
                ((List<TaskMemberMap>)ViewState["TaskMembers"]).Add(taskMember);
            }
        }



        /// <summary>
        /// Binds the task member roles.
        /// </summary>
        /// <param name="taskMemberRoles">The task member roles.</param>
        private void BindTaskMemberRoles(DropDownList taskMemberRoles)
        {
            taskMemberRoles.Items.Clear();
            foreach (var taskMemberRole in EnumHelper.EnumToList<TaskMemberRole>())
                taskMemberRoles.Items.Add(new ListItem(EnumHelper.GetEnumDescription(taskMemberRole), ((int)taskMemberRole).ToString()));
            taskMemberRoles.Items.Insert(0, new ListItem("Выберите значение", "-1") { Selected = true });
        }



        /// <summary>
        /// Saves the specified task id.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        public bool Save(Guid taskId)
        {            
            using (var scope = new TransactionScope())
            {
                _dataManager.TaskMember.DeleteAll(taskId);
                var taskMembers = TaskMembersList.Select(tm => new tbl_TaskMember()
                {
                    ID = tm.ID,
                    ContractorID = tm.ContractorID,
                    ContactID = tm.ContactID,
                    OrderID = tm.OrderID,
                    OrderProductsID = tm.OrderProductsID,
                    TaskMemberRoleID = tm.TaskMemberRoleID,
                    TaskMemberStatusID = tm.TaskMemberStatusID,
                    Comment = tm.Comment,
                    UserComment = tm.UserComment,
                    IsInformed = tm.IsInformed
                });

                foreach (var taskMember in taskMembers)
                {
                    if (taskMember.OrderProductsID.HasValue)
                    {
                        var orderProduct = _dataManager.OrderProducts.SelectById((Guid) taskMember.OrderProductsID);
                        if (orderProduct != null && orderProduct.ParentOrderProductID.HasValue 
                            && _dataManager.OrderProducts.StockQuantity((Guid) orderProduct.ParentOrderProductID) <= 0)
                            return false;
                    }
                }

                _dataManager.TaskMember.Add(taskMembers.ToList(), taskId);
                scope.Complete();
            }

            /*if (TypeCategory == TaskTypeCategory.Event)
                ucSelectEventTime.Save(taskId);

            if (TypeCategory == TaskTypeCategory.Meeting)
                ucSelectMeetingTime.Save(taskId);*/

            var membersToActions = (List<TaskMemberMap>) ViewState["ActionMembers"];
            if (membersToActions == null)
                return true;
            
            foreach (var memberToAction in membersToActions)
            {
                var taskMember = _dataManager.TaskMember.SelectById(memberToAction.ID);
                if (taskMember != null && CurrentStatus.HasValue)
                {
                    //Если не инфорировать, меняем статус, иначе меняем реквизит
                    if (CurrentStatus != 1)
                        taskMember.TaskMemberStatusID = CurrentStatus;
                    else
                        taskMember.IsInformed = true;

                    _dataManager.TaskMember.Update(taskMember);
                }

                switch (CurrentStatus)
                {                        
                    case (int)TaskMemberStatus.Invited:
                        if (TypeCategory == TaskTypeCategory.Meeting)
                            _dataManager.User.InvitePortalUser(SiteId, (Guid)memberToAction.ContactID, taskId, BusinessLogicLayer.Configuration.SiteActionTemplates.InvitationMeetingTemplate);
                        else
                            _dataManager.User.InvitePortalUser(SiteId, (Guid)memberToAction.ContactID, taskId, BusinessLogicLayer.Configuration.SiteActionTemplates.InvitationTemplate);
                        break;
                    case 1:
                        _dataManager.User.InvitePortalUser(SiteId, (Guid)memberToAction.ContactID, taskId, BusinessLogicLayer.Configuration.SiteActionTemplates.ClientInformationTemplate);
                        break;
                }
            }

            return true;
        }



        /// <summary>
        /// Gets the member with case.
        /// </summary>
        /// <param name="membersCount">The members count.</param>
        /// <returns></returns>
        public static string GetMemberWithCase(int membersCount)
        {
            if (membersCount < 0)
                return string.Empty;

            var bonuses = membersCount.ToString();
            var bonusesCase = "участников";
            var lastChar = bonuses[bonuses.Length - 1];

            if (bonuses.Length >= 2 && bonuses[bonuses.Length - 2] == '1')            
                bonusesCase = "участников";            
            else if (lastChar == '1')            
                bonusesCase = "участник";            
            else if (lastChar == '2' || lastChar == '3' || lastChar == '4')                           
                bonusesCase = "участника";            

            return string.Format("{0} {1}", bonuses, bonusesCase);
        }

        #endregion

        #region Events
        
        /// <summary>
        /// Handles the NeedDataSource event of the rgTaskMembers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgTaskMembers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgTaskMembers.DataSource = ViewState["TaskMembers"];
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgTaskMembers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgTaskMembers_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;

                gridEditFormItem.FindControl("plContactPay").Visible = TypePaymentScheme == TaskTypePaymentScheme.ContactPayment;

                var dcbContractor = (DictionaryComboBox)gridEditFormItem.FindControl("dcbContractor");
                dcbContractor.SiteID = SiteId;
                dcbContractor.BindData();

                var ucContact = (ContactComboBox)gridEditFormItem.FindControl("ucContact");                

                //Если задача добавляется с карточки компании
                if (CompanyId.HasValue)
                    dcbContractor.SelectedId = (Guid)CompanyId;

                //Если задача добавляется с карточки контакта
                if (ContactId.HasValue)
                    ucContact.SelectedValue = ContactId;

                var dcbOrder = (DictionaryComboBox)gridEditFormItem.FindControl("dcbOrder");
                dcbOrder.SiteID = SiteId;
                dcbOrder.BindData();                

                var ddlTaskMemberRole = (DropDownList)gridEditFormItem.FindControl("ddlTaskMemberRole");
                BindTaskMemberRoles(ddlTaskMemberRole);
                ddlTaskMemberRole.SelectedIndex = ddlTaskMemberRole.Items.IndexOf(ddlTaskMemberRole.Items.FindByValue(((int) TaskMemberRole.Responsible).ToString()));
                ((RequiredFieldValidator)gridEditFormItem.FindControl("rfvTaskMemberRole")).InitialValue = "-1";
                
                var item = e.Item as GridEditableItem;

                var ucOrderProduct = (OrderProductsComboBox)gridEditFormItem.FindControl("ucOrderProduct");
                ucOrderProduct.ProductId = ProductId;

                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var taskMember = (TaskMemberMap)item.DataItem;
                    if (taskMember.ContractorID.HasValue)
                        dcbContractor.SelectedId = (Guid)taskMember.ContractorID;
                    if (taskMember.OrderID.HasValue)
                    {
                        dcbOrder.SelectedId = (Guid) taskMember.OrderID;
                                                
                        ucOrderProduct.OrderId = dcbOrder.SelectedId;
                        ucOrderProduct.SelectedValue = taskMember.OrderProductsID;
                    }
                    ucContact.SelectedValue = taskMember.ContactID;
                    ddlTaskMemberRole.SelectedIndex = ddlTaskMemberRole.Items.IndexOf(ddlTaskMemberRole.Items.FindByValue(taskMember.TaskMemberRoleID.ToString()));
                    ((Literal)item.FindControl("lrlTaskMemberStatus")).Text = EnumHelper.GetEnumDescription((TaskMemberStatus)taskMember.TaskMemberStatusID);
                    ((CheckBox) item.FindControl("chxIsInformed")).Checked = taskMember.IsInformed;
                    ((TextBox)item.FindControl("txtComment")).Text = taskMember.Comment;
                    ((TextBox)item.FindControl("txtUserComment")).Text = taskMember.UserComment;

                    RefreshOrders(dcbOrder);
                }                
            }
            else if (e.Item is GridDataItem)
            {
                var taskMember = e.Item.DataItem as TaskMemberMap;
                if (taskMember != null)
                {
                    if (taskMember.ContractorID.HasValue)
                        ((Literal)e.Item.FindControl("lrlContractor")).Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Company((Guid)taskMember.ContractorID), _dataManager.Company.SelectById(SiteId, (Guid)taskMember.ContractorID).Name);

                    ((Literal)e.Item.FindControl("lrlContact")).Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact((Guid)taskMember.ContactID), _dataManager.Contact.SelectById(SiteId, (Guid)taskMember.ContactID).UserFullName);

                    if (taskMember.OrderID.HasValue)
                        ((Literal)e.Item.FindControl("lrlOrder")).Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_OrderEdit((Guid)taskMember.OrderID), _dataManager.Order.SelectById(SiteId, (Guid)taskMember.OrderID).Number);

                    if (taskMember.OrderProductsID.HasValue)
                    {
                        var orderProduct = _dataManager.OrderProducts.SelectById((Guid) taskMember.OrderProductsID);
                        ((Literal) e.Item.FindControl("lrlOrderProduct")).Text = string.Format(
                            "<a href=\"{0}\">{1}</a>", UrlsData.AP_ProductEdit(orderProduct.ProductID), orderProduct.tbl_Product.Title);
                    }
                    
                    ((Literal)e.Item.FindControl("lrlRole")).Text = EnumHelper.GetEnumDescription((TaskMemberRole)taskMember.TaskMemberRoleID);
                    ((Literal)e.Item.FindControl("lrlStatus")).Text = EnumHelper.GetEnumDescription((TaskMemberStatus)taskMember.TaskMemberStatusID);
                    ((CheckBox) e.Item.FindControl("chxIsInformed")).Checked = taskMember.IsInformed;
                }
            }
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgOrderProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgTaskMembers_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<TaskMemberMap>)ViewState["TaskMembers"]).Remove(
                ((List<TaskMemberMap>)ViewState["TaskMembers"]).Where(s => s.ID == id).FirstOrDefault());
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgTaskMembers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgTaskMembers_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgTaskMembers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgTaskMembers_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgTaskMembers.MasterTableView.IsItemInserted = false;
            rgTaskMembers.Rebind();
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnInviteMembers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnAction_OnClick(object sender, EventArgs e)
        {
            var warningCount = 0;
            var successCount = 0;
            string actionName;
            var isEmailWarning = false;
            var fio = new List<string>();

            //!!! 1 = Информирован

            CurrentStatus = int.Parse((((RadButton)sender).CommandArgument));

            switch (CurrentStatus)
            {
                case (int)TaskMemberStatus.Invited:
                    actionName = "приглашен";
                    break;
                case 1:
                    actionName = "информирован";
                    break;
                default:
                    actionName = EnumHelper.GetEnumDescription((TaskMemberStatus)CurrentStatus);
                    break;
            }

            ViewState["ActionMembers"] = new List<TaskMemberMap>();

            lrlWarningMessage.Text = string.Empty;
            lrlSuccessMessage.Text = string.Empty;

            foreach (GridDataItem item in rgTaskMembers.SelectedItems)
            {
                var taskMemberId = Guid.Parse(item.GetDataKeyValue("ID").ToString());
                var taskMember = ((List<TaskMemberMap>)ViewState["TaskMembers"]).Where(s => s.ID == taskMemberId).FirstOrDefault();
                if (taskMember == null || !taskMember.ContactID.HasValue) continue;

                var contact = _dataManager.Contact.SelectById(SiteId, (Guid)taskMember.ContactID);
                if (contact == null) continue;

                if ((CurrentStatus == (int)TaskMemberStatus.Invited || CurrentStatus == 1))
                {
                    if (!string.IsNullOrEmpty(contact.Email) && taskMember.TaskMemberStatusID == (int)TaskMemberStatus.Plan && CurrentStatus == (int)TaskMemberStatus.Invited)
                    {
                        var user = _dataManager.User.SelectByEmail(SiteId, contact.Email);

                        if (user == null || user.ContactID == contact.ID)
                        {
                            ((List<TaskMemberMap>) ViewState["ActionMembers"]).Add(taskMember);
                            successCount++;
                        }
                        else
                            fio.Add(contact.UserFullName);
                    }
                    else if (!string.IsNullOrEmpty(contact.Email) && CurrentStatus == 1)
                    {
                        var user = _dataManager.User.SelectByEmail(SiteId, contact.Email);

                        if (user == null || user.ContactID == contact.ID)
                        {
                            ((List<TaskMemberMap>)ViewState["ActionMembers"]).Add(taskMember);
                            successCount++;
                        }
                        else
                            fio.Add(contact.UserFullName);
                    }
                    else if (string.IsNullOrEmpty(contact.Email))
                        warningCount++;
                    else
                    {
                        warningCount++;
                        isEmailWarning = true;
                    }
                }
                else
                {
                    if ((CurrentStatus == (int)TaskMemberStatus.MemberConfirmed || CurrentStatus == (int)TaskMemberStatus.ParticipatedCanceled) &&
                        taskMember.TaskMemberStatusID == (int)TaskMemberStatus.Plan)
                    {
                        ((List<TaskMemberMap>)ViewState["ActionMembers"]).Add(taskMember);
                        successCount++;
                    }
                    else if ((CurrentStatus == (int)TaskMemberStatus.MemberConfirmed ||
                              CurrentStatus == (int)TaskMemberStatus.RefusedNotInterest ||
                              CurrentStatus == (int)TaskMemberStatus.RefusedFailureNoWay ||
                              CurrentStatus == (int)TaskMemberStatus.ParticipatedCanceled) &&
                             taskMember.TaskMemberStatusID == (int)TaskMemberStatus.Invited)
                    {
                        ((List<TaskMemberMap>)ViewState["ActionMembers"]).Add(taskMember);
                        successCount++;
                    }
                    else if ((CurrentStatus == (int)TaskMemberStatus.OrganizerConfirmed || CurrentStatus == (int)TaskMemberStatus.ParticipatedCanceled) &&
                             taskMember.TaskMemberStatusID == (int)TaskMemberStatus.MemberConfirmed)
                    {
                        ((List<TaskMemberMap>)ViewState["ActionMembers"]).Add(taskMember);
                        successCount++;
                    }
                    else if ((CurrentStatus == (int)TaskMemberStatus.Participated || CurrentStatus == (int)TaskMemberStatus.ParticipatedCanceled) &&
                             taskMember.TaskMemberStatusID == (int)TaskMemberStatus.InWork)
                    {
                        ((List<TaskMemberMap>)ViewState["ActionMembers"]).Add(taskMember);
                        successCount++;
                    }
                    else
                        warningCount++;
                }
            }

            if (warningCount > 0 && (CurrentStatus == (int)TaskMemberStatus.Invited || CurrentStatus == 1) && !isEmailWarning)
                lrlWarningMessage.Text = string.Format("<div class=\"warning\"><b>{0}</b> не будет {1}, так как у {2} не заполнено поле email.</div><br/>", GetMemberWithCase(warningCount), warningCount == 1 ? actionName : actionName + "о", warningCount == 1 ? "него" : "них");
            else if (warningCount > 0 && (CurrentStatus == (int)TaskMemberStatus.Invited || CurrentStatus == 1) && isEmailWarning)
                lrlWarningMessage.Text = string.Format("<div class=\"warning\"><b>{0}</b> не будет {1}, так как у {2} статус \"Приглашен\" или \"Информирован\".</div><br/>", GetMemberWithCase(warningCount), warningCount == 1 ? actionName : actionName + "о", warningCount == 1 ? "него" : "них");

            if (successCount > 0 && (CurrentStatus == (int)TaskMemberStatus.Invited || CurrentStatus == 1))
                lrlSuccessMessage.Text = string.Format("<div class=\"success\"><b>{0}</b> будет {1} после нажатия на кнопку \"Сохранить\".</div><br/>", GetMemberWithCase(successCount), successCount == 1 ? actionName : actionName + "о");

            if (successCount > 0 && CurrentStatus != (int)TaskMemberStatus.Invited && CurrentStatus != 1)
                lrlSuccessMessage.Text = string.Format("<div class=\"success\"><b>{0} участник{1}</b> будет установлен статус \"{2}\" после нажатия на кнопку \"Сохранить\".</div><br/>", successCount, successCount == 1 ? "у" : "ам", actionName);

            if (warningCount > 0 && CurrentStatus != (int)TaskMemberStatus.Invited && CurrentStatus != 1)
                lrlWarningMessage.Text = string.Format("<div class=\"warning\"><b>{0} участник{1}</b> не будет установлен статус \"{2}\".</div><br/>", warningCount, warningCount == 1 ? "у" : "ам", actionName);
            
            if (fio.Count > 0)
                lrlUserExistWarning.Text = string.Format("<div class=\"warning\">Пользовател{0} <b>{1}</b> с таким{2} email уже зарегистрирован{3}.</div><br/>", fio.Count == 1 ? "ь" : "и", string.Join(", ", fio), fio.Count == 1 ? "" : "и", fio.Count == 1 ? "" : "ы");

        }



        /// <summary>
        /// Handles the OnClick event of the rbtnSelectEventTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnSelectEventTime_OnClick(object sender, EventArgs e)
        {
            ucSelectEventTime.Task.ID = Task.ID;
            ucSelectEventTime.Task.Title = ((TextBox)Parent.FindControl("txtTitle")).Text;
            ucSelectEventTime.Task.StartDate = (DateTime)((RadDateTimePicker)Parent.FindControl("rdpStartDate")).SelectedDate;
            ucSelectEventTime.Task.EndDate = (DateTime)((RadDateTimePicker)Parent.FindControl("rdpEndDate")).SelectedDate;
            ucSelectEventTime.BindData();

            if (!Page.ClientScript.IsStartupScriptRegistered("ShowSelectEventTimeRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowSelectEventTimeRadWindow", "ShowSelectEventTimeRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnSelectMeetingTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnSelectMeetingTime_OnClick(object sender, EventArgs e)
        {
            ucSelectMeetingTime.Task.ID = Task.ID;
            ucSelectMeetingTime.Task.Title = ((TextBox)Parent.FindControl("txtTitle")).Text;
            ucSelectMeetingTime.Task.StartDate = (DateTime)((RadDateTimePicker)Parent.FindControl("rdpStartDate")).SelectedDate;
            ucSelectMeetingTime.Task.EndDate = (DateTime)((RadDateTimePicker)Parent.FindControl("rdpEndDate")).SelectedDate;

            if (((RadNumericTextBox)Parent.FindControl("rntxtPlanDurationHours")).Value.HasValue)
                ucSelectMeetingTime.Task.PlanDurationHours = (int)((RadNumericTextBox)Parent.FindControl("rntxtPlanDurationHours")).Value;

            if (((RadNumericTextBox)Parent.FindControl("rntxtPlanDurationMinutes")).Value.HasValue)
                ucSelectMeetingTime.Task.PlanDurationMinutes = (int)((RadNumericTextBox)Parent.FindControl("rntxtPlanDurationMinutes")).Value;

            ucSelectMeetingTime.BindData();

            if (!Page.ClientScript.IsStartupScriptRegistered("ShowSelectMeetingTimeRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowSelectMeetingTimeRadWindow", "ShowSelectMeetingTimeRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbOrder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbOrder_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var dcbOrder = sender as DictionaryComboBox;
            if (dcbOrder != null)
            {
                var ucOrderProduct = dcbOrder.Parent.FindControl("ucOrderProduct") as OrderProductsComboBox;

                if (ucOrderProduct != null)
                {
                    ucOrderProduct.OrderId = dcbOrder.SelectedId;
                    ucOrderProduct.SelectedValue = null;
                }
            }
        }

        #endregion

        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbContractor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbContractor_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var dcbContractor = sender as DictionaryComboBox;
            if (dcbContractor != null)
            {
                var dcbOrder = dcbContractor.Parent.FindControl("dcbOrder") as DictionaryComboBox;
                RefreshOrders(dcbOrder);
            }            
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ucContact control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucContact_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var ucContact = sender as ContactComboBox;
            if (ucContact != null)
            {
                var dcbOrder = ucContact.Parent.FindControl("dcbOrder") as DictionaryComboBox;
                RefreshOrders(dcbOrder);
            }
        }



        /// <summary>
        /// Refreshes the orders.
        /// </summary>
        /// <param name="dcbOrder">The DCB order.</param>
        protected void RefreshOrders(DictionaryComboBox dcbOrder)
        {
            dcbOrder.Filters.Clear();
            var ucContact = dcbOrder.Parent.FindControl("ucContact") as ContactComboBox;
            var dcbContractor = dcbOrder.Parent.FindControl("dcbContractor") as DictionaryComboBox;

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