using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.ActivityRibbon
{
    public partial class CreatePublication : System.Web.UI.UserControl
    {
        protected RadAjaxManager radAjaxManager = null;
        private readonly DataManager _dataManager = new DataManager();
        protected string PublicationLogoRootPath;

        public event PublicationAddedEventHandler PublicationAdded;
        public delegate void PublicationAddedEventHandler(object sender);
        
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SelectedValue
        {
            get
            {
                foreach (RadTab tab in rtsPublicationTypes.Tabs)
                {
                    if (tab.Selected)
                        return Guid.Parse(tab.Value);
                }

                return Guid.Empty;
            }
            set
            {
                if (value != Guid.Empty)
                {
                    foreach (RadTab tab in rtsPublicationTypes.Tabs)
                    {
                        if (Guid.Parse(tab.Value) == value)
                        {
                            tab.Selected = true;
                            UpdateTabContainer(value);
                            break;
                        }
                    }
                }
                else
                {
                    if (rtsPublicationTypes.Tabs.Count > 0)
                    {
                        rtsPublicationTypes.Tabs[0].Selected = true;
                        UpdateTabContainer(Guid.Parse(rtsPublicationTypes.Tabs[0].Value));
                    }
                }
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid PublicationCategoryId
        {            
            set
            {
                sccPublicationCategory.SelectedCategoryId = value;
                sccPublicationCategory.BindData();
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxRequest += radAjaxManager_AjaxRequest;
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, plEditPublication);
            PublicationLogoRootPath = BusinessLogicLayer.Configuration.Settings.DictionaryLogoPath(CurrentUser.Instance.SiteID, "tbl_PublicationType");
            
            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            BindPublicationAccessComment();
            BindPublicationAccessRecord();

            sccPublicationCategory.SiteID = CurrentUser.Instance.SiteID;
            rtsPublicationTypes.Tabs.Clear();
            var publicationTypes = _dataManager.PublicationType.SelectByPublicationKindID(CurrentUser.Instance.SiteID, (int)PublicationKind.Discussion).OrderBy(pt => pt.Order).ToList();
            foreach (var publicationType in publicationTypes)
            {
                rtsPublicationTypes.Tabs.Add(new RadTab() { Text = publicationType.TextAdd, ImageUrl = PublicationLogoRootPath + publicationType.Logo, Value = publicationType.ID.ToString(), ToolTip = publicationType.TextMarkToAdd});
            }

            txtComment.Text = string.Empty;
            rcbPublicationType.Text = string.Empty;
            rcbPublicationType.Items.Clear();
            rcbPublicationType.DataSource = publicationTypes;
            rcbPublicationType.DataTextField = "Title";
            rcbPublicationType.DataValueField = "ID";
            rcbPublicationType.DataBind();

            if (rtsPublicationTypes.Tabs.Count > 0)
            {
                rtsPublicationTypes.Tabs[0].Selected = true;
                txtPublicationText.EmptyMessage = publicationTypes[0].TextMarkToAdd;                
                rcbPublicationType.SelectedValue = publicationTypes[0].ToString();
            }
            
            BindPublicationStatuses();
            sccPublicationCategory.BindData();
        }



        /// <summary>
        /// Binds the publication access record.
        /// </summary>
        private void BindPublicationAccessRecord()
        {
            ddlAccessRecord.Items.Clear();
            foreach (var publicationAccessRecord in EnumHelper.EnumToList<PublicationAccessRecord>())
                ddlAccessRecord.Items.Add(new ListItem(EnumHelper.GetEnumDescription(publicationAccessRecord), ((int)publicationAccessRecord).ToString()));
        }



        /// <summary>
        /// Binds the publication access comment.
        /// </summary>
        private void BindPublicationAccessComment()
        {
            ddlAccessComment.Items.Clear();
            foreach (var publicationAccessComment in EnumHelper.EnumToList<PublicationAccessComment>())
                ddlAccessComment.Items.Add(new ListItem(EnumHelper.GetEnumDescription(publicationAccessComment), ((int)publicationAccessComment).ToString()));
        }



        /// <summary>
        /// Handles the AjaxRequest event of the radAjaxManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void radAjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument != "Init")
            {
                var publicationId = Guid.Empty;
                if (Guid.TryParse(e.Argument, out publicationId))
                {
                    var publication = _dataManager.Publication.SelectById(CurrentUser.Instance.SiteID, publicationId);
                    if (publication != null)
                    {
                        txtTitle.Text = publication.Title;
                        txtComment.Text = publication.Text;
                        rcbPublicationType.SelectedValue = publication.PublicationTypeID.ToString();
                        if (rcbPublicationType.Items.FindItemByValue(publication.PublicationTypeID.ToString()) != null)
                            rcbPublicationType.Text = rcbPublicationType.Items.FindItemByValue(publication.PublicationTypeID.ToString()).Text;
                        BindPublicationStatuses(publication.PublicationTypeID);
                        rcbPublicationStatus.SelectedValue = publication.PublicationStatusID.ToString();
                        sccPublicationCategory.SelectedCategoryId = publication.PublicationCategoryID;
                        sccPublicationCategory.BindData();

                        if (publication.AccessRecord == (int)PublicationAccessRecord.Company)
                        {
                            plCompany.Visible = true;
                            ucCompany.SelectedValue = publication.AccessCompanyID;
                        }

                        ddlAccessRecord.SelectedIndex =
                            ddlAccessRecord.Items.IndexOf(ddlAccessRecord.Items.FindByValue(publication.AccessRecord == null ? "0" : ((int)publication.AccessRecord).ToString()));

                        ddlAccessComment.SelectedIndex =
                            ddlAccessComment.Items.IndexOf(ddlAccessComment.Items.FindByValue(publication.AccessComment == null ? "0" : ((int)publication.AccessComment).ToString()));

                        lbtnAddPublication.CommandArgument = e.Argument;                        
                    }
                }
            }
            else
            {                
                txtTitle.Text = txtPublicationText.Text;
                txtComment.Text = string.Empty;
                UpdateTabContainer(SelectedValue);
                sccPublicationCategory.CategoryId = Guid.Empty;
                sccPublicationCategory.BindData();
                lbtnAddPublication.CommandArgument = string.Empty;
            }

            var jsShow = "$find('" + addPublicationRadWindow.ClientID + "').show();$('.rwTitlebarControls td em').html($('#update-buttons a.btn span').html());";
            if (!Page.ClientScript.IsStartupScriptRegistered("ShowAddPublicationPopup"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowAddPublicationPopup", jsShow, true);
        }



        /// <summary>
        /// Binds the publication statuses.
        /// </summary>
        private void BindPublicationStatuses(Guid? publicationTypeId = null)
        {
            rcbPublicationStatus.Text = string.Empty;
            rcbPublicationStatus.Items.Clear();
            rcbPublicationStatus.Items.Insert(0, new RadComboBoxItem("Выберите значение", Guid.Empty.ToString()));

            var publicationStatuses = _dataManager.PublicationStatus.SelectByPublicationTypeID(publicationTypeId.HasValue ? (Guid)publicationTypeId : SelectedValue);
            foreach (var publicationStatus in publicationStatuses)
            {
                var rcbItem = new RadComboBoxItem(publicationStatus.Title, publicationStatus.ID.ToString());
                if (publicationStatus.isFirst.HasValue && (bool)publicationStatus.isFirst)
                    rcbItem.Selected = true;
                rcbPublicationStatus.Items.Add(rcbItem);
            }
            rcbPublicationStatus.DataBind();

            var publicationType = _dataManager.PublicationType.SelectById(publicationTypeId.HasValue ? (Guid)publicationTypeId : SelectedValue);
            if (publicationType != null)
            {
                lbtnAddPublication.Text = string.Format("<em>&nbsp;</em><span>{0}</span>", publicationType.TextAdd);
                addPublicationRadWindow.Title = publicationType.TextAdd;
            }
        }



        /// <summary>
        /// Updates the tab container.
        /// </summary>
        /// <param name="publicationTypeId">The publication type id.</param>
        private void UpdateTabContainer(Guid publicationTypeId)
        {
            var publicationTypes = _dataManager.PublicationType.SelectById(publicationTypeId);            
            txtPublicationText.EmptyMessage = publicationTypes.TextMarkToAdd;           
            rcbPublicationType.SelectedValue = publicationTypeId.ToString();
            if (rcbPublicationType.Items.FindItemByValue(publicationTypeId.ToString()) != null)
                rcbPublicationType.Text = rcbPublicationType.Items.FindItemByValue(publicationTypeId.ToString()).Text;
            BindPublicationStatuses();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnAddPublication control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnAddPublication_OnClick(object sender, EventArgs e)
        {            
            var publicationId = !string.IsNullOrEmpty(lbtnAddPublication.CommandArgument)
                                    ? Guid.Parse(lbtnAddPublication.CommandArgument)
                                    : Guid.Empty;

            var publication = _dataManager.Publication.SelectById(CurrentUser.Instance.SiteID, publicationId) ?? new tbl_Publication();
            publication.Title = txtTitle.Text;
            publication.Text = txtComment.Text;
            publication.PublicationStatusID = Guid.Parse(rcbPublicationStatus.SelectedValue);
            publication.PublicationTypeID = Guid.Parse(rcbPublicationType.SelectedValue);
            publication.PublicationCategoryID = sccPublicationCategory.SelectedCategoryId;

            publication.AccessRecord = int.Parse(ddlAccessRecord.SelectedValue);
            publication.AccessComment = int.Parse(ddlAccessComment.SelectedValue);

            if (publication.AccessRecord == (int)PublicationAccessRecord.Company)
                publication.AccessCompanyID = ucCompany.SelectedValue;
            else
                publication.AccessCompanyID = null;

            var fsp = new FileSystemProvider();                        
            if (rauFile.UploadedFiles.Count > 0)
            {
                if (!string.IsNullOrEmpty(publication.FileName))
                    fsp.Delete(CurrentUser.Instance.SiteID, "Publications", publication.FileName, FileType.Attachment);

                publication.FileName = fsp.Upload(CurrentUser.Instance.SiteID, "Publications", rauFile.UploadedFiles[0].FileName, rauFile.UploadedFiles[0].InputStream, FileType.Attachment);
            }            

            if (publicationId == Guid.Empty)
            {
                publication.SiteID = CurrentUser.Instance.SiteID;
                publication.Date = DateTime.Now;
                publication.AuthorID = (Guid) CurrentUser.Instance.ContactID;
                publication.OwnerID = CurrentUser.Instance.ContactID;
                _dataManager.Publication.Add(publication);

                if (PublicationAdded != null)
                    PublicationAdded(this);

                if (!Page.ClientScript.IsStartupScriptRegistered("CloseAddPublicationRadWindow"))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseAddPublicationRadWindow", "CloseAddPublicationRadWindow(true);", true);
            }
            else
            {
                _dataManager.Publication.Update(publication);
                if (!Page.ClientScript.IsStartupScriptRegistered("CloseUpdatePublicationRadWindow"))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseUpdatePublicationRadWindow", "CloseUpdatePublicationRadWindow('"+ publicationId +"');", true);
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the rcbPublicationType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void rcbPublicationType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindPublicationStatuses(Guid.Parse(rcbPublicationType.SelectedValue));
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