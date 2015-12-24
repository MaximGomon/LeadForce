using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class EditorSiteActionTemplate : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();
        private Guid _siteId = CurrentUser.Instance.SiteID;
        protected RadAjaxManager radAjaxManager = null;


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteActionTemplateId
        {
            get
            {
                if (ViewState["SiteActionTemplateId"] == null)
                    ViewState["SiteActionTemplateId"] = Guid.Empty;

                return (Guid)ViewState["SiteActionTemplateId"];
            }
            set
            {
                ViewState["SiteActionTemplateId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool ShowMessageCaption
        {
            get
            {
                if (ViewState["ShowMessageCaption"] == null)
                    ViewState["ShowMessageCaption"] = true;

                return (bool)ViewState["ShowMessageCaption"];
            }
            set
            {
                ViewState["ShowMessageCaption"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string ValidationGroup
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    rfvMessageCaption.ValidationGroup = value;
                }
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool EnableClientScript
        {
            get
            {
                if (ViewState["EnableClientScript"] == null)
                    ViewState["EnableClientScript"] = true;

                return (bool)ViewState["EnableClientScript"];
            }
            set
            {
                ViewState["EnableClientScript"] = value;
            }
        }

        public bool FromSession { get; set; }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindSiteColumns();
                BindFiles();
            }

            if (!ShowMessageCaption)
                pnlMessageCaption.Visible = false;

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rauFile, lbAddFile);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbAddFile, rlbFiles);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbAddFile, rauFile, null, UpdatePanelRenderMode.Inline);

            rfvMessageCaption.EnableClientScript = EnableClientScript;
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            if (SiteActionTemplateId != Guid.Empty)
            {
                tbl_SiteActionTemplate siteActionTemplate;
                if (!FromSession)
                    siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(_siteId, SiteActionTemplateId);
                else
                {
                    siteActionTemplate = ((List<tbl_SiteActionTemplate>)Session["SiteActionTemplates"]).FirstOrDefault(a => a.ID == SiteActionTemplateId);
                    if (siteActionTemplate == null)
                        siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(_siteId, SiteActionTemplateId);
                }
                txtMessageCaption.Text = siteActionTemplate.MessageCaption;
                ucHtmlEditor.Content = siteActionTemplate.MessageBody;
            }
            else
            {
                txtMessageCaption.Text = string.Empty;
                ucHtmlEditor.Content = string.Empty;
            }
        }



        /// <summary>
        /// Rebinds the validators.
        /// </summary>
        /// <param name="validationGroup">The validation group.</param>
        public void RebindValidators(string validationGroup)
        {
            rfvMessageCaption.ValidationGroup = validationGroup;
        }



        /// <summary>
        /// Binds the site columns.
        /// </summary>
        public void BindSiteColumns()
        {
            rlbSiteColumns.Items.Add(new RadListBoxItem("Ф.И.О.", "#User.UserFullName#"));
            rlbSiteColumns.Items.Add(new RadListBoxItem("Фамилия", "#User.LastName#"));
            rlbSiteColumns.Items.Add(new RadListBoxItem("Имя", "#User.FirstName#"));
            rlbSiteColumns.Items.Add(new RadListBoxItem("Отчество", "#User.MiddleName#"));
            rlbSiteColumns.Items.Add(new RadListBoxItem("Email", "#User.Email#"));
            rlbSiteColumns.Items.Add(new RadListBoxItem("Телефон", "#User.Phone#"));
            rlbSiteColumns.Items.Add(new RadListBoxItem("Баллы", "#User.Score#"));
            rlbSiteColumns.Items.Add(new RadListBoxItem("Text", "#User.Text#"));
            rlbSiteColumns.Items.Add(new RadListBoxItem("Текст \"Отписаться\"", "#User.Unsubscribe#"));
            rlbSiteColumns.Items.Add(new RadListBoxItem("Ссылка \"Отписаться\"", "#User.UnsubscribeLink#"));            
            rlbSiteColumns.Items.Add(new RadListBoxItem("Рекламный блок", "#User.Advert#"));

            var siteColumns = _dataManager.SiteColumns.SelectAll(_siteId);
            foreach (var siteColumn in siteColumns)
                rlbSiteColumns.Items.Add(new RadListBoxItem(siteColumn.Name, string.Format("#User.{0}#", siteColumn.Code)));
        }



        /// <summary>
        /// Binds the files.
        /// </summary>
        public void BindFiles()
        {
            var files = _dataManager.Links.SelectByRuleType(_siteId, new List<int> { 5 });
            rlbFiles.Items.Clear();
            foreach (var file in files)
                rlbFiles.Items.Add(new RadListBoxItem(file.Name, string.Format("#Link.{0}#", file.Code)));
        }



        /// <summary>
        /// Handles the OnClick event of the lbAddFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbAddFile_OnClick(object sender, EventArgs e)
        {
            var fsp = new FileSystemProvider();
            if (rauFile.UploadedFiles.Count > 0)
            {
                string fileName = null;
                if (rauFile.UploadedFiles.Count > 0)
                {                    
                    IFileProvider fileProvider = new FSProvider();
                    fileName = fileProvider.GetFilename(_siteId, rauFile.UploadedFiles[0].FileName);
                    fsp.Upload(_siteId, fileName, rauFile.UploadedFiles[0].InputStream);

                }
                var link = new tbl_Links();
                link.SiteID = _siteId;
                link.Name = fileName;
                link.RuleTypeID = (int)RuleType.File;
                link.URL = fileName;
                link.FileSize = rauFile.UploadedFiles[0].InputStream.Length;
                string code = String.Format("file_[{0}]_[{1}]", DateTime.Now.ToString("ddMMyyyy"), DateTime.Now.ToString("mmss"));
                int maxCode = _dataManager.Links.SelectByCode(_siteId, code);
                if (maxCode != 0) maxCode++;
                link.Code = code + (maxCode != 0 ? String.Format("[{0}]", maxCode >= 10 ? maxCode.ToString() : "0" + maxCode.ToString()) : "");

                _dataManager.Links.Add(link);

                BindFiles();
            }
        }



        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            tbl_SiteActionTemplate siteActionTemplate;
            if (!FromSession)
                siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(_siteId, SiteActionTemplateId);
            else
                siteActionTemplate = ((List<tbl_SiteActionTemplate>)Session["SiteActionTemplates"]).FirstOrDefault(a => a.ID == SiteActionTemplateId);
            siteActionTemplate.MessageCaption = txtMessageCaption.Text;
            siteActionTemplate.MessageBody = ucHtmlEditor.Content;

            if (!FromSession)
                _dataManager.SiteActionTemplate.Update(siteActionTemplate);
        }
    }
}