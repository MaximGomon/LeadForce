using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls.Contact
{
    public partial class ContactList : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? OrderId
        {
            get
            {
                object o = ViewState["OrderId"];
                return (o == null ? null : (Guid?)o);
            }
            set
            {
                ViewState["OrderId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? CompanyId
        {
            get
            {
                object o = ViewState["CompanyId"];
                return (o == null ? null : (Guid?)o);
            }
            set
            {
                ViewState["CompanyId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteId
        {
            get
            {
                object o = ViewState["SiteId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["SiteId"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            gridContacts.AddNavigateUrl = UrlsData.AP_ContactAdd();
            gridContacts.SiteID = SiteId;

            if (CompanyId.HasValue)
            {
                gridContacts.Where.Add(new GridWhere { CustomQuery = string.Format("tbl_Company.ID = '{0}'", CompanyId) });
                if (CompanyId != Guid.Empty)
                    gridContacts.AddNavigateUrl = UrlsData.AP_ContactAdd() + "?cid=" + CompanyId;
            }
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

                ((Literal)item.FindControl("litPriority")).Text = data["tbl_Priorities_Title"].ToString();
                if (!string.IsNullOrEmpty(data["tbl_Priorities_Image"].ToString()))
                {
                    ((Image)item.FindControl("imgPriority")).ImageUrl = BusinessLogicLayer.Configuration.Settings.DictionaryLogoPath(SiteId, "tbl_Priorities") + data["tbl_Priorities_Image"];
                    ((Image)item.FindControl("imgPriority")).AlternateText = ((Image)item.FindControl("imgPriority")).ToolTip = data["tbl_Priorities_Title"].ToString();
                    ((Image)item.FindControl("imgPriority")).Visible = true;
                }

                ((Literal)item.FindControl("litReadyToSell")).Text = data["tbl_ReadyToSell_Title"].ToString();
                if (!string.IsNullOrEmpty(data["tbl_ReadyToSell_Image"].ToString()))
                {
                    ((Image)item.FindControl("imgReadyToSell")).ImageUrl = BusinessLogicLayer.Configuration.Settings.DictionaryLogoPath(SiteId, "tbl_ReadyToSell") + data["tbl_ReadyToSell_Image"];
                    ((Image)item.FindControl("imgReadyToSell")).AlternateText = ((Image)item.FindControl("imgReadyToSell")).ToolTip = data["tbl_ReadyToSell_Title"].ToString();
                    ((Image)item.FindControl("imgReadyToSell")).Visible = true;
                }
            }
        }

    }
}