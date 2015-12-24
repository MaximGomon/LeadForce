using System;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls.ActivityRibbon
{
    public partial class PublicationTypes : System.Web.UI.UserControl
    {
        private readonly DataManager _dataManager = new DataManager();
        protected string PublicationLogoRootPath;

        public event SelectedIndexChangedEventHandler SelectedIndexChanged;
        public delegate void SelectedIndexChangedEventHandler(object sender, SelectedIndexChangedEventArgs e);
        public class SelectedIndexChangedEventArgs : EventArgs
        {
            public Guid SelectedValue { get; set; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            PublicationLogoRootPath = BusinessLogicLayer.Configuration.Settings.DictionaryLogoPath(CurrentUser.Instance.SiteID, "tbl_PublicationType");

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var publicationTypes = _dataManager.PublicationType.SelectByPublicationKindID(CurrentUser.Instance.SiteID, (int)PublicationKind.Discussion).OrderBy(pt => pt.Order);

            rlbPublicationTypes.Items.Add(new RadListBoxItem("Все обсуждения", Guid.Empty.ToString()));

            foreach (var publicationType in publicationTypes)
            {
                var item = new RadListBoxItem {Text = publicationType.Title, Value = publicationType.ID.ToString()};
                item.Attributes["Logo"] = PublicationLogoRootPath + publicationType.Logo;
                rlbPublicationTypes.Items.Add(item);
            }

            
            rlbPublicationTypes.Items[0].Selected = true;

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, new SelectedIndexChangedEventArgs() { SelectedValue = Guid.Parse(rlbPublicationTypes.SelectedValue) });
              
          
            rlbPublicationTypes.DataBind();
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the rlbPublicationTypes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rlbPublicationTypes_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)            
                SelectedIndexChanged(this, new SelectedIndexChangedEventArgs() { SelectedValue = Guid.Parse(rlbPublicationTypes.SelectedValue) });
        }
    }
}