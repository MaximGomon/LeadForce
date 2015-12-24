using System;
using System.ComponentModel;
using System.Linq;
using Labitec.UI.Address.Controls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using Labitec.UI.Address;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class Address : System.Web.UI.UserControl
    {
        private DataManager _dataManager;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? AddressId
        {
            get
            {
                object o = ViewState["AddressId"];
                return (o == null ? null : (Guid?)o);
            }
            set
            {
                ViewState["AddressId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid CompanyId
        {
            get
            {
                object o = ViewState["CompanyId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["CompanyId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid ContactId
        {
            get
            {
                object o = ViewState["ContactId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["ContactId"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataManager = ((LeadForceBasePage)Page).DataManager;
            lbcAddress.NeedCountries += lbcAddress_NeedCountries;
            lbcAddress.NeedRegions += lbcAddress_NeedRegions;
            lbcAddress.NeedDistricts += lbcAddress_NeedDistricts;
            lbcAddress.NeedCities += lbcAddress_NeedCities;
            lbcAddress.SelectedIndexChanged += lbcAddress_SelectedIndexChanged;
        }        



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            if (AddressId.HasValue)
            {
                _dataManager = ((LeadForceBasePage)Page).DataManager;
                var address = _dataManager.Address.SelectById((Guid)AddressId);
                if (address != null)
                {
                    lbcAddress.DefaultAddress = address.Address;

                    if (address.CountryID.HasValue)
                    {
                        var country = _dataManager.Country.SelectById(address.CountryID.Value);
                        rcbCountry.SelectedValue = country.ID.ToString();
                        rcbCountry.Text = country.Name;
                    }

                    if (address.RegionID.HasValue)
                    {
                        var region = _dataManager.Region.SelectById(address.RegionID.Value);
                        rcbRegion.SelectedValue = region.ID.ToString();
                        rcbRegion.Text = region.Name;
                    }


                    if (address.DistrictID.HasValue)
                    {
                        var district = _dataManager.District.SelectById(address.DistrictID.Value);
                        rcbDistrict.SelectedValue = district.ID.ToString();
                        rcbDistrict.Text = district.Name;
                    }

                    if (address.CityID.HasValue)
                    {
                        var city = _dataManager.City.SelectById(address.CityID.Value);
                        rcbCity.SelectedValue = city.ID.ToString();
                        rcbCity.Text = city.Name;
                    }

                    //BindСountries();
                    //rcbCountry.SelectedIndex = rcbCountry.Items.FindItemIndexByValue(address.CountryID.ToString());
                    //BindRegions(address.CountryID);
                    //rcbRegion.SelectedIndex = rcbRegion.Items.FindItemIndexByValue(address.RegionID.ToString());
                    //BindDistricts(address.CountryID);
                    //rcbDistrict.SelectedIndex = rcbDistrict.Items.FindItemIndexByValue(address.DistrictID.ToString());
                    //BindCities(address.CountryID);
                    //rcbCity.SelectedIndex = rcbCity.Items.FindItemIndexByValue(address.CityID.ToString());
                }
            }
        }



        /// <summary>
        /// LBCs the address selected index changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void lbcAddress_SelectedIndexChanged(object sender)
        {
            
            var selectedAddress = lbcAddress.SelectedAddress;

            rcbCountry.SelectedValue = selectedAddress.Country.Id.ToString();
            rcbCountry.Text = selectedAddress.Country.Title;

            rcbRegion.SelectedValue = selectedAddress.Region.Id.ToString();
            rcbRegion.Text = selectedAddress.Region.Title;            

            rcbDistrict.SelectedValue = selectedAddress.District.Id.ToString();
            rcbDistrict.Text = selectedAddress.District.Title;

            rcbCity.SelectedValue = selectedAddress.City.Id.ToString();
            rcbCity.Text = selectedAddress.City.Title;


            //BindСountries();                        
            //rcbCountry.SelectedIndex = rcbCountry.Items.FindItemIndexByValue(selectedAddress.Country.Id.ToString());
            //if (selectedAddress.Country.Id != Guid.Empty)
            //{
            //    BindRegions(selectedAddress.Country.Id);
            //    BindDistricts(selectedAddress.Country.Id);
            //    BindCities(selectedAddress.Country.Id);               
            //}
            //rcbRegion.SelectedIndex = rcbRegion.Items.FindItemIndexByValue(selectedAddress.Region.Id.ToString());                        
            //rcbDistrict.SelectedIndex = rcbDistrict.Items.FindItemIndexByValue(selectedAddress.District.Id.ToString());
            //rcbCity.SelectedIndex = rcbCity.Items.FindItemIndexByValue(selectedAddress.City.Id.ToString());            
        }



        /// <summary>
        /// LBCs the address need countries.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void lbcAddress_NeedCountries(object sender)
        {
            ((AddressComboBox) sender).Countries = _dataManager.Country.SelectAll().Select(country => new Country() {Id = country.ID, Title = country.Name}).ToList();
       }



        /// <summary>
        /// LBCs the address need regions.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void lbcAddress_NeedRegions(object sender)
        {            
            ((AddressComboBox) sender).Regions = _dataManager.Region.SelectAll().Select(region => new Region() {Id = region.ID, Title = region.Name, CountryId = region.CountryID}).ToList();
        }



        /// <summary>
        /// LBCs the address_ need districts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        void lbcAddress_NeedDistricts(object sender)
        {
            ((AddressComboBox)sender).Districts = _dataManager.District.SelectAll().Select(district => new District() { Id = district.ID, Title = district.Name, CountryId = district.CountryID, RegionId = district.RegionID }).ToList();            
        }



        /// <summary>
        /// LBCs the address_ need cities.
        /// </summary>
        /// <param name="sender">The sender.</param>
        protected void lbcAddress_NeedCities(object sender)
        {
            ((AddressComboBox)sender).Cities = _dataManager.City.SelectAll().Select(city => new City() { Id = city.ID, RegionId = city.RegionID, DistrictId = city.DistrictID, Title = city.Name }).ToList();
        }



        /// <summary>
        /// Binds the cities.
        /// </summary>
        /// <param name="countryId">The country id.</param>
        protected void BindCities(Guid? countryId = null)
        {
            rcbCity.Items.Clear();
            rcbCity.DataSource = countryId == null ? _dataManager.City.SelectAll() : _dataManager.City.SelectByCountryId((Guid)countryId);
            rcbCity.DataValueField = "ID";
            rcbCity.DataTextField = "Name";
            rcbCity.DataBind();
            rcbCity.Items.Insert(0, new RadComboBoxItem("", "") { Selected = true });
        }



        /// <summary>
        /// Binds the districts.
        /// </summary>
        /// <param name="countryId">The country id.</param>
        /// <param name="regionId">The region id.</param>
        protected void BindDistricts(Guid? countryId = null, Guid? regionId = null)
        {
            rcbDistrict.Items.Clear();
            rcbDistrict.DataSource = countryId == null ? _dataManager.District.SelectAll() : _dataManager.District.SelectByCountryAndRegionId(countryId, regionId);
            rcbDistrict.DataValueField = "ID";
            rcbDistrict.DataTextField = "Name";
            rcbDistrict.DataBind();
            rcbDistrict.Items.Insert(0, new RadComboBoxItem("", "") { Selected = true });
        }



        /// <summary>
        /// Binds the regions.
        /// </summary>
        /// <param name="countryId">The country id.</param>
        protected void BindRegions(Guid? countryId = null)
        {
            rcbRegion.Items.Clear();
            rcbRegion.DataSource = countryId == null ? _dataManager.Region.SelectAll() : _dataManager.Region.SelectByCountryId((Guid)countryId);
            rcbRegion.DataValueField = "ID";
            rcbRegion.DataTextField = "Name";
            rcbRegion.DataBind();
            rcbRegion.Items.Insert(0, new RadComboBoxItem("", "") { Selected = true });
        }



        /// <summary>
        /// Binds the сountries.
        /// </summary>
        protected void BindСountries()
        {
            rcbCountry.Items.Clear();
            rcbCountry.DataSource = _dataManager.Country.SelectAll();
            rcbCountry.DataValueField = "ID";
            rcbCountry.DataTextField = "Name";
            rcbCountry.DataBind();
            rcbCountry.Items.Insert(0, new RadComboBoxItem("", "") { Selected = true });
        }



        /// <summary>
        /// Saves this instance.
        /// </summary>
        public Guid? Save()
        {
            var address = _dataManager.Address.SelectById(AddressId ?? Guid.Empty) ?? new tbl_Address();
            address.Address = lbcAddress.Text;
            if (!string.IsNullOrEmpty(rcbCountry.SelectedValue) && Guid.Parse(rcbCountry.SelectedValue) != Guid.Empty)
            {
                address.CountryID = Guid.Parse(rcbCountry.SelectedValue);
                address.RegionID = rcbRegion.SelectedValue != string.Empty && Guid.Parse(rcbRegion.SelectedValue) != Guid.Empty ? (Guid?)Guid.Parse(rcbRegion.SelectedValue) : null;
                address.DistrictID = rcbDistrict.SelectedValue != string.Empty && Guid.Parse(rcbDistrict.SelectedValue) != Guid.Empty ? (Guid?)Guid.Parse(rcbDistrict.SelectedValue) : null;
                address.CityID = rcbCity.SelectedValue != string.Empty && Guid.Parse(rcbCity.SelectedValue) != Guid.Empty ? (Guid?)Guid.Parse(rcbCity.SelectedValue) : null;

                if (address.ID == Guid.Empty)
                {
                    address.ID = Guid.NewGuid();
                    AddressId = _dataManager.Address.Add(address);
                }
                else
                {
                    _dataManager.Address.Update(address);
                    AddressId = address.ID;
                }
            }

            return AddressId;
        }
    }
}