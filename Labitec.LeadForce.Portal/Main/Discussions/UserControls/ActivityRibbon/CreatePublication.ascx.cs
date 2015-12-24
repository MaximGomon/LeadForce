using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon
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

                return InternalSelectedValue ?? Guid.Empty;
            }
            set
            {
                InternalSelectedValue = value;
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
                        rtsPublicationTypes.Tabs[0].Selected = true;
                }
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool UseOnlySearch
        {
            get
            {
                if (ViewState["UseOnlySearch"] == null)
                    ViewState["UseOnlySearch"] = false;

                return (bool)ViewState["UseOnlySearch"];
            }
            set
            {
                ViewState["UseOnlySearch"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        protected Guid? InternalSelectedValue
        {
            get
            {
                return (Guid?)ViewState["InternalSelectedValue"];
            }
            set
            {
                ViewState["InternalSelectedValue"] = value;
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
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnAddPublication, txtPublicationText);

            if (UseOnlySearch)
                radAjaxPanel.CssClass = "publication-create only-search clearfix";
            else
                radAjaxPanel.CssClass = "publication-create clearfix";
            
            PublicationLogoRootPath = WebCounter.BusinessLogicLayer.Configuration.Settings.DictionaryLogoPath(((LeadForcePortalBasePage)Page).SiteId, "tbl_PublicationType");

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            sccPublicationCategory.SiteID = ((LeadForcePortalBasePage)Page).SiteId;
            rtsPublicationTypes.Tabs.Clear();
            var publicationTypes = _dataManager.PublicationType.SelectByPublicationKindID(((LeadForcePortalBasePage)Page).SiteId, (int)PublicationKind.Discussion).OrderBy(pt => pt.Order).ToList();

            if (!UseOnlySearch)
            {                
                foreach (var publicationType in publicationTypes)
                {
                    rtsPublicationTypes.Tabs.Add(new RadTab() { Text = publicationType.TextAdd, ImageUrl = PublicationLogoRootPath + publicationType.Logo, Value = publicationType.ID.ToString(), ToolTip = publicationType.TextMarkToAdd });
                }
            }
            else
            {
                txtPublicationText.EmptyMessage = "Поиск...";
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

            if (InternalSelectedValue.HasValue && InternalSelectedValue != Guid.Empty)
            {
                foreach (RadTab tab in rtsPublicationTypes.Tabs)
                {
                    if (Guid.Parse(tab.Value) == InternalSelectedValue)
                    {
                        tab.Selected = true;
                        UpdateTabContainer((Guid)InternalSelectedValue);
                        break;
                    }
                }
            }

            BindPublicationStatuses();
            sccPublicationCategory.BindData();
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
                    var publication = _dataManager.Publication.SelectByIdForPortal(((LeadForcePortalBasePage)Page).SiteId, publicationId);
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

            var publicationStatuses = _dataManager.PublicationStatus.SelectByPublicationTypeID(publicationTypeId.HasValue ? (Guid)publicationTypeId : SelectedValue).Where(ps => ps.isActive.HasValue && (bool)ps.isActive);
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
                if (!Page.ClientScript.IsStartupScriptRegistered("UpdateTitle"))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "UpdateTitle", "$('.rwTitlebarControls td em').html($('#update-buttons a.btn span').html());", true);
            }
        }



        /// <summary>
        /// Updates the tab container.
        /// </summary>
        /// <param name="publicationTypeId">The publication type id.</param>
        private void UpdateTabContainer(Guid publicationTypeId)
        {
            var publicationTypes = _dataManager.PublicationType.SelectById(publicationTypeId);
            if (!UseOnlySearch)
                txtPublicationText.EmptyMessage = publicationTypes.TextMarkToAdd;
            else
                txtPublicationText.EmptyMessage = "Поиск...";

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

            var publication = _dataManager.Publication.SelectByIdForPortal(((LeadForcePortalBasePage)Page).SiteId, publicationId) ?? new tbl_Publication();
            publication.Title = txtTitle.Text;
            publication.Text = txtComment.Text;
                        
            publication.PublicationTypeID = Guid.Parse(rcbPublicationType.SelectedValue);

            var publicationStatuses = _dataManager.PublicationStatus.SelectByPublicationTypeID(publication.PublicationTypeID);
            var publicationStatus = publicationStatuses.FirstOrDefault(ps => ps.isFirst.HasValue && (bool) ps.isFirst && ps.isActive.HasValue && (bool) ps.isActive);
            if (publicationStatus != null)
            {
                var defaultPublicationStatusId = publicationStatus.ID;

                publication.PublicationStatusID = defaultPublicationStatusId;
            }
            publication.PublicationCategoryID = sccPublicationCategory.SelectedCategoryId;

            var fsp = new FileSystemProvider();
            if (rauFile.UploadedFiles.Count > 0)
            {
                if (!string.IsNullOrEmpty(publication.FileName))
                    fsp.Delete(((LeadForcePortalBasePage)Page).SiteId, "Publications", publication.FileName, FileType.Attachment);

                publication.FileName = fsp.Upload(((LeadForcePortalBasePage)Page).SiteId, "Publications", rauFile.UploadedFiles[0].FileName, rauFile.UploadedFiles[0].InputStream, FileType.Attachment);
            }

            if (publicationId == Guid.Empty)
            {
                publication.SiteID = ((LeadForcePortalBasePage)Page).SiteId;
                publication.Date = DateTime.Now;
                publication.AuthorID = (Guid)CurrentUser.Instance.ContactID;
                publication.OwnerID = CurrentUser.Instance.ContactID;

                var publicationType = _dataManager.PublicationType.SelectById(publication.PublicationTypeID);
                publication.AccessRecord = publicationType.PublicationAccessRecordID;
                publication.AccessComment = publicationType.PublicationAccessCommentID;

                if (publicationType.PublicationAccessRecordID == (int)PublicationAccessRecord.Company)
                {
                    publication.AccessCompanyID = CurrentUser.Instance.CompanyID;
                }

                _dataManager.Publication.Add(publication);

                if (PublicationAdded != null)
                    PublicationAdded(this);

                txtPublicationText.Text = string.Empty;

                if (!Page.ClientScript.IsStartupScriptRegistered("CloseAddPublicationRadWindow"))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseAddPublicationRadWindow", "CloseAddPublicationRadWindow(true);", true);
            }
            else
            {
                _dataManager.Publication.Update(publication);
                if (!Page.ClientScript.IsStartupScriptRegistered("CloseUpdatePublicationRadWindow"))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseUpdatePublicationRadWindow", "CloseUpdatePublicationRadWindow('" + publicationId + "');", true);
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
    }
}