using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.DocumentManagement;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Services;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class Requirement : LeadForceBasePage
    {
        private Guid _requirementId;
        public Access access;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.RouteData.Values["id"] != null)
                _requirementId = Guid.Parse(Page.RouteData.Values["id"] as string);

            dprReminder.RegisterSkipCheckingTrigger(RadTabStrip1);                       
            dprReminder.RegisterSkipCheckingTrigger(ucContentComments.TextBoxEditor);
            dprReminder.RegisterSkipCheckingTrigger(ucContentComments.HtmlEditor);
            dprReminder.RegisterSkipCheckingTrigger(lbtnSave);

            if (_requirementId == Guid.Empty)
                dprReminder.MarkPageClean();

            Title = "Требование - LeadForce";

            if (!Page.IsPostBack && !DataManager.RequirementStatus.SelectAll(SiteId).Any())
            {
                radWindowManager.RadAlert("Перед тем как создавать требование, нужно заполнить справочники для запросов и требований", 420, 100, "Предупреждение", "RedirectToRequirements");
                return;
            }

            access = Access.Check();
            if (!access.Write)
                lbtnSave.Visible = false;
            
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbServiceLevel, dcbServiceLevel, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbServiceLevel, rdpRealizationDatePlanned, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbRequirementType, dcbRequirementType, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbRequirementType, ucRequirementStatus, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbRequirementType, dcbRequirementSeverityOfExposure, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbRequests, dcbServiceLevel, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbCompany, ucParentRequirment, null, UpdatePanelRenderMode.Inline);            
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucRequirementStatus, ucResponsible, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbCompany, ucRequirementStatus, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbServiceLevel, ucRequirementStatus, null, UpdatePanelRenderMode.Inline);            

            ucRequirementStatus.RequirementStatusChanged += ucRequirementStatus_RequirementStatusChanged;
            ajaxPanel.AjaxRequest += ajaxPanel_AjaxRequest;                        

            ucContentComments.ContentId = _requirementId;
            gridRequirementHistory.Where.Add(new GridWhere() { Column = "RequirementID", Value = _requirementId.ToString() });

            hlCancel.NavigateUrl = UrlsData.AP_Requirements();
            hlCancelCloseRequests.NavigateUrl = UrlsData.AP_Requirements();

            ucRequirementStatus.RequirementId = _requirementId;

            gridRequests.Actions.Add(new GridAction { Text = "Карточка запроса", NavigateUrl = string.Format("~/{0}/Requests/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridRequests.SiteID = SiteId;            
            
            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Handles the RequirementStatusChanged event of the ucRequirementStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.Requirement.RequirementStatus.RequirementStatusChangedEventArgs"/> instance containing the event data.</param>
        protected void ucRequirementStatus_RequirementStatusChanged(object sender, UserControls.Requirement.RequirementStatus.RequirementStatusChangedEventArgs e)
        {
            if (e.ResponsibleId.HasValue && e.ResponsibleId != Guid.Empty)
                ucResponsible.SelectedValue = e.ResponsibleId;            
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            plComment.Visible = true;
            dcbContract.MaskFilters.Clear();
            dcbContract.MaskFilters.Add("#Number", "Number");
            dcbContract.MaskFilters.Add("#CreatedAt", "CreatedAt");

            dcbRequests.MaskFilters.Clear();
            dcbRequests.MaskFilters.Add("#Number", "Number");
            dcbRequests.MaskFilters.Add("#CreatedAt", "CreatedAt");
            dcbRequests.MaskFilters.Add("#RequestType", "tbl_RequestSourceType.Title");

            dcbRequirementSeverityOfExposure.SiteID = dcbRequests.SiteID = dcbProducts.SiteID = dcbCompany.SiteID =
            dcbRequirementType.SiteID = dcbServiceLevel.SiteID = dcbRequirementPriority.SiteID =
            dcbRequirementComplexity.SiteID = ucPublicationCategory.SiteID = 
            dcbContract.SiteID = dcbOrder.SiteID = dcbInvoice.SiteID =
            dcbEvaluationRequirementsProducts.SiteID = dcbCurrency.SiteID =
            dcbInternalUnit.SiteID = dcbUnit.SiteID = dcbRequirementImplementationComplete.SiteID = dcbRequirementSpeedTime.SiteID = 
            dcbRequestSourceType.SiteID = dcbRequirementSatisfaction.SiteID = SiteId;
            ucPublicationCategory.BindData();
            rdpCreatedAt.SelectedDate = DateTime.Now;
            
            if (CurrentUser.Instance.CompanyID.HasValue)
            {
                dcbCompany.SelectedId = (Guid)CurrentUser.Instance.CompanyID;
                dcbCompany.SelectedText = DataManager.Company.SelectById(SiteId, (Guid)CurrentUser.Instance.CompanyID).Name;
            }

            ucResponsible.SelectedValue = CurrentUser.Instance.ContactID;

            if (Session["Selected-Text"] != null)
            {                
                ucComment.Content = HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(((string)Session["Selected-Text"])));
                Session["Selected-Text"] = null;
            }

            if (Session["Source-RequestId"] != null)
            {
                dcbRequests.SelectedIdNullable = (Guid?) Session["Source-RequestId"];

                var request = DataManager.Request.SelectById(SiteId, (Guid) Session["Source-RequestId"]);
                if (request != null)
                {
                    dcbRequests.SelectedText = dcbRequests.Mask.Replace("#Number", request.Number)
                                                                  .Replace("#CreatedAt", request.CreatedAt.ToString("dd.MM.yyyy"))
                                                                  .Replace("#RequestType", request.tbl_RequestSourceType.Title);

                    dcbRequestSourceType.SelectedIdNullable = request.RequestSourceTypeID;                        
                }

                Session["Source-RequestId"] = null;                
                ProceedRequest(dcbRequests.SelectedIdNullable);
            }

            var requirement = DataManager.Requirement.SelectById(SiteId, _requirementId);
            if (requirement != null)
            {
                plComments.Visible = true;
                plComment.Visible = false;

                gridRequests.Where.Add(new GridWhere()
                {
                    CustomQuery =
                        string.Format(
                            "(tbl_Request.ID = '{0}' OR tbl_Request.ID IN " +
                            "(SELECT RequestID FROM tbl_RequestToRequirement WHERE RequirementID = '{1}'))",
                            requirement.RequestID.HasValue ? (Guid)requirement.RequestID : Guid.Empty,
                            _requirementId.ToString())
                });

                ucParentRequirment.CurrentRequirementId = requirement.ID;
                ucParentRequirment.ParentRequirementId = requirement.ParentID;

                ucRequirementStatus.RequirementStatusId = requirement.RequirementStatusID;
                ucRequirementStatus.RequirementTypeId = requirement.RequirementTypeID;
                ucRequirementStatus.CompanyId = requirement.CompanyID;
                ucRequirementStatus.ServiceLevelId = requirement.ServiceLevelID;

                lrlNumber.Text = requirement.Number;
                rdpCreatedAt.SelectedDate = requirement.CreatedAt;
                txtShortDescription.Text = requirement.ShortDescription;                
                dcbRequestSourceType.SelectedIdNullable = requirement.RequestSourceTypeID;                                

                if (requirement.ProductID.HasValue)
                {
                    dcbProducts.SelectedIdNullable = requirement.ProductID;
                    dcbProducts.SelectedText = DataManager.Product.SelectById(SiteId, (Guid) requirement.ProductID).Title;
                }
                
                if (requirement.CompanyID.HasValue)
                {
                    dcbCompany.SelectedIdNullable = requirement.CompanyID;
                    dcbCompany.SelectedText = requirement.tbl_Company.Name;
                }
                
                ucContact.SelectedValue = requirement.ContactID;
                txtProductSeriesNumber.Text = requirement.ProductSeriesNumber;
                dcbRequirementType.SelectedIdNullable = requirement.RequirementTypeID;
                dcbRequirementSeverityOfExposure.SelectedIdNullable = requirement.RequirementSeverityOfExposureID;

                dcbRequirementSeverityOfExposure.Filters.Clear();
                 
                dcbRequirementSeverityOfExposure.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn()
                                                                {
                                                                    Name = "RequirementTypeID",
                                                                    DbType = DbType.Guid,
                                                                    Value = requirement.RequirementTypeID.ToString()
                                                                });
                 
                dcbServiceLevel.SelectedIdNullable = requirement.ServiceLevelID;                
                dcbRequirementPriority.SelectedIdNullable = requirement.RequirementPriorityID;
                dcbRequirementComplexity.SelectedIdNullable = requirement.RequirementComplexityID;
                if (requirement.PublicationCategoryID.HasValue)
                    ucPublicationCategory.SelectedCategoryId = (Guid)requirement.PublicationCategoryID;
                
                ucResponsible.SelectedValue = requirement.ResponsibleID;
                rdpRealizationDateActual.SelectedDate = requirement.RealizationDateActual;
                rdpRealizationDatePlanned.SelectedDate = requirement.RealizationDatePlanned;

                if (requirement.ContractID.HasValue)
                {
                    dcbContract.SelectedIdNullable = requirement.ContractID;
                    dcbContract.SelectedText = dcbContract.Mask.Replace("#Number", requirement.tbl_Contract.Number)
                                                               .Replace("#CreatedAt", requirement.tbl_Contract.CreatedAt.ToString("dd.MM.yyyy"));
                }                

                if (requirement.OrderID.HasValue)
                {
                    dcbOrder.SelectedIdNullable = requirement.OrderID;
                    dcbOrder.SelectedText = requirement.tbl_Order.Number;
                }

                if (requirement.InvoiceID.HasValue)
                {
                    dcbInvoice.SelectedIdNullable = requirement.InvoiceID;
                    dcbInvoice.SelectedText = requirement.tbl_Invoice.Number;
                }
                
                if (requirement.EvaluationRequirementsProductID.HasValue)
                {
                    dcbEvaluationRequirementsProducts.SelectedIdNullable = requirement.EvaluationRequirementsProductID;
                    dcbEvaluationRequirementsProducts.SelectedText = DataManager.Product.SelectById(SiteId, (Guid)requirement.EvaluationRequirementsProductID).Title;
                }

                dcbInternalUnit.SelectedIdNullable = requirement.InternalUnitID;
                rntxtInternalQuantity.Value = (double)requirement.InternalQuantity;
                txtEstimateCommentInternal.Text = requirement.EstimateCommentInternal;
                txtEstimateCommentForClient.Text = requirement.EstimateCommentForClient;

                txtAnyProductName.Text = requirement.AnyProductName;
                rntxtQuantity.Value = (double)requirement.Quantity;
                dcbCurrency.SelectedIdNullable = requirement.CurrencyID;
                rntxtCurrencyPrice.Value = (double)requirement.CurrencyPrice;
                rntxtCurrencyAmount.Value = (double) requirement.CurrencyAmount;
                dcbUnit.SelectedIdNullable = requirement.UnitID;
                rntxtRate.Value = (double) requirement.Rate;
                rntxtPrice.Value = (double) requirement.Price;
                rntxtAmount.Value = (double) requirement.Amount;
                dcbRequirementImplementationComplete.SelectedIdNullable = requirement.RequirementImplementationCompleteID;
                dcbRequirementSpeedTime.SelectedIdNullable = requirement.RequirementSpeedTimeID;
                dcbRequirementSatisfaction.SelectedIdNullable = requirement.RequirementSatisfactionID;
                txtEstimationComment.Text = requirement.EstimationComment;                

                UpdateRequestsList();

                if (requirement.RequestID.HasValue)
                {
                    dcbRequests.SelectedIdNullable = requirement.RequestID;
                    var request = DataManager.Request.SelectById(SiteId, (Guid)requirement.RequestID);
                    dcbRequests.SelectedText = dcbRequests.Mask.Replace("#Number", request.Number)
                                                               .Replace("#CreatedAt", request.CreatedAt.ToString("dd.MM.yyyy"))
                                                               .Replace("#RequestType", request.tbl_RequestSourceType.Title);
                }
            }
            else
            {
                gridRequests.Where.Add(new GridWhere()
                {
                    CustomQuery = string.Format("(tbl_Request.ID = '{0}')", Guid.Empty)
                });

                var requirementType = DataManager.RequirementType.SelectDefault(SiteId);
                if (requirementType != null)
                {
                    ucRequirementStatus.RequirementTypeId = requirementType.ID;
                    dcbRequirementSeverityOfExposure.Filters.Clear();
                    dcbRequirementSeverityOfExposure.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn()
                                                                         {
                                                                             Name = "RequirementTypeID",
                                                                             DbType = DbType.Guid,
                                                                             Value = requirementType.ID.ToString()
                                                                         });                    
                }
            }
            hlGoToRequest.Visible = dcbRequests.SelectedIdNullable.HasValue;
            if (dcbRequests.SelectedIdNullable.HasValue)
                hlGoToRequest.NavigateUrl = UrlsData.AP_RequestEdit(dcbRequests.SelectedId);
        }



        /// <summary>
        /// Handles the AjaxRequest event of the ajaxPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void ajaxPanel_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            RecalculatePrices(e.Argument);
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

            var requirement = DataManager.Requirement.SelectById(SiteId, _requirementId) ?? new tbl_Requirement();

            requirement.ShortDescription = (txtShortDescription.Text.Length > 2048
                                                ? txtShortDescription.Text.Substring(0, 2048)
                                                : txtShortDescription.Text);

            //requirement.LongDescription = ucLongDescription.Content;

            requirement.RequestSourceTypeID = dcbRequestSourceType.SelectedIdNullable;            

            requirement.RequirementStatusID = ucRequirementStatus.RequirementStatusId;
            
            requirement.RequestID = dcbRequests.SelectedIdNullable;
            requirement.ProductID = dcbProducts.SelectedIdNullable;
            requirement.CompanyID = dcbCompany.SelectedIdNullable;
            requirement.ContactID = ucContact.SelectedValue;
            requirement.ProductSeriesNumber = txtProductSeriesNumber.Text;
            requirement.RequirementTypeID = dcbRequirementType.SelectedId;

            requirement.RequirementSeverityOfExposureID = dcbRequirementSeverityOfExposure.SelectedIdNullable;

            requirement.ServiceLevelID = dcbServiceLevel.SelectedId;

            requirement.ParentID = ucParentRequirment.ParentRequirementId;

            requirement.RequirementPriorityID = dcbRequirementPriority.SelectedIdNullable;
            requirement.RequirementComplexityID = dcbRequirementComplexity.SelectedIdNullable;

            if (ucPublicationCategory.SelectedCategoryId != Guid.Empty)
                requirement.PublicationCategoryID = ucPublicationCategory.SelectedCategoryId;
            else
                requirement.PublicationCategoryID = null;
            
            requirement.ResponsibleID = ucResponsible.SelectedValue;
            requirement.RealizationDatePlanned = rdpRealizationDatePlanned.SelectedDate;
            requirement.RealizationDateActual = rdpRealizationDateActual.SelectedDate;
            requirement.ContractID = dcbContract.SelectedIdNullable;
            requirement.OrderID = dcbOrder.SelectedIdNullable;
            requirement.InvoiceID = dcbInvoice.SelectedIdNullable;
            requirement.EvaluationRequirementsProductID = dcbEvaluationRequirementsProducts.SelectedIdNullable;
            requirement.AnyProductName = txtAnyProductName.Text;
            requirement.Quantity = (decimal)rntxtQuantity.Value;
            requirement.CurrencyID = dcbCurrency.SelectedIdNullable;
            requirement.CurrencyPrice = (decimal)rntxtCurrencyPrice.Value;
            requirement.CurrencyAmount = (decimal)rntxtCurrencyAmount.Value;
            requirement.UnitID = dcbUnit.SelectedIdNullable;
            requirement.Rate = (decimal)rntxtRate.Value;
            requirement.Price = (decimal)rntxtPrice.Value;
            requirement.Amount = (decimal)rntxtAmount.Value;
            requirement.RequirementImplementationCompleteID = dcbRequirementImplementationComplete.SelectedIdNullable;
            requirement.RequirementSpeedTimeID = dcbRequirementSpeedTime.SelectedIdNullable;
            requirement.RequirementSatisfactionID = dcbRequirementSatisfaction.SelectedIdNullable;
            requirement.EstimationComment = txtEstimationComment.Text;

            requirement.InternalUnitID = dcbInternalUnit.SelectedIdNullable;
            requirement.InternalQuantity = (decimal)rntxtInternalQuantity.Value;
            requirement.EstimateCommentInternal = txtEstimateCommentInternal.Text;
            requirement.EstimateCommentForClient = txtEstimateCommentForClient.Text;            

            if (requirement.ID == Guid.Empty)
            {
                requirement.SiteID = SiteId;
                requirement.OwnerID = CurrentUser.Instance.ContactID;
                requirement.CreatedAt = DateTime.Now;                

                var requestSourceType = DataManager.RequirementType.SelectById(SiteId, requirement.RequirementTypeID);
                if (requestSourceType != null)
                {
                    var documentNumerator = DocumentNumerator.GetNumber((Guid)requestSourceType.NumeratorID, requirement.CreatedAt, requestSourceType.tbl_Numerator.Mask, "tbl_Requirement");
                    requirement.Number = documentNumerator.Number;
                    requirement.SerialNumber = documentNumerator.SerialNumber;
                }
                
                DataManager.Requirement.Add(requirement);
                    
                if (!string.IsNullOrEmpty(ucComment.Content))
                {
                    var contactId = requirement.ContactID ?? CurrentUser.Instance.ContactID;

                    var user = DataManager.User.SelectByContactIdExtended(SiteId, (Guid)contactId) ??
                               DataManager.User.AddPortalUser(SiteId, (Guid)contactId);

                    if (user != null)
                    {
                        ContentCommentRepository.Add(CurrentUser.Instance.SiteID, user.ID, requirement.ID, ucComment.Content, null, null, string.Empty,
                                                 CommentTables.tbl_RequirementComment, false, false);
                    }
                }

                if (!string.IsNullOrEmpty(txtComment.Text))
                {
                    var fileName = string.Empty;
                    if (rauFileUpload.UploadedFiles.Count > 0)
                    {
                        var fsp = new FileSystemProvider();
                        fileName = fsp.Upload(CurrentUser.Instance.SiteID,
                                                  EnumHelper.GetEnumDescription(CommentTables.tbl_RequirementComment),
                                                  rauFileUpload.UploadedFiles[0].FileName, rauFileUpload.UploadedFiles[0].InputStream, FileType.Attachment);


                    }

                    ContentCommentRepository.Add(CurrentUser.Instance.SiteID, CurrentUser.Instance.ID, requirement.ID, txtComment.Text, null, null, fileName,
                                                 CommentTables.tbl_RequirementComment, chxIsOfficialAnswer.Checked, false);
                }                
            }
            else
            {                
                DataManager.Requirement.Update(requirement);
            }
            
            if (!CheckRequestsToClose(requirement))
                Response.Redirect(UrlsData.AP_Requirements());
        }



        /// <summary>
        /// Checks the requests to close.
        /// </summary>
        /// <param name="requirement">The requirement.</param>
        /// <returns></returns>
        protected bool CheckRequestsToClose(tbl_Requirement requirement)
        {
            if (!requirement.tbl_RequirementStatus.IsLast)
                return false;

            var requestsToClose = new List<tbl_Request>();
            foreach (tbl_Request request in requirement.tbl_Request)
            {
                if (request.tbl_Requirement.Count == request.tbl_Requirement.Count(o => o.tbl_RequirementStatus.IsLast))
                    requestsToClose.Add(request);
            }

            if (requirement.RequestID.HasValue)
            {
                var requestSource = DataManager.Request.SelectById(SiteId, requirement.RequestID.Value);
                if (requestSource.tbl_Requirement.Count == requestSource.tbl_Requirement.Count(o => o.tbl_RequirementStatus.IsLast))
                    requestsToClose.Add(requestSource);
            }

            if (!requestsToClose.Any())
                return false;

            chxRequestsList.Items.Clear();

            foreach (tbl_Request request in requestsToClose)
            {
                var item = new ListItem(
                    string.Format("{0} №{1} от {2}", request.ShortDescription, request.Number,
                                  request.CreatedAt.ToString("dd.MM.yyyy")), request.ID.ToString());
                item.Selected = true;
                chxRequestsList.Items.Add(item);
            }

            if (!Page.ClientScript.IsStartupScriptRegistered("ShowCloseRequestRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "ShowCloseRequestRadWindow", "ShowCloseRequestRadWindow();", true);

            return true;
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

            if (dcbCompany.SelectedId != Guid.Empty)            
                ucContact.CompanyId = dcbCompany.SelectedId;

            ucParentRequirment.CompanyId = dcbCompany.SelectedIdNullable;
            ucParentRequirment.BindData();

            UpdateRequestsList();
            RefreshRequirementStatusControl();
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbEvaluationRequirementsProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbEvaluationRequirementsProducts_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var product = DataManager.Product.SelectById(SiteId, dcbEvaluationRequirementsProducts.SelectedId);
            if (product != null && product.Price.HasValue)            
                rntxtCurrencyPrice.Value = (double)product.Price;            
            else
                rntxtCurrencyPrice.Value = 0;

            RecalculatePrices("rntxtCurrencyPrice");
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
                    rdpRealizationDatePlanned.SelectedDate = rdpCreatedAt.SelectedDate.Value.AddHours(serviceLevel.ResponseTime);
            }
            else
            {
                rdpRealizationDatePlanned.SelectedDate = null;
            }

            RefreshRequirementStatusControl();
        }



        /// <summary>
        /// Refreshes the requirement status control.
        /// </summary>
        private void RefreshRequirementStatusControl()
        {
            ucRequirementStatus.CompanyId = dcbCompany.SelectedIdNullable;
            ucRequirementStatus.ServiceLevelId = dcbServiceLevel.SelectedId;
        }



        /// <summary>
        /// Recalculates the prices.
        /// </summary>
        /// <param name="senderId">The sender id.</param>
        private void RecalculatePrices(string senderId)
        {
            var quantity = rntxtQuantity.Value ?? 0;
            var currencyPrice = rntxtCurrencyPrice.Value ?? 0;
            var currencyAmount = rntxtCurrencyAmount.Value ?? 0;
            var rate = rntxtRate.Value ?? 0;
            var price = rntxtPrice.Value ?? 0;
            var amount = rntxtAmount.Value ?? 0;

            switch (senderId)
            {
                case "rntxtQuantity":
                case "rntxtCurrencyPrice":
                case "rntxtRate":
                    currencyAmount = currencyPrice * quantity;
                    price = currencyPrice * rate;
                    amount = currencyAmount * rate;
                    rntxtCurrencyAmount.Value = currencyAmount;
                    rntxtPrice.Value = price;
                    rntxtAmount.Value = amount;
                    break;
                case "rntxtCurrencyAmount":
                    currencyPrice = currencyAmount / quantity;
                    price = currencyPrice * rate;
                    amount = currencyAmount * rate;
                    rntxtCurrencyPrice.Value = currencyPrice;
                    rntxtPrice.Value = price;
                    rntxtAmount.Value = amount;
                    break;
                case "rntxtPrice":
                    currencyPrice = price / rate;
                    currencyAmount = currencyPrice * quantity;
                    amount = currencyAmount * rate;
                    rntxtCurrencyPrice.Value = currencyPrice;
                    rntxtCurrencyAmount.Value = currencyAmount;
                    rntxtAmount.Value = amount;
                    break;
                case "rntxtAmount":
                    currencyAmount = amount / rate;
                    price = amount / quantity;
                    currencyPrice = currencyAmount / quantity;
                    rntxtCurrencyPrice.Value = currencyPrice;
                    rntxtCurrencyAmount.Value = currencyAmount;
                    rntxtPrice.Value = price;
                    break;
            }
        }




        /// <summary>
        /// Updates the requests list.
        /// </summary>
        private void UpdateRequestsList()
        {
            dcbRequests.Filters.Clear();

            dcbRequests.SelectedIdNullable = null;
            dcbRequests.SelectedText = string.Empty;

            if (dcbRequestSourceType.SelectedIdNullable.HasValue)
            {
                dcbRequests.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                            {
                                                Name = "RequestSourceTypeID",
                                                DbType = DbType.Guid,
                                                Operation = FilterOperation.Equal,
                                                Value = dcbRequestSourceType.SelectedId.ToString()
                                            });                
            }

            if (dcbCompany.SelectedIdNullable.HasValue)
            {                
                dcbRequests.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                           {
                                               Name = "CompanyID",
                                               DbType = DbType.Guid,
                                               Operation = FilterOperation.Equal,
                                               Value = dcbCompany.SelectedIdNullable.Value.ToString()
                                           });
            }

            dcbRequests.InitDataSource();
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbRequirementType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbRequirementType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ucRequirementStatus.RequirementTypeId = dcbRequirementType.SelectedId;
            ucRequirementStatus.BindData();

            dcbRequirementSeverityOfExposure.Filters.Clear();

            if (dcbRequirementType.SelectedIdNullable.HasValue)
            {
                dcbRequirementSeverityOfExposure.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn()
                                                                 {
                                                                     Name = "RequirementTypeID",
                                                                     DbType = DbType.Guid,
                                                                     Value = dcbRequirementType.SelectedId.ToString()
                                                                 });
                dcbRequirementSeverityOfExposure.BindData();
            }            
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbRequests control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbRequests_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            hlGoToRequest.Visible = dcbRequests.SelectedIdNullable.HasValue;
            if (dcbRequests.SelectedIdNullable.HasValue)
                hlGoToRequest.NavigateUrl = UrlsData.AP_RequestEdit(dcbRequests.SelectedId);

            ProceedRequest(dcbRequests.SelectedIdNullable);
        }



        /// <summary>
        /// Proceeds the request.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        protected void ProceedRequest(Guid? requestId)
        {
            if (requestId.HasValue)
            {
                var request = DataManager.Request.SelectById(SiteId, (Guid) requestId);
                if (request != null)
                {
                    if (request.ProductID.HasValue)
                    {
                        dcbProducts.SelectedIdNullable = request.ProductID;
                        dcbProducts.SelectedText = request.tbl_Product.Title;
                    }
                    if (request.CompanyID.HasValue)
                    {
                        dcbCompany.SelectedIdNullable = request.CompanyID;
                        dcbCompany.SelectedText = request.tbl_Company.Name;
                    }
                    ucContact.SelectedValue = request.ContactID;
                    txtProductSeriesNumber.Text = request.ProductSeriesNumber;
                    
                    dcbServiceLevel.SelectedIdNullable = request.ServiceLevelID;

                    if (request.ServiceLevelID.HasValue)
                        ucRequirementStatus.ServiceLevelId = (Guid)request.ServiceLevelID;

                    ucRequirementStatus.CompanyId = request.CompanyID;
                }
            }
            else
            {
                dcbProducts.SelectedIdNullable = null;
                dcbProducts.SelectedText = string.Empty;
                dcbCompany.SelectedIdNullable = null;
                dcbCompany.SelectedText = string.Empty;
                ucContact.SelectedValue = null;
                txtProductSeriesNumber.Text = string.Empty;
                dcbServiceLevel.SelectedIdNullable = null;
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbRequestSourceType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbRequestSourceType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            UpdateRequestsList();
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridRequirementHistory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void gridRequirementHistory_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;
                
                var lrlUserFullName = (Literal)item.FindControl("lrlUserFullName");
                var lrlResponsibleUserFullName = (Literal)item.FindControl("lrlResponsibleUserFullName");

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lrlUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), data["tbl_Contact_UserFullName"]);

                if (!string.IsNullOrEmpty(data["c1_UserFullName"].ToString()))
                    lrlResponsibleUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["c1_ID"].ToString())), data["c1_UserFullName"]);
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridRequests control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridRequests_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var lrlCompanyName = (Literal)item.FindControl("lrlCompanyName");
                var lrlUserFullName = (Literal)item.FindControl("lrlUserFullName");

                ((Literal)item.FindControl("lrlRequestStatus")).Text = EnumHelper.GetEnumDescription((RequestStatus)int.Parse(data["tbl_Request_RequestStatusID"].ToString()));

                if (!string.IsNullOrEmpty(data["tbl_Company_Name"].ToString()))
                    lrlCompanyName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Company(Guid.Parse(data["tbl_Company_ID"].ToString())), data["tbl_Company_Name"]);

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lrlUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), data["tbl_Contact_UserFullName"]);
            }
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
                var contact = DataManager.Contact.SelectById(CurrentUser.Instance.SiteID, (Guid)ucContact.SelectedValue);
                if (contact != null && contact.CompanyID.HasValue)
                {
                    var company = DataManager.Company.SelectById(CurrentUser.Instance.SiteID, (Guid)contact.CompanyID);
                    dcbCompany.SelectedIdNullable = contact.CompanyID;
                    dcbCompany.SelectedText = company.Name;
                }
            }
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnAddFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnAddFile_OnClick(object sender, EventArgs e)
        {
            rbtnAddFile.Visible = false;
            rauFileUpload.Visible = true;
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnCloseRequests control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnCloseRequests_OnClick(object sender, EventArgs e)
        {            
            foreach (ListItem item in chxRequestsList.Items)
            {
                if (item.Selected)
                {
                    var request = DataManager.Request.SelectById(SiteId, Guid.Parse(item.Value));
                    request.RequestStatusID = (int) RequestStatus.Closed;
                    DataManager.Request.Update(request);
                }
            }

            Response.Redirect(UrlsData.AP_Requirements());
        }
    }
}