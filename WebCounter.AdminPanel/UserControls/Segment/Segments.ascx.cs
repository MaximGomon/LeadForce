using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Contact;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;
using Page = System.Web.UI.Page;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class Segments : System.Web.UI.UserControl
    {

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<Guid> SelectedContactList
        {
            get
            {
                if (ViewState["SelectedContactList"] == null)
                    ViewState["SelectedContactList"] = new List<Guid>();
                return (List<Guid>)ViewState["SelectedContactList"];
            }
            set
            {
                ViewState["SelectedContactList"] = value;
            }
        }

        private DataManager _dataManager = new DataManager();
        public Access access;

        
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public int ObjectId
        {
            get
            {
                object o = ViewState["ObjectId"];
                return (o == null ? 0 : (int)o);
            }
            set
            {
                ViewState["ObjectId"] = value;
            }
        }


        public Guid SiteId = new Guid();



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            access = Access.Check();

            SiteId = ((LeadForceBasePage)Page).SiteId;            

            gridSegments.AddNavigateUrl = UrlsData.AP_ContactSegmentAdd();
            gridSegments.SiteID = SiteId;
            gridSegments.Where.Add(new GridWhere() { CustomQuery = "(tbl_SiteTags.ObjectTypeID = '"+ObjectId.ToString()+"')" });

            var radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnAdd, gridSegments);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnAdd, txtSegment, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnSave, gridSegments);
            radAjaxManager.AjaxSettings.AddAjaxSetting(ucSelectContacts, gridContacts);
        }
        


        /// <summary>
        /// Handles the OnItemDataBound event of the gridContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridSegments_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (access == null)
                    access = Access.Check();

                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;


                ((Literal)item.FindControl("lrlName")).Text = data["tbl_SiteTags_Name"].ToString();
                ((Literal)item.FindControl("lrlDescription")).Text = data["tbl_SiteTags_Description"].ToString();

                var lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                lbDelete.CommandArgument = data["ID"].ToString();                
                lbDelete.Visible = access.Delete;
                var lbEdit = (LinkButton) e.Item.FindControl("lbEdit");
                lbEdit.CommandArgument = data["ID"].ToString();
                lbEdit.Visible = access.Write;

                ((TextBox)item.FindControl("txtSegment")).Text = data["tbl_SiteTags_Name"].ToString();
                ((TextBox)item.FindControl("txtDescription")).Text = data["tbl_SiteTags_Description"].ToString();
                ((RadButton)e.Item.FindControl("lbtnUpdate")).CommandArgument = data["ID"].ToString();                
                var segmentContacts = _dataManager.SiteTagObjects.SelectIdsByTagID(Guid.Parse(data["ID"].ToString())).Where(o => o != Guid.Empty).ToList();
                ((Literal)item.FindControl("lrlCount")).Text = segmentContacts.Count.ToString();
                ((LinkButton)e.Item.FindControl("lbContacts")).CommandArgument = data["ID"].ToString();

                var lbContacts = e.Item.FindControl("lbContacts");                

                RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbContacts, gridContacts);
                RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbContacts, lbtnSave);
                RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbContacts, ucSelectContacts);                
            }
        }


        /// <summary>
        /// Handles the OnCommand event of the lbDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            _dataManager.SiteTagObjects.DeleteByTagID(Guid.Parse(e.CommandArgument.ToString()));
            _dataManager.SiteTags.Delete(Guid.Parse(e.CommandArgument.ToString()));            
            gridSegments.Rebind();
        }



        /// <summary>
        /// Handles the OnCommand event of the lbEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbEdit_OnCommand(object sender, CommandEventArgs e)
        {
            ((LinkButton)sender).Parent.FindControl("plEdit").Visible = true;            
        }




        /// <summary>
        /// Handles the OnClick event of the lbtnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnUpdate_OnClick(object sender, EventArgs e)
        {
            var lbtnUpdate = (RadButton)sender;
            var txtUpdateSegment = (TextBox)lbtnUpdate.Parent.FindControl("txtSegment");
            var txtUpdateDescription = (TextBox)lbtnUpdate.Parent.FindControl("txtDescription");                        

            var segmentId = Guid.Parse(lbtnUpdate.CommandArgument);

            var segment = _dataManager.SiteTags.SelectById(segmentId);
            segment.Name = txtUpdateSegment.Text;
            segment.Description = txtUpdateDescription.Text;
            _dataManager.SiteTags.Update(segment);
            
            gridSegments.Rebind();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnAdd_OnClick(object sender, EventArgs e)
        {
            var segment = new tbl_SiteTags
                              {
                                  SiteID = CurrentUser.Instance.SiteID,
                                  Name = txtSegment.Text,
                                  //Description = txtDescription.Text,
                                  ObjectTypeID = 1,
                                  UserID = CurrentUser.Instance.ID
                              };

            _dataManager.SiteTags.Add(segment);

            txtSegment.Text = string.Empty;
            //txtDescription.Text = string.Empty;

            gridSegments.Rebind();
        }



        /// <summary>
        /// Handles the OnSelectedChanged event of the ucSelectContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.Contact.SelectContacts.SelectedChangedEventArgs"/> instance containing the event data.</param>
        protected void ucSelectContacts_OnSelectedChanged(object sender, SelectContacts.SelectedChangedEventArgs e)
        {            
            BindGridContacts(e.ContactList);
            gridContacts.Rebind();
        }



        /// <summary>
        /// Binds the grid contacts.
        /// </summary>
        /// <param name="contactList">The contact list.</param>       
        protected void BindGridContacts(List<Guid> contactList)
        {
            var selectedItem = new List<Guid>();
            selectedItem = contactList;
            if (selectedItem.Count == 0)
                selectedItem.Add(Guid.Empty);
            var query = new StringBuilder();
            foreach (var item in selectedItem)
                query.AppendFormat("'{0}',", item);
            
            gridContacts.Where = new List<GridWhere>();
            gridContacts.Where.Add(new GridWhere { CustomQuery = string.Format("tbl_Contact.ID IN ({0})", query.ToString().TrimEnd(new[] { ',' })) });
            ucSelectContacts.SelectedItems = contactList;
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

                var ibtnDelete = (ImageButton)item.FindControl("ibtnDelete");
                ibtnDelete.CommandArgument = data["ID"].ToString();
            }
        }



        /// <summary>
        /// Handles the OnCommand event of the ibtnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void ibtnDelete_OnCommand(object sender, CommandEventArgs e)
        {            
            ucSelectContacts.SelectedItems.Remove(e.CommandArgument.ToString().ToGuid());
            BindGridContacts(ucSelectContacts.SelectedItems);
            gridContacts.Rebind();
        }



        /// <summary>
        /// Handles the OnCommand event of the lbContacts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbContacts_OnCommand(object sender, CommandEventArgs e)
        {
            var segmentId = Guid.Parse(((LinkButton) sender).CommandArgument);
            lbtnSave.CommandArgument = segmentId.ToString();
            ucSelectContacts.SelectedItems = _dataManager.SiteTagObjects.SelectIdsByTagID(segmentId).Where(o => o != Guid.Empty).ToList();
            BindGridContacts(ucSelectContacts.SelectedItems);
            gridContacts.Rebind();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowContactsListRadWindow", "ShowContactsListRadWindow();", true);
        }


        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            var segmentId = Guid.Parse(lbtnSave.CommandArgument);

            var objects = _dataManager.SiteTagObjects.SelectIdsByTagID(segmentId);
            foreach (var guid in objects)
            {
                var obj = _dataManager.SiteTagObjects.Select(segmentId, guid);
                _dataManager.SiteTagObjects.Delete(obj);
            }
            var selectedItems = ucSelectContacts.SelectedItems;
            if (selectedItems.Count != 0)
            {
                foreach (var selectedItem in selectedItems)
                {
                    var obj = new tbl_SiteTagObjects()
                                  {
                                      ID = Guid.NewGuid(),
                                      ObjectID = selectedItem,
                                      SiteTagID = segmentId,
                                  };
                    _dataManager.SiteTagObjects.Add(obj);
                }
            }

            gridSegments.Rebind();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseContactsListRadWindow", "CloseContactsListRadWindow();", true);
        }
    }
}