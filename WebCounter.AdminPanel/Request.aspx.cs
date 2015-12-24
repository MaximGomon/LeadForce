using System;
using System.ComponentModel;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.DocumentManagement;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class Request : LeadForceBasePage
    {
        private Guid _requestId;
        public Access access;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public RequestStatus CurrentStatus
        {
            get
            {
                if (ViewState["CurrentStatus"] == null)
                    ViewState["CurrentStatus"] = RequestStatus.New;
                return (RequestStatus)ViewState["CurrentStatus"];
            }
            set
            {
                ViewState["CurrentStatus"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            dprReminder.RegisterSkipCheckingTrigger(lbtnSave);
            dprReminder.RegisterSkipCheckingTrigger(lbtnCreateRequirement);            
            dprReminder.RegisterSkipCheckingTrigger(ucContentComments.TextBoxEditor);
            dprReminder.RegisterSkipCheckingTrigger(ucContentComments.HtmlEditor);            
            
            access = Access.Check();
            if (!access.Write)
                lbtnSave.Visible = false;

            Title = "Запрос - LeadForce";
            
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbServiceLevel, dcbServiceLevel, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbServiceLevel, rdpReactionDatePlanned, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbRequestSource, ucLongDescription, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbRequestSource, lrlSourceFiles, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbCompany, dcbServiceLevel, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbCompany, ucAssignToRequiremts, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbCompany, ucRegisterComment, null, UpdatePanelRenderMode.Inline);

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rbtnClose, rdpReactionDateActual, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rbtnDuplicate, rdpReactionDateActual, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rbtnInWork, rdpReactionDateActual, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucAssignToRequiremts, gridRequirements, null, UpdatePanelRenderMode.Inline);

            ucAssignToRequiremts.RequirementsAssigned += ucAssignToRequiremts_RequirementsAssigned;

            if (Page.RouteData.Values["id"] != null)
                _requestId = Guid.Parse(Page.RouteData.Values["id"] as string);

            gridRequirements.SiteID = SiteId;            
            gridRequirements.Where.Add(new GridWhere()
                                           {
                                               CustomQuery =
                                                   string.Format(
                                                       "(tbl_Requirement.RequestID = '{0}' " +
                                                       "OR tbl_Requirement.ID IN " +
                                                       "(SELECT RequirementID FROM tbl_RequestToRequirement WHERE RequestID = '{0}'))",
                                                       _requestId.ToString())
                                           });

            gridRequirements.Actions.Add(new GridAction { Text = "Карточка требования", NavigateUrl = string.Format("~/{0}/Requirements/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });

            gridRequestHistory.Where.Add(new GridWhere() { Column = "RequestID", Value = _requestId.ToString() });

            hlCancel.NavigateUrl = UrlsData.AP_Requests();

            ucContentComments.ContentId = _requestId;

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Ucs the assign to requiremts_ requirements assigned.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void ucAssignToRequiremts_RequirementsAssigned(object sender)
        {
            gridRequirements.Rebind();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {            
            dcbServiceLevel.SiteID = dcbProducts.SiteID = 
            dcbCompany.SiteID = dcbRequestSource.SiteID = dcbRequestSourceType.SiteID = SiteId;

            dcbServiceLevel.Filters.Clear();
            dcbServiceLevel.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                            {
                                                Name = "IsActive",
                                                DbType = DbType.Boolean,
                                                Value = "TRUE"
                                            });
                        
            rdpCreatedAt.SelectedDate = DateTime.Now;

            if (CurrentUser.Instance.CompanyID.HasValue)
            {
                dcbCompany.SelectedId = (Guid)CurrentUser.Instance.CompanyID;
                dcbCompany.SelectedText = DataManager.Company.SelectById(SiteId, (Guid)CurrentUser.Instance.CompanyID).Name;
            }

            ucResponsible.SelectedValue = CurrentUser.Instance.ContactID;            

            var request = DataManager.Request.SelectById(SiteId, _requestId);
            if (request != null)
            {
                plComments.Visible = true;

                CurrentStatus = (RequestStatus) request.RequestStatusID;
                lbtnCreateRequirement.Visible = true;
                lrlNumber.Text = request.Number;
                rdpCreatedAt.SelectedDate = request.CreatedAt;
                if (request.RequestSourceTypeID.HasValue)
                {
                    dcbRequestSourceType.SelectedId = (Guid) request.RequestSourceTypeID;
                    dcbRequestSource.Enabled = true;
                }

                UpdateRequestSource(request.RequestSourceID, request.RequestSourceTypeID);                

                if (request.ProductID.HasValue)
                {
                    dcbProducts.SelectedId = (Guid)request.ProductID;
                    dcbProducts.SelectedText = request.tbl_Product.Title;
                }
                if (request.CompanyID.HasValue)
                {
                    dcbCompany.SelectedId = (Guid)request.CompanyID;
                    dcbCompany.SelectedText = request.tbl_Company.Name;
                }

                ucContact.SelectedValue = request.ContactID;
                txtProductSeriesNumber.Text = request.ProductSeriesNumber;                
                ucResponsible.SelectedValue = request.ResponsibleID;

                if (request.ServiceLevelID.HasValue)
                {
                    dcbServiceLevel.SelectedIdNullable = request.ServiceLevelID;
                    dcbServiceLevel.SelectedText = request.tbl_ServiceLevel.Title;
                }                

                rdpReactionDatePlanned.SelectedDate = request.ReactionDatePlanned;
                rdpReactionDateActual.SelectedDate = request.ReactionDateActual;

                txtShortDescription.Text = request.ShortDescription;
                ucLongDescription.Content = request.LongDescription;

                var requestFiles = DataManager.RequestFile.SelectByRequestId(request.ID);
                if (requestFiles.Any())
                {
                    var fsp = new FileSystemProvider();
                    var sb = new StringBuilder();
                    foreach (var requestFile in requestFiles)
                    {
                        sb.Append(string.Format("<a href='{0}' target='_blank'>{1}</a> ",
                                                                fsp.GetLink(SiteId, "Requests", requestFile.FileName, FileType.Attachment),
                                                                requestFile.FileName));
                    }

                    lrlRequestFiles.Text = sb.ToString();
                }                
                ucAssignToRequiremts.RequestId = request.ID;
                ucRegisterComment.RequestId = request.ID;
            }

            ucAssignToRequiremts.CompanyId = dcbCompany.SelectedIdNullable;
            ucRegisterComment.CompanyId = dcbCompany.SelectedIdNullable;
            
            RefreshStatusButtons();
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbCompany control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbCompany_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ucContact.SelectedValue = null;
            ucContact.CompanyId = null;

            ucAssignToRequiremts.CompanyId = dcbCompany.SelectedIdNullable;
            ucAssignToRequiremts.BindData(true);

            ucRegisterComment.CompanyId = dcbCompany.SelectedIdNullable;
            ucRegisterComment.BindData(true);

            if (dcbCompany.SelectedId != Guid.Empty)
            {
                ucContact.CompanyId = dcbCompany.SelectedId;

                var serviceLevel = DataManager.ServiceLevel.SelectByCompanyIdOrDefault(SiteId, dcbCompany.SelectedId);
                if (serviceLevel != null)
                {
                    dcbServiceLevel.SelectedId = serviceLevel.ID;
                    dcbServiceLevel.SelectedText = serviceLevel.Title;
                }
                else
                {
                    dcbServiceLevel.SelectedIdNullable = null;
                    dcbServiceLevel.SelectedText = string.Empty;
                }
            }            
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            if (!access.Write)
                return;

            var request = DataManager.Request.SelectById(SiteId, _requestId) ?? new tbl_Request();

            request.RequestSourceTypeID = dcbRequestSourceType.SelectedIdNullable;            
            request.RequestSourceID = dcbRequestSource.SelectedIdNullable;
            request.CompanyID = dcbCompany.SelectedIdNullable;            
            request.ContactID = ucContact.SelectedValue;
            request.ProductID = dcbProducts.SelectedIdNullable;
            request.ProductSeriesNumber = txtProductSeriesNumber.Text;            
            
            request.RequestStatusID = (int)CurrentStatus;
            request.ResponsibleID = ucResponsible.SelectedValue;
            request.ServiceLevelID = dcbServiceLevel.SelectedId;            
            request.ShortDescription = txtShortDescription.Text;
            request.LongDescription = ucLongDescription.Content;

            switch (CurrentStatus)
            {
                case RequestStatus.InWork:
                case RequestStatus.Closed:
                    if (!request.ReactionDateActual.HasValue)
                        request.ReactionDateActual = DateTime.Now;
                    break;
            }

            var serviceLevel = DataManager.ServiceLevel.SelectById(SiteId, (Guid)request.ServiceLevelID);

            if (request.ID == Guid.Empty)
            {
                request.SiteID = SiteId;
                request.OwnerID = CurrentUser.Instance.ContactID;
                request.CreatedAt = DateTime.Now;
                request.ReactionDatePlanned = request.CreatedAt.AddHours(serviceLevel.ReactionTime);

                var requestSourceType = DataManager.RequestSourceType.SelectById(SiteId, (Guid)request.RequestSourceTypeID);
                if (requestSourceType != null)
                {
                    var documentNumerator = DocumentNumerator.GetNumber((Guid)requestSourceType.NumeratorID, request.CreatedAt, requestSourceType.tbl_Numerator.Mask, "tbl_Request");
                    request.Number = documentNumerator.Number;
                    request.SerialNumber = documentNumerator.SerialNumber;
                }

                var requirements = DataManager.Requirement.SelectAll(SiteId).Where(o => ucAssignToRequiremts.SelectedRequirments.Contains(o.ID));
                foreach (var requirement in requirements)
                    request.tbl_Requirement.Add(requirement);

                DataManager.Request.Add(request);

                ucRegisterComment.SaveComment(request);
            }
            else
            {
                request.ReactionDatePlanned = request.CreatedAt.AddHours(serviceLevel.ReactionTime);
                
                DataManager.Request.Update(request);
            }

            if (rauRequestFiles.UploadedFiles.Count > 0)
            {
                var fsp = new FileSystemProvider();
                foreach (UploadedFile file in rauRequestFiles.UploadedFiles)
                {
                    var fileName = fsp.Upload(SiteId, "Requests", file.FileName, file.InputStream, FileType.Attachment);
                    DataManager.RequestFile.Add(new tbl_RequestFile() {RequestID = request.ID, FileName = fileName});
                }
            }

            Response.Redirect(UrlsData.AP_Requests());
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbServiceLevel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbServiceLevel_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (dcbServiceLevel.SelectedIdNullable.HasValue)
            {
                var serviceLevel = DataManager.ServiceLevel.SelectById(SiteId, dcbServiceLevel.SelectedId);
                if (serviceLevel != null && rdpCreatedAt.SelectedDate.HasValue)                
                    rdpReactionDatePlanned.SelectedDate = rdpCreatedAt.SelectedDate.Value.AddHours(serviceLevel.ReactionTime);                
            }
            else
            {
                rdpReactionDatePlanned.SelectedDate = null;
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbRequestSourceType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbRequestSourceType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            UpdateRequestSource();
        }




        /// <summary>
        /// Updates the request source.
        /// </summary>
        /// <param name="requestSourceId">The request source id.</param>
        /// <param name="requestSourceTypeId">The request source type id.</param>
        private void UpdateRequestSource(Guid? requestSourceId = null, Guid? requestSourceTypeId = null)
        {            
            dcbRequestSource.Filters.Clear();
            tbl_RequestSourceType requestSourceType = null;
            if (!requestSourceTypeId.HasValue)
                requestSourceType = DataManager.RequestSourceType.SelectById(SiteId, dcbRequestSourceType.SelectedId);
            else
                requestSourceType = DataManager.RequestSourceType.SelectById(SiteId, (Guid)requestSourceTypeId);

            UpdateRequestSourceFiles(requestSourceId, requestSourceTypeId);

            if (requestSourceType != null)
            {
                switch ((RequestSourceCategory)requestSourceType.RequestSourceCategoryID)
                {
                    case RequestSourceCategory.Message:
                        dcbRequestSource.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                        {
                            Name = "DirectionID",
                            DbType = DbType.Int32,
                            Value = ((int)Direction.In).ToString()
                        });
                        dcbRequestSource.DictionaryName = "tbl_SiteAction";
                        dcbRequestSource.DataTextField = "MessageTitle";
                        dcbRequestSource.DataValueField = "ID";
                        
                        break;
                    case RequestSourceCategory.Request:
                        dcbRequestSource.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                         {
                                                             Name = "tbl_PublicationType.PublicationKindID",
                                                             DbType = DbType.Int32,
                                                             Value = ((int)PublicationKind.Discussion).ToString()
                                                         });
                        
                        dcbRequestSource.DictionaryName = "tbl_Publication";
                        dcbRequestSource.DataTextField = "Title";
                        dcbRequestSource.DataValueField = "ID";
                        
                        break;
                    default:
                        dcbRequestSource.DictionaryName = null;
                        dcbRequestSource.DataTextField = null;
                        dcbRequestSource.DataValueField = null;
                        break;
                }
            }
            else
            {
                dcbRequestSource.DictionaryName = null;
                dcbRequestSource.DataTextField = null;
                dcbRequestSource.DataValueField = null;
            }

            dcbRequestSource.InitDataSource();
        }



        /// <summary>
        /// Updates the request source files.
        /// </summary>
        /// <param name="requestSourceId">The request source id.</param>
        /// <param name="requestSourceTypeId">The request source type id.</param>
        private void UpdateRequestSourceFiles(Guid? requestSourceId = null, Guid? requestSourceTypeId = null)
        {
            tbl_RequestSourceType requestSourceType = null;
            if (!requestSourceTypeId.HasValue)
                requestSourceType = DataManager.RequestSourceType.SelectById(SiteId, dcbRequestSourceType.SelectedId);
            else
                requestSourceType = DataManager.RequestSourceType.SelectById(SiteId, (Guid) requestSourceTypeId);

            if (requestSourceType != null)
            {
                switch ((RequestSourceCategory) requestSourceType.RequestSourceCategoryID)
                {
                    case RequestSourceCategory.Message:
                        if (requestSourceId.HasValue)
                        {
                            var siteAction = DataManager.SiteAction.SelectById(SiteId, (Guid)requestSourceId);
                            if (siteAction != null)
                            {
                                dcbRequestSource.SelectedId = siteAction.ID;
                                dcbRequestSource.SelectedText = siteAction.MessageTitle;

                                var attachments = siteAction.tbl_SiteActionAttachment;
                                if (attachments.Count > 0)
                                {
                                    var sb = new StringBuilder();
                                    var fsp = new FileSystemProvider();
                                    foreach (tbl_SiteActionAttachment attachment in attachments)
                                    {
                                        sb.Append(string.Format("<a href='{0}' target='_blank'>{1}</a> ",
                                                                fsp.GetLink(SiteId, "POP3SourceMonitorings", attachment.FileName, FileType.Attachment),
                                                                attachment.FileName));
                                    }

                                    lrlSourceFiles.Text = sb.ToString();
                                }
                            }
                        }
                        break;
                    case RequestSourceCategory.Request:
                        if (requestSourceId.HasValue)
                        {
                            var publication = DataManager.Publication.SelectById(SiteId, (Guid)requestSourceId);
                            if (publication != null)
                            {
                                dcbRequestSource.SelectedId = publication.ID;
                                dcbRequestSource.SelectedText = publication.Title;

                                if (!string.IsNullOrEmpty(publication.FileName))
                                {
                                    var fsp = new FileSystemProvider();
                                    lrlSourceFiles.Text = string.Format("<a href='{0}' target='_blank'>{1}</a> ",
                                                                  fsp.GetLink(SiteId, "Publications",
                                                                              publication.FileName, FileType.Attachment),
                                                                  publication.FileName);
                                }
                            }
                        }
                        break;
                }
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbRequestSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbRequestSource_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var requestSourceType = DataManager.RequestSourceType.SelectById(SiteId, dcbRequestSourceType.SelectedId);
            if (requestSourceType != null && dcbRequestSource.SelectedId != Guid.Empty)
            {
                switch ((RequestSourceCategory)requestSourceType.RequestSourceCategoryID)
                {
                    case RequestSourceCategory.Message:
                        var siteAction = DataManager.SiteAction.SelectById(SiteId, dcbRequestSource.SelectedId);
                        if (siteAction != null)
                            ucLongDescription.Content = siteAction.MessageText;                         
                        break;
                    case RequestSourceCategory.Request:
                        var publication = DataManager.Publication.SelectById(SiteId, dcbRequestSource.SelectedId);
                        if (publication != null)
                            ucLongDescription.Content = publication.Text;                        
                        break;
                    default:
                        ucLongDescription.Content = string.Empty;
                        break;
                }
            }
            else
                ucLongDescription.Content = string.Empty;

            UpdateRequestSourceFiles(dcbRequestSource.SelectedIdNullable, dcbRequestSourceType.SelectedIdNullable);
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnCreateRequirement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnCreateRequirement_OnClick(object sender, EventArgs e)
        {
            Session["Selected-Text"] = hfSelection.Value;

            if (_requestId != Guid.Empty)
                Session["Source-RequestId"] = _requestId;

            Response.Write(string.Format("<script>window.open('{0}','_blank');</script>", UrlsData.AP_RequirementAdd()));
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridRequirements control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridRequirements_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var lrlCompanyName = (Literal)item.FindControl("lrlCompanyName");
                var lrlUserFullName = (Literal)item.FindControl("lrlUserFullName");                
                
                if (!string.IsNullOrEmpty(data["tbl_Company_Name"].ToString()))
                    lrlCompanyName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Company(Guid.Parse(data["tbl_Company_ID"].ToString())), data["tbl_Company_Name"]);

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lrlUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), data["tbl_Contact_UserFullName"]);
            }
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnStatus_OnClick(object sender, EventArgs e)
        {
            var rbtnStatus = (RadButton) sender;
            CurrentStatus = (RequestStatus)Enum.Parse(typeof(RequestStatus), rbtnStatus.CommandArgument);

            switch (CurrentStatus)
            {
                case RequestStatus.InWork:
                case RequestStatus.Closed:
                    if (!ucResponsible.SelectedValue.HasValue)
                        ucResponsible.SelectedValue = CurrentUser.Instance.ContactID;

                    if (!rdpReactionDateActual.SelectedDate.HasValue)
                        rdpReactionDateActual.SelectedDate = DateTime.Now;

                    break;
            }

            RefreshStatusButtons();

            if (_requestId != Guid.Empty)
            {
                var request = DataManager.Request.SelectById(SiteId, _requestId);
                if (request != null)
                {                    
                    request.RequestStatusID = (int) CurrentStatus;
                    switch (CurrentStatus)
                    {
                        case RequestStatus.InWork:
                        case RequestStatus.Closed:
                            if (!request.ReactionDateActual.HasValue)
                                request.ReactionDateActual = DateTime.Now;
                            break;
                    }

                    DataManager.Request.Update(request);
                }            
            }
        }



        /// <summary>
        /// Refreshes the status button.
        /// </summary>
        protected void RefreshStatusButtons()
        {
            rbtnClose.Visible = rbtnDuplicate.Visible = rbtnInWork.Visible = false;

            switch (CurrentStatus)
            {
                case RequestStatus.InWork:
                    rbtnClose.Visible = true;
                    break;
                case RequestStatus.New:
                    rbtnClose.Visible = rbtnDuplicate.Visible = rbtnInWork.Visible = true;
                    break;
            }

            lrlRequestStatus.Text = EnumHelper.GetEnumDescription(CurrentStatus);
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ucContact control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucContact_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (ucContact.SelectedValue.HasValue)
            {
                var contact = DataManager.Contact.SelectById(CurrentUser.Instance.SiteID, (Guid) ucContact.SelectedValue);
                if (contact != null && contact.CompanyID.HasValue)
                {
                    var company = DataManager.Company.SelectById(CurrentUser.Instance.SiteID, (Guid) contact.CompanyID);
                    dcbCompany.SelectedIdNullable = contact.CompanyID;
                    dcbCompany.SelectedText = company.Name;
                }
            }
        }


        protected void gridRequestHistory_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var lrlUserFullName = (Literal)item.FindControl("lrlUserFullName");
                var lrlResponsibleUserFullName = (Literal)item.FindControl("lrlResponsibleUserFullName");

                ((Literal)item.FindControl("lrlRequestStatus")).Text = EnumHelper.GetEnumDescription((RequestStatus)int.Parse(data["tbl_RequestHistory_RequestStatusID"].ToString()));

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lrlUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), data["tbl_Contact_UserFullName"]);

                if (!string.IsNullOrEmpty(data["c1_UserFullName"].ToString()))
                    lrlResponsibleUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["c1_ID"].ToString())), data["c1_UserFullName"]);
            }
        }

        protected void rbtnAddFile_OnClick(object sender, EventArgs e)
        {
            rbtnAddFile.Visible = false;
            rauRequestFiles.Visible = true;
        }
    }
}