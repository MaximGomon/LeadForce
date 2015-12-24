using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class Prompt : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Page_Init(object sender, EventArgs e)
        {
            var a = ((LeadForceBasePage)Page).Code;
            edsPrompts.WhereParameters.Clear();
            if (a != "")
            {
                edsPrompts.Where = "it.[Code] = '" + a + "'";
            }
        }

        protected void rrPrompt_OnDataBound(object sender, EventArgs e)
        {
            if (rrPrompt.Items.Count==0)
            {
                rrPrompt.Parent.Parent.Visible = false;
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

        private void AddNavigationButtons()
        {
            foreach (RadRotatorItem item in rrPrompt.Items)
            {
                LinkButton linkButton = CreateLinkButton(item.Index, "Mouseclick");
                plNavigation.Controls.Add(linkButton);
            }
        }

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