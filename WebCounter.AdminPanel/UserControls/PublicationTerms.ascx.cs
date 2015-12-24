using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class PublicationTerms : System.Web.UI.UserControl
    {
        private DataManager dataManager = new DataManager();

        [Serializable]
        private class PublicationTermStructure
        {
            public Guid ID { get; set; }
            public Guid PublicationID { get; set; }
            public string Term { get; set; }
            public string PublicationCode { get; set; }
            public string ElementCode { get; set; }
            public string Description { get; set; }
        }

        private List<PublicationTermStructure> _publicationTermStructure = new List<PublicationTermStructure>();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid PublicationID
        {
            get
            {
                object o = ViewState["PublicationID"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["PublicationID"] = value;
            }
        }



        /// <summary>
        /// Gets or sets the siteID.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        public Guid SiteID
        {
            get
            {
                if (ViewState["SiteID"] == null)
                    return Guid.Empty;
                return (Guid)ViewState["SiteID"];
            }
            set { ViewState["SiteID"] = value; }
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
                BindPublicationTerms();
                rgPublicationTerms.Culture = new CultureInfo("ru-RU");
            }

        }



        private void BindPublicationTerms()
        {

            foreach (var v in dataManager.PublicationTerms.SelectByPublicationId(PublicationID).ToList())
            {
                _publicationTermStructure.Add(new PublicationTermStructure() { ID = v.ID, PublicationID = PublicationID, PublicationCode = v.PublicationCode, ElementCode = v.ElementCode, Description = v.Description, Term = v.Term });
            }
            ViewState["PublicationTerms"] = _publicationTermStructure;
        }




        /// <summary>
        /// Handles the NeedDataSource event of the rgPublicationTerms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationTerms_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgPublicationTerms.DataSource = ViewState["PublicationTerms"];
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgPublicationTerms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationTerms_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var id = Guid.Parse(item.GetDataKeyValue("ID").ToString());
            var publicationTerm = ((List<PublicationTermStructure>)ViewState["PublicationTerms"]).Where(s => s.ID == id).FirstOrDefault();
            publicationTerm.Term = ((TextBox)item.FindControl("txtTerm")).Text;
            publicationTerm.PublicationCode = ((TextBox)item.FindControl("txtPublicationCode")).Text;
            publicationTerm.ElementCode = ((TextBox)item.FindControl("txtElementCode")).Text;
            publicationTerm.Description = ((TextBox)item.FindControl("txtDescription")).Text;
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgPublicationTerms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationTerms_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var publicationTerm = new PublicationTermStructure();
            publicationTerm.ID = Guid.NewGuid();
            publicationTerm.PublicationID = PublicationID;
            publicationTerm.Term = ((TextBox)item.FindControl("txtTerm")).Text;
            publicationTerm.PublicationCode = ((TextBox)item.FindControl("txtPublicationCode")).Text;
            publicationTerm.ElementCode = ((TextBox)item.FindControl("txtElementCode")).Text;
            publicationTerm.Description = ((TextBox)item.FindControl("txtDescription")).Text;
            ((List<PublicationTermStructure>)ViewState["PublicationTerms"]).Add(publicationTerm);
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgPublicationTerms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationTerms_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<PublicationTermStructure>)ViewState["PublicationTerms"]).Remove(
                ((List<PublicationTermStructure>)ViewState["PublicationTerms"]).Where(s => s.ID == id).FirstOrDefault());
        }




        public void Save(Guid BasePublicationID)
        {
            var publicationTerm = new List<tbl_PublicationTerms>();

            foreach (var v in (List<PublicationTermStructure>)ViewState["PublicationTerms"])
            {
                publicationTerm.Add(new tbl_PublicationTerms() { ID = v.ID, PublicationID = BasePublicationID, Term = v.Term, PublicationCode = v.PublicationCode, ElementCode = v.ElementCode, Description = v.Description});
            }

            dataManager.PublicationTerms.DeleteAll(BasePublicationID);
            dataManager.PublicationTerms.Add(publicationTerm);

        }

        protected void rgPublicationTerms_DataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var item = e.Item as GridEditableItem;
                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var publicationTerms = (PublicationTermStructure)item.DataItem;

                    ((TextBox)item.FindControl("txtTerm")).Text = publicationTerms.Term;
                    ((TextBox)item.FindControl("txtPublicationCode")).Text = publicationTerms.PublicationCode;
                    ((TextBox)item.FindControl("txtElementCode")).Text = publicationTerms.ElementCode;
                    ((TextBox)item.FindControl("txtDescription")).Text = publicationTerms.Description;

                }
            }
        }
    }
}