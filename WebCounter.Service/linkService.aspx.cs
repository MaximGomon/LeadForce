using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.Service
{
    public partial class linkService : System.Web.UI.Page
    {
        private DataManager dataManager = new DataManager();
        private DateTime datetimeNow;


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid _siteActionLinkID;

            if (Guid.TryParse(Request.QueryString["ID"], out _siteActionLinkID))
            {
                var siteActionLink = dataManager.SiteActionLink.SelectById(_siteActionLinkID);
                var siteID = siteActionLink.tbl_Contact.SiteID;
                var site = dataManager.Sites.SelectById(siteID);

                if (!string.IsNullOrEmpty(site.LinkProcessingURL))
                    Response.Redirect(site.LinkProcessingURL + "#lg:" + _siteActionLinkID);
                else
                {
                    var linkProcessing = CounterServiceHelper.LinkProcessing(_siteActionLinkID);
                    if (linkProcessing != null)
                    {
                        var activityCode = string.Empty;
                        var redirectUrl = string.Empty;
                        var activityType = ActivityType.Link;

                        if (linkProcessing.SiteActivityRuleID != null)
                        {
                            activityCode = linkProcessing.SiteActivityRuleCode;
                            redirectUrl = linkProcessing.SiteActivityRuleURL;

                            if ((RuleType)linkProcessing.SiteActivityRuleTypeID == RuleType.File)
                            {
                                activityType = ActivityType.DownloadFile;
                                redirectUrl = string.Format("{0}/DownloadFile.aspx?id={1}", WebConfigurationManager.AppSettings["webServiceUrl"], linkProcessing.SiteActivityRuleID);
                            }
                        }
                        else
                        {
                            activityCode = linkProcessing.LinkURL;
                            redirectUrl = linkProcessing.LinkURL;
                        }

                        CounterServiceHelper.AddContact(linkProcessing.SiteID, linkProcessing.ContactID, activityType, activityCode, "", null, null, null);
                        Response.Redirect(redirectUrl);
                    }
                }
            }
        }
    }
}