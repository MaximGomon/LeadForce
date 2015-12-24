using System;
using System.Linq;
using Telerik.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard
{
    public partial class SiteTemplateComboBox : System.Web.UI.UserControl
    {
        public event RadComboBoxSelectedIndexChangedEventHandler SelectedIndexChanged;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Unit Width
        {
            get { return rcbSiteTemplate.Width; }
            set { rcbSiteTemplate.Width = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool Enabled
        {
            get { return rcbSiteTemplate.Enabled; }
            set { rcbSiteTemplate.Enabled = value; }
        }


        public Guid? SelectedValue
        {
            get
            {
                if (!string.IsNullOrEmpty(rcbSiteTemplate.SelectedValue))
                    return Guid.Parse(rcbSiteTemplate.SelectedValue);

                return null;
            }
            set { rcbSiteTemplate.SelectedIndex = rcbSiteTemplate.FindItemIndexByValue(value.ToString()); }
        }



        public string SelectedText
        {
            get
            {
                if (rcbSiteTemplate.SelectedItem != null)
                    return rcbSiteTemplate.SelectedItem.Text;

                return string.Empty;
            }
        }




        /// <summary>
        /// Gets or sets the  Validation Group.
        /// </summary>
        /// <value>
        /// The empty item text.
        /// </value>
        public string ValidationGroup
        {
            get
            {
                object o = ViewState["ValidationGroup"];
                return (o == null ? string.Empty : (string)o);
            }
            set { ViewState["ValidationGroup"] = value; }
        }



        /// <summary>
        /// Gets or sets the  Validation Error Message.
        /// </summary>
        /// <value>
        /// The empty item text.
        /// </value>
        public string ValidationErrorMessage
        {
            get
            {
                object o = ViewState["ValidationErrorMessage"];
                return (o == null ? string.Empty : (string)o);
            }
            set { ViewState["ValidationErrorMessage"] = value; }
        }



        /// <summary>
        /// Gets or sets a value indicating whether [auto post back].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [auto post back]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoPostBack
        {
            get { return rcbSiteTemplate.AutoPostBack; }
            set { rcbSiteTemplate.AutoPostBack = value; }
        }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ValidationGroup))
                rfvDictionary.ValidationGroup = ValidationGroup;
            else
                rfvDictionary.Enabled = false;            

            rfvDictionary.ErrorMessage = ValidationErrorMessage;

            if (SelectedIndexChanged != null)
                rcbSiteTemplate.SelectedIndexChanged += rcbSiteTemplate_SelectedIndexChanged;            
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the rcbSiteTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void rcbSiteTemplate_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }
    }
}