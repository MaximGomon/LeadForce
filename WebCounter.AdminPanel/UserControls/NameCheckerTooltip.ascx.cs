using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;


namespace WebCounter.AdminPanel.UserControls
{
    public partial class NameCheckerTooltip : System.Web.UI.UserControl
    {
        DataManager _dataManager = new DataManager();
        public event NameConfirmEventHandler NameConfirmClicked;
        public delegate void NameConfirmEventHandler(object sender, NameConfirmEventArgs e);
        public class NameConfirmEventArgs : EventArgs
        {
            public string FullName { get; set; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string FullName
        {
            get
            {
                object o = ViewState["FullName"];
                return (o == null ? string.Empty : (string)o);
            }
            set
            {
                ViewState["FullName"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid ContactId
        {
            get
            {
                object o = ViewState["ContactId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["ContactId"] = value;
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

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool NameConfirmed
        {
            get
            {
                var o = ViewState["NameConfirmed"];
                return (o != null && (bool)o);
            }
            set
            {
                ViewState["NameConfirmed"] = value;
            }
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);            

            var contact = _dataManager.Contact.SelectById(SiteId, ContactId);

            if (contact != null)
            {
                if (!contact.IsNameChecked || contact.UserFullName != FullName)
                {
                    var nameChecker = new NameChecker(ConfigurationManager.AppSettings["ADONETConnectionString"]);
                    var nameCheck = nameChecker.CheckName(FullName, NameCheckerFormat.FIO, Correction.Correct);
                    if (!string.IsNullOrEmpty(nameCheck))
                    {
                        txtName.Text = nameChecker.Name;
                        txtPatronymic.Text = nameChecker.Patronymic;
                        txtSurname.Text = nameChecker.Surname;
                        switch (nameChecker.Gender)
                        {
                            case Gender.M:
                                ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(((int)ContactGender.Male).ToString()));
                                break;
                            case Gender.F:
                                ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(((int)ContactGender.Female).ToString()));
                                break;
                            default:
                                ddlGender.SelectedIndex = -1;
                                break;
                        }
                    }
                }
                else
                {
                    txtName.Text = contact.Name;
                    txtPatronymic.Text = contact.Patronymic;
                    txtSurname.Text = contact.Surname;
                    if (contact.Gender.HasValue)
                        ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(contact.Gender.ToString()));
                }
            }                        

            base.DataBind();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnConfirm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnConfirm_OnClick(object sender, EventArgs e)
        {
            NameConfirmed = true;

            FullName = string.Empty;

            if (!string.IsNullOrEmpty(txtSurname.Text))
                FullName += txtSurname.Text;
            if (!string.IsNullOrEmpty(txtName.Text))
                FullName += " " + txtName.Text;
            if (!string.IsNullOrEmpty(txtPatronymic.Text))
                FullName += " " + txtPatronymic.Text;            

            var contact = _dataManager.Contact.SelectById(SiteId, ContactId);

            if (contact != null)
            {
                contact.UserFullName = FullName;
                contact.Name = txtName.Text;
                contact.Surname = txtSurname.Text;
                contact.Patronymic = txtPatronymic.Text;
                contact.IsNameChecked = true;
                if (ddlGender.SelectedValue == string.Empty)
                    contact.Gender = null;
                else
                    contact.Gender = int.Parse(ddlGender.SelectedValue);

                _dataManager.Contact.Update(contact);
            }

            if (NameConfirmClicked != null)
            {
                NameConfirmClicked(this, new NameConfirmEventArgs() { FullName = FullName});
            }

            if (!Page.ClientScript.IsStartupScriptRegistered("HideTooltip"))
                ScriptManager.RegisterStartupScript(this, typeof(ContactEdit), "HideTooltip", "HideTooltip();", true);
        }
    }
}