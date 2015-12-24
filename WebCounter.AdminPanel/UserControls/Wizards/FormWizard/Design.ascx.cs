using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations.FormCode;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.FormWizard
{
    public partial class Design : FormWizardStep
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public override void BindData()
        {
            base.BindData();

            if (EditObjectId.HasValue)
            {
                var siteActivityRule = DataManager.SiteActivityRules.SelectById(EditObjectId.Value);
                ProceedSiteActivityRule(siteActivityRule);                

                txtFormWidth.Value = siteActivityRule.FormWidth;
                ucCssEditorButton.Css = siteActivityRule.CSSButton;

                var siteActivityRuleLayouts = siteActivityRule.tbl_SiteActivityRuleLayout.Where(o => !string.IsNullOrEmpty(o.LayoutParams)).OrderBy(o => o.Order);
                foreach (var activityRuleLayout in siteActivityRuleLayouts)
                {
                    var layoutParams = LayoutParams.Deserialize(activityRuleLayout.LayoutParams);
                    if (!string.IsNullOrEmpty(layoutParams.GetValue("ShowInMaster")) && int.Parse(layoutParams.GetValue("ShowInMaster")) > 1)
                    {
                        if (!string.IsNullOrEmpty(activityRuleLayout.CSSStyle))
                        {
                            ucCssEditorInstruction.Css = activityRuleLayout.CSSStyle;
                            break;
                        }
                    }
                }

                siteActivityRuleLayouts = siteActivityRule.tbl_SiteActivityRuleLayout.Where(o => string.IsNullOrEmpty(o.LayoutParams) && !string.IsNullOrEmpty(o.CSSStyle)).OrderBy(o => o.Order);
                if (siteActivityRuleLayouts.Any())
                {
                    var tblSiteActivityRuleLayout = siteActivityRuleLayouts.FirstOrDefault();
                    if (tblSiteActivityRuleLayout != null)
                        ucCssEditorColumns.Css = tblSiteActivityRuleLayout.CSSStyle;
                }

                DecodeCss(siteActivityRule.CSSForm);
            }
            else            
                ProceedSiteActivityRule(DataManager.SiteActivityRules.SelectById(CurrentForm));            

            ucCssEditorButton.BindData();
            ucCssEditorColumns.BindData();
            ucCssEditorInstruction.BindData();
        }



        /// <summary>
        /// Proceeds the site activity rule.
        /// </summary>
        /// <param name="siteActivityRule">The site activity rule.</param>
        private void ProceedSiteActivityRule(tbl_SiteActivityRules siteActivityRule)
        {
            if (siteActivityRule == null)
                return;

            if (siteActivityRule.tbl_SiteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.Feedback))
            {
                plColumns.Visible = false;
                plInstruction.Visible = false;
            }
            if (siteActivityRule.tbl_SiteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.InviteFriend))
                plColumns.Visible = false;                
        }        



        /// <summary>
        /// Decodes the CSS.
        /// </summary>
        /// <param name="css">The CSS.</param>
        protected void DecodeCss(string css)
        {
            if (string.IsNullOrEmpty(css))
                return;

            var attributes = css.Split(new[] { ';' });
            foreach (var attribute in attributes)
            {
                var attr = attribute.Split(new[] { ':' });
                switch (attr[0])
                {                    
                    case "background-color":
                        rcpBackgroundColor.SelectedColor = ColorTranslator.FromHtml(attr[1]);
                        break;
                }
            }
        }
    }
}