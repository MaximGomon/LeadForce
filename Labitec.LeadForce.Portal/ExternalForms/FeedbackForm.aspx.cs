using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Labitec.LeadForce.Portal.Shared.UserControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.FormCode;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Portal.ExternalForms
{
    public partial class FeedbackForm : System.Web.UI.Page
    {        
        private int _currentStep = 1;
        private DataManager _dataManager = new DataManager();

        protected Guid _siteId = Guid.Empty;
        private int _step = 1;
        private int _knowledgeBase = 1;

        private List<Guid> _publicationType = new List<Guid>();

        /// <summary>
        /// Gets or sets the type of the current publication.
        /// </summary>
        /// <value>
        /// The type of the current publication.
        /// </value>
        protected Guid? CurrentPublicationType
        {
            get { return (Guid?) ViewState["CurrentPublicationType"]; }
            set
            {
                ViewState["CurrentPublicationType"] = value;
                hfPublicationType.Value = value.ToString();
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["sarId"]))
                return;

            _publicationType.Clear(); 

            var siteActivityRule = _dataManager.SiteActivityRules.SelectById(Guid.Parse(Request.QueryString["sarId"]));
            if (siteActivityRule != null)
            {
                var layout = siteActivityRule.tbl_SiteActivityRuleLayout.FirstOrDefault(o => (LayoutType) o.LayoutType == LayoutType.Feedback);
                if (layout == null || string.IsNullOrEmpty(layout.LayoutParams))
                    return;

                var layoutParams = LayoutParams.Deserialize(layout.LayoutParams);
                if (!string.IsNullOrEmpty(layoutParams.GetValue("pt")))
                {
                    var types = layoutParams.GetValue("pt").Split(',');
                    foreach (var pType in types)
                        _publicationType.Add(Guid.Parse(pType));
                }

                if (!string.IsNullOrEmpty(layoutParams.GetValue("step")))
                    _step = int.Parse(layoutParams.GetValue("step"));

                if (!string.IsNullOrEmpty(layoutParams.GetValue("kb")))
                    _knowledgeBase = int.Parse(layoutParams.GetValue("kb"));

                if (!string.IsNullOrEmpty(siteActivityRule.CSSForm))
                    plContainer.Attributes.Add("style", siteActivityRule.CSSForm);

                if (!string.IsNullOrEmpty(siteActivityRule.CSSButton))
                {
                    lbtnSecondStepNext.Attributes.Add("style", siteActivityRule.CSSButton);
                    lbtnThirdStepNext.Attributes.Add("style", siteActivityRule.CSSButton);
                    ucLoggedAs.SetCSS(siteActivityRule.CSSButton);
                }
            }            

            if (!string.IsNullOrEmpty(Request.QueryString["sId"]))
                _siteId = Guid.Parse(Request.QueryString["sId"]);

            if (!Page.IsPostBack)
            {                
                var portalSettings = _dataManager.PortalSettings.SelectBySiteId(_siteId);
                if (portalSettings != null)
                {
                    if (!string.IsNullOrEmpty(portalSettings.Domain))                    
                        hlPortal.NavigateUrl = string.Format("http://{0}",portalSettings.Domain.Replace("http://", string.Empty));                    
                    else                    
                        hlPortal.NavigateUrl = Settings.LabitecLeadForcePortalUrl + "/" + portalSettings.ID;                                   
                }                
            }

            hfSiteId.Value = _siteId.ToString();                       

            Page.Header.DataBind();

            ucLoggedAs.SiteId = _siteId;

            if (!Page.IsPostBack)
                BindData();            
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            switch ((FormFeedBackSteps) _step)
            {
                case FormFeedBackSteps.One:
                    _currentStep = 2;                    
                    BindPublicationTypesTabs();
                    break;
                case FormFeedBackSteps.Two:
                    _currentStep = 1;                    
                    BindPublicationTypesList();
                    break;
            }

            sccPublicationCategory.SiteID = _siteId;
            sccPublicationCategory.BindData();

            ProceedStep();
        }



        /// <summary>
        /// Proceeds the step.
        /// </summary>
        protected void ProceedStep()
        {            
            plFirstStep.Visible = false;
            rtsPublicationTypes.Visible = false;
            plSecondStep.Visible = false;
            plThirdStep.Visible = false;
            plFourthStep.Visible = false;
            plSuccess.Visible = false;

            switch (_currentStep)
            {
                case 1:                    
                    plFirstStep.Visible = true;                    
                    plContainer.Width = 500;                    
                    break;
                case 2:                                        
                    plSecondStep.Visible = true;                                        
                    switch ((FormFeedBackSteps)_step)
                    {
                        case FormFeedBackSteps.One:
                            plSecondStep.CssClass = "one-step";
                            lbtnSecondStepBack.Visible = false;
                            rtsPublicationTypes.Visible = true;
                            lbtnSecondStepNext.CssClass = "btn";
                            break;
                        case FormFeedBackSteps.Two:
                            plSecondStep.CssClass = "step";
                            BindPublicationTypeTitle();
                            break;
                    }
                    switch ((FormFeedBackKnowledgeBase)_knowledgeBase)
                    {
                        case FormFeedBackKnowledgeBase.None:
                        case FormFeedBackKnowledgeBase.SeparateStep:
                            plContainer.Width = 500;
                            break;
                        case FormFeedBackKnowledgeBase.WithRequest:
                            plContainer.Width = 920;
                            plThirdStep.Visible = true;
                            plThirdStep.Width = 385;
                            plThirdStep.Attributes.Add("style", "margin-left: 10px;float:left");
                            searchText.Attributes.Add("onkeyup", "KeyUp(event, this.value);");                            
                            break;
                    }                                        
                    break;
                case 3:
                    plContainer.Width = 500;
                    switch ((FormFeedBackKnowledgeBase)_knowledgeBase)
                    {
                        case FormFeedBackKnowledgeBase.SeparateStep:
                            plThirdStep.Visible = true;
                            plThirdStepButtons.Visible = true;                                                        
                            break;
                        default:
                            if (CurrentUser.Instance == null)
                            {
                                plFourthStep.Visible = true;
                                plContainer.Width = 400;
                            }
                            else
                                AddPublication(CurrentUser.Instance.ID);
                            break;
                    }
                    break;
                case 4:
                    plContainer.Width = 500;
                    if (CurrentUser.Instance == null)
                        plFourthStep.Visible = true;
                    else
                    {
                        AddPublication(CurrentUser.Instance.ID);
                    }
                    break;            
            }
        }




        /// <summary>
        /// Proceeds the publication types.
        /// </summary>
        private void BindPublicationTypesTabs()
        {
            var publicationTypes = _dataManager.PublicationType.SelectByIds(_publicationType);
            rtsPublicationTypes.Tabs.Clear();
            var publicationLogoRootPath =
                WebCounter.BusinessLogicLayer.Configuration.Settings.DictionaryLogoPath(_siteId, "tbl_PublicationType");
            foreach (var publicationType in publicationTypes)
            {
                rtsPublicationTypes.Tabs.Add(new RadTab()
                                                 {
                                                     Text = publicationType.TextAdd,
                                                     ImageUrl = publicationLogoRootPath + publicationType.Logo,
                                                     Value = publicationType.ID.ToString()
                                                 });
            }
            if (rtsPublicationTypes.Tabs.Any())
            {
                CurrentPublicationType = Guid.Parse(rtsPublicationTypes.Tabs[0].Value);
                rtsPublicationTypes.Tabs[0].Selected = true;
            }                
        }



        /// <summary>
        /// Proceeds the publication types list.
        /// </summary>
        private void BindPublicationTypesList()
        {
            var publicationTypes = _dataManager.PublicationType.SelectByIds(_publicationType);
            lvPublicationTypes.DataSource = publicationTypes;
            lvPublicationTypes.DataBind();
        }
        



        /// <summary>
        /// Handles the OnClick event of the lbtnSecondStepNext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnPublicationType_OnClick(object sender, EventArgs e)
        {
            CurrentPublicationType = Guid.Parse(((LinkButton)sender).CommandArgument);
            _currentStep = 2;
            ProceedStep();
        }





        /// <summary>
        /// Handles the OnClick event of the lbtnAddPublication control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSecondStepNext_OnClick(object sender, EventArgs e)
        {
            _currentStep = 3;
            ProceedStep();            
        }        



        /// <summary>
        /// Handles the OnClick event of the lbtnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSecondStepBack_OnClick(object sender, EventArgs e)
        {
            _currentStep = 1;
            ProceedStep();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnThirdStepNext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnThirdStepNext_OnClick(object sender, EventArgs e)
        {
            _currentStep = 4;
            ProceedStep();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnThirdStepBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnThirdStepBack_OnClick(object sender, EventArgs e)
        {
            _currentStep = 2;
            ProceedStep();
        }



        /// <summary>
        /// Binds the publication type title.
        /// </summary>
        private void BindPublicationTypeTitle()
        {
            if (CurrentPublicationType.HasValue)
            {
                var pt = _dataManager.PublicationType.SelectById((Guid) CurrentPublicationType);
                lrlPublicationTypeTitle.Text = string.Format("<h1>{0}</h1>", pt.TextAdd);
            }
        }



        /// <summary>
        /// Adds the publication.
        /// </summary>
        private void AddPublication(Guid userId)
        {
            var user = _dataManager.User.SelectById(userId);

            var publication = new tbl_Publication
                                  {
                                      Title = searchText.Text,
                                      Text = txtComment.Text,
                                      PublicationTypeID = (Guid) CurrentPublicationType
                                  };

            var publicationStatuses = _dataManager.PublicationStatus.SelectByPublicationTypeID(publication.PublicationTypeID);
            var publicationStatus = publicationStatuses.FirstOrDefault(ps => ps.isFirst.HasValue && (bool)ps.isFirst && ps.isActive.HasValue && (bool)ps.isActive);
            if (publicationStatus != null)                           
                publication.PublicationStatusID = publicationStatus.ID;            

            publication.PublicationCategoryID = sccPublicationCategory.SelectedCategoryId;

            var fsp = new FileSystemProvider();
            if (rauFile.UploadedFiles.Count > 0)            
                publication.FileName = fsp.Upload(_siteId, "Publications", rauFile.UploadedFiles[0].FileName, rauFile.UploadedFiles[0].InputStream, FileType.Attachment);            
            
            publication.SiteID = user.SiteID;
            publication.Date = DateTime.Now;
            publication.AuthorID = (Guid)user.ContactID;
            publication.OwnerID = user.ContactID;            

            var publicationType = _dataManager.PublicationType.SelectById(publication.PublicationTypeID);
            if (chxIsHideFromPublic.Checked)
                publication.AccessRecord = (int)PublicationAccessRecord.Personal;
            else
                publication.AccessRecord = publicationType.PublicationAccessRecordID;
            publication.AccessComment = publicationType.PublicationAccessCommentID;

            if (publicationType.PublicationAccessRecordID == (int)PublicationAccessRecord.Company)            
                publication.AccessCompanyID = user.tbl_Contact.CompanyID;            

            _dataManager.Publication.Add(publication);           
            
            plThirdStep.Visible = false;
            plFourthStep.Visible = false;
            plSuccess.Visible = true;
        }



        /// <summary>
        /// Handles the OnUserAuthorized event of the ucLoggedAs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Labitec.LeadForce.Portal.Shared.UserControls.LoggedAs.UserAuthorizedEventArgs"/> instance containing the event data.</param>
        protected void ucLoggedAs_OnUserAuthorized(object sender, LoggedAs.UserAuthorizedEventArgs e)
        {
            AddPublication(e.UserId);
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnFourthStepBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnFourthStepBack_OnClick(object sender, EventArgs e)
        {
            switch ((FormFeedBackKnowledgeBase)_knowledgeBase)
            {
                case FormFeedBackKnowledgeBase.None:
                case FormFeedBackKnowledgeBase.WithRequest:
                    _currentStep = 2;
                    break;
                case FormFeedBackKnowledgeBase.SeparateStep:
                    _currentStep = 3;
                    break;
            }

            ProceedStep();
        }



        /// <summary>
        /// Handles the OnTabClick event of the rtsPublicationTypes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTabStripEventArgs"/> instance containing the event data.</param>
        protected void rtsPublicationTypes_OnTabClick(object sender, RadTabStripEventArgs e)
        {
            CurrentPublicationType = Guid.Parse(rtsPublicationTypes.SelectedTab.Value);            
        }
    }
}