using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class Company : System.Web.UI.UserControl
    {
        public Access access;
        protected Guid CompanyId;
        public Guid SiteId = new Guid();
        private DataManager dataManager = new DataManager();



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Section Section
        {
            get
            {
                object o = ViewState["Section"];
                return (o == null ? Section.Monitoring : (Section)o);
            }
            set
            {
                ViewState["Section"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteId = ((LeadForceBasePage)Page).SiteId;

            ((Site)Page.Master).HideInaccessibleTabs(ref RadTabStrip1, ref RadMultiPage1);

            access = Access.Check();
            if (!access.Write)
                lbtnSave.Visible = false;

            var companyId = Page.RouteData.Values["id"] as string;

            hlCancel.NavigateUrl = UrlsData.AP_Companies();

            if (!string.IsNullOrEmpty(companyId))
                CompanyId = Guid.Parse(companyId);

            ucTaskList.CompanyId = CompanyId;
            ucTaskList.SiteId = SiteId;
           
            ucContactList.CompanyId = CompanyId;
            ucContactList.SiteId = SiteId;

            tagsCompany.ObjectID = CompanyId;
            
            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            ucParentCompany.CurrentCompanyId = CompanyId;
            ucOwner.SelectedValue = CurrentUser.Instance.ContactID;
            BindStatus();
            BindCompanyTypes();
            BindCompanySizes();
            BindCompanySectors();

            foreach (var emailStatus in EnumHelper.EnumToList<EmailStatus>())
                ddlEmailStatus.Items.Add(new ListItem(EnumHelper.GetEnumDescription(emailStatus), ((int)emailStatus).ToString()));

            var company = dataManager.Company.SelectById(SiteId, CompanyId);

            if (company != null)
            {
                ucCompanyLegalAccount.CompanyId = company.ID;
                txtName.Text = company.Name;
                ucParentCompany.SelectedValue = company.ParentID;
                ddlCompanyType.SelectedIndex = ddlCompanyType.Items.IndexOf(ddlCompanyType.Items.FindByValue(company.CompanyTypeID.ToString()));
                ucOwner.SelectedValue  = company.OwnerID;
                if (company.tbl_Priorities != null)
                    lrlPriority.Text = company.tbl_Priorities.Title;
                if (company.tbl_ReadyToSell != null)
                    lrlReadyToSell.Text = company.tbl_ReadyToSell.Title;
                ddlCompanySize.SelectedIndex = ddlCompanySize.Items.IndexOf(ddlCompanySize.Items.FindByValue(company.CompanySizeID.ToString()));
                ddlCompanySector.SelectedIndex = ddlCompanySector.Items.IndexOf(ddlCompanySector.Items.FindByValue(company.CompanySectorID.ToString()));
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(company.StatusID.ToString()));
                txtPhone1.Text = company.Phone1;
                txtPhone2.Text = company.Phone2;
                txtFax.Text = company.Fax;
                txtWeb.Text = company.Web;
                txtEmail.Text = company.Email;
                ddlEmailStatus.SelectedIndex = ddlEmailStatus.Items.IndexOf(ddlEmailStatus.Items.FindByValue(company.EmailStatusID.ToString()));
                lrlCreatedAt.Text = company.CreatedAt.ToString();
                lrlScore.Text = company.Score.ToString();
                lrlBehaviorScore.Text = company.BehaviorScore.ToString();
                lrlCharacteristicsScore.Text = company.CharacteristicsScore.ToString();
                ucLocationAddress.AddressId = company.LocationAddressID;
                ucLocationAddress.BindData();
                ucPostalAddress.AddressId = company.PostalAddressID;
                ucPostalAddress.BindData();
            }
        }



        /// <summary>
        /// Binds the company types.
        /// </summary>
        private void BindCompanyTypes()
        {
            ddlCompanyType.DataSource = dataManager.CompanyType.SelectAll(SiteId);
            ddlCompanyType.DataValueField = "ID";
            ddlCompanyType.DataTextField = "Name";
            ddlCompanyType.DataBind();
            ddlCompanyType.Items.Insert(0, new ListItem("Выберите значение", string.Empty) { Selected = true });
        }



        /// <summary>
        /// Binds the company sizes.
        /// </summary>
        private void BindCompanySizes()
        {
            ddlCompanySize.DataSource = dataManager.CompanySize.SelectAll(SiteId);
            ddlCompanySize.DataValueField = "ID";
            ddlCompanySize.DataTextField = "Name";
            ddlCompanySize.DataBind();
            ddlCompanySize.Items.Insert(0, new ListItem("Выберите значение", string.Empty) { Selected = true });
        }



        /// <summary>
        /// Binds the company sectors.
        /// </summary>
        private void BindCompanySectors()
        {
            ddlCompanySector.DataSource = dataManager.CompanySector.SelectAll(SiteId);
            ddlCompanySector.DataValueField = "ID";
            ddlCompanySector.DataTextField = "Name";
            ddlCompanySector.DataBind();
            ddlCompanySector.Items.Insert(0, new ListItem("Выберите значение", string.Empty) { Selected = true });
        }



        /// <summary>
        /// Binds the status.
        /// </summary>
        private void BindStatus()
        {
            ddlStatus.DataSource = dataManager.Status.SelectAll(SiteId);
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataTextField = "Title";
            ddlStatus.DataBind();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var company = dataManager.Company.SelectById(SiteId, CompanyId) ?? new tbl_Company();

            company.Name = txtName.Text;
            company.ParentID = ucParentCompany.SelectedValue;
            company.CompanyTypeID = ddlCompanyType.SelectedValue != string.Empty ? (Guid?)Guid.Parse(ddlCompanyType.SelectedValue) : null;
            company.OwnerID = ucOwner.SelectedValue;
            company.CompanySizeID = ddlCompanySize.SelectedValue != string.Empty ? (Guid?)Guid.Parse(ddlCompanySize.SelectedValue) : null;
            company.CompanySectorID = ddlCompanySector.SelectedValue != string.Empty ? (Guid?)Guid.Parse(ddlCompanySector.SelectedValue) : null;

            company.StatusID = Guid.Parse(ddlStatus.SelectedValue);
            company.Phone1 = txtPhone1.Text;
            company.Phone2 = txtPhone2.Text;
            company.Fax = txtFax.Text;
            company.Web = txtWeb.Text;
            company.Email = txtEmail.Text;
            company.EmailStatusID = int.Parse(ddlEmailStatus.SelectedValue);
            company.LocationAddressID = ucLocationAddress.Save();
            company.PostalAddressID = ucPostalAddress.Save();

            if (company.ID == Guid.Empty)
            {
                company.SiteID = SiteId;
                company.OwnerID = CurrentUser.Instance.ContactID;
                dataManager.Company.Add(company);
            }
            else
                dataManager.Company.Update(company);

            dataManager.CompanyLegalAccount.Update(ucCompanyLegalAccount.CompanyLegalAccountList, company.ID);

            // Save tags
            tagsCompany.SaveTags(company.ID);

            Response.Redirect(UrlsData.AP_Companies());
        }
    }
}