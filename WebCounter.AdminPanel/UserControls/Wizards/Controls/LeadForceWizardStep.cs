using System;
using System.Web;
using WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard;

namespace WebCounter.AdminPanel.UserControls.Wizards.Controls
{
    public class LeadForceWizardStep : BaseWizardStep
    {        
        /// <summary>
        /// Gets or sets the current lead force product.
        /// </summary>
        /// <value>
        /// The current lead force product.
        /// </value>
        protected Guid CurrentLeadForceProduct
        {
            get
            {
                if (HttpContext.Current.Session["CurrentLeadForceProduct"] == null)
                    HttpContext.Current.Session["CurrentLeadForceProduct"] = Guid.Empty;

                return (Guid) HttpContext.Current.Session["CurrentLeadForceProduct"];
            }
            set { HttpContext.Current.Session["CurrentLeadForceProduct"] = value; }
        }



        /// <summary>
        /// Gets or sets the current lead force edition.
        /// </summary>
        /// <value>
        /// The current lead force edition.
        /// </value>
        protected Guid CurrentLeadForceEdition
        {
            get
            {
                if (HttpContext.Current.Session["CurrentLeadForceEdition"] == null)
                    HttpContext.Current.Session["CurrentLeadForceEdition"] = Guid.Empty;

                return (Guid)HttpContext.Current.Session["CurrentLeadForceEdition"];
            }
            set { HttpContext.Current.Session["CurrentLeadForceEdition"] = value; }
        }



        /// <summary>
        /// Gets or sets the current site template.
        /// </summary>
        /// <value>
        /// The current site template.
        /// </value>
        protected Guid CurrentSiteTemplate
        {
            get
            {
                if (HttpContext.Current.Session["CurrentSiteTemplate"] == null)
                    HttpContext.Current.Session["CurrentSiteTemplate"] = Guid.Empty;

                return (Guid)HttpContext.Current.Session["CurrentSiteTemplate"];
            }
            set { HttpContext.Current.Session["CurrentSiteTemplate"] = value; }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public override void BindData()
        {
            base.BindData();
            
        }
    }
}