using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class KnowledgeBase : LeadForceBasePage
    {
        public Guid _publicationID;
        protected tbl_Publication publicationData = null;
        protected RadAjaxManager radAjaxManager = null;


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Карточка публикации - LeadForce";

            var fsp = new FileSystemProvider();
            
            if (Page.RouteData.Values["id"] != null)
                _publicationID = Guid.Parse(Page.RouteData.Values["id"] as string);


            hlCancel.NavigateUrl = UrlsData.AP_Publications();


            if (!Page.IsPostBack)
            {
                BindData();
            }            

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(RadUpload1, rbiImage);

            radAjaxManager.AjaxSettings.AddAjaxSetting(rcbPublicationType, rcbPublicationStatus, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rcbPublicationType, ddlAccessRecord, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rcbPublicationType, ddlAccessComment, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(ddlAccessRecord, plCompany, null, UpdatePanelRenderMode.Inline);            

            tagsPublication.ObjectID = _publicationID;
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            BindPublicationAccessRecord();
            BindPublicationAccessComment();
            BindPublicationType(1);
            
            ucAuthor.ValidationGroup = ValidationSummary.ValidationGroup;            
            ucAuthor.SelectedValue = CurrentUser.Instance.ContactID;

            rdpDate.SelectedDate = DateTime.Now;

            sccPublicationCategory.SiteID = SiteId;
            sccPublicationCategory.ValidationGroup = ValidationSummary.ValidationGroup;



            publicationData = DataManager.Publication.SelectById(SiteId, _publicationID);

            RelatedPublication.SiteID = SiteId;
            PublicationTerms.SiteID = SiteId;
            PublicationComment.SiteID = SiteId;
            PublicationMark.SiteID = SiteId;

            if (publicationData != null)
            {

                txtTitle.Text = publicationData.Title;
                txtCode.Text = publicationData.Code;
                rdpDate.SelectedDate = publicationData.Date;

                ucAuthor.SelectedValue = publicationData.AuthorID;

                rcbPublicationType.SelectedValue = publicationData.PublicationTypeID.ToString();
                PopulateStatusList(rcbPublicationStatus, publicationData.PublicationTypeID.ToString());
                rcbPublicationStatus.ClearSelection();
                rcbPublicationStatus.SelectedValue = publicationData.PublicationStatusID.ToString();

                sccPublicationCategory.SelectedCategoryId = publicationData.PublicationCategoryID;

                txtNoun.Text = publicationData.Noun;
                ucHtmlEditor.Content = publicationData.Text;

                RelatedPublication.PublicationID = publicationData.ID;
                PublicationTerms.PublicationID = publicationData.ID;
                PublicationComment.PublicationID = publicationData.ID;
                PublicationMark.PublicationID = publicationData.ID;

                rbiImage.DataValue = publicationData.Img;

                if (publicationData.AccessRecord == (int)PublicationAccessRecord.Company)
                {
                    plCompany.Visible = true;
                    ucCompany.SelectedValue = publicationData.AccessCompanyID;
                }

                //if (!string.IsNullOrEmpty(publicationData.FileName))
                //{
                //    pFile.Visible = true;
                //    pUploadFile.Visible = false;
                //    txtFile.Text = publicationData.FileName;
                //    lbDeleteFile.CommandArgument = publicationData.ID.ToString();
                //}

                ddlAccessRecord.SelectedIndex =
                    ddlAccessRecord.Items.IndexOf(ddlAccessRecord.Items.FindByValue(publicationData.AccessRecord == null ? "0" : ((int)publicationData.AccessRecord).ToString()));

                ddlAccessComment.SelectedIndex =
                    ddlAccessComment.Items.IndexOf(ddlAccessComment.Items.FindByValue(publicationData.AccessComment == null ? "0" : ((int)publicationData.AccessComment).ToString()));
            }
            sccPublicationCategory.BindData();
            RelatedPublication.DataBind();
            PublicationTerms.DataBind();
        }




        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            tbl_Publication publicationData = DataManager.Publication.SelectById(SiteId, _publicationID) ?? new tbl_Publication();

            publicationData.Title = txtTitle.Text;
            publicationData.Code = txtCode.Text;
            publicationData.Date = rdpDate.SelectedDate;
            publicationData.AuthorID = (Guid)ucAuthor.SelectedValue;
            publicationData.PublicationStatusID = Guid.Parse(rcbPublicationStatus.SelectedValue);
            publicationData.PublicationCategoryID = sccPublicationCategory.SelectedCategoryId;
            publicationData.PublicationTypeID = Guid.Parse(rcbPublicationType.SelectedValue);
            publicationData.Noun = txtNoun.Text;
            publicationData.Text = ucHtmlEditor.Content;
            publicationData.SiteID = SiteId;
            publicationData.AccessRecord = int.Parse(ddlAccessRecord.SelectedValue);
            publicationData.AccessComment = int.Parse(ddlAccessComment.SelectedValue);

            if (publicationData.AccessRecord == (int)PublicationAccessRecord.Company)
                publicationData.AccessCompanyID = ucCompany.SelectedValue;
            else
                publicationData.AccessCompanyID = null;

            if (RadUpload1.UploadedFiles.Count > 0)
            {
                byte[] imageData = new byte[RadUpload1.UploadedFiles[0].InputStream.Length];
                RadUpload1.UploadedFiles[0].InputStream.Read(imageData, 0, (int)RadUpload1.UploadedFiles[0].InputStream.Length);
                publicationData.Img = imageData;
            }


            var fsp = new FileSystemProvider();

            if (!string.IsNullOrEmpty(publicationData.FileName))
                fsp.Delete(CurrentUser.Instance.SiteID, "KnowledgeBases", publicationData.FileName, FileType.Attachment);

            string fileName = null;
            if (RadUpload2.UploadedFiles.Count > 0)
                fileName = fsp.Upload(CurrentUser.Instance.SiteID, "KnowledgeBases", RadUpload2.UploadedFiles[0].FileName, RadUpload2.UploadedFiles[0].InputStream, FileType.Attachment);

            publicationData.FileName = fileName;

            if (publicationData.ID == Guid.Empty)
                DataManager.Publication.Add(publicationData);
            else
                DataManager.Publication.Update(publicationData);

            RelatedPublication.Save(publicationData.ID);
            PublicationTerms.Save(publicationData.ID);
            PublicationComment.Save(publicationData.ID);
            PublicationMark.Save(publicationData.ID);

            tagsPublication.SaveTags(publicationData.ID);

            Response.Redirect(UrlsData.AP_KnowledgeBase());

        }


        protected void AsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            Context.Cache.Remove(Session.SessionID + "UploadedFile");
            using (Stream stream = e.File.InputStream)
            {
                byte[] imgData = new byte[stream.Length];
                stream.Read(imgData, 0, (int)stream.Length);
                rbiImage.DataValue = imgData;
            }
        }

        protected void AsyncUpload2_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            pFile.Visible = true;
            pUploadFile.Visible = false;
            txtFile.Text = e.File.FileName;
        }

        protected void lbDeleteFile_Click(object sender, EventArgs e)
        {
            txtFile.Text = "";

            if (((LinkButton)sender).CommandArgument != null && ((LinkButton)sender).CommandArgument != Guid.Empty.ToString())
            {
                var fsp = new FileSystemProvider();

                if (!string.IsNullOrEmpty(((LinkButton)sender).CommandArgument))
                    fsp.Delete(CurrentUser.Instance.SiteID, "KnowledgeBases", ((LinkButton)sender).CommandArgument, FileType.Attachment);                
            }

            pFile.Visible = false;
            pUploadFile.Visible = true;
        }


        private void BindPublicationAccessRecord()
        {
            ddlAccessRecord.Items.Clear();
            foreach (var publicationAccessRecord in EnumHelper.EnumToList<PublicationAccessRecord>())
                ddlAccessRecord.Items.Add(new ListItem(EnumHelper.GetEnumDescription(publicationAccessRecord), ((int)publicationAccessRecord).ToString()));
        }



        private void BindPublicationAccessComment()
        {
            ddlAccessComment.Items.Clear();
            foreach (var publicationAccessComment in EnumHelper.EnumToList<PublicationAccessComment>())
                ddlAccessComment.Items.Add(new ListItem(EnumHelper.GetEnumDescription(publicationAccessComment), ((int)publicationAccessComment).ToString()));
        }

        private void BindPublicationType(int publicationKindID)
        {
            rcbPublicationType.Items.Clear();
            var publicationTypes = DataManager.PublicationType.SelectByPublicationKindID(SiteId, publicationKindID);
            foreach (var tblPublicationType in publicationTypes)
            {
                rcbPublicationType.Items.Add(new RadComboBoxItem(tblPublicationType.Title, tblPublicationType.ID.ToString()));
                if (tblPublicationType.IsDefault)
                {
                    rcbPublicationType.Items.FindItemByValue(tblPublicationType.ID.ToString()).Selected = true;
                    ddlAccessRecord.SelectedIndex =
                        ddlAccessRecord.Items.IndexOf(ddlAccessRecord.Items.FindByValue(tblPublicationType.PublicationAccessRecordID.ToString()));
                    ddlAccessComment.SelectedIndex =
                        ddlAccessComment.Items.IndexOf(ddlAccessComment.Items.FindByValue(tblPublicationType.PublicationAccessCommentID.ToString()));

                }
            }
            rcbPublicationType.DataBind();
            rcbPublicationType.Items.Insert(0, new RadComboBoxItem("Выберите значение", Guid.Empty.ToString()));
            PopulateStatusList(rcbPublicationStatus, rcbPublicationType.SelectedValue);

        }



        protected void PopulateStatusList(object sender, string publicationTypeId)
        {
            RadComboBox rcbPublicationStatus = (RadComboBox)sender;
            rcbPublicationStatus.Items.Clear();
            rcbPublicationStatus.Items.Insert(0, new RadComboBoxItem("Выберите значение", Guid.Empty.ToString()));
            Guid gTypeId;
            if (Guid.TryParse(publicationTypeId, out gTypeId))
            {
                var publicationStatuses = DataManager.PublicationStatus.SelectByPublicationTypeID(gTypeId);
                foreach (var publicationStatus in publicationStatuses)
                {
                    rcbPublicationStatus.Items.Add(new RadComboBoxItem(publicationStatus.Title, publicationStatus.ID.ToString()));
                    if (publicationStatus.isFirst != null && (bool)publicationStatus.isFirst)
                    {
                        rcbPublicationStatus.SelectedIndex =
                            rcbPublicationStatus.FindItemIndexByValue(publicationStatus.ID.ToString());
                        rcbPublicationStatus.SelectedValue = publicationStatus.ID.ToString();
                        rcbPublicationStatus.Text = publicationStatus.Title;
                    }
                }
                rcbPublicationStatus.DataBind();
            }
        }

        protected void rcbPublicationType_IndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateStatusList(rcbPublicationStatus, e.Value);
            Guid gPublicationTypeID;
            if (Guid.TryParse(e.Value, out gPublicationTypeID))
            {
                var publicationType = DataManager.PublicationType.SelectById(gPublicationTypeID);
                if (publicationType != null)
                {
                    ddlAccessRecord.SelectedIndex =
                        ddlAccessRecord.Items.IndexOf(
                            ddlAccessRecord.Items.FindByValue(publicationType.PublicationAccessRecordID.ToString()));
                    ddlAccessComment.SelectedIndex =
                        ddlAccessComment.Items.IndexOf(
                            ddlAccessComment.Items.FindByValue(publicationType.PublicationAccessCommentID.ToString()));
                }
            }
        }


        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlAccessRecord control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlAccessRecord_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            plCompany.Visible = int.Parse(ddlAccessRecord.SelectedValue) == (int)PublicationAccessRecord.Company;
            ucCompany.SelectedValue = plCompany.Visible ? CurrentUser.Instance.CompanyID : null;
        }
    }
}