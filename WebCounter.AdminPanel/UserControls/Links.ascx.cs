using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;


namespace WebCounter.AdminPanel.UserControls
{
    public partial class Links : System.Web.UI.UserControl
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            gridLinks.AddNavigateUrl = UrlsData.AP_SiteActivityRuleAdd((int)RuleType.Link);
            gridLinks.SiteID = ((LeadForceBasePage)Page).SiteId;

            gridLinks.Where = new List<GridWhere>();
            gridLinks.Where.Add(new GridWhere { Column = "RuleTypeID", Value = ((int)RuleType.Link).ToString() });                    
        }




        /// <summary>
        /// Handles the OnItemDataBound event of the gridLinks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridLinks_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;
                
                ((Literal)item.FindControl("lSubstitutionCode")).Text = string.Format("&lt;a href=\"javascript:WebCounter.LG_Link('{0}')\"&gt;{1}&lt;/a&gt;", data["tbl_Links_Code"], data["tbl_Links_URL"]);                
                ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = UrlsData.AP_SiteActivityRule(Guid.Parse(data["ID"].ToString()), (int)RuleType.Link);
            }
        }
    }
}