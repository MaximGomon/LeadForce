using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;

namespace Labitec.LeadForce.Portal.Shared.UserControls
{
    public partial class HTMLEditor : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Module
        {
            get
            {                
                return (string)ViewState["Module"];
            }
            set
            {
                ViewState["Module"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool IsDeleteEnabled
        {
            get
            {
                if (ViewState["IsDeleteEnabled"] == null)
                    ViewState["IsDeleteEnabled"] = true;

                return (bool)ViewState["IsDeleteEnabled"];
            }
            set
            {
                ViewState["IsDeleteEnabled"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool IsUploadEnabled
        {
            get
            {
                if (ViewState["IsUploadEnabled"] == null)
                    ViewState["IsUploadEnabled"] = true;

                return (bool)ViewState["IsUploadEnabled"];
            }
            set
            {
                ViewState["IsUploadEnabled"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Content
        {
            set { reHtmlEditor.Content = value; }
            get { return reHtmlEditor.Content; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Unit Width
        {
            set { reHtmlEditor.Width = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Unit Height
        {
            set { reHtmlEditor.Height = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string CallFunctionOnKeyUp
        {
            get { return (string)ViewState["CallFunctionOnKeyUp"]; }
            set { ViewState["CallFunctionOnKeyUp"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string CallFunctionOnKeyPress
        {
            get { return (string)ViewState["CallFunctionOnKeyPress"]; }
            set { ViewState["CallFunctionOnKeyPress"] = value; }
        }
            

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            reHtmlEditor.OnClientLoad = string.Format("{0}_OnClientLoad", ClientID);
            reHtmlEditor.OnClientCommandExecuting = string.Format("{0}_OnClientCommandExecuting", ClientID);            

            if (!Page.IsPostBack)
                InitRadEditor();
        }



        /// <summary>
        /// Inits the RAD editor.
        /// </summary>
        private void InitRadEditor()
        {
            if (string.IsNullOrEmpty(Module))
                throw new Exception("Не указан модуль");

            var fileSystemProvider = new FileSystemProvider();

            //reHtmlEditor.ImageManager.ViewPaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Image)) };
            //reHtmlEditor.DocumentManager.ViewPaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Document)) };
            //reHtmlEditor.FlashManager.ViewPaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Flash)) };
            //reHtmlEditor.MediaManager.ViewPaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Media)) };

            //if (IsDeleteEnabled)
            //{
            //    reHtmlEditor.ImageManager.DeletePaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Image)) };
            //    reHtmlEditor.DocumentManager.DeletePaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Document)) };
            //    reHtmlEditor.FlashManager.DeletePaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Flash)) };
            //    reHtmlEditor.MediaManager.DeletePaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Media)) };
            //}

            //if (IsUploadEnabled)
            //{
            //    reHtmlEditor.ImageManager.UploadPaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Image)) };
            //    reHtmlEditor.DocumentManager.UploadPaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Document)) };
            //    reHtmlEditor.FlashManager.UploadPaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Flash)) };
            //    reHtmlEditor.MediaManager.UploadPaths = new[] { ResolveUrl(fileSystemProvider.GetViewDirectory(CurrentUser.Instance.SiteID, Module, FileType.Media)) };
            //}



            var dataManager = new DataManager();

            var site = dataManager.Sites.SelectById(CurrentUser.Instance.SiteID);

            switch ((HtmlEditorMode)site.HtmlEditorModeID)
            {
                case HtmlEditorMode.Simple:
                    reHtmlEditor.ToolsFile = ResolveUrl("~/RadEditor/HtmlEditorToolsSimple.xml");
                    break;
                case HtmlEditorMode.Extended:
                    reHtmlEditor.ToolsFile = ResolveUrl("~/RadEditor/HtmlEditorToolsExtended.xml");
                    break;
            }
        }
    }
}