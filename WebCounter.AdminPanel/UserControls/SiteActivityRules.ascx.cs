using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using System.Data;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SiteActivityRules : System.Web.UI.UserControl
    {
        private DataManager dataManager = new DataManager();
        public Guid siteID = new Guid();

        public int _ruleTypeId = 0;

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

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string SectionTitle
        {
            get
            {
                object o = ViewState["SectionTitle"];
                return (o == null ? null : (string)o);
            }
            set
            {
                ViewState["SectionTitle"] = value;
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            siteID = ((LeadForceBasePage)Page).SiteId;

            string ruleTypeId = Page.RouteData.Values["ruletypeid"] as string;

            int.TryParse(ruleTypeId, out _ruleTypeId);

            gridSiteActivityRules.AddNavigateUrl = UrlsData.AP_SiteActivityRuleAdd(_ruleTypeId);
            gridSiteActivityRules.SiteID = siteID;

            gridSiteActivityRules.Where = new List<GridWhere>();
            switch ((RuleType)_ruleTypeId)
            {
                case RuleType.Link:
                    gridSiteActivityRules.Where.Add(new GridWhere { Column = "RuleTypeID", Value = ((int)RuleType.Link).ToString() });
                    break;
                case RuleType.Form:
                    gridSiteActivityRules.Where.Add(new GridWhere { Column = "RuleTypeID", Value = ((int)RuleType.Form).ToString() });
                    break;
                case RuleType.File:
                    gridSiteActivityRules.Where.Add(new GridWhere { Column = "RuleTypeID", Value = ((int)RuleType.File).ToString() });
                    break;
                case RuleType.ExternalForm:
                    gridSiteActivityRules.Where.Add(new GridWhere { Column = "RuleTypeID", Value = ((int)RuleType.ExternalForm).ToString() });
                    break;
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridSiteActivityRules control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridSiteActivityRules_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                if ((RuleType)int.Parse(data["tbl_SiteActivityRules_RuleTypeID"].ToString()) == RuleType.Link
                    || (RuleType)int.Parse(data["tbl_SiteActivityRules_RuleTypeID"].ToString()) == RuleType.File)
                {
                    ((Literal)item.FindControl("lSubstitutionCode")).Text = string.Format("&lt;a href=\"javascript:WebCounter.LG_Link('{0}')\"&gt;{1}&lt;/a&gt;", data["tbl_SiteActivityRules_Code"], data["tbl_SiteActivityRules_URL"]);
                }

                if ((RuleType)int.Parse(data["tbl_SiteActivityRules_RuleTypeID"].ToString()) == RuleType.Form)
                {
                    var aGetCode = (HtmlAnchor)item.FindControl("aGetCode");
                    aGetCode.Visible = true;
                    aGetCode.Attributes.Add("onclick", string.Format("openRadWindow('{0}'); return false;", data["tbl_SiteActivityRules_Code"]));
                }

                ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = _ruleTypeId != 0 ? UrlsData.AP_SiteActivityRule(Guid.Parse(data["ID"].ToString()), _ruleTypeId) : UrlsData.AP_SiteActivityRule(Guid.Parse(data["ID"].ToString()), int.Parse(data["tbl_SiteActivityRules_RuleTypeID"].ToString()));
            }
        }
    }
}