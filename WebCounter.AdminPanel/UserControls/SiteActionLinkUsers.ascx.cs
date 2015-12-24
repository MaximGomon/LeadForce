using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SiteActionLinkUsers : System.Web.UI.UserControl
    {
        private DataManager dataManager = new DataManager();



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteActionTemplateID
        {
            get
            {
                object o = ViewState["SiteActionTemplateID"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["SiteActionTemplateID"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? SiteActivityRuleID
        {
            get
            {
                object o = ViewState["SiteActivityRuleID"];
                return (o == null ? (Guid?)null : (Guid)o);
            }
            set
            {
                ViewState["SiteActivityRuleID"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string LinkURL
        {
            get
            {
                object o = ViewState["LinkURL"];
                return (o == null ? null : (string)o);
            }
            set
            {
                ViewState["LinkURL"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<Guid?> ActionIDs
        {
            get
            {
                object o = ViewState["ActionIDs"];
                return (o == null ? null : (List<Guid?>)o);
            }
            set
            {
                ViewState["ActionIDs"] = value;
            }
        }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var siteActionLinks = dataManager.SiteActionLink.SelectByActionTemplateID(SiteActionTemplateID).Where(a => a.SiteActivityRuleID == SiteActivityRuleID && a.LinkURL == LinkURL && a.ActionLinkDate != null);
            if (ActionIDs != null && ActionIDs.Count > 0)
                siteActionLinks = siteActionLinks.Where(a => a.SiteActionID != null && ActionIDs.Contains(a.SiteActionID));

            rgSiteActionLinkUsers.DataSource = siteActionLinks;
            rgSiteActionLinkUsers.DataBind();
        }



        /// <summary>
        /// Handles the ItemDataBound event of the rgSiteActionLinkUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgSiteActionLinkUsers_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                var item = (tbl_SiteActionLink)e.Item.DataItem;

                if (item != null)
                {
                    if (!string.IsNullOrEmpty(item.tbl_Contact.UserFullName))
                        ((Literal)e.Item.FindControl("litContact")).Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(item.tbl_Contact.ID), item.tbl_Contact.UserFullName);
                    else
                        ((Literal)e.Item.FindControl("litContact")).Text = string.Format("&lt;<a href=\"{0}\">Незнакомец</a> от {1}&gt;", UrlsData.AP_Contact(item.tbl_Contact.ID), item.tbl_Contact.CreatedAt.ToString("dd.MM.yyyy HH:mm"));
                }
            }
        }
    }
}