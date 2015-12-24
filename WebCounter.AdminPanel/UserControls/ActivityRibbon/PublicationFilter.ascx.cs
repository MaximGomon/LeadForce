using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace WebCounter.AdminPanel.UserControls.ActivityRibbon
{
    public partial class PublicationFilter : System.Web.UI.UserControl
    {
        public event FilterChangedEventHandler FilterChanged;
        public delegate void FilterChangedEventHandler(object sender, FilterChangedEventArgs e);
        public class FilterChangedEventArgs : EventArgs
        {
            public string Filter { get; set; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Handles the OnButtonClick event of the rtbPublicationFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadToolBarEventArgs"/> instance containing the event data.</param>
        protected void rtbPublicationFilter_OnButtonClick(object sender, RadToolBarEventArgs e)
        {                        
            if (FilterChanged != null)
            {
                var eventArgs = new FilterChangedEventArgs {Filter = ((RadToolBarButton) e.Item).CommandArgument};
                FilterChanged(this, eventArgs);
            }
        }
    }
}