using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using AForge.Imaging.Filters;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class CompanyLegalAccount : System.Web.UI.UserControl
    {
        private DataManager _dataManager;
        public event EventHandler CompanyLegalAccountsChanged;
        protected RadAjaxManager radAjaxManager = null;

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


        /// <summary>
        /// Gets the company legal account list.
        /// </summary>
        public List<CompanyLegalAccountMap> CompanyLegalAccountList
        {
            get { return (List<CompanyLegalAccountMap>)ViewState["CompanyLegalAccounts"]; }
        }




        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {                    
            _dataManager = ((LeadForceBasePage)Page).DataManager;

            rgCompanyLegalAccounts.Culture = new CultureInfo("ru-RU");

            if (!Page.IsPostBack)
                BindData();
        }

        

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (CompanyId != null)
                ViewState["CompanyLegalAccounts"] = _dataManager.CompanyLegalAccount.SelectAll((Guid)CompanyId).Select(cla => new CompanyLegalAccountMap()
                                                                                                                  {
                                                                                                                    ID = cla.ID,
                                                                                                                    CompanyID = cla.CompanyID,
                                                                                                                    SiteID = cla.SiteID,
                                                                                                                    Title = cla.Title,
                                                                                                                    OfficialTitle = cla.OfficialTitle,
                                                                                                                    LegalAddress = cla.LegalAddress,
                                                                                                                    OGRN = cla.OGRN,
                                                                                                                    RegistrationDate = cla.RegistrationDate,
                                                                                                                    INN = cla.INN,
                                                                                                                    KPP = cla.KPP,
                                                                                                                    RS = cla.RS,
                                                                                                                    BankID = cla.BankID,
                                                                                                                    IsPrimary = cla.IsPrimary,
                                                                                                                    IsActive = cla.IsActive,
                                                                                                                    HeadID = cla.HeadID,
                                                                                                                    HeadSignatureFileName = cla.HeadSignatureFileName,
                                                                                                                    AccountantID = cla.AccountantID,
                                                                                                                    AccountantSignatureFileName = cla.AccountantSignatureFileName,
                                                                                                                    StampFileName = cla.StampFileName
                                                                                                                  }).ToList();
            else
                ViewState["CompanyLegalAccounts"] = new List<CompanyLegalAccountMap>();
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgCompanyLegalAccounts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgCompanyLegalAccounts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgCompanyLegalAccounts.DataSource = ViewState["CompanyLegalAccounts"];
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgCompanyLegalAccounts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgCompanyLegalAccounts_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {                
                var gridEditFormItem = (GridEditFormItem)e.Item;

                var dcbBanks = (DictionaryOnDemandComboBox)gridEditFormItem.FindControl("dcbBanks");
                dcbBanks.BindData();

                var item = e.Item as GridEditableItem;

                ((ContactComboBox)item.FindControl("ucHeadContact")).CompanyId = CompanyId;
                ((ContactComboBox)item.FindControl("ucAccountantContact")).CompanyId = CompanyId;
                
                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var companyLegalAccount = (CompanyLegalAccountMap)item.DataItem;                    
                    
                    ((TextBox)item.FindControl("txtTitle")).Text = companyLegalAccount.Title;
                    ((TextBox)item.FindControl("txtOfficialTitle")).Text = companyLegalAccount.OfficialTitle;
                    ((TextBox)item.FindControl("txtLegalAddress")).Text = companyLegalAccount.LegalAddress;
                    ((RadDatePicker)item.FindControl("rdpRegistrationDate")).SelectedDate = companyLegalAccount.RegistrationDate;
                    ((TextBox)item.FindControl("txtOGRN")).Text = companyLegalAccount.OGRN;
                    ((TextBox)item.FindControl("txtINN")).Text = companyLegalAccount.INN;
                    ((TextBox)item.FindControl("txtKPP")).Text = companyLegalAccount.KPP;
                    ((TextBox)item.FindControl("txtRS")).Text = companyLegalAccount.RS;
                    ((CheckBox)item.FindControl("chxIsPrimary")).Checked = companyLegalAccount.IsPrimary;
                    ((CheckBox)item.FindControl("chxIsActive")).Checked = companyLegalAccount.IsActive;
                    ((ContactComboBox)item.FindControl("ucHeadContact")).SelectedValue = companyLegalAccount.HeadID;
                    ((ContactComboBox)item.FindControl("ucAccountantContact")).SelectedValue = companyLegalAccount.AccountantID;
                    if (companyLegalAccount.BankID.HasValue)
                    {
                        dcbBanks.SelectedId = (Guid)companyLegalAccount.BankID;
                        dcbBanks.SelectedText = _dataManager.Bank.SelectById((Guid) companyLegalAccount.BankID).Title;
                    }
                    if (!string.IsNullOrEmpty(companyLegalAccount.HeadSignatureFileName))
                    {
                        var fsp = new FileSystemProvider();
                        ((RadBinaryImage)item.FindControl("rbiHeadSignature")).ImageUrl =
                            fsp.GetLink(CurrentUser.Instance.SiteID, "CompanyLegalAccounts", companyLegalAccount.HeadSignatureFileName, FileType.Attachment);
                    }
                    if (!string.IsNullOrEmpty(companyLegalAccount.StampFileName))
                    {
                        var fsp = new FileSystemProvider();
                        ((RadBinaryImage)item.FindControl("rbiStamp")).ImageUrl =
                            fsp.GetLink(CurrentUser.Instance.SiteID, "CompanyLegalAccounts", companyLegalAccount.StampFileName, FileType.Attachment);
                    }
                    if (!string.IsNullOrEmpty(companyLegalAccount.AccountantSignatureFileName))
                    {
                        var fsp = new FileSystemProvider();
                        ((RadBinaryImage)item.FindControl("rbiAccountantSignature")).ImageUrl =
                            fsp.GetLink(CurrentUser.Instance.SiteID, "CompanyLegalAccounts", companyLegalAccount.AccountantSignatureFileName, FileType.Attachment);
                    } 
                }
            }
        }        



        /// <summary>
        /// Handles the DeleteCommand event of the rgCompanyLegalAccounts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgCompanyLegalAccounts_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());

            var companyLegalAccount = CompanyLegalAccountList.FirstOrDefault(s => s.ID == id);

            if (companyLegalAccount != null && !string.IsNullOrEmpty(companyLegalAccount.HeadSignatureFileName))
            {
                var fsp = new FileSystemProvider();
                fsp.Delete(CurrentUser.Instance.SiteID, "CompanyLegalAccounts", companyLegalAccount.HeadSignatureFileName, FileType.Attachment);
            }

            if (companyLegalAccount != null && !string.IsNullOrEmpty(companyLegalAccount.AccountantSignatureFileName))
            {
                var fsp = new FileSystemProvider();
                fsp.Delete(CurrentUser.Instance.SiteID, "CompanyLegalAccounts", companyLegalAccount.AccountantSignatureFileName, FileType.Attachment);
            }

            CompanyLegalAccountList.Remove(CompanyLegalAccountList.FirstOrDefault(s => s.ID == id));

            if (CompanyLegalAccountsChanged != null)
                CompanyLegalAccountsChanged(this, new EventArgs());
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgCompanyLegalAccounts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgCompanyLegalAccounts_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            
            SaveToViewState(Guid.Empty, item);

            rgCompanyLegalAccounts.MasterTableView.IsItemInserted = false;
            rgCompanyLegalAccounts.Rebind();
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgCompanyLegalAccounts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgCompanyLegalAccounts_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }




        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="orderProductId">The order product id.</param>
        /// <param name="item">The item.</param>
        private void SaveToViewState(Guid orderProductId, GridEditableItem item)
        {
            var companyLegalAccount = ((List<CompanyLegalAccountMap>)ViewState["CompanyLegalAccounts"]).FirstOrDefault(s => s.ID == orderProductId) ?? new CompanyLegalAccountMap();

            companyLegalAccount.Title = ((TextBox)item.FindControl("txtTitle")).Text;
            companyLegalAccount.OfficialTitle = ((TextBox)item.FindControl("txtOfficialTitle")).Text;
            companyLegalAccount.LegalAddress = ((TextBox)item.FindControl("txtLegalAddress")).Text;
            companyLegalAccount.RegistrationDate = ((RadDatePicker)item.FindControl("rdpRegistrationDate")).SelectedDate;
            companyLegalAccount.OGRN = ((TextBox)item.FindControl("txtOGRN")).Text;
            companyLegalAccount.INN = ((TextBox)item.FindControl("txtINN")).Text;
            companyLegalAccount.KPP = ((TextBox)item.FindControl("txtKPP")).Text;
            companyLegalAccount.RS = ((TextBox)item.FindControl("txtRS")).Text;
            companyLegalAccount.IsPrimary = ((CheckBox)item.FindControl("chxIsPrimary")).Checked;
            companyLegalAccount.IsActive = ((CheckBox)item.FindControl("chxIsActive")).Checked;
            companyLegalAccount.HeadID = ((ContactComboBox) item.FindControl("ucHeadContact")).SelectedValue;
            companyLegalAccount.AccountantID = ((ContactComboBox)item.FindControl("ucAccountantContact")).SelectedValue;
            if (((DictionaryOnDemandComboBox)item.FindControl("dcbBanks")).SelectedId != Guid.Empty)
                companyLegalAccount.BankID = ((DictionaryOnDemandComboBox)item.FindControl("dcbBanks")).SelectedId;
            else
                companyLegalAccount.BankID = null;

            var result = ProceedUploadedFile((RadAsyncUpload) item.FindControl("rauHeadSignature"), companyLegalAccount.HeadSignatureFileName);
            if (!string.IsNullOrEmpty(result))
                companyLegalAccount.HeadSignatureFileName = result;

            result = ProceedUploadedFile((RadAsyncUpload)item.FindControl("rauAccountantSignature"), companyLegalAccount.AccountantSignatureFileName);
            if (!string.IsNullOrEmpty(result))
                companyLegalAccount.AccountantSignatureFileName = result;

            result = ProceedUploadedFile((RadAsyncUpload)item.FindControl("rauStamp"), companyLegalAccount.StampFileName);
            if (!string.IsNullOrEmpty(result))
                companyLegalAccount.StampFileName = result;

            
            if (companyLegalAccount.ID == Guid.Empty)
            {
                companyLegalAccount.ID = Guid.NewGuid();
                companyLegalAccount.SiteID = CurrentUser.Instance.SiteID;
                ((List<CompanyLegalAccountMap>) ViewState["CompanyLegalAccounts"]).Add(companyLegalAccount);
            }

            if (CompanyLegalAccountsChanged != null)
                CompanyLegalAccountsChanged(this, new EventArgs());
        }



        /// <summary>
        /// Proceeds the uploaded file.
        /// </summary>
        /// <param name="radFileUpload">The RAD file upload.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        protected string ProceedUploadedFile(RadAsyncUpload radFileUpload, string fileName)
        {
            if (radFileUpload.UploadedFiles.Count > 0)
            {
                var fsp = new FileSystemProvider();
                if (!string.IsNullOrEmpty(fileName))
                {
                    try
                    {
                        fsp.Delete(CurrentUser.Instance.SiteID, "CompanyLegalAccounts", fileName, FileType.Attachment);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Ошибка удаления файла", ex);
                    }
                }

                var bmp = new Bitmap(radFileUpload.UploadedFiles[0].InputStream);
                bmp.MakeTransparent();
                var filter = new ExtractBiggestBlob();
                var biggestBlobsImage = filter.Apply(bmp);
                var ms = new MemoryStream();
                biggestBlobsImage.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                return fsp.Upload(CurrentUser.Instance.SiteID, "CompanyLegalAccounts", Path.ChangeExtension(radFileUpload.UploadedFiles[0].FileName, "png"), ms, FileType.Attachment);
            }

            return string.Empty;
        }
    }
}
