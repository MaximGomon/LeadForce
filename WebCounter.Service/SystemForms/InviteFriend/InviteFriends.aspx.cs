using System;
using System.Configuration;
using System.Linq;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.Service.SystemForms.InviteFriend
{
    public partial class InviteFriends : System.Web.UI.Page
    {
        protected DataManager DataManager = new DataManager();

        /// <summary>
        /// Gets or sets the site action template id.
        /// </summary>
        /// <value>
        /// The site action template id.
        /// </value>
        protected Guid? SiteActionTemplateId
        {
            get { return (Guid?)ViewState["SiteActionTemplateId"]; }
            set { ViewState["SiteActionTemplateId"] = value; }
        }



        /// <summary>
        /// Gets or sets the workflow template id.
        /// </summary>
        /// <value>
        /// The workflow template id.
        /// </value>
        protected Guid? WorkflowTemplateId
        {
            get { return (Guid?)ViewState["WorkflowTemplateId"]; }
            set { ViewState["WorkflowTemplateId"] = value; }
        }



        /// <summary>
        /// Gets or sets the contact id.
        /// </summary>
        /// <value>
        /// The contact id.
        /// </value>
        protected Guid? ContactId
        {
            get { return (Guid?)ViewState["ContactId"]; }
            set { ViewState["ContactId"] = value; }
        }


        /// <summary>
        /// Gets or sets the site id.
        /// </summary>
        /// <value>
        /// The site id.
        /// </value>
        protected Guid? SiteId
        {
            get { return (Guid?)ViewState["SiteId"]; }
            set { ViewState["SiteId"] = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["sarId"]) && !string.IsNullOrEmpty(Request.QueryString["sId"]) && !string.IsNullOrEmpty(Request.QueryString["cId"]))
            {
                ContactId = Guid.Parse(Request.QueryString["cId"]);
                SiteId = Guid.Parse(Request.QueryString["sId"]);
                var refferContact = DataManager.Contact.SelectById(SiteId.Value, ContactId.Value);
                if (refferContact != null)
                {
                    txtFullName.Text = refferContact.UserFullName;
                    txtEmail.Text = refferContact.Email;
                }

                var siteActivityRule = DataManager.SiteActivityRules.SelectById(Guid.Parse(Request.QueryString["sarId"]));
                if (siteActivityRule != null)
                {
                    var layout = siteActivityRule.tbl_SiteActivityRuleLayout.FirstOrDefault(o => (LayoutType) o.LayoutType == LayoutType.InviteFriend);
                    if (layout != null)
                    {
                        var layoutParams = LayoutParams.Deserialize(layout.LayoutParams);
                        if (layoutParams.SingleOrDefault(o => o.Name == "SiteActionTemplateID") != null)
                        {
                            Guid _siteActionTemplateId = Guid.Empty;                            
                            if (!Guid.TryParse(layoutParams.SingleOrDefault(o => o.Name == "SiteActionTemplateID").Value,out _siteActionTemplateId))
                            {
                                plForm.Visible = false;
                                plMessage.Visible = true;
                            }
                            else                                                            
                                SiteActionTemplateId = _siteActionTemplateId;                            
                        }
                        if (layoutParams.SingleOrDefault(o => o.Name == "WorkflowTemplateID") != null)
                        {
                            Guid _workflowTemplateId = Guid.Empty;
                            if (Guid.TryParse(layoutParams.SingleOrDefault(o => o.Name == "WorkflowTemplateID").Value, out _workflowTemplateId))
                                WorkflowTemplateId = _workflowTemplateId;
                        }
                    }

                    var layouts = siteActivityRule.tbl_SiteActivityRuleLayout.Where(o => (LayoutType)o.LayoutType != LayoutType.InviteFriend).OrderBy(o => o.Order).ToList();
                    if (layouts.Count > 0)
                        lrlHeaderInstruction.Text = string.Format("<p class=\"instruction\" style=\"{1}\">{0}</p>", layouts[0].Description, layouts[0].CSSStyle);
                    if (layouts.Count > 1)
                        lrlFooterInstruction.Text = string.Format("<p class=\"instruction\" style=\"{1}\">{0}</p>", layouts[1].Description, layouts[0].CSSStyle);
                    
                    if (!string.IsNullOrEmpty(siteActivityRule.TextButton))
                        lrlButtonText.Text = siteActivityRule.TextButton;

                    if (!string.IsNullOrEmpty(siteActivityRule.CSSButton))
                        lbtnInviteFriend.Attributes.Add("style", siteActivityRule.CSSButton);

                    if (!string.IsNullOrEmpty(siteActivityRule.CSSForm))
                        plContainer.Attributes.Add("style", siteActivityRule.CSSForm);
                }
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnInviteFriend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnInviteFriend_OnClick(object sender, EventArgs e)
        {
            var refferContact = DataManager.Contact.SelectById(SiteId.Value, ContactId.Value);

            if (refferContact != null)
            {
                refferContact.Email = txtEmail.Text;
                refferContact.UserFullName = txtFullName.Text;

                CheckName(ref refferContact);
                
                DataManager.Contact.Update(refferContact);

                var contact = new tbl_Contact
                                  {
                                      SiteID = SiteId.Value,
                                      Email = txtFriendEmail.Text,
                                      UserFullName = txtFriendName.Text,
                                      RefferID = ContactId,
                                      RefferURL = string.Empty,
                                      IsNameChecked = false,
                                      UserIP = string.Empty,
                                      StatusID = DataManager.Status.SelectDefault(SiteId.Value).ID
                                  };

                CheckName(ref contact);

                DataManager.Contact.Add(contact);
                
                var siteAction = new tbl_SiteAction
                {
                    SiteID = SiteId.Value,
                    SiteActionTemplateID = SiteActionTemplateId.Value,
                    ContactID = contact.ID,
                    ActionStatusID = (int)ActionStatus.Scheduled,
                    ActionDate = DateTime.Now
                };                

                siteAction.tbl_SiteActionTagValue.Add(new tbl_SiteActionTagValue()
                                                          {
                                                              ID = Guid.NewGuid(),
                                                              SiteActionID = siteAction.ID,
                                                              Tag = "#InviteFriend.Comment#",
                                                              Value = txtComment.Text
                                                          });
                siteAction.tbl_SiteActionTagValue.Add(new tbl_SiteActionTagValue()
                {
                    ID = Guid.NewGuid(),
                    SiteActionID = siteAction.ID,
                    Tag = "#System.SenderEmail#",
                    Value = refferContact.Email
                });

                siteAction.tbl_SiteActionTagValue.Add(new tbl_SiteActionTagValue()
                {
                    ID = Guid.NewGuid(),
                    SiteActionID = siteAction.ID,
                    Tag = "#System.SenderUserFullName#",
                    Value = refferContact.UserFullName
                });

                DataManager.SiteAction.Add(siteAction);

                if (WorkflowTemplateId.HasValue && WorkflowTemplateId.Value != Guid.Empty)
                    DataManager.WorkflowTemplate.WorkflowInit(contact.ID, WorkflowTemplateId.Value);

                plForm.Visible = false;
                plSuccess.Visible = true;
            }
        }



        /// <summary>
        /// Checks the name.
        /// </summary>
        /// <param name="contact">The contact.</param>
        private void CheckName(ref tbl_Contact contact)
        {
            var nameChecker = new NameChecker(ConfigurationManager.AppSettings["ADONETConnectionString"]);
            var nameCheck = nameChecker.CheckName(contact.UserFullName, NameCheckerFormat.FIO, Correction.Correct);
            if (!string.IsNullOrEmpty(nameCheck))
            {
                contact.UserFullName = nameCheck;
                contact.Surname = nameChecker.Surname;
                contact.Name = nameChecker.Name;
                contact.Patronymic = nameChecker.Patronymic;
                contact.IsNameChecked = nameChecker.IsNameCorrect;
            }
            else
            {
                contact.UserFullName = contact.UserFullName;
                contact.Name = string.Empty;
                contact.Surname = string.Empty;
                contact.Patronymic = string.Empty;
                contact.IsNameChecked = false;
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnAddAnotherFriend control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnAddAnotherFriend_OnClick(object sender, EventArgs e)
        {
            plForm.Visible = true;
            plSuccess.Visible = false;

            txtFriendEmail.Text = string.Empty;
            txtFriendName.Text = string.Empty;
        }
    }
}