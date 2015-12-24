using System;
using System.ComponentModel;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Services;

namespace Labitec.LeadForce.Portal.Main.RequirementModule
{
    public partial class Requirement : LeadForcePortalBasePage
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid ResponsibleId
        {
            get
            {
                if (ViewState["ResponsibleId"] == null)
                    ViewState["ResponsibleId"] = Guid.Empty;
                return (Guid)ViewState["ResponsibleId"];
            }
            set
            {
                ViewState["ResponsibleId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            var accessCheck = Access.Check(TblUser, "Requirements");
            if (!accessCheck.Read)
                Response.Redirect(UrlsData.LFP_AccessDenied(PortalSettingsId));

            ucContentComments.ContentId = ObjectId;            

            ucRequirementStatus.RequirementId = ObjectId;
            ucRequirementStatus.RequirementStatusChanged += ucRequirementStatus_RequirementStatusChanged;

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Handles the RequirementStatusChanged event of the ucRequirementStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Labitec.LeadForce.Portal.Main.RequirementModule.UserControls.RequirementStatus.RequirementStatusChangedEventArgs"/> instance containing the event data.</param>
        protected void ucRequirementStatus_RequirementStatusChanged(object sender, UserControls.RequirementStatus.RequirementStatusChangedEventArgs e)
        {
            if (e.ResponsibleId.HasValue && e.ResponsibleId != Guid.Empty)
                ResponsibleId = (Guid)e.ResponsibleId;
            else
                ResponsibleId = Guid.Empty;

            ChangeStatus();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var requirement = DataManager.Requirement.SelectById(SiteId, ObjectId);
            if (requirement != null)
            {
                CheckReadAccess(requirement.OwnerID, "Requirements");

                Title = lrlTitle.Text = string.Format("Требование №{0} от {1}", requirement.Number, requirement.CreatedAt.ToString("dd.MM.yyyy"));

                ucRequirementStatus.RequirementStatusId = requirement.RequirementStatusID;
                ucRequirementStatus.RequirementTypeId = requirement.RequirementTypeID;

                lrlRequirementType.Text = requirement.tbl_RequirementType.Title;                

                if (requirement.RequestID.HasValue)
                {
                    var request = DataManager.Request.SelectById(SiteId, (Guid) requirement.RequestID);
                    lrlRequest.Text = string.Format("Запрос №{0} от {1}", request.Number, request.CreatedAt.ToString("dd.MM.yyyy"));
                }   
                
                if (requirement.ParentID.HasValue)
                {
                    var parentRequirement = DataManager.Requirement.SelectById(SiteId, (Guid) requirement.ParentID);
                    lrlParentRequirement.Text =
                        string.Format("<div class='row'><label>Родительское требование: </label>Требование №{0} от {1}</div>",
                                      parentRequirement.Number, parentRequirement.CreatedAt.ToString("dd.MM.yyyy"));
                }

                if (requirement.ContractID.HasValue)
                {
                    var contract = requirement.tbl_Contract;
                    lrlContract.Text = string.Format("Договор №{0} от {1}", contract.Number, contract.CreatedAt.ToString("dd.MM.yyyy"));
                }

                if (requirement.InvoiceID.HasValue)
                {
                    var invoice = requirement.tbl_Invoice;
                    lrlInvoice.Text = string.Format("Счет №{0} от {1}", invoice.Number, invoice.CreatedAt.ToString("dd.MM.yyyy"));
                }

                if (requirement.RealizationDateActual.HasValue)
                    lrlRealizationDateActual.Text = requirement.RealizationDateActual.Value.ToString("dd.MM.yyyy hh:mm");

                if (requirement.RealizationDatePlanned.HasValue)
                    lrlRealizationDatePlanned.Text = requirement.RealizationDatePlanned.Value.ToString("dd.MM.yyyy hh:mm");

                if (requirement.Amount > 0)
                {
                    plEstimate.Visible = true;
                    var unit = string.Empty;
                    if (requirement.UnitID.HasValue)                    
                        unit = DataManager.Unit.SelectNameById((Guid) requirement.UnitID);                    
                    lrlEstimate.Text = string.Format("{0} ({1} {2} * {3})", String.Format("{0:0.00}",requirement.Amount), String.Format("{0:0.00}",requirement.Quantity), unit, String.Format("{0:0.00}",requirement.Price));
                }

                if (!string.IsNullOrEmpty(requirement.EstimateCommentForClient))
                {
                    plComment.Visible = true;
                    lrlComment.Text = requirement.EstimateCommentForClient;
                }

                lrlShortDescription.Text = requirement.ShortDescription;
            }
        }




        /// <summary>
        /// Changes the status.
        /// </summary>
        protected void ChangeStatus()
        {
            var requirement = DataManager.Requirement.SelectById(SiteId, ObjectId);
            if (requirement != null)
            {
                CheckWriteAccess(requirement.OwnerID, "Requirements");
                
                requirement.RequirementStatusID = ucRequirementStatus.RequirementStatusId;

                if (ResponsibleId != Guid.Empty)
                {
                    requirement.ResponsibleID = ResponsibleId;
                    RequestNotificationService.ChangeResponsible(SiteId, requirement.ID, (Guid)requirement.ResponsibleID);
                }

                DataManager.Requirement.Update(requirement);                
            }            
        }
    }
}