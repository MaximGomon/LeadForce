using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations.FormCode;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.FormWizard
{
    public partial class InstructionAndText : FormWizardStep
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

            tbl_SiteActivityRules siteActivityRule = null;

            if (!IsEditMode)
                siteActivityRule = DataManager.SiteActivityRules.SelectById(CurrentForm);
            else if (EditObjectId.HasValue)
            {
                siteActivityRule = DataManager.SiteActivityRules.SelectById(EditObjectId.Value);
                txtFormTitleRequiredFieldValidator.ValidationGroup = "groupEdit";
            }
            else
                return;
            
            if (siteActivityRule == null)
                return;

            var siteActivityRuleLayouts = siteActivityRule.tbl_SiteActivityRuleLayout.Where(o => !string.IsNullOrEmpty(o.LayoutParams)).OrderBy(o => o.Order);

            txtFormTitle.Text = siteActivityRule.Name;
            txtTextButton.Text = siteActivityRule.TextButton;

            foreach (var activityRuleLayout in siteActivityRuleLayouts)
            {
                var layoutParams = LayoutParams.Deserialize(activityRuleLayout.LayoutParams);
                if (!string.IsNullOrEmpty(layoutParams.GetValue("ShowInMaster")))
                {
                    var panel = new Panel() {CssClass = "row"};
                    var label = new HtmlGenericControl {TagName = "label", InnerText = string.Format("{0}:", activityRuleLayout.Name)};                    
                    panel.Controls.Add(label);


                    switch ((ShowTextBlockInMaster)int.Parse(layoutParams.GetValue("ShowInMaster")))
                    {
                        case ShowTextBlockInMaster.Text:
                            var textBox = new TextBox() {ID = activityRuleLayout.ID.ToString(), CssClass = "area-text", TextMode = TextBoxMode.MultiLine, Height = new Unit(30, UnitType.Pixel), Width = new Unit(689, UnitType.Pixel)};
                            textBox.Text = activityRuleLayout.Description;
                            panel.Controls.Add(textBox);
                            break;
                        case ShowTextBlockInMaster.HTML:
                            panel.CssClass = "row-html-editor clearfix";
                            panel.Attributes.Add("style", "margin-bottom:10px");
                            var htmlEditor = new RadEditor()
                                                 {
                                                     ID = activityRuleLayout.ID.ToString(),
                                                     EnableResize = false,
                                                     ToolsFile = "~/RadEditor/Tools.xml",
                                                     CssClass = "rad-editor",
                                                     AutoResizeHeight = false,
                                                     Width = new Unit(700, UnitType.Pixel),
                                                     Height = new Unit(130, UnitType.Pixel),
                                                     Skin = "Windows7",
                                                     ContentAreaMode = EditorContentAreaMode.Iframe
                                                 };
                            htmlEditor.Content = activityRuleLayout.Description;
                            panel.Controls.Add(htmlEditor);
                            break;                            
                    }

                    plTextBlocksContainer.Controls.Add(panel);
                }
            }
        }
    }
}