using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class CompanyComboBox : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid CurrentCompanyId
        {
            get
            {
                object o = ViewState["CurrentCompanyId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["CurrentCompanyId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Unit Width
        {
            get { return rcbCompany.Width; }
            set { rcbCompany.Width = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool Enabled
        {
            get { return rcbCompany.Enabled; }
            set { rcbCompany.Enabled = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool AllowCustomText
        {
            get { return rcbCompany.AllowCustomText; }
            set { rcbCompany.AllowCustomText = value; }
        }

        public string Text
        {
            get { return rcbCompany.Text == "Выберите значение" ? string.Empty : rcbCompany.Text; }
        }


        public Guid? SelectedValue
        {
            get
            {
                if (!string.IsNullOrEmpty(rcbCompany.SelectedValue))
                    return Guid.Parse(rcbCompany.SelectedValue);

                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    var company = DataManager.Company.SelectById(CurrentUser.Instance.SiteID, (Guid)value);
                    if (company != null)
                    {
                        rcbCompany.Items.Add(new RadComboBoxItem(company.Name, company.ID.ToString()) { Selected = true });
                    }
                }
                else
                {
                    rcbCompany.ClearSelection();
                    rcbCompany.Items.Clear();                    
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {            
            rcbCompany.EmptyMessage = "Выберите значение";
        }



        /// <summary>
        /// Handles the ItemsRequested event of the rcbCompany control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs"/> instance containing the event data.</param>
        protected void rcbCompany_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            rcbCompany.Items.Clear();

            var companies = DataManager.Company.SelectAll(CurrentUser.Instance.SiteID);

            if (CurrentCompanyId != Guid.Empty)
                companies = companies.Where(c => c.ID != CurrentCompanyId);            

            if (!string.IsNullOrEmpty(e.Text))
                companies = companies.Where(c => c.Name.ToLower().StartsWith(e.Text.ToLower()));
            var listOfCompanies = companies.ToList();
            var count = listOfCompanies.Count;
            int itemOffset = e.NumberOfItems;
            int endOffset = Math.Min(itemOffset + 15, count);
            e.EndOfItems = endOffset == count;

            for (int i = itemOffset; i < endOffset; i++)            
                rcbCompany.Items.Add(new RadComboBoxItem(listOfCompanies[i].Name, listOfCompanies[i].ID.ToString()));            

            e.Message = GetStatusMessage(endOffset, count);    
        }



        /// <summary>
        /// Gets the status message.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="total">The total.</param>
        /// <returns></returns>
        private static string GetStatusMessage(int offset, int total)
        {
            if (total <= 0)
                return "Пусто";

            return String.Format("Элементы <b>1</b>-<b>{0}</b> из <b>{1}</b>", offset, total);
        }
    }
}