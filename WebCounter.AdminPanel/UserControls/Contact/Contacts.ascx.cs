using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class Contacts : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Section Section
        {
            get
            {
                object o = ViewState["Section"];
                return (o == null ? Section.Monitoring : (Section)o);
            }
            set
            {
                ViewState["Section"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool IsDeletedCategory
        {
            get
            {
                object o = ViewState["IsDeletedCategory"];
                return (o == null ? false : (bool)o);
            }
            set
            {
                ViewState["IsDeletedCategory"] = value;
            }
        }


        public Access access;
        public Guid SiteId = new Guid();
        public DataManager _dataManager;



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            access = Access.Check();

            SiteId = ((LeadForceBasePage)Page).SiteId;
            _dataManager = ((LeadForceBasePage)Page).DataManager;
            
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(gridContacts, ucLeftColumn);
            
            gridContacts.SiteID = SiteId;

            var category = Request.QueryString["c"];
            var filter = Request.QueryString["f"];
            if (!string.IsNullOrEmpty(category))
            {
                gridContacts.AddNavigateUrl = UrlsData.AP_ContactAdd() + "?c=" + category;

                switch (category)
                {
                    case "known":
                        gridContacts.Where.Add(new GridWhere { CustomQuery = "(tbl_Contact.Category=0 OR tbl_Contact.Category=1 OR tbl_Contact.Category=2)" });                        
                        break;
                    case "active":
                        gridContacts.Where.Add(new GridWhere { CustomQuery = "(tbl_Contact.Category=1)" });                        
                        break;
                    case "anonymous":
                        gridContacts.Where.Add(new GridWhere { CustomQuery = "(tbl_Contact.Category=4)" });                        
                        break;
                    case "all":
                        gridContacts.Where.Add(new GridWhere { CustomQuery = "(tbl_Contact.Category<>3)" });                        
                        break;
                    case "deleted":
                        gridContacts.Where.Add(new GridWhere { CustomQuery = "(tbl_Contact.Category=3)" });                        
                        IsDeletedCategory = true;
                        break;
                }
            }
            else if (!string.IsNullOrEmpty(filter))
            {
                gridContacts.AddNavigateUrl = UrlsData.AP_ContactAdd();

                var startDate = DateTime.Now.AddDays(1).AddMonths(-1).Date;
                var endDate = DateTime.Now.Date.AddDays(1);

                var period = string.Format(" tbl_Contact.CreatedAt >= '{0}' AND tbl_Contact.CreatedAt <= '{1}' ",
                                           startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));

                switch (filter)
                {
                    case "all":
                        gridContacts.Where.Add(new GridWhere { CustomQuery = string.Format("(tbl_Contact.ID IN (SELECT DISTINCT ContactID FROM tbl_ContactSessions WHERE SiteID = '{0}' AND SessionDate >= '{1}' AND SessionDate <= '{2}'))", SiteId, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")) });
                        break;
                    case "new":
                        gridContacts.Where.Add(new GridWhere { CustomQuery = string.Format("({0})", period) });
                        break;
                    case "reffer":
                        gridContacts.Where.Add(new GridWhere { CustomQuery = string.Format("(tbl_Contact.RefferID IS NOT NULL AND {0})", period) });
                        break;
                    case "direct":
                        gridContacts.Where.Add(new GridWhere { CustomQuery = string.Format("((tbl_Contact.RefferURL IS NULL OR tbl_Contact.RefferURL = '') AND {0})", period) });
                        IsDeletedCategory = true;
                        break;
                    case "repeat":
                        gridContacts.Where.Add(new GridWhere { CustomQuery = string.Format("(tbl_Contact.ID IN (SELECT DISTINCT ContactID FROM tbl_ContactSessions WHERE SiteID = '{0}' AND SessionDate >= '{1}' AND SessionDate <= '{2}' AND UserSessionNumber > 1))", SiteId, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")) });
                        IsDeletedCategory = true;
                        break;
                    default:
                        Guid advertisingPlatformId;
                        if (Guid.TryParse(filter, out  advertisingPlatformId))
                            gridContacts.Where.Add(new GridWhere { CustomQuery = string.Format("(tbl_Contact.AdvertisingPlatformID = '{0}' AND {1})", advertisingPlatformId, period) });
                        break;
                }
            }
            else
            {
                gridContacts.AddNavigateUrl = UrlsData.AP_ContactAdd();
                gridContacts.Where.Add(new GridWhere { CustomQuery = "(tbl_Contact.Category=0 OR tbl_Contact.Category=1 OR tbl_Contact.Category=2)" });                
            }

            gridContacts.ShowDeleteButton = !IsDeletedCategory && access.Delete;
            gridContacts.ShowRestoreButton = IsDeletedCategory;            
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
                if (access == null)
                    access = Access.Check();

                var item = (GridDataItem) e.Item;
                var data = (DataRowView) e.Item.DataItem;

                var category = Request.QueryString["c"];
                if (!string.IsNullOrEmpty(category))
                    ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = UrlsData.AP_Contact(Guid.Parse(data["ID"].ToString())) + "?c=" + category;
                else
                    ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = UrlsData.AP_Contact(Guid.Parse(data["ID"].ToString()));

                ((LinkButton)item.FindControl("lbDelete")).CommandArgument = data["ID"].ToString();
                ((LinkButton)item.FindControl("lbRestore")).CommandArgument = data["ID"].ToString();
                item.FindControl("lbDelete").Visible = !IsDeletedCategory && access.Delete;
                item.FindControl("lbRestore").Visible = IsDeletedCategory;

                ((Literal)item.FindControl("litPriority")).Text = data["tbl_Priorities_Title"].ToString();
                if (!string.IsNullOrEmpty(data["tbl_Priorities_Image"].ToString()))
                {
                    ((Image)item.FindControl("imgPriority")).ImageUrl = BusinessLogicLayer.Configuration.Settings.DictionaryLogoPath(SiteId, "tbl_Priorities") + data["tbl_Priorities_Image"];
                    ((Image) item.FindControl("imgPriority")).AlternateText = ((Image) item.FindControl("imgPriority")).ToolTip = data["tbl_Priorities_Title"].ToString();
                    ((Image) item.FindControl("imgPriority")).Visible = true;
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



        /// <summary>
        /// Handles the OnDeleteAll event of the gridContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Labitec.UI.BaseWorkspace.Grid.DeleteAllEventArgs"/> instance containing the event data.</param>
        protected void gridContacts_OnDeleteAll(object sender, Grid.DeleteAllEventArgs e)
        {
            _dataManager.Contact.DeleteAll(SiteId, e.IDs);
            gridContacts.Rebind();         
        }



        /// <summary>
        /// Handles the OnRestoreAll event of the gridContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Labitec.UI.BaseWorkspace.Grid.RestoreAllEventArgs"/> instance containing the event data.</param>
        protected void gridContacts_OnRestoreAll(object sender, Grid.RestoreAllEventArgs e)
        {
            _dataManager.Contact.RestoreAll(SiteId, e.IDs);
            gridContacts.Rebind();
        }



        /// <summary>
        /// Handles the OnCommand event of the Action control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void Action_OnCommand(object sender, CommandEventArgs e)
        {
            var contactId = e.CommandArgument.ToString().ToGuid();
            var contact = _dataManager.Contact.SelectById(SiteId, contactId);

            if (e.CommandName == "Delete")
                contact.Category = (int)ContactCategory.Deleted;

            if (e.CommandName == "Restore")
            {
                if (!string.IsNullOrEmpty(contact.UserFullName) || !string.IsNullOrEmpty(contact.Email))
                    contact.Category = (int)ContactCategory.Known;
                else
                    contact.Category = (int)ContactCategory.Anonymous;
            }

            _dataManager.Contact.Update(contact);
            
            gridContacts.Rebind();
        }
    }
}