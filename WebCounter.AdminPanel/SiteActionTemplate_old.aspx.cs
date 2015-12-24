using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using WebCounter.AdminPanel.UserControls;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class SiteActionTemplate_old : LeadForceBasePage
    {
        public Access access;        
        private Guid _siteActionTemplateId;
        public Guid siteID = new Guid();


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Шаблоны сообщений - LeadForce";

            access = Access.Check();
            if (!access.Write)
                BtnUpdate.Visible = false;

            var siteActionTemplateId = Page.RouteData.Values["ID"] as string;
            siteID = ((LeadForceBasePage)Page).SiteId;

            hlCancel.NavigateUrl = UrlsData.AP_SiteActionTemplates();

            if (!string.IsNullOrEmpty(siteActionTemplateId))
                Guid.TryParse(siteActionTemplateId, out _siteActionTemplateId);

            ucSelectSiteActionTemplate.SiteActionTemplateId = _siteActionTemplateId;

            if (!Page.IsPostBack)
            {
                BindData();

                var ddlSiteTemplateCategory = ((DropDownList)fvSiteActionTemplate.FindControl("ddlSiteTemplateCategory"));
                EnumHelper.EnumToDropDownList<SiteActionTemplateCategory>(ref ddlSiteTemplateCategory, false);

                var dcbParentTemplate = ((DictionaryOnDemandComboBox)fvSiteActionTemplate.FindControl("dcbParentTemplate"));
                dcbParentTemplate.SiteID = siteID;
                dcbParentTemplate.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn
                {
                    Name = "ID",
                    DbType = DbType.Guid,
                    Operation = FilterOperation.NotEqual,
                    Value = _siteActionTemplateId.ToString()
                });

                var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(_siteActionTemplateId);

                ((DropDownList)fvSiteActionTemplate.FindControl("ddlActionType")).DataSource = DataManager.ActionTypes.SelectAll();
                ((DropDownList)fvSiteActionTemplate.FindControl("ddlActionType")).DataTextField = "Title";
                ((DropDownList)fvSiteActionTemplate.FindControl("ddlActionType")).DataValueField = "ID";
                (fvSiteActionTemplate.FindControl("ddlActionType")).DataBind();

                BindReplaceLinks();
                var ddlReplaceLinks = ((DropDownList)fvSiteActionTemplate.FindControl("ddlReplaceLinks"));

                ddlReplaceLinks.SelectedIndex = ddlReplaceLinks.Items.IndexOf(ddlReplaceLinks.Items.FindByValue(((int)ReplaceLinks.GoogleLinks).ToString()));

                if (_siteActionTemplateId != Guid.Empty && siteActionTemplate != null)
                {
                    ddlSiteTemplateCategory.SelectedIndex =
                        ddlSiteTemplateCategory.FindItemIndexByValue(
                            siteActionTemplate.SiteActionTemplateCategoryID.ToString());

                    if (siteActionTemplate.SiteActionTemplateCategoryID.HasValue)
                    {
                        if (siteActionTemplate.UsageID.HasValue)
                        {
                            var hlUsage = ((HyperLink)fvSiteActionTemplate.FindControl("hlUsage"));
                            switch ((SiteActionTemplateCategory)siteActionTemplate.SiteActionTemplateCategoryID)
                            {
                                case SiteActionTemplateCategory.MassMail:
                                    var massMail = DataManager.MassMail.SelectById(siteID, (Guid)siteActionTemplate.UsageID);
                                    if (massMail != null)
                                    {
                                        hlUsage.NavigateUrl = UrlsData.AP_MassMail(massMail.ID);
                                        hlUsage.Text = massMail.Name;
                                    }
                                    break;
                                case SiteActionTemplateCategory.Workflow:
                                    var workflowTemplate = DataManager.WorkflowTemplate.SelectById(siteID, (Guid)siteActionTemplate.UsageID);
                                    if (workflowTemplate != null)
                                    {
                                        hlUsage.NavigateUrl = UrlsData.AP_WorkflowTemplateEdit(workflowTemplate.ID);
                                        hlUsage.Text = workflowTemplate.Name;
                                    }
                                    break;
                                case SiteActionTemplateCategory.Event:
                                    var siteEventTemplate = DataManager.SiteEventTemplates.SelectById((Guid)siteActionTemplate.UsageID);
                                    if (siteEventTemplate != null)
                                    {
                                        hlUsage.NavigateUrl = UrlsData.AP_SiteEventTemplate(siteEventTemplate.ID);
                                        hlUsage.Text = siteEventTemplate.Title;
                                    }
                                    break;
                            }
                        }

                        if (siteActionTemplate.SiteActionTemplateCategoryID != (int)SiteActionTemplateCategory.BaseTemplate)
                            fvSiteActionTemplate.FindControl("plNotBase").Visible = true;
                    }

                    if (siteActionTemplate.ParentID.HasValue)
                    {
                        var parentSiteAction = DataManager.SiteActionTemplate.SelectById(siteActionTemplate.ID);
                        dcbParentTemplate.SelectedId = parentSiteAction.ID;
                        dcbParentTemplate.SelectedText = parentSiteAction.Title;
                        dcbParentTemplate.Enabled = true;
                    }

                    ((DropDownList)fvSiteActionTemplate.FindControl("ddlActionType")).Items.FindByValue(siteActionTemplate.ActionTypeID.ToString()).Selected = true;

                    ddlReplaceLinks.SelectedIndex = ddlReplaceLinks.Items.IndexOf(ddlReplaceLinks.Items.FindByValue(siteActionTemplate.ReplaceLinksID.ToString()));

                    if ((ActionType)siteActionTemplate.ActionTypeID == ActionType.EmailToFixed)
                        (fvSiteActionTemplate.FindControl("pToEmail")).Visible = true;

                    var siteActions = DataManager.SiteAction.SelectBySiteActionTemplateID(siteID, _siteActionTemplateId).Where(a => (ActionStatus)a.ActionStatusID == ActionStatus.Done).ToList();
                    if (siteActions != null)
                    {
                        var sendingCount = siteActions.Count;
                        var resoponseCount = siteActions.Count(a => a.ResponseDate != null);
                        litSending.Text = sendingCount.ToString();
                        litResponse.Text = resoponseCount.ToString();
                        if (resoponseCount != 0)
                            litConversion.Text = string.Format("{0}%", Math.Round(((double)resoponseCount / sendingCount) * 100, 2));

                        BindActionLinks();
                    }
                }
                else
                {
                    pStats.Visible = false;
                    rtsTabs.FindTabByValue("tab-action-links").Visible = false;
                }

                rUserColumnValues.DataSource = DataManager.SiteColumns.SelectAll(siteID);
                rUserColumnValues.DataBind();
            }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(_siteActionTemplateId);
            var fakeList = new List<tbl_SiteActionTemplate>();
            fakeList.Add(siteActionTemplate);
            fvSiteActionTemplate.DataSource = fakeList;
            fvSiteActionTemplate.DataBind();

            //if (siteActionTemplate != null)
                //ucHtmlEditor.Content = siteActionTemplate.MessageBody;
        }



        /// <summary>
        /// Binds the action links.
        /// </summary>
        private void BindActionLinks()
        {
            var siteActionLinks =
                DataManager.SiteActionLink.SelectByActionTemplateID(_siteActionTemplateId)
                .Where(a => a.ActionLinkDate != null)
                .GroupBy(a => new { a.SiteActivityRuleID, a.LinkURL })
                .Select(a => new { a.Key.SiteActivityRuleID, a.Key.LinkURL, Count = a.Count() });


            lvSiteActionLink.DataSource = siteActionLinks;
            lvSiteActionLink.DataBind();
        }



        /// <summary>
        /// Binds the replace links.
        /// </summary>
        private void BindReplaceLinks()
        {
            foreach (var replaceLink in EnumHelper.EnumToList<ReplaceLinks>())
                ((DropDownList)fvSiteActionTemplate.FindControl("ddlReplaceLinks")).Items.Add(new ListItem(EnumHelper.GetEnumDescription(replaceLink), ((int)replaceLink).ToString()));
        }



        /// <summary>
        /// Handles the Click event of the BtnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(_siteActionTemplateId);

            if (siteActionTemplate == null)
                siteActionTemplate = new tbl_SiteActionTemplate();

            siteActionTemplate.SiteID = siteID;
            siteActionTemplate.Title = ((TextBox)fvSiteActionTemplate.FindControl("txtTitle")).Text;
            siteActionTemplate.ActionTypeID = int.Parse(((DropDownList)fvSiteActionTemplate.FindControl("ddlActionType")).SelectedValue);
            siteActionTemplate.ReplaceLinksID = int.Parse(((DropDownList)fvSiteActionTemplate.FindControl("ddlReplaceLinks")).SelectedValue);
            siteActionTemplate.ToEmail = ((TextBox)fvSiteActionTemplate.FindControl("txtToEmail")).Text;
            siteActionTemplate.FromEmail = ((TextBox)fvSiteActionTemplate.FindControl("txtFromEmail")).Text;
            siteActionTemplate.FromName = ((TextBox)fvSiteActionTemplate.FindControl("txtFromName")).Text;
            siteActionTemplate.ReplyToEmail = ((TextBox)fvSiteActionTemplate.FindControl("txtReplyEmail")).Text;
            siteActionTemplate.ReplyToName = ((TextBox)fvSiteActionTemplate.FindControl("txtReplyName")).Text;
            siteActionTemplate.MessageCaption = ((TextBox)fvSiteActionTemplate.FindControl("txtMessageCaption")).Text;
            //siteActionTemplate.MessageBody = ucHtmlEditor.Content;

            if (_siteActionTemplateId != Guid.Empty)
                DataManager.SiteActionTemplate.Update(siteActionTemplate);
            else
            {
                siteActionTemplate.SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.BaseTemplate;
                siteActionTemplate.OwnerID = CurrentUser.Instance.ContactID;
                siteActionTemplate = DataManager.SiteActionTemplate.Add(siteActionTemplate);
            }

            var thumbnail = GetWebSiteThumb.ClassWSThumb.GetWebSiteThumbnail(string.Format("{0}/Handlers/SiteActionTemplateThumbnail.aspx?siteid={1}&id={2}",
                                                                                BusinessLogicLayer.Configuration.Settings.LeadForceSiteUrl,
                                                                                siteActionTemplate.SiteID, siteActionTemplate.ID), 800, 800, 200, 200);
            using (var stream = new System.IO.MemoryStream())
            {
                var fileProvider = new FileSystemProvider();
                thumbnail.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0;
                fileProvider.Upload(SiteId, "SiteActionTemplates", siteActionTemplate.ID.ToString() + ".png", stream, FileType.Thumbnail, false);
            }

            Response.Redirect(UrlsData.AP_SiteActionTemplates());
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlActionType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(((DropDownList)sender).SelectedValue) && (ActionType)int.Parse(((DropDownList)sender).SelectedValue) == ActionType.EmailToFixed)
                (fvSiteActionTemplate.FindControl("pToEmail")).Visible = true;
            else
                (fvSiteActionTemplate.FindControl("pToEmail")).Visible = false;
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

                ((SiteActionLinkUsers)e.Item.FindControl("ucSiteActionLinkUsers")).SiteActionTemplateID = _siteActionTemplateId;
                ((SiteActionLinkUsers)e.Item.FindControl("ucSiteActionLinkUsers")).SiteActivityRuleID = siteActionLink.SiteActivityRuleID;
                ((SiteActionLinkUsers)e.Item.FindControl("ucSiteActionLinkUsers")).LinkURL = siteActionLink.LinkURL;
            }
        }        
    }
}