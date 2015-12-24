using System;
using System.ComponentModel;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon
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


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SelectedValue
        {
            get
            {
                object o = ViewState["PublicationTypeId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["PublicationTypeId"] = value;
            }
        }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            PublicationLogoRootPath = WebCounter.BusinessLogicLayer.Configuration.Settings.DictionaryLogoPath(((LeadForcePortalBasePage)Page).SiteId, "tbl_PublicationType");

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var publicationTypes = _dataManager.PublicationType.SelectByPublicationKindID(((LeadForcePortalBasePage)Page).SiteId, (int)PublicationKind.Discussion).OrderBy(pt => pt.Order);

            rlbPublicationTypes.Items.Add(new RadListBoxItem("Все обсуждения", Guid.Empty.ToString()));

            foreach (var publicationType in publicationTypes)
            {
                var item = new RadListBoxItem { Text = publicationType.Title, Value = publicationType.ID.ToString() };
                item.Attributes["Logo"] = PublicationLogoRootPath + publicationType.Logo;
                rlbPublicationTypes.Items.Add(item);
            }

            if (SelectedValue == Guid.Empty)
                rlbPublicationTypes.Items[0].Selected = true;
            else            
                rlbPublicationTypes.SelectedIndex = rlbPublicationTypes.FindItemIndexByValue(SelectedValue.ToString());            

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