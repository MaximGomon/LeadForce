using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using System.Data;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class StatsSiteActionTemplate : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();
        private Guid _siteId;



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteActionTemplateId
        {
            get
            {
                if (ViewState["SiteActionTemplateId"] == null)
                    ViewState["SiteActionTemplateId"] = Guid.Empty;

                return (Guid)ViewState["SiteActionTemplateId"];
            }
            set
            {
                ViewState["SiteActionTemplateId"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _siteId = CurrentUser.Instance.SiteID;

            var siteActions = _dataManager.SiteAction.SelectBySiteActionTemplateID(_siteId, SiteActionTemplateId).Where(a => (ActionStatus)a.ActionStatusID == ActionStatus.Done).ToList();
            if (siteActions != null)
            {
                var sendingCount = siteActions.Count;
                var resoponseCount = siteActions.Count(a => a.ResponseDate != null);
                litSending.Text = sendingCount.ToString();
                litResponse.Text = resoponseCount.ToString();
                if (resoponseCount != 0)
                    litConversion.Text = string.Format("{0}%", Math.Round(((double)resoponseCount / sendingCount) * 100, 2));

                //BindActionLinks();
            }
        }



        /// <summary>
        /// Handles the OnNeedDataSource event of the rgStats control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgStats_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                var siteActionLinks =
                    _dataManager.SiteActionLink.SelectByActionTemplateID(SiteActionTemplateId)
                        .Where(a => a.ActionLinkDate != null)
                        .GroupBy(a => new { a.SiteActivityRuleID, a.LinkURL })
                        .Select(a => new { a.Key.SiteActivityRuleID, a.Key.LinkURL, Count = a.Count() });

                rgStats.DataSource = siteActionLinks;
            }
        }



        /// <summary>
        /// Handles the OnDetailTableDataBind event of the rgStats control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridDetailTableDataBindEventArgs"/> instance containing the event data.</param>
        protected void rgStats_OnDetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            var item = (GridDataItem)e.DetailTableView.ParentItem;
            Guid? siteActivityRuleId = null;
            string linkUrl = null;

            if (item.GetDataKeyValue("SiteActivityRuleID") != null)
                siteActivityRuleId = item.GetDataKeyValue("SiteActivityRuleID").ToString().ToGuid();
            if (item.GetDataKeyValue("LinkURL") != null)
                linkUrl = item.GetDataKeyValue("LinkURL").ToString();

            var siteActionLinks =
                _dataManager.SiteActionLink.SelectByActionTemplateID(SiteActionTemplateId).Where(
                    a => a.SiteActivityRuleID == siteActivityRuleId && a.LinkURL == linkUrl && a.ActionLinkDate != null);

            e.DetailTableView.DataSource = siteActionLinks;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgStats control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgStats_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (e.Item.OwnerTableView.Name == "Detail")
                {
                    var item = (tbl_SiteActionLink)e.Item.DataItem;
                    var hlUserFullName = ((HyperLink)e.Item.FindControl("hlUserFullName"));
                    if (!string.IsNullOrEmpty(item.tbl_Contact.UserFullName))
                        ((Literal)e.Item.FindControl("litContact")).Text = string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", UrlsData.AP_Contact(item.tbl_Contact.ID), item.tbl_Contact.UserFullName);
                    else
                        ((Literal)e.Item.FindControl("litContact")).Text = string.Format("&lt;<a href=\"{0}\" target=\"_blank\">Незнакомец</a> от {1}&gt;", UrlsData.AP_Contact(item.tbl_Contact.ID), item.tbl_Contact.CreatedAt.ToString("dd.MM.yyyy HH:mm"));
                }
                else
                {
                    dynamic siteActionLink = e.Item.DataItem;
                    if (siteActionLink.SiteActivityRuleID != null)
                    {
                        var siteActivityRule = _dataManager.Links.SelectById((Guid)siteActionLink.SiteActivityRuleID);
                        if (siteActivityRule != null)
                        {
                            ((HyperLink)e.Item.FindControl("hlLink")).Text = siteActivityRule.Code;
                            ((HyperLink)e.Item.FindControl("hlLink")).NavigateUrl = UrlsData.AP_SiteActivityRule(siteActivityRule.ID, siteActivityRule.RuleTypeID);
                        }
                    }
                    else if (!string.IsNullOrEmpty(siteActionLink.LinkURL))
                    {
                        ((HyperLink)e.Item.FindControl("hlLink")).Text = siteActionLink.LinkURL;
                        ((HyperLink)e.Item.FindControl("hlLink")).NavigateUrl = siteActionLink.LinkURL;
                    }   
                }
            }
        }
    }
}