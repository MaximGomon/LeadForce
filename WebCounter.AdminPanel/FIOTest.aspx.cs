using System;
using System.Configuration;
using System.Web.UI.WebControls;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel
{
    public partial class FIOTest : LeadForceBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            plError.Visible = false;
            plSuccess.Visible = false;

            //var locationDetect = new LocationDetection(ConfigurationManager.AppSettings["ADONETConnectionString"]);
            //lrlIP.Text = Request.UserHostAddress;

            //if (locationDetect.Detect(Request.UserHostAddress))
            //{
            //    lrlCountry.Text = locationDetect.LocationInfo.Country;
            //    lrlRegion.Text = locationDetect.LocationInfo.Region;
            //    lrlDistrict.Text = locationDetect.LocationInfo.District;
            //    lrlCity.Text = locationDetect.LocationInfo.City;
            //    if (locationDetect.LocationInfo.Latitude.HasValue)
            //        lrlLatitude.Text = locationDetect.LocationInfo.Latitude.ToString();
            //    if (locationDetect.LocationInfo.Longitude.HasValue)
            //        lrlLongitude.Text = locationDetect.LocationInfo.Longitude.ToString();
            //}

            //if (!Page.IsPostBack)
            //{
            //    rblFormat.Items.Clear();
            //    foreach (var namecheckerFormat in EnumHelper.EnumToList<NameCheckerFormat>())
            //        rblFormat.Items.Add(new ListItem(EnumHelper.GetEnumDescription(namecheckerFormat), ((int)namecheckerFormat).ToString()));

            //    rblCorrection.Items.Clear();
            //    foreach (var correction in EnumHelper.EnumToList<Correction>())
            //        rblCorrection.Items.Add(new ListItem(EnumHelper.GetEnumDescription(correction), ((int)correction).ToString()));

            //    rblCorrection.SelectedIndex = 0;
            //    rblFormat.SelectedIndex = 0;
            //}

            //DataManager.Contact.CheckNames(SiteId);
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnCheck control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnCheck_OnClick(object sender, EventArgs e)
        {
            var nameChecker = new NameChecker(ConfigurationManager.AppSettings["ADONETConnectionString"]);

            var result = nameChecker.CheckName(txtFIO.Text, (NameCheckerFormat)int.Parse(rblFormat.SelectedValue), (Correction)int.Parse(rblCorrection.SelectedValue));

            if (!string.IsNullOrEmpty(result))
            {
                lrlResult.Text = result;
                lrlGender.Text = nameChecker.Gender.ToString();
                lrlSurname.Text = nameChecker.Surname;
                lrlName.Text = nameChecker.Name;
                lrlPatronymic.Text = nameChecker.Patronymic;
                lrlIsNameCorrect.Text = nameChecker.IsNameCorrect.ToString();
                plSuccess.Visible = true;
            }
            else
            {
                plError.Visible = true;
            }
        }
    }
}