using System;
using System.ComponentModel;

namespace WebCounter.AdminPanel.UserControls.Shared
{
    public partial class NotificationMessage : System.Web.UI.UserControl
    {
        private string _style = string.Empty;
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Style 
        { 
            get
            {
                if (!string.IsNullOrEmpty(_style))
                {
                    return string.Format("style='{0}'", _style);
                }

                return string.Empty;
            }
            set { _style = value; } 
        }        

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
                            lrlNotificationMessage.Text = string.Format("<div class=\"warning\" {0}>{1}</div><br/>", Style, value);
                            break;
                        case NotificationMessageType.Success:
                            lrlNotificationMessage.Text = string.Format("<div class=\"success\" {0}>{1}</div><br/>", Style, value);
                            break;
                    }   
                }                
            }
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