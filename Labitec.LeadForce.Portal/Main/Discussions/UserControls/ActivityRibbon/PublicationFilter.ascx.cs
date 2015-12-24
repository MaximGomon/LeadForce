using System;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;

namespace Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon
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
            if (!Page.IsPostBack)
            {                
                rtbPublicationFilter.Items.Add(new RadToolBarButton("Новые")
                {
                    Checked =  true,
                    CheckOnClick = true,
                    Group = "Filter",
                    CommandArgument = "New"
                });

                rtbPublicationFilter.Items.Add(new RadToolBarButton { IsSeparator = true });

                rtbPublicationFilter.Items.Add(new RadToolBarButton("Популярные")
                {                   
                    CheckOnClick = true,
                    Group = "Filter",
                    CommandArgument = "Top"
                });

                if (CurrentUser.Instance != null)
                {
                    rtbPublicationFilter.Items.Add(new RadToolBarButton { IsSeparator = true });
                    rtbPublicationFilter.Items.Add(new RadToolBarButton("Мои публикации")
                    {
                        CheckOnClick = true,
                        Group = "Filter",
                        CommandArgument = "MyPublication"
                    });
                }
            }            
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
                var eventArgs = new FilterChangedEventArgs { Filter = ((RadToolBarButton)e.Item).CommandArgument };
                FilterChanged(this, eventArgs);
            }
        }
    }
}