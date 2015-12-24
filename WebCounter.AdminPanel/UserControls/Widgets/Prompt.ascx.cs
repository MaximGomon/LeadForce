using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel.UserControls.Widgets
{
    public partial class Prompt : WidgetBase
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
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            var a = ((LeadForceBasePage)Page).Code;
            edsPrompts.WhereParameters.Clear();
            if (a != "")
            {
                edsPrompts.Where = "it.[Code] = '" + a + "'";
            }
        }



        /// <summary>
        /// Handles the OnDataBound event of the rrPrompt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rrPrompt_OnDataBound(object sender, EventArgs e)
        {
            if (rrPrompt.Items.Count == 0)
            {                
                FindControlRecursive(Page, WidgetId.ToString()).Visible = false;
            }
            else if (rrPrompt.Items.Count == 1)
            {
                plNavigation.Visible = false;
            }
            else
            {
                AddNavigationButtons();
            }
        }



        /// <summary>
        /// Adds the navigation buttons.
        /// </summary>
        private void AddNavigationButtons()
        {
            foreach (RadRotatorItem item in rrPrompt.Items)
            {
                LinkButton linkButton = CreateLinkButton(item.Index, "Mouseclick");
                plNavigation.Controls.Add(linkButton);
            }
        }



        /// <summary>
        /// Creates the link button.
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <param name="indexChangemode">The index changemode.</param>
        /// <returns></returns>
        private LinkButton CreateLinkButton(int itemIndex, string indexChangemode)
        {
            // Create the LinkButton
            LinkButton button = new LinkButton();
            button.Text = " ";// The test of the button
            button.ID = string.Format("Button{0}", itemIndex);// Assign an unique ID
            button.ClientIDMode = ClientIDMode.Static;
            if (indexChangemode == "Mouseclick")
            {
                // Attach a JavaScript handler to the click event
                button.OnClientClick = string.Format("showItemByIndex({0}); return false;", itemIndex);
            }
            else
            {// Mouseover
                button.Attributes["onmouseover"] = string.Format("showItemByIndex({0}); return false;", itemIndex);
                button.OnClientClick = ";return false;";// Cancel the postback
            }

            // Class which is applied to the newly added button
            button.CssClass = "navigation-btn";
            return button;
        }
    }
}