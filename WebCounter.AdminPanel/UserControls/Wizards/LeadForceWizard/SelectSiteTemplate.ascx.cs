using System;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;

namespace WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard
{
    public partial class SelectSiteTemplate : LeadForceWizardStep
    {        
        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ucSiteTemplateComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucSiteTemplateComboBox_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CurrentSiteTemplate = (Guid)ucSiteTemplateComboBox.SelectedValue;
        }
    }
}