using System;
using System.ComponentModel;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SessionInfo : System.Web.UI.UserControl
    {
        private DataManager dataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SessionId
        {
            get
            {
                object o = ViewState["SessionId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["SessionId"] = value;
            }
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var contactSession = dataManager.ContactSessions.SelectById(((LeadForceBasePage)Page).SiteId, SessionId) ?? new tbl_ContactSessions();

            lrlNumber.Text = contactSession.UserSessionNumber.ToString();
            lrlSessionDate.Text = contactSession.SessionDate.ToString("dd.MM.yyyy HH:mm");
            lrlUserIP.Text = contactSession.UserIP;
            lrlBrowser.Text = contactSession.tbl_Browsers != null ? contactSession.tbl_Browsers.Name + " " + contactSession.tbl_Browsers.Version : string.Empty;            
            lrlOS.Text = contactSession.tbl_OperatingSystems != null ? contactSession.tbl_OperatingSystems.Name + " " + contactSession.tbl_OperatingSystems.Version: string.Empty;
            lrlResolution.Text = contactSession.tbl_Resolutions !=null ? contactSession.tbl_Resolutions.Value : string.Empty;
            lrlMobileDevice.Text = contactSession.tbl_MobileDevices != null ? contactSession.tbl_MobileDevices.Name : string.Empty;
            lrlRefferURL.Text = contactSession.RefferURL;            

            this.DataBind();
        }
    }
}