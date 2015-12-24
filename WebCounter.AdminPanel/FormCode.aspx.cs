using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.FileExplorer;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.FormCode;
using WebCounter.BusinessLogicLayer.Files;

namespace WebCounter.AdminPanel
{
    public partial class FormCode : System.Web.UI.Page
    {
        private string _activityCode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();

            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                _activityCode = Request.QueryString["code"];

            rblFormMode.Items.Clear();
            tsFormMode.Tabs.Clear();
            foreach (var mode in EnumHelper.EnumToList<FormMode>())
            {
                rblFormMode.Items.Add(new ListItem(EnumHelper.GetEnumDescription(mode), ((int)mode).ToString()));
                tsFormMode.Tabs.Add(new Telerik.Web.UI.RadTab(EnumHelper.GetEnumDescription(mode), ((int)mode).ToString()));
            }
            rblFormMode.SelectedIndex = 0;

            lrlCounterCode.Text = HttpUtility.HtmlEncode(ScriptTemplates.Form(CurrentUser.Instance.SiteID, _activityCode)
                .Replace("$Mode", ((int)FormMode.Inner).ToString())
                .Replace("$FromVisit", string.Empty)
                .Replace("$Through", string.Empty)
                .Replace("$Period", string.Empty)
                .Replace("$Parameter", string.Empty));

            //rblPeriod.Items.Clear();
            foreach (var period in EnumHelper.EnumToList<Period>())
                rblPeriod.Items.Add(new ListItem(EnumHelper.GetEnumDescription(period), ((int)period).ToString()));

            foreach (var category in EnumHelper.EnumToList<FormContactCategory>())
                rblContactCategory.Items.Add(new ListItem(EnumHelper.GetEnumDescription(category), ((int)category).ToString()));
            rblContactCategory.Items.FindByValue("0").Selected = true;

            foreach (var align in EnumHelper.EnumToList<PopupAlign>())
                rblPopupAlign.Items.Add(new ListItem("", ((int)align).ToString()));
            rblPopupAlign.Items.FindByValue("4").Selected = true;

            foreach (var effect in EnumHelper.EnumToList<PopupEffectAppear>())
                ddlPopupEffectAppear.Items.Add(new ListItem(EnumHelper.GetEnumDescription(effect), ((int)effect).ToString()));

            foreach (var position in EnumHelper.EnumToList<FloatingButtonPosition>())
                ddlFloatingButtonPosition.Items.Add(new ListItem(EnumHelper.GetEnumDescription(position), ((int)position).ToString()));            

            if (!Page.ClientScript.IsClientScriptIncludeRegistered("FormCodeJS"))
                ScriptManager.RegisterClientScriptInclude(this, typeof(FormCode), "FormCodeJS", ResolveUrl("~/Scripts/formcode.js"));

            if (!Page.ClientScript.IsClientScriptIncludeRegistered("HighlightJS"))
                ScriptManager.RegisterClientScriptInclude(this, typeof(FormCode), "HighlightJS", ResolveUrl("~/Scripts/highlight.pack.js"));

            if (!Page.ClientScript.IsClientScriptBlockRegistered("FormCodeTemplateJS"))
            {
                var script = string.Format("<script type=\"text/javascript\">var popupFormTemplate = \"{0}\"; var integratedFormTemplate = \"{1}\"; var autocallFormTemplate = \"{2}\"; var floatingButtonFormTemplate = \"{3}\"; var callOnClosingFormTemplate = \"{4}\";$('pre code').each(function(i, e) {{ hljs.highlightBlock(e); }});</script>",
                    HttpUtility.HtmlEncode(ScriptTemplates.PopupForm(CurrentUser.Instance.SiteID, _activityCode)),
                    HttpUtility.HtmlEncode(ScriptTemplates.IntegratedForm(CurrentUser.Instance.SiteID, _activityCode)),
                    HttpUtility.HtmlEncode(ScriptTemplates.AutoCallForm(CurrentUser.Instance.SiteID, _activityCode)),
                    HttpUtility.HtmlEncode(ScriptTemplates.FloatingButtonForm(CurrentUser.Instance.SiteID, _activityCode)),
                    HttpUtility.HtmlEncode(ScriptTemplates.CallOnClosingForm(CurrentUser.Instance.SiteID, _activityCode))).Replace("\r\n", "\\r\\n");
                ScriptManager.RegisterClientScriptBlock(this, typeof(FormCode), "FormCodeTemplateJS", script, false);
            }

            var fsProvider = new FileSystemProvider();
            var floatingButtonDirectory = fsProvider.GetFloatingButtonDirectory(CurrentUser.Instance.SiteID);
            rfeFloatingButtonIcon.Configuration.ViewPaths = new[] { floatingButtonDirectory };
            rfeFloatingButtonIcon.Configuration.UploadPaths = new[] { floatingButtonDirectory };
            rfeFloatingButtonIcon.Configuration.DeletePaths = new[] { floatingButtonDirectory };
            rfeFloatingButtonIcon.Configuration.MaxUploadFileSize = 1048576;

            hlSettings.NavigateUrl = UrlsData.AP_Settings("Settings").Replace("/FormCode.aspx", "");
        }
    }
}