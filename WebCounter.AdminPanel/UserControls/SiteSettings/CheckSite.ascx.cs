using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using System.Text.RegularExpressions;

namespace WebCounter.AdminPanel.UserControls.SiteSettings
{
    public partial class CheckSite : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();        
        protected RadAjaxManager radAjaxManager = null;
        public event DomainCheckedEventHandler DomainChecked;
        public delegate void DomainCheckedEventHandler(object sender);

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteDomainId
        {
            get
            {
                if (ViewState["SiteDomainId"] == null)
                    ViewState["SiteDomainId"] = Guid.Empty;

                return (Guid) ViewState["SiteDomainId"];
            }
            set { ViewState["SiteDomainId"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool IsCheckOnLoad
        {
            get
            {
                if (ViewState["IsCheckOnLoad"] == null)
                    ViewState["IsCheckOnLoad"] = false;

                return (bool)ViewState["IsCheckOnLoad"];
            }
            set { ViewState["IsCheckOnLoad"] = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }



        protected void lbtnSendRequest_OnClick(object sender, EventArgs e)
        {
            ucNotificationMessage.Text = string.Empty;
            lvResult.DataSource = new List<CheckSiteStatus>();
            lvResult.DataBind();

            try
            {
                Check(DataManager.SiteDomain.SelectById(SiteDomainId));
            }
            catch(Exception ex)
            {
                Log.Error(string.Format("Возникла ошибка проверки домена. SiteDomainId {0}", SiteDomainId), ex);
                ucNotificationMessage.Text = "Возникла ошибка проверки домена.";
                var siteDomain = DataManager.SiteDomain.SelectById(SiteDomainId);
                siteDomain.StatusID = (int)SiteDomainStatus.CheckingFailed;
                DataManager.SiteDomain.Update(siteDomain);
            }

            if (Parent.FindControl("lrlSiteDomainStatus") != null)
            {
                var siteDomain = DataManager.SiteDomain.SelectById(SiteDomainId);
                if (siteDomain != null)
                {
                    ((Literal)Parent.FindControl("lrlSiteDomainStatus")).Text =
                        EnumHelper.GetEnumDescription((SiteDomainStatus)siteDomain.StatusID);
                }
            }

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "HideLoader", string.Format("$('#result{0}').show();$('#startCheckMessage{0}').hide();", ClientID), true);
        }


        /// <summary>
        /// Checks the specified site domain.
        /// </summary>
        /// <param name="siteDomain">The site domain.</param>
        public void Check(tbl_SiteDomain siteDomain)
        {
            if (siteDomain == null)
                return;
            
            lvResult.DataSource = DataManager.SiteDomain.CheckAll(siteDomain);
            lvResult.DataBind();

            if (DomainChecked != null)
                DomainChecked(this);
        }
    }
}