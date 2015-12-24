using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel.UserControls.Contact
{
    public partial class SelectContacts : System.Web.UI.UserControl
    {
        protected RadAjaxManager radAjaxManager = null;

        public List<Guid> SelectedItems
        {
            get
            {
                if (ViewState["SelectedItems"] == null)
                    ViewState["SelectedItems"] = new List<Guid>();
                return (List<Guid>)ViewState["SelectedItems"];
            }
            set
            {
                ViewState["SelectedItems"] = value;
            }
        }



        public event SelectedChangedEventHandler SelectedChanged;
        public delegate void SelectedChangedEventHandler(object sender, SelectedChangedEventArgs e);
        public class SelectedChangedEventArgs : EventArgs
        {
            public List<Guid> ContactList { get; set; }
        }

        public LinkButton SaveButton
        {
            get { return lbtnSave; }
        }

        public bool HideButton { get; set; }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            gridContacts.SiteID = CurrentUser.Instance.SiteID;
            gridContacts.Where = new List<GridWhere>();
            gridContacts.Where.Add(new GridWhere { CustomQuery = "(tbl_Contact.Email IS NOT NULL AND tbl_Contact.Email <> '')" });
            gridContacts.SelectedItems = SelectedItems;

            hlCancel.Attributes.Add("onclick", this.ClientID + "_CloseContactsRadWindow(); return false;");

            rbAdd.Visible = !HideButton;

            //radAjaxManager = RadAjaxManager.GetCurrent(Page);
            //radAjaxManager.AjaxSettings.AddAjaxSetting(lbAdd, gridContacts);
        }


        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        public void Rebind()
        {
            gridContacts.SelectedItems = SelectedItems;
            gridContacts.Rebind();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (SelectedChanged != null)
                SelectedChanged(this, new SelectedChangedEventArgs { ContactList = gridContacts.GetSelected() });

            if (!this.Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_CloseContactsRadWindow"))
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), this.ClientID + "_CloseContactsRadWindow", this.ClientID + "_CloseContactsRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridContacts_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = UrlsData.AP_Contact(Guid.Parse(data["ID"].ToString()));
                ((HyperLink)item.FindControl("hlEdit")).Target = "_blank";
            }
        }



        /// <summary>
        /// Handles the OnClick event of the rbAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbAdd_OnClick(object sender, EventArgs e)
        {
            Show();
        }


        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void Show()
        {
            Rebind();

            if (!this.Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_ShowContactsRadWindow"))
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), this.ClientID + "_ShowContactsRadWindow", this.ClientID + "_ShowContactsRadWindow();", true);
        }
    }
}