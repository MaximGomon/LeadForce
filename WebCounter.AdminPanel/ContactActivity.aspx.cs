using System;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel
{
    public partial class ContactActivity : LeadForceBasePage
    {
        private Guid _contactID;


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Лог действий на сайте - LeadForce";

            var contactId = Page.RouteData.Values["id"] as string;

            if (contactId != "null" && !Guid.TryParse(contactId, out _contactID))
            {
                Response.Redirect(UrlsData.AP_Contacts());
            }

            if (!Page.IsPostBack)
            {
                if (_contactID != Guid.Empty)
                {
                    var contact = DataManager.Contact.SelectById(SiteId, _contactID);
                    lUserFullName.Text = contact.UserFullName;
                    lScore.Text = contact.Score.ToString();
                    lBehaviorScore.Text = contact.BehaviorScore.ToString();
                    lCharacteristicsScore.Text = contact.CharacteristicsScore.ToString();
                    pUserInfo.Visible = true;
                }                                
            }
        }
    }
}