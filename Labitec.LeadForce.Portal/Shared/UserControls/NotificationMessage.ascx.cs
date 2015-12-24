using System;
using System.ComponentModel;

namespace Labitec.LeadForce.Portal.Shared.UserControls
{
    public partial class NotificationMessage : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Text
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    lrlNotificationMessage.Text = string.Empty;
                else
                {
                    switch (MessageType)
                    {
                        case NotificationMessageType.Warning:
                            lrlNotificationMessage.Text = string.Format("<div class=\"warning\">{0}</div>", value);
                            break;
                        case NotificationMessageType.Success:
                            lrlNotificationMessage.Text = string.Format("<div class=\"success\">{0}</div>", value);
                            break;
                    }

                    if (NeedBottomPadding)
                        lrlNotificationMessage.Text += "<br/>";
                }
            }
            get { return lrlNotificationMessage.Text; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public NotificationMessageType MessageType
        {
            get
            {
                if (ViewState["MessageType"] == null)
                    ViewState["MessageType"] = NotificationMessageType.Success;

                return (NotificationMessageType)ViewState["MessageType"];
            }
            set
            {
                ViewState["MessageType"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool NeedBottomPadding
        {
            get
            {
                if (ViewState["NeedBottomPadding"] == null)
                    ViewState["NeedBottomPadding"] = true;

                return (bool)ViewState["NeedBottomPadding"];
            }
            set
            {
                ViewState["NeedBottomPadding"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

    public enum NotificationMessageType
    {
        Success = 0,
        Warning = 1
    }
}