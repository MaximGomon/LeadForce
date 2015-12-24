using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class MassMail_old : LeadForceBasePage
    {
        public Access access;
        public Guid _massMailId;
        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Рассылки - LeadForce";

            access = Access.Check();
            if (!access.Write)
            {
                BtnSave.Visible = false;
                BtnSendFocusGroup.Visible = false;
                BtnSend.Visible = false;
            }

            string massMailId = Page.RouteData.Values["ID"] as string;
            if (!string.IsNullOrEmpty(massMailId))
                Guid.TryParse(massMailId, out _massMailId);         

            tagsMassMail.ObjectID = _massMailId;

            if (!Page.IsPostBack)
            {
                hlCancel.NavigateUrl = UrlsData.AP_MassMails();

                BindData();

                if (_massMailId == Guid.Empty)
                {
                    pButtons.Visible = false;
                    rtsTabs.FindTabByValue("tab-recipients").Visible = false;
                    rtsTabs.FindTabByValue("tab-action-links").Visible = false;
                }
                else
                {
                    BindContacts();
                    BindActionLinks();

                    rwUsers.NavigateUrl = string.Format("{0}?massmailid={1}", ResolveUrl("~/Handlers/MassMailSelectContacts.aspx"), _massMailId);
                }
            }            
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var massMail = DataManager.MassMail.SelectById(SiteId, _massMailId);
            var fakeList = new List<tbl_MassMail>();            

            fakeList.Add(massMail);
            fvMassMail.DataSource = fakeList;
            fvMassMail.DataBind();

            BindStats();
            
            if (massMail != null)
            {
                //((UserControls.SiteActionTemplate)fvMassMail.FindControl("ucSiteActionTemplate")).SelectedSiteActionTemplateId = massMail.SiteActionTemplateID;
                ((SelectSiteActionTemplate)fvMassMail.FindControl("ucSelectSiteActionTemplate")).SiteActionTemplateId = massMail.SiteActionTemplateID;
            }            
        }



        /// <summary>
        /// Binds the stats.
        /// </summary>
        public void BindStats()
        {
            var massMail = DataManager.MassMail.SelectById(SiteId, _massMailId);

            if (massMail == null)
                return;

            var massMailContact = DataManager.MassMailContact.SelectByMassMailId(SiteId, _massMailId);
            litRecipients.Text = massMailContact.Count.ToString();

            massMailContact = massMailContact.Where(a => a.SiteActionID != null).ToList();
            var actionIDs = massMailContact.Select(a => a.SiteActionID);
            var sendingCount = massMailContact.Count;
            litSending.Text = sendingCount.ToString();
            var resoposneCount = DataManager.SiteAction.SelectBySiteActionTemplateID(SiteId, massMail.SiteActionTemplateID).Where(a => actionIDs.Contains(a.ID) && a.ResponseDate != null).Count();
            litResponse.Text = resoposneCount.ToString();
            if (resoposneCount != 0)
                litConversion.Text = string.Format("{0}%", Math.Round(((double)resoposneCount / sendingCount) * 100, 2));
        }



        /// <summary>
        /// Binds the contacts.
        /// </summary>
        private void BindContacts()
        {
            bool sortAscending = this.SortDirection == SortDirection.Ascending ? true : false;
            var massMailContacts = new List<Guid>();
            massMailContacts = DataManager.MassMailContact.SelectByMassMailId(SiteId, _massMailId).Select(a => a.ContactID).ToList();

            var contacts = DataManager.Contact.SelectAll(SiteId).Where(a => massMailContacts.Contains(a.ID)).ToList();

            switch (this.SortExpression)
            {
                case "UserFullName":
                    contacts = sortAscending ? contacts.OrderBy(u => u.UserFullName).ToList() : contacts.OrderByDescending(u => u.UserFullName).ToList();
                    break;
                case "Email":
                    contacts = sortAscending ? contacts.OrderBy(u => u.Email).ToList() : contacts.OrderByDescending(u => u.Email).ToList();
                    break;
                case "tbl_ReadyToSell.Title":
                    contacts = sortAscending ? contacts.OrderBy(u => u.tbl_ReadyToSell.Title).ToList() : contacts.OrderByDescending(u => u.tbl_ReadyToSell.Title).ToList();
                    break;
                case "tbl_Priorities.Title":
                    contacts = sortAscending ? contacts.OrderBy(u => u.tbl_Priorities.Title).ToList() : contacts.OrderByDescending(u => u.tbl_Priorities.Title).ToList();
                    break;
                case "tbl_Status.Title":
                    contacts = sortAscending ? contacts.OrderBy(u => u.tbl_Status.Title).ToList() : contacts.OrderByDescending(u => u.tbl_Status.Title).ToList();
                    break;
            }

            gvContacts.DataSource = contacts;
            gvContacts.DataBind();
        }



        /// <summary>
        /// Binds the action links.
        /// </summary>
        private void BindActionLinks()
        {
            var massMail = DataManager.MassMail.SelectById(SiteId, _massMailId);
            var massMailContact = DataManager.MassMailContact.SelectByMassMailId(SiteId, _massMailId).Where(a => a.SiteActionID != null).ToList();
            var actionIDs = massMailContact.Select(a => a.SiteActionID);

            var siteActionLinks =
                DataManager.SiteActionLink.SelectByActionTemplateID(massMail.SiteActionTemplateID)
                .Where(a => a.ActionLinkDate != null && actionIDs.Contains(a.SiteActionID))
                .GroupBy(a => new { a.SiteActivityRuleID, a.LinkURL })
                .Select(a => new { a.Key.SiteActivityRuleID, a.Key.LinkURL, Count = a.Count() });


            lvSiteActionLink.DataSource = siteActionLinks;
            lvSiteActionLink.DataBind();
        }



        /// <summary>
        /// Handles the ItemCreated event of the fvMassMail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void fvMassMail_ItemCreated(object sender, EventArgs e)
        {
            var massMailStatuses = DataManager.MassMailStatus.SelectAll();
            foreach (var item in massMailStatuses)
                ((DropDownList)((FormView)sender).FindControl("ddlMassMailStatus")).Items.Add(new ListItem { Text = item.Title, Value = item.ID.ToString() });                                    
        }



        /// <summary>
        /// Handles the Click event of the BtnSendFocusGroup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnSendFocusGroup_Click(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var massMail = DataManager.MassMail.SelectById(SiteId, _massMailId);

            massMail.SiteID = SiteId;
            massMail.Name = ((TextBox)fvMassMail.FindControl("txtName")).Text;            
            massMail.MailDate = DateTime.Now;
            
            massMail.SiteActionTemplateID = (Guid)((UserControls.SiteActionTemplate)fvMassMail.FindControl("ucSiteActionTemplate")).SelectedSiteActionTemplateId;

            ((UserControls.SiteActionTemplate)fvMassMail.FindControl("ucSiteActionTemplate")).UpdateSiteActionTemplate(massMail.ID, massMail.SiteActionTemplateID);

            massMail.MassMailStatusID = (int)MassMailStatusType.DoneFocusGroup;
            if (!string.IsNullOrEmpty(((TextBox)fvMassMail.FindControl("txtFocusGroup")).Text))
                massMail.FocusGroup = int.Parse(((TextBox)fvMassMail.FindControl("txtFocusGroup")).Text);
            DataManager.MassMail.Update(massMail);

            var massMailContacts = DataManager.MassMailContact.SelectByMassMailId(SiteId, _massMailId).Where(a => a.SiteActionID == null).Take(int.Parse(((TextBox)fvMassMail.FindControl("txtFocusGroup")).Text));

            var siteAction = new tbl_SiteAction();
            foreach (var massMailContact in massMailContacts)
            {
                siteAction = new tbl_SiteAction();                
                siteAction.SiteID = SiteId;
                siteAction.SiteActionTemplateID = massMail.SiteActionTemplateID;
                siteAction.ContactID = massMailContact.ContactID;
                siteAction.ObjectID = massMail.ID;
                siteAction.MessageTypeID = (int) MessageType.MassMail;
                siteAction.ActionStatusID = (int)ActionStatus.Scheduled;
                siteAction.ActionDate = DateTime.Now;
                siteAction.OwnerID = CurrentUser.Instance.ContactID;
                DataManager.SiteAction.Add(siteAction);

                massMailContact.SiteActionID = siteAction.ID;
                DataManager.MassMailContact.Update(massMailContact);
            }

            //BindData();
            Response.Redirect(UrlsData.AP_MassMail(massMail.ID));
        }



        /// <summary>
        /// Handles the Click event of the BtnSend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnSend_Click(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var massMail = DataManager.MassMail.SelectById(SiteId, _massMailId);
            massMail.SiteID = SiteId;
            massMail.Name = ((TextBox)fvMassMail.FindControl("txtName")).Text;            
            massMail.MailDate = DateTime.Now;
            //massMail.SiteActionTemplateID = Guid.Parse(((TextBox)fvMassMail.FindControl("txtSiteActionTemplateId")).Text);

            massMail.SiteActionTemplateID = (Guid)((UserControls.SiteActionTemplate)fvMassMail.FindControl("ucSiteActionTemplate")).SelectedSiteActionTemplateId;
            ((UserControls.SiteActionTemplate)fvMassMail.FindControl("ucSiteActionTemplate")).UpdateSiteActionTemplate(massMail.ID, massMail.SiteActionTemplateID);
            massMail.MassMailStatusID = (int)MassMailStatusType.Done;
            if (!string.IsNullOrEmpty(((TextBox)fvMassMail.FindControl("txtFocusGroup")).Text))
                massMail.FocusGroup = int.Parse(((TextBox)fvMassMail.FindControl("txtFocusGroup")).Text);
            DataManager.MassMail.Update(massMail);

            var massMailContacts = DataManager.MassMailContact.SelectByMassMailId(SiteId, _massMailId).Where(a => a.SiteActionID == null);

            var siteAction = new tbl_SiteAction();
            foreach (var massMailContact in massMailContacts)
            {
                siteAction = new tbl_SiteAction();
                siteAction.SiteID = SiteId;
                siteAction.SiteActionTemplateID = massMail.SiteActionTemplateID;
                siteAction.ContactID = massMailContact.ContactID;
                siteAction.ObjectID = massMail.ID;
                siteAction.MessageTypeID = (int)MessageType.MassMail;
                siteAction.ActionStatusID = (int)ActionStatus.Scheduled;
                siteAction.ActionDate = DateTime.Now;
                siteAction.OwnerID = CurrentUser.Instance.ContactID;
                DataManager.SiteAction.Add(siteAction);

                massMailContact.SiteActionID = siteAction.ID;
                DataManager.MassMailContact.Update(massMailContact);
            }

            //BindData();

            Response.Redirect(UrlsData.AP_MassMail(massMail.ID));
        }



        /// <summary>
        /// Handles the PageIndexChanging event of the gvSiteUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvContacts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContacts.PageIndex = e.NewPageIndex;
            BindContacts();
        }



        /// <summary>
        /// Handles the Sorting event of the gvSiteUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void gvContacts_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (this.SortExpression == e.SortExpression)
            {
                this.SortDirection = this.SortDirection == SortDirection.Ascending ?
                    SortDirection.Descending : SortDirection.Ascending;
            }
            else
            {
                this.SortDirection = SortDirection.Ascending;
            }
            this.SortExpression = e.SortExpression;
            gvContacts.EditIndex = -1;
            gvContacts.SelectedIndex = -1;
        }



        /// <summary>
        /// Handles the Sorted event of the gvSiteUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void gvContacts_Sorted(object sender, EventArgs e)
        {
            BindContacts();
        }



        /// <summary>
        /// Handles the RowDeleting event of the gvSiteUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void gvContacts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataManager.MassMailContact.Delete(SiteId, _massMailId, Guid.Parse(gvContacts.DataKeys[e.RowIndex].Value.ToString()));
            BindContacts();

            BindStats();
        }



        /// <summary>
        /// Handles the Click event of the BtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var massMail = new tbl_MassMail();

            if (_massMailId != Guid.Empty)
                massMail = DataManager.MassMail.SelectById(SiteId, _massMailId);

            massMail.SiteID = SiteId;
            massMail.Name = ((TextBox)fvMassMail.FindControl("txtName")).Text;       
            
            //massMail.SiteActionTemplateID = (Guid)((UserControls.SiteActionTemplate)fvMassMail.FindControl("ucSiteActionTemplate")).SelectedSiteActionTemplateId;            
            massMail.SiteActionTemplateID = ((SelectSiteActionTemplate)fvMassMail.FindControl("ucSelectSiteActionTemplate")).SiteActionTemplateId;

            massMail.MassMailStatusID = int.Parse(((DropDownList)fvMassMail.FindControl("ddlMassMailStatus")).SelectedValue);
            if (!string.IsNullOrEmpty(((TextBox)fvMassMail.FindControl("txtFocusGroup")).Text))
                massMail.FocusGroup = int.Parse(((TextBox)fvMassMail.FindControl("txtFocusGroup")).Text);

            if (_massMailId != Guid.Empty)
                DataManager.MassMail.Update(massMail);
            else
            {
                massMail.OwnerID = CurrentUser.Instance.ContactID;
                massMail = DataManager.MassMail.Add(massMail);
            }

            //((UserControls.SiteActionTemplate)fvMassMail.FindControl("ucSiteActionTemplate")).UpdateSiteActionTemplate(massMail.ID, massMail.SiteActionTemplateID);

            tagsMassMail.SaveTags(massMail.ID);

            Response.Redirect(UrlsData.AP_MassMail(massMail.ID));
        }



        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            BindContacts();
            BindStats();
        }



                /// <summary>
        /// Handles the ItemDataBound event of the lvSiteActionLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewItemEventArgs"/> instance containing the event data.</param>
        protected void lvSiteActionLink_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var massMail = DataManager.MassMail.SelectById(SiteId, _massMailId);
                var massMailContact = DataManager.MassMailContact.SelectByMassMailId(SiteId, _massMailId).Where(a => a.SiteActionID != null).ToList();
                var actionIDs = massMailContact.Select(a => a.SiteActionID).ToList();

                dynamic siteActionLink = e.Item.DataItem;
                if (siteActionLink.SiteActivityRuleID != null)
                {
                    var siteActivityRule = DataManager.SiteActivityRules.SelectById((Guid)siteActionLink.SiteActivityRuleID);
                    if (siteActivityRule != null)
                    {
                        ((HyperLink)e.Item.FindControl("hlActionLink")).Text = siteActivityRule.Code;
                        ((HyperLink)e.Item.FindControl("hlActionLink")).NavigateUrl = UrlsData.AP_SiteActivityRule(siteActivityRule.ID, siteActivityRule.RuleTypeID);
                    }
                }
                else if (!string.IsNullOrEmpty(siteActionLink.LinkURL))
                {
                    ((HyperLink)e.Item.FindControl("hlActionLink")).Text = siteActionLink.LinkURL;
                    ((HyperLink)e.Item.FindControl("hlActionLink")).NavigateUrl = siteActionLink.LinkURL;
                }

                ((Literal)e.Item.FindControl("litCountConversions")).Text = siteActionLink.Count.ToString();

                ((SiteActionLinkUsers)e.Item.FindControl("ucSiteActionLinkUsers")).SiteActionTemplateID = massMail.SiteActionTemplateID;
                ((SiteActionLinkUsers)e.Item.FindControl("ucSiteActionLinkUsers")).SiteActivityRuleID = siteActionLink.SiteActivityRuleID;
                ((SiteActionLinkUsers)e.Item.FindControl("ucSiteActionLinkUsers")).LinkURL = siteActionLink.LinkURL;
                ((SiteActionLinkUsers)e.Item.FindControl("ucSiteActionLinkUsers")).ActionIDs = actionIDs;
            }
        }



        protected string SortExpression
        {
            get
            {
                return ViewState["SortExpression"] as string;
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }



        protected SortDirection SortDirection
        {
            get
            {
                object o = ViewState["SortDirection"];
                if (o == null)
                    return SortDirection.Ascending;
                else
                    return (SortDirection)o;
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }
    }
}