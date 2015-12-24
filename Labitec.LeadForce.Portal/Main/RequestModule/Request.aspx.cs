using System;
using System.Linq;
using System.Text;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.DocumentManagement;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Portal.Main.RequestModule
{
    public partial class Request : LeadForcePortalBasePage
    {        
        protected tbl_ServiceLevel ServiceLevel;
        protected tbl_Contact Contact;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            var accessCheck = Access.Check(TblUser, "Requests");
            if (!accessCheck.Read)
                Response.Redirect(UrlsData.LFP_AccessDenied(PortalSettingsId));

            Title = lrlTitle.Text = "Новый запрос";

            ucContentComments.ContentId = ObjectId;

            gridRequirements.SiteID = SiteId;            
            gridRequirements.Where.Add(new GridWhere()
            {
                CustomQuery =
                    string.Format(
                        "(tbl_Requirement.RequestID = '{0}' " +
                        "OR tbl_Requirement.ID IN " +
                        "(SELECT RequirementID FROM tbl_RequestToRequirement WHERE RequestID = '{0}'))",
                        ObjectId.ToString())
            });

            if (ObjectId != Guid.Empty)
            {
                plViewRequest.Visible = true;                
            }
            else
            {
                Contact = DataManager.Contact.SelectById(SiteId, (Guid)CurrentUser.Instance.ContactID);
                ServiceLevel = DataManager.ServiceLevel.SelectForContact(SiteId, Contact);

                if (ServiceLevel != null)
                {
                    plAddRequest.Visible = true;
                    hlCancel.NavigateUrl = UrlsData.LFP_Requests(PortalSettingsId);
                }
                else
                    ucNotificationMessage.Text = "Не определен уровень обслуживания.";
            }

            gridRequirements.Actions.Add(new GridAction { Text = "Карточка требования", NavigateUrl = "~/" + PortalSettingsId + "/Main/Requirements/Edit/{0}", ImageUrl = "~/App_Themes/Default/images/icoView.png" });

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (ObjectId != Guid.Empty)
            {
                var request = DataManager.Request.SelectById(SiteId, ObjectId);
                if (request != null)
                {
                    CheckReadAccess(request.OwnerID, "Requests");

                    Title =
                        lrlTitle.Text =
                        string.Format("Запрос №{0} от {1}", request.Number,
                                      request.CreatedAt.ToString("dd.MM.yyyy"));
                    lrlStatus.Text = string.Format("Состояние: {0}",
                                                   EnumHelper.GetEnumDescription((RequestStatus) request.RequestStatusID));

                    lrlShortDescription.Text = string.Format("<div class='text-container'>{0}</div>", request.ShortDescription);
                    lrlLongDescription.Text = string.Format("<div class='text-container'>{0}</div>", request.LongDescription);

                    if (request.ContactID.HasValue)
                    {
                        var contact = DataManager.Contact.SelectById(SiteId, (Guid) request.ContactID);
                        if (contact != null)
                            lrlContact.Text = contact.UserFullName;
                    }

                    if (request.ResponsibleID.HasValue)
                    {
                        var responsible = DataManager.Contact.SelectById(SiteId, (Guid) request.ResponsibleID);
                        if (responsible != null)
                            lrlResponsible.Text = responsible.UserFullName;
                    }

                    if (request.ReactionDateActual.HasValue)
                        lrlReactionDateActual.Text = request.ReactionDateActual.Value.ToString("dd.MM.yyyy hh:mm");

                    if (request.ReactionDatePlanned.HasValue)
                        lrlReactionDatePlanned.Text = request.ReactionDatePlanned.Value.ToString("dd.MM.yyyy hh:mm");

                    if (request.RequestSourceID.HasValue && request.RequestSourceTypeID.HasValue)
                        UpdateRequestSourceFiles((Guid) request.RequestSourceID, (Guid) request.RequestSourceTypeID);

                    var requestFiles = DataManager.RequestFile.SelectByRequestId(request.ID);
                    if (requestFiles.Any())
                    {
                        var fsp = new FileSystemProvider();
                        var sb = new StringBuilder();
                        foreach (var requestFile in requestFiles)
                        {
                            sb.Append(string.Format("<a href='{0}' target='_blank'>{1}</a> ",
                                                    fsp.GetLink(SiteId, "Requests", requestFile.FileName,
                                                                FileType.Attachment),
                                                    requestFile.FileName));
                        }

                        lrlRequestFiles.Text = sb.ToString();
                    }
                }
            }            
        }



        /// <summary>
        /// Updates the request source files.
        /// </summary>
        /// <param name="requestSourceId">The request source id.</param>
        /// <param name="requestSourceTypeId">The request source type id.</param>
        private void UpdateRequestSourceFiles(Guid requestSourceId, Guid requestSourceTypeId)
        {
            tbl_RequestSourceType requestSourceType = null;
            requestSourceType = DataManager.RequestSourceType.SelectById(SiteId, requestSourceTypeId);

            if (requestSourceType != null)
            {
                switch ((RequestSourceCategory)requestSourceType.RequestSourceCategoryID)
                {
                    case RequestSourceCategory.Message:                        
                            var siteAction = DataManager.SiteAction.SelectById(SiteId, (Guid)requestSourceId);
                            if (siteAction != null)
                            {                             
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
                        break;
                    case RequestSourceCategory.Request:                        
                            var publication = DataManager.Publication.SelectById(SiteId, (Guid)requestSourceId);
                            if (publication != null)
                            {
                                if (!string.IsNullOrEmpty(publication.FileName))
                                {
                                    var fsp = new FileSystemProvider();
                                    lrlSourceFiles.Text = string.Format("<a href='{0}' target='_blank'>{1}</a> ",
                                                                  fsp.GetLink(SiteId, "Publications",
                                                                              publication.FileName, FileType.Attachment),
                                                                  publication.FileName);
                                }
                            }                        
                        break;
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
            var accessCheck = Access.Check(TblUser, "Requests");
            if (!accessCheck.Write)
                Response.Redirect(UrlsData.LFP_AccessDenied(PortalSettingsId));

            var requestSourceType = DataManager.RequestSourceType.SelectBySourceCategoryId(SiteId, RequestSourceCategory.Request);

            if (requestSourceType == null)
            {
                ucNotificationMessage.Text = "Запрос не зарегистрирован. В справочнике отсутствует тип запроса с категорией \"Обращение\"";
                return;                
            }

            var request = new tbl_Request
            {
                SiteID = SiteId,
                CreatedAt = DateTime.Now,
                ServiceLevelID = ServiceLevel.ID,
                ContactID = Contact.ID,
                CompanyID = Contact.CompanyID,                
                RequestStatusID = (int)RequestStatus.New,
                RequestSourceTypeID = requestSourceType.ID,
                ShortDescription = txtShortDescription.Text,
                LongDescription = ucLongDescription.Content,
                ReactionDateActual = null,                
            };

            request.ReactionDatePlanned = request.CreatedAt.AddHours(ServiceLevel.ReactionTime);
            
            
            var documentNumerator = DocumentNumerator.GetNumber((Guid)requestSourceType.NumeratorID,
                                                                request.CreatedAt,
                                                                requestSourceType.tbl_Numerator.Mask, "tbl_Request");
            request.Number = documentNumerator.Number;
            request.SerialNumber = documentNumerator.SerialNumber;
            

            DataManager.Request.Add(request);

            if (rauRequestFiles.UploadedFiles.Count > 0)
            {
                var fsp = new FileSystemProvider();
                foreach (UploadedFile file in rauRequestFiles.UploadedFiles)
                {
                    var fileName = fsp.Upload(SiteId, "Requests", file.FileName, file.InputStream, FileType.Attachment);
                    DataManager.RequestFile.Add(new tbl_RequestFile() { RequestID = request.ID, FileName = fileName });
                }
            }

            Response.Redirect(UrlsData.LFP_Requests(PortalSettingsId));
        }
    }
}