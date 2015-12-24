using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using HtmlAgilityPack;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.FormCode;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Services;
using WebCounter.DataAccessLayer;
using WufooSharp;
using SortDirection = WufooSharp.SortDirection;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SiteActivityRule : System.Web.UI.UserControl
    {
        public Access access;
        private DataManager dataManager = new DataManager();
        private Guid _siteActivityRuleId;
        private Guid _newSiteActivityRuleId;
        public string strCommandArgument = string.Empty;
        public ContactData contactData;

        public Guid siteID = new Guid();

        public int _ruleTypeId = 0;

        private string _cacheKey = "Image_";

        private List<tbl_SiteColumns> dynamicSiteColumns;
        private List<tbl_SiteActivityRuleLayout> dynamicRuleLayout;

        private List<tbl_SiteActivityRuleExternalForms> dynamicExternalForms;
        private List<tbl_SiteActivityRuleExternalFormFields> dynamicExternalFormFields;

        private List<KeyValuePair<string, string>> WufooFileds = null;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Section Section
        {
            get
            {
                object o = ViewState["Section"];
                return (o == null ? Section.Monitoring : (Section)o);
            }
            set
            {
                ViewState["Section"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string SectionTitle
        {
            get
            {
                object o = ViewState["SectionTitle"];
                return (o == null ? null : (string)o);
            }
            set
            {
                ViewState["SectionTitle"] = value;
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            access = Access.Check();
            if (!access.Write)
                btnUpdate.Visible = false;

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rauImageFile, rbiImage);

            if ((CurrentUser.Instance.AccessLevel == AccessLevel.Administrator || CurrentUser.Instance.AccessLevel == AccessLevel.SystemAdministrator) && CurrentUser.Instance.IsTemplateSite)
            {
                plShowTextBlockInMaster.Visible = true;
                plIsUsedForAdditionalDetails.Visible = true;
            }

            string siteActivityRuleId = Page.RouteData.Values["ID"] as string;
            string ruleTypeId = Page.RouteData.Values["ruletypeid"] as string;
            int.TryParse(ruleTypeId, out _ruleTypeId);

            siteID = ((LeadForceBasePage)Page).SiteId;

            contactData = new ContactData(siteID);

            //dcbSiteActionTemplate.SiteID = siteID;
            dcbWorkflowTemplate.SiteID = siteID;

            hlCancel.NavigateUrl = _ruleTypeId != 0 ? UrlsData.AP_SiteActivityRules(_ruleTypeId) : UrlsData.AP_SiteActivityRules();

            if (!string.IsNullOrEmpty(siteActivityRuleId))
                Guid.TryParse(siteActivityRuleId, out _siteActivityRuleId);
            /*else
                _newSiteActivityRuleId = Guid.NewGuid();*/

            if (!string.IsNullOrEmpty(siteActivityRuleId) && siteActivityRuleId.ToLower() == "add")
            {
                if (ViewState["NewSiteActivityRuleId"] == null)
                {
                    _newSiteActivityRuleId = Guid.NewGuid();
                    ViewState["NewSiteActivityRuleId"] = _newSiteActivityRuleId;
                }
                else
                    _newSiteActivityRuleId = (Guid)ViewState["NewSiteActivityRuleId"];
            }

            if (!Page.IsPostBack)
            {
                ClearCache();

                ViewState["DynamicRuleLayout"] = dataManager.SiteActivityRuleLayout.SelectBySiteActivityRuleId(_siteActivityRuleId) ?? new List<tbl_SiteActivityRuleLayout>();
                List<tbl_SiteColumns> siteColumns = null;
                if (_siteActivityRuleId != Guid.Empty)
                    siteColumns = dataManager.SiteColumns.SelectAll(siteID).Where(a => a.SiteActivityRuleID == _siteActivityRuleId).ToList();
                ViewState["DynamicSiteColumns"] = siteColumns ?? new List<tbl_SiteColumns>();

                List<tbl_SiteColumnValues> siteColumnValues = null;
                siteColumnValues = dataManager.SiteColumnValues.SelectBySiteActivityRuleID(_siteActivityRuleId);
                ViewState["DynamicSiteColumnValues"] = siteColumnValues ?? new List<tbl_SiteColumnValues>();
                ViewState["DynamicSiteColumnValuesEdit"] = new List<tbl_SiteColumnValues>();
                ViewState["DynamicRuleLayoutRemoveIDs"] = new List<Guid>();

                ViewState["DynamicExternalForms"] = dataManager.SiteActivityRuleExternalForms.SelectByRuleId(_siteActivityRuleId) ?? new List<tbl_SiteActivityRuleExternalForms>();
                ViewState["DynamicExternalFormFields"] = new List<tbl_SiteActivityRuleExternalFormFields>();

                BindData();
                BindTree();
                BindExternalForms();

                ddlOrientation.Items.Add(new ListItem("По вертикали", "1"));
                ddlOrientation.Items.Add(new ListItem("По горизонтали", "2"));

                ddlOutputFormat.Items.Add(new ListItem("Скрытая группа", "1"));
                ddlOutputFormat.Items.Add(new ListItem("Группа с заголовком", "2"));
                //ddlOutputFormat.Items.Add(new ListItem("Вкладка", "3"));

                ddlOutputFormatFields.Items.Add(new ListItem("Заголовок над элементом", "1"));
                ddlOutputFormatFields.Items.Add(new ListItem("Заголовок слева от элемента", "2"));
                ddlOutputFormatFields.Items.Add(new ListItem("Без заголовка", "3"));
                ddlOutputFormatFields.Items.Add(new ListItem("Заголовок в элементе", "4"));

                EnumHelper.EnumToDropDownList<ShowTextBlockInMaster>(ref ddlShowTextBlockInMaster, false);
            }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            EnumHelper.EnumToDropDownList<ActionOnFillForm>(ref ddlActionOnFillForm, false);

            var ruleTypes = new List<tbl_RuleTypes>();
            switch ((RuleType)_ruleTypeId)
            {
                case RuleType.Link:
                    ruleTypes = dataManager.RuleTypes.SelectAll().Where(a => a.ID == (int)RuleType.Link).ToList();
                    break;
                case RuleType.Form:
                    ruleTypes = dataManager.RuleTypes.SelectAll().Where(a => a.ID == (int)RuleType.Form).ToList();
                    break;
                case RuleType.File:
                    ruleTypes = dataManager.RuleTypes.SelectAll().Where(a => a.ID == (int)RuleType.File).ToList();
                    break;
                case RuleType.ExternalForm:
                    ruleTypes = dataManager.RuleTypes.SelectAll().Where(a => a.ID == (int)RuleType.ExternalForm).ToList();
                    break;
                case RuleType.LPgenerator:
                    ruleTypes = dataManager.RuleTypes.SelectAll().Where(a => a.ID == (int)RuleType.LPgenerator).ToList();
                    break;
                default:
                    ruleTypes = dataManager.RuleTypes.SelectAll();
                    break;
            }
            if ((RuleType)_ruleTypeId == RuleType.Form || (RuleType)_ruleTypeId == RuleType.ExternalForm || (RuleType)_ruleTypeId == RuleType.LPgenerator)
            {
                plRuleType.Visible = false;
                plDescription.Visible = true;
            }
            ddlRuleTypeID.DataSource = ruleTypes;
            ddlRuleTypeID.DataTextField = "Title";
            ddlRuleTypeID.DataValueField = "ID";
            ddlRuleTypeID.DataBind();

            cblSiteColumns.DataSource = dataManager.SiteColumns.SelectAll(siteID).OrderBy(a => a.Name);
            cblSiteColumns.DataTextField = "Name";
            cblSiteColumns.DataValueField = "ID";
            cblSiteColumns.DataBind();

            EnumHelper.EnumToDropDownList<Skin>(ref ddlSkin, false);

            EnumHelper.EnumToDropDownList<WufooUpdatePeriod>(ref ddlWufooUpdatePeriod);

            if (_siteActivityRuleId != Guid.Empty)
            {
                var siteActivityRules = dataManager.SiteActivityRules.SelectById(CurrentUser.Instance.SiteID, _siteActivityRuleId);
                txtName.Text = Server.HtmlEncode(siteActivityRules.Name);
                ddlRuleTypeID.Items.FindByValue(siteActivityRules.RuleTypeID.ToString()).Selected = true;
                txtCode.Text = Server.HtmlEncode(siteActivityRules.Code);
                txtCode.ReadOnly = true;
                ddlActionOnFillForm.Items.FindByValue(siteActivityRules.ActionOnFillForm.ToString()).Selected = true;
                txtURL.Text = Server.HtmlEncode(siteActivityRules.URL);
                cbSendFields.Checked = siteActivityRules.SendFields;
                txtSuccessMessage.Text = siteActivityRules.SuccessMessage;
                txtDescription.Text = siteActivityRules.Description;
                cbUserFullName.Checked = siteActivityRules.UserFullName;
                cbEmail.Checked = siteActivityRules.Email;
                cbPhone.Checked = siteActivityRules.Phone;
                txtYandexGoals.Text = siteActivityRules.YandexGoals;

                txtFormWidth.Text = siteActivityRules.FormWidth.ToString();
                txtCountExtraFields.Text = siteActivityRules.CountExtraFields.ToString();

                txtExternalFormUrl.Text = siteActivityRules.ExternalFormURL;
                txtRepostURL.Text = siteActivityRules.RepostURL;
                txtErrorMessage.Text = siteActivityRules.ErrorMessage;

                txtWufooName.Text = siteActivityRules.WufooName;
                txtWufooAPIKey.Text = siteActivityRules.WufooAPIKey;
                rdtpRevisionDate.SelectedDate = siteActivityRules.WufooRevisionDate;
                rdtpLoadDataDate.SelectedDate = siteActivityRules.WufooRevisionDate;
                if (siteActivityRules.WufooUpdatePeriod.HasValue)
                    ddlWufooUpdatePeriod.SelectedValue = siteActivityRules.WufooUpdatePeriod.Value.ToString();

                if ((RuleType)siteActivityRules.RuleTypeID == RuleType.Form)
                {
                    var siteActivityRuleFormColumns = dataManager.SiteActivityRuleFormColumns.SelectBySiteActivityRuleID(_siteActivityRuleId);
                    if (siteActivityRuleFormColumns != null && siteActivityRuleFormColumns.Count > 0)
                    {
                        foreach (var siteActivityRuleFormColumn in siteActivityRuleFormColumns)
                        {
                            cblSiteColumns.Items.FindByValue(siteActivityRuleFormColumn.SiteColumnID.ToString()).Selected = true;
                        }
                    }

                    DecodeCss(siteActivityRules.CSSForm);
                    ddlSkin.Items.FindByValue(siteActivityRules.Skin.ToString()).Selected = true;
                    ucCssEditorButton.Css = siteActivityRules.CSSButton;
                    txtTextButton.Text = siteActivityRules.TextButton ?? "";

                    var siteActivityRuleLayouts = siteActivityRules.tbl_SiteActivityRuleLayout.Where(o => !string.IsNullOrEmpty(o.LayoutParams)).OrderBy(o => o.Order);
                    foreach (var activityRuleLayout in siteActivityRuleLayouts)
                    {
                        var layoutParams = LayoutParams.Deserialize(activityRuleLayout.LayoutParams);
                        if (!string.IsNullOrEmpty(layoutParams.GetValue("ShowInMaster")) && int.Parse(layoutParams.GetValue("ShowInMaster")) > 1)
                        {
                            if (!string.IsNullOrEmpty(activityRuleLayout.CSSStyle))
                            {
                                ucCssEditorInstruction.Css = activityRuleLayout.CSSStyle;
                                break;
                            }
                        }
                    }

                    siteActivityRuleLayouts = siteActivityRules.tbl_SiteActivityRuleLayout.Where(o => string.IsNullOrEmpty(o.LayoutParams) && !string.IsNullOrEmpty(o.CSSStyle)).OrderBy(o => o.Order);
                    if (siteActivityRuleLayouts.Any())
                    {
                        var tblSiteActivityRuleLayout = siteActivityRuleLayouts.FirstOrDefault();
                        if (tblSiteActivityRuleLayout != null)
                            ucCssEditorColumns.Css = tblSiteActivityRuleLayout.CSSStyle;
                    }

                    ucExternalResources.DestinationId = _siteActivityRuleId;

                    pnlTextButton.Visible = true;
                    //pFormFields.Visible = true;
                    RadTabStrip1.FindTabByValue("FormTemplate").Visible = true;
                    RadTabStrip1.FindTabByValue("DesignForm").Visible = true;
                    RadTabStrip1.FindTabByValue("ExternalResource").Visible = true;
                    ////RadPageView3.Visible = false;
                }

                if ((RuleType)siteActivityRules.RuleTypeID == RuleType.File)
                {
                    pURL.Visible = false;
                    pFile.Visible = true;
                }

                if ((RuleType)siteActivityRules.RuleTypeID == RuleType.ExternalForm || (RuleType)siteActivityRules.RuleTypeID == RuleType.LPgenerator)
                {
                    RadTabStrip1.FindTabByValue("ExternalForm").Visible = true;
                    ////RadPageView2.Visible = false;
                    pURL.Visible = false;
                }

                if ((RuleType)siteActivityRules.RuleTypeID == RuleType.WufooForm)
                {
                    RadTabStrip1.Tabs[0].Text = "Основная информация";
                    RadTabStrip1.FindTabByValue("ExternalForm").Visible = true;
                    RadTabStrip1.FindTabByValue("ExternalForm").Text = "Форма Wufoo";
                    ////RadPageView2.Visible = false;
                    pURL.Visible = false;
                    plWufooForm.Visible = true;
                    plDescription.Visible = true;
                    plExternalFormUrls.Visible = false;
                    plRuleType.Visible = false;
                    lbtnLoadData.Visible = true;
                }

                switch ((ActionOnFillForm)siteActivityRules.ActionOnFillForm)
                {
                    case ActionOnFillForm.Redirect:
                        pnlActionMessage.Visible = false;
                        break;
                    case ActionOnFillForm.PopupMessage:
                    case ActionOnFillForm.Message:
                        pnlActionRedirect.Visible = false;
                        break;
                }
            }
            else
            {
                if ((RuleType)_ruleTypeId == RuleType.Form)
                {

                    ucCssEditorButton.Css = string.Empty;
                    rcpBackgroundColor.SelectedColor = Color.Transparent;
                    ddlSkin.SelectedIndex = 0;
                    ucCssEditorInstruction.Css = string.Empty;
                    ucCssEditorColumns.Css = string.Empty;
                    txtTextButton.Text = string.Empty;
                    pnlTextButton.Visible = true;
                    //pFormFields.Visible = true;
                    RadTabStrip1.FindTabByValue("FormTemplate").Visible = true;
                    RadTabStrip1.FindTabByValue("DesignForm").Visible = true;
                    RadTabStrip1.FindTabByValue("ExternalResource").Visible = true;
                    ////RadPageView3.Visible = false;
                }

                if ((RuleType)_ruleTypeId == RuleType.File)
                {
                    pURL.Visible = false;
                    pFile.Visible = true;
                }

                if ((RuleType)_ruleTypeId == RuleType.ExternalForm || (RuleType)_ruleTypeId == RuleType.LPgenerator)
                {
                    RadTabStrip1.FindTabByValue("ExternalForm").Visible = true;
                    ////RadPageView2.Visible = false;
                    pURL.Visible = false;
                }

                pnlActionMessage.Visible = false;
            }
        }



        /// <summary>
        /// Binds the tree.
        /// </summary>
        protected void BindTree()
        {
            Reorder();

            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();
            var dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();
            dynamicSiteColumns.AddRange(dataManager.SiteColumns.SelectAll(siteID).Where(a => a.SiteActivityRuleID == null));
            var dynamicRuleLayoutWithRoot = dynamicRuleLayout.Select(a => new { a.ID, ParentID = (Guid?)a.ParentID ?? (Guid?)Guid.Empty, Name = a.SiteColumnID == null ? a.Name : dynamicSiteColumns.FirstOrDefault(x => x.ID == a.SiteColumnID).Name, Order = (int?)a.Order, a.LayoutType }).ToList();
            dynamicRuleLayoutWithRoot.Add(new { ID = Guid.Empty, ParentID = (Guid?)null, Name = string.IsNullOrEmpty(txtName.Text) ? "Форма" : txtName.Text, Order = (int?)0, LayoutType = (int)LayoutType.Root });
            dynamicRuleLayoutWithRoot = dynamicRuleLayoutWithRoot.OrderBy(a => a.Order).ToList();

            rtvFormTree.DataSource = dynamicRuleLayoutWithRoot;
            rtvFormTree.DataFieldID = "ID";
            rtvFormTree.DataValueField = "ID";
            rtvFormTree.DataFieldParentID = "ParentID";
            rtvFormTree.DataTextField = "Name";
            rtvFormTree.DataBind();

            rtvFormTree.Nodes[0].ImageUrl = ResolveUrl("~/App_Themes/Default/images/icoForm.png");
            foreach (var item in dynamicRuleLayoutWithRoot)
            {
                switch ((LayoutType)item.LayoutType)
                {
                    case LayoutType.GroupFields:
                        rtvFormTree.FindNodeByValue(item.ID.ToString()).ImageUrl = ResolveUrl("~/App_Themes/Default/images/icoGroupFields.png");
                        break;
                    case LayoutType.ProfileField:
                    /*case LayoutType.FullName:
                    case LayoutType.Email:
                    case LayoutType.Phone:
                    case LayoutType.Surname:
                    case LayoutType.Name:
                    case LayoutType.Patronymic:*/
                        rtvFormTree.FindNodeByValue(item.ID.ToString()).ImageUrl = ResolveUrl("~/App_Themes/Default/images/icoProfileField.png");
                        break;
                    case LayoutType.FormField:
                        rtvFormTree.FindNodeByValue(item.ID.ToString()).ImageUrl = ResolveUrl("~/App_Themes/Default/images/icoFormField.png");
                        break;
                    case LayoutType.TextBlock:
                        rtvFormTree.FindNodeByValue(item.ID.ToString()).ImageUrl = ResolveUrl("~/App_Themes/Default/images/icoTextBlock.png");
                        break;
                    case LayoutType.Image:
                        rtvFormTree.FindNodeByValue(item.ID.ToString()).ImageUrl = ResolveUrl("~/App_Themes/Default/images/icoImage.png");
                        break;
                }

            }

            if (rtvFormTree.SelectedNode == null) rtvFormTree.Nodes[0].Selected = true;
        }



        protected void BindExternalForms()
        {
            dynamicExternalForms = (List<tbl_SiteActivityRuleExternalForms>)ViewState["DynamicExternalForms"] ?? new List<tbl_SiteActivityRuleExternalForms>();
            lvExternalForms.DataSource = dynamicExternalForms;
            lvExternalForms.DataBind();

            dynamicExternalFormFields = (List<tbl_SiteActivityRuleExternalFormFields>)ViewState["DynamicExternalFormFields"] ?? new List<tbl_SiteActivityRuleExternalFormFields>();
            foreach (var item in dynamicExternalForms)
            {
                var externalFormFields = dataManager.SiteActivityRuleExternalFormFields.SelectByExternalFormId(item.ID);
                dynamicExternalFormFields.AddRange(externalFormFields);
            }

            if ((RuleType)_ruleTypeId == RuleType.LPgenerator && dynamicExternalForms.Any())
            {
                rgExternalFormFields.DataSource = dynamicExternalFormFields.Where(a => a.SiteActivityRuleExternalFormID == dynamicExternalForms.FirstOrDefault().ID);
                rgExternalFormFields.DataBind();
                rgExternalFormFields.Visible = true;
            }

            if ((RuleType)_ruleTypeId == RuleType.WufooForm)
            {
                GetWufooForm();
            }
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlRuleTypeID control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlRuleTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //pFormFields.Visible = false;
            pLandingPage.Visible = false;
            pFile.Visible = false;
            pURL.Visible = true;
            RadTabStrip1.FindTabByValue("FormTemplate").Visible = false;
            RadTabStrip1.FindTabByValue("ExternalForm").Visible = false;
            RadTabStrip1.FindTabByValue("DesignForm").Visible = false;
            RadTabStrip1.FindTabByValue("ExternalResource").Visible = false;
            switch ((RuleType)int.Parse(ddlRuleTypeID.SelectedValue))
            {
                case RuleType.Form:
                    //pFormFields.Visible = true;
                    RadTabStrip1.FindTabByValue("FormTemplate").Visible = true;
                    RadTabStrip1.FindTabByValue("DesignForm").Visible = true;
                    RadTabStrip1.FindTabByValue("ExternalResource").Visible = true;
                    break;
                case RuleType.LandingPage:
                    pLandingPage.Visible = true;
                    break;
                case RuleType.File:
                    pURL.Visible = false;
                    pFile.Visible = true;
                    break;
                case RuleType.ExternalForm:
                case RuleType.LPgenerator:
                    RadTabStrip1.FindTabByValue("ExternalForm").Visible = true;
                    break;
            }
        }



        /// <summary>
        /// Handles the Click event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var siteActivityRule = new tbl_SiteActivityRules();

            var checkCode = dataManager.SiteActivityRules.Select(siteID, txtCode.Text);
            var filename = string.Empty;

            if (_siteActivityRuleId != Guid.Empty)
            {
                siteActivityRule = dataManager.SiteActivityRules.SelectById(_siteActivityRuleId);
                if (checkCode != null && checkCode.ID == _siteActivityRuleId)
                    checkCode = null;
            }

            ErrorMessage.Text = "";
            if (checkCode == null)
            {
                siteActivityRule.SiteID = siteID;
                siteActivityRule.Name = txtName.Text;
                siteActivityRule.RuleTypeID = int.Parse(ddlRuleTypeID.SelectedValue);
                if (_siteActivityRuleId == Guid.Empty)
                    siteActivityRule.Code = txtCode.Text;

                IFileProvider fileProvider = new FSProvider();
                if ((RuleType)int.Parse(ddlRuleTypeID.SelectedValue) == RuleType.File && fuFile.HasFile)
                {
                    filename = fileProvider.GetFilename(siteID, fuFile.FileName);
                    siteActivityRule.URL = filename;
                }
                else
                {
                    siteActivityRule.ActionOnFillForm = ddlActionOnFillForm.SelectedValue.ToInt();
                    siteActivityRule.URL = null;
                    siteActivityRule.SendFields = false;
                    siteActivityRule.SuccessMessage = null;
                    switch ((ActionOnFillForm)ddlActionOnFillForm.SelectedValue.ToInt())
                    {
                          case ActionOnFillForm.Redirect:
                            siteActivityRule.URL = txtURL.Text;
                            siteActivityRule.SendFields = cbSendFields.Checked;
                            break;
                          case ActionOnFillForm.PopupMessage:
                          case ActionOnFillForm.Message:
                            siteActivityRule.SuccessMessage = txtSuccessMessage.Text;
                            break;
                    }
                    siteActivityRule.YandexGoals = txtYandexGoals.Text;
                }

                siteActivityRule.ErrorMessage = txtErrorMessage.Text;
                siteActivityRule.Description = txtDescription.Text;
                siteActivityRule.UserFullName = cbUserFullName.Checked;
                siteActivityRule.Email = cbEmail.Checked;
                siteActivityRule.Phone = cbPhone.Checked;

                if ((RuleType)int.Parse(ddlRuleTypeID.SelectedValue) == RuleType.Form)
                {
                    siteActivityRule.FormWidth = !string.IsNullOrEmpty(txtFormWidth.Text) ? int.Parse(txtFormWidth.Text) : (int?)null;
                    siteActivityRule.CountExtraFields = !string.IsNullOrEmpty(txtCountExtraFields.Text) ? int.Parse(txtCountExtraFields.Text) : (int?)null;
                    siteActivityRule.CSSForm = EncodeBackgroundCss(rcpBackgroundColor);
                    siteActivityRule.Skin = ddlSkin.SelectedValue.ToInt();
                    siteActivityRule.CSSButton = ucCssEditorButton.GetCss();
                    siteActivityRule.TextButton = txtTextButton.Text != "" ? txtTextButton.Text : null;
                }
                else
                {
                    siteActivityRule.FormWidth = null;
                    siteActivityRule.CountExtraFields = null;
                    siteActivityRule.CSSButton = null;
                    siteActivityRule.TextButton = null;
                }

                if ((RuleType)int.Parse(ddlRuleTypeID.SelectedValue) == RuleType.ExternalForm || (RuleType)int.Parse(ddlRuleTypeID.SelectedValue) == RuleType.LPgenerator)
                {
                    siteActivityRule.ExternalFormURL = txtExternalFormUrl.Text;
                    siteActivityRule.RepostURL = txtRepostURL.Text;
                }
                else
                {
                    siteActivityRule.ExternalFormURL = null;
                    siteActivityRule.RepostURL = null;
                }

                if ((RuleType)int.Parse(ddlRuleTypeID.SelectedValue) == RuleType.WufooForm)
                {
                    siteActivityRule.URL = string.Format("http://{0}.wufoo.com/forms/{1}/", siteActivityRule.WufooName, siteActivityRule.Code);
                    siteActivityRule.WufooRevisionDate = rdtpRevisionDate.SelectedDate;
                    if (!string.IsNullOrEmpty(ddlWufooUpdatePeriod.SelectedValue))
                        siteActivityRule.WufooUpdatePeriod = int.Parse(ddlWufooUpdatePeriod.SelectedValue);
                }

                if (_siteActivityRuleId != Guid.Empty)
                {
                    dataManager.SiteActivityRules.Update(siteActivityRule);
                }
                else
                {
                    siteActivityRule.OwnerID = CurrentUser.Instance.ContactID;
                    siteActivityRule.ID = _newSiteActivityRuleId;
                    siteActivityRule = dataManager.SiteActivityRules.Add(siteActivityRule);
                    _siteActivityRuleId = siteActivityRule.ID;
                }

                // Add/Update/Delete layouts
                if ((RuleType)siteActivityRule.RuleTypeID == RuleType.Form)
                {
                    LoadProperties();

                    dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();
                    foreach (var item in dynamicSiteColumns)
                    {
                        if (dataManager.SiteColumns.SelectById(siteID, item.ID) == null)
                        {
                            item.SiteActivityRuleID = _siteActivityRuleId;
                            dataManager.SiteColumns.Add(item);
                        }
                        else
                            dataManager.SiteColumns.Update(item);
                    }

                    dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();
                    foreach (var item in dynamicRuleLayout)
                    {
                        item.SiteActivityRuleID = _siteActivityRuleId;
                        if (item.ParentID == Guid.Empty)
                            item.ParentID = null;

                        if (!string.IsNullOrEmpty(item.LayoutParams))
                        {
                            var lp = LayoutParams.Deserialize(item.LayoutParams);
                            if (!string.IsNullOrEmpty(lp.GetValue("ShowInMaster")))
                            {
                                item.CSSStyle = ucCssEditorInstruction.GetCss();
                            }
                        }

                        if (string.IsNullOrEmpty(item.LayoutParams) && item.LayoutType != (int)LayoutType.Feedback
                             && item.LayoutType != (int)LayoutType.GroupFields && item.LayoutType != (int)LayoutType.InviteFriend
                             && item.LayoutType != (int)LayoutType.Root && item.LayoutType != (int)LayoutType.TextBlock && item.LayoutType != (int)LayoutType.Image)
                        {
                            //item.CSSStyle = ucCssEditorColumns.GetCss();
                            if (!string.IsNullOrEmpty(item.CSSStyle))
                                item.CSSStyle = Regex.Replace(item.CSSStyle, @"(font-family:.*?;)|(font-size:.*?;)|(font-weight:.*?;)|(color:.*?;)|(background-color:.*?;)", "", RegexOptions.IgnoreCase) + ucCssEditorColumns.GetCss();
                            else
                                item.CSSStyle = ucCssEditorColumns.GetCss();
                        }

                        if (item.LayoutType == (int)LayoutType.Image)
                        {
                            var fsp = new FileSystemProvider();

                            if (HttpContext.Current.Cache.Get(_cacheKey + item.ID.ToString()) != null)
                            {
                                fsp.Delete(siteID, "SiteActivityRule", item.ID.ToString() + ".png", FileType.Image);
                                var image = (byte[])HttpContext.Current.Cache.Get(_cacheKey + item.ID.ToString());
                                Stream stream = new MemoryStream(image);
                                fsp.Upload(siteID, "SiteActivityRule", item.ID.ToString() + ".png", stream, FileType.Image);
                                HttpContext.Current.Cache.Remove(_cacheKey + item.ID.ToString());
                            }
                        }

                        if (dataManager.SiteActivityRuleLayout.SelectById(item.ID) == null)
                            dataManager.SiteActivityRuleLayout.Add(item);
                        else
                            dataManager.SiteActivityRuleLayout.Update(item);
                    }

                    var dynamicRuleLayoutRemoveIDs = (List<Guid>)ViewState["DynamicRuleLayoutRemoveIDs"] ?? new List<Guid>();
                    foreach (var item in dynamicRuleLayoutRemoveIDs)
                    {
                        dataManager.SiteActivityRuleLayout.Delete(item);
                        var fsp = new FileSystemProvider();
                        fsp.Delete(siteID, "SiteActivityRule", item.ToString() + ".png", FileType.Image);
                    }

                    var dynamicSiteColumnValues = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValues"] ?? new List<tbl_SiteColumnValues>();
                    foreach (var item in dynamicSiteColumnValues)
                    {
                        if (dataManager.SiteColumnValues.SelectById(item.ID) == null)
                            dataManager.SiteColumnValues.Add(item);
                        else
                            dataManager.SiteColumnValues.Update(item);
                    }

                    var removeSiteColumnValues = dataManager.SiteColumnValues.SelectBySiteActivityRuleID(_siteActivityRuleId).Where(a => !dynamicSiteColumnValues.Select(x => x.ID).Contains(a.ID)).ToList();
                    foreach (var item in removeSiteColumnValues)
                    {
                        dataManager.SiteColumnValues.Delete(item);
                    }

                    dataManager.ExternalResource.Update(ucExternalResources.ExternalResourceList, _siteActivityRuleId);

                    /*var dynamicSiteColumnsRemoveIDs = (List<Guid>)ViewState["DynamicSiteColumnsRemoveIDs"] ?? new List<Guid>();
                    foreach (var item in dynamicSiteColumnsRemoveIDs)
                    {
                        //dataManager.SiteColumns.
                    }*/
                }
                else
                {
                    var ruleLayouts = dataManager.SiteActivityRuleLayout.SelectBySiteActivityRuleId(siteActivityRule.ID);
                    foreach (var item in ruleLayouts)
                    {
                        dataManager.SiteActivityRuleLayout.Delete(item.ID);
                    }
                }


                // External form
                if ((RuleType)int.Parse(ddlRuleTypeID.SelectedValue) == RuleType.ExternalForm
                    || (RuleType)int.Parse(ddlRuleTypeID.SelectedValue) == RuleType.WufooForm
                    || (RuleType)int.Parse(ddlRuleTypeID.SelectedValue) == RuleType.LPgenerator)
                {
                    dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();
                    foreach (var item in dynamicSiteColumns)
                    {
                        if (dataManager.SiteColumns.SelectById(siteID, item.ID) == null)
                            dataManager.SiteColumns.Add(item);
                        else
                            dataManager.SiteColumns.Update(item);
                    }

                    var dynamicSiteColumnValues = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValues"] ?? new List<tbl_SiteColumnValues>();
                    foreach (var item in dynamicSiteColumnValues)
                    {
                        if (dataManager.SiteColumnValues.SelectById(item.ID) == null)
                            dataManager.SiteColumnValues.Add(item);
                        else
                            dataManager.SiteColumnValues.Update(item);
                    }

                    var removeSiteColumnValues = dataManager.SiteColumnValues.SelectBySiteActivityRuleID(_siteActivityRuleId).Where(a => !dynamicSiteColumnValues.Select(x => x.ID).Contains(a.ID)).ToList();
                    foreach (var item in removeSiteColumnValues)
                    {
                        dataManager.SiteColumnValues.Delete(item);
                    }


                    var oldExternalForms = dataManager.SiteActivityRuleExternalForms.SelectByRuleId(siteActivityRule.ID);
                    dynamicExternalForms = (List<tbl_SiteActivityRuleExternalForms>)ViewState["DynamicExternalForms"] ?? new List<tbl_SiteActivityRuleExternalForms>();
                    foreach (var item in dynamicExternalForms)
                    {
                        item.SiteActivityRuleID = siteActivityRule.ID;
                        if (dataManager.SiteActivityRuleExternalForms.Select(item.Name, item.SiteActivityRuleID) == null)
                            dataManager.SiteActivityRuleExternalForms.Add(item);
                        /*else
                            dataManager.SiteActivityRuleExternalForms.Update(item);*/

                        if (oldExternalForms.Count > 0 && oldExternalForms.FindIndex(a => a.ID == item.ID) != -1)
                            oldExternalForms.RemoveAt(oldExternalForms.FindIndex(a => a.ID == item.ID));
                    }

                    if (dynamicExternalForms.Count > 0)
                        foreach (var item in oldExternalForms)
                            dataManager.SiteActivityRuleExternalForms.Delete(item);



                    var oldExternalFormFields = new List<tbl_SiteActivityRuleExternalFormFields>();
                    foreach (var item in dynamicExternalForms)
                    {
                        var externalFormFields = dataManager.SiteActivityRuleExternalFormFields.SelectByExternalFormId(item.ID);
                        oldExternalFormFields.AddRange(externalFormFields);
                    }

                    UpdateExternalFormNames();


                    dynamicExternalFormFields = (List<tbl_SiteActivityRuleExternalFormFields>)ViewState["DynamicExternalFormFields"] ?? new List<tbl_SiteActivityRuleExternalFormFields>();
                    foreach (var item in dynamicExternalFormFields)
                    {
                        //item.SiteActivityRuleID = siteActivityRule.ID;
                        if (//dataManager.SiteActivityRuleExternalFormFields.Select(item.Name, item.SiteActivityRuleExternalFormID) == null &&
                            dataManager.SiteActivityRuleExternalFormFields.Select(item.ID, item.SiteActivityRuleExternalFormID) == null)
                            dataManager.SiteActivityRuleExternalFormFields.Add(item);
                        else
                            dataManager.SiteActivityRuleExternalFormFields.Update(item);

                        if (oldExternalFormFields.Count > 0 && oldExternalFormFields.FindIndex(a => a.ID == item.ID) != -1)
                            oldExternalFormFields.RemoveAt(oldExternalFormFields.FindIndex(a => a.ID == item.ID));
                    }

                    if (dynamicExternalFormFields.Count > 0)
                        foreach (var item in oldExternalFormFields)
                            dataManager.SiteActivityRuleExternalFormFields.Delete(item);
                }


                /*dataManager.SiteActivityRuleFormColumns.DeleteBySiteActivityRuleID(_siteActivityRuleId);
                for (var i = 0; i < cblSiteColumns.Items.Count; i++)
                {
                    if (cblSiteColumns.Items[i].Selected)
                    {
                        var siteActivityRuleFormColumns = new tbl_SiteActivityRuleFormColumns();
                        siteActivityRuleFormColumns.SiteActivityRuleID = _siteActivityRuleId;
                        siteActivityRuleFormColumns.SiteColumnID = Guid.Parse(cblSiteColumns.Items[i].Value);
                        dataManager.SiteActivityRuleFormColumns.Add(siteActivityRuleFormColumns);
                    }
                }*/

                if ((RuleType)int.Parse(ddlRuleTypeID.SelectedValue) == RuleType.File && fuFile.HasFile)
                {
                    fileProvider.Upload(siteID, filename, fuFile);
                }

                Response.Redirect(_ruleTypeId != 0 ? UrlsData.AP_SiteActivityRules(_ruleTypeId) : UrlsData.AP_SiteActivityRules());
            }
            else
                ErrorMessage.Text = "Правило с таким кодом уже существует.<br /><br />";
        }



        /// <summary>
        /// Updates the external form names.
        /// </summary>
        private void UpdateExternalFormNames()
        {
            if ((RuleType)int.Parse(ddlRuleTypeID.SelectedValue) == RuleType.LPgenerator)
            {
                dynamicExternalFormFields = (List<tbl_SiteActivityRuleExternalFormFields>)ViewState["DynamicExternalFormFields"] ?? new List<tbl_SiteActivityRuleExternalFormFields>();

                foreach (GridDataItem rgExternalFormField in rgExternalFormFields.Items)
                {
                    var id = Guid.Parse(rgExternalFormField.GetDataKeyValue("ID").ToString());
                    var siteActivityRuleExternalFormId = Guid.Parse(rgExternalFormField.GetDataKeyValue("SiteActivityRuleExternalFormID").ToString());
                    var externalFormField = dynamicExternalFormFields.FirstOrDefault(o => o.ID == id && o.SiteActivityRuleExternalFormID == siteActivityRuleExternalFormId);
                    if (externalFormField != null)
                        externalFormField.Name = ((TextBox)rgExternalFormField.FindControl("txtName")).Text;
                }
            }
        }



        /// <summary>
        /// Handles the OnClick event of the btnAddGroupFields control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnAddGroupFields_OnClick(object sender, EventArgs e)
        {
            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();

            if (rtvFormTree.SelectedNode != null)
            {
                var currentNode = dynamicRuleLayout.FirstOrDefault(a => a.ID == Guid.Parse(rtvFormTree.SelectedNode.Value));
                if (currentNode != null && (LayoutType)currentNode.LayoutType != LayoutType.GroupFields)
                    return;
            }

            RadTreeNode selectedNode = null;
            var siteActivityRuleLayout = new tbl_SiteActivityRuleLayout { ID = Guid.NewGuid(), SiteID = siteID, Name = "Группа полей", LayoutType = (int)LayoutType.GroupFields };
            if (rtvFormTree.SelectedNode != null)
            {
                siteActivityRuleLayout.ParentID = Guid.Parse(rtvFormTree.SelectedNode.Value);
                selectedNode = rtvFormTree.SelectedNode;
            }

            dynamicRuleLayout.Add(siteActivityRuleLayout);

            BindTree();

            if (selectedNode != null)
                rtvFormTree.FindNodeByValue(selectedNode.Value).Selected = true;
        }



        /// <summary>
        /// Handles the OnClick event of the btnAddProfileField control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnAddProfileField_OnClick(object sender, EventArgs e)
        {
            //var siteColumn = dataManager.SiteColumns.SelectAll(siteID).Where(a => a.SiteActivityRuleID == null).FirstOrDefault();
            //if (siteColumn != null)
            //{
            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();

            if (rtvFormTree.SelectedNode != null)
            {
                var currentNode = dynamicRuleLayout.FirstOrDefault(a => a.ID == Guid.Parse(rtvFormTree.SelectedNode.Value));
                if (currentNode != null && (LayoutType)currentNode.LayoutType != LayoutType.GroupFields)
                    return;
            }

            RadTreeNode selectedNode = null;
            //var siteActivityRuleLayout = new tbl_SiteActivityRuleLayout { ID = Guid.NewGuid(), SiteColumnID = siteColumn.ID, SiteID = siteID, LayoutType = (int)LayoutType.ProfileField };
            //var siteActivityRuleLayout = new tbl_SiteActivityRuleLayout { ID = Guid.NewGuid(), Name = "Ф.И.О.", SiteID = siteID, LayoutType = (int)LayoutType.FullName };
            var siteActivityRuleLayout = new tbl_SiteActivityRuleLayout { ID = Guid.NewGuid(), Name = "Ф.И.О.", SiteID = siteID, LayoutType = (int)LayoutType.ProfileField, SysField = "sys_fullname"};
            if (rtvFormTree.SelectedNode != null)
            {
                siteActivityRuleLayout.ParentID = Guid.Parse(rtvFormTree.SelectedNode.Value);
                selectedNode = rtvFormTree.SelectedNode;
            }
            dynamicRuleLayout.Add(siteActivityRuleLayout);

            BindTree();

            if (selectedNode != null)
                rtvFormTree.FindNodeByValue(selectedNode.Value).Selected = true;
            //}
        }



        /// <summary>
        /// Handles the OnClick event of the btnAddFormField control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnAddFormField_OnClick(object sender, EventArgs e)
        {
            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();

            if (rtvFormTree.SelectedNode != null)
            {
                var currentNode = dynamicRuleLayout.FirstOrDefault(a => a.ID == Guid.Parse(rtvFormTree.SelectedNode.Value));
                if (currentNode != null && (LayoutType)currentNode.LayoutType != LayoutType.GroupFields)
                    return;
            }

            dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();
            var siteColumn = new tbl_SiteColumns();
            siteColumn.ID = Guid.NewGuid();
            siteColumn.SiteID = siteID;
            if (_siteActivityRuleId != Guid.Empty)
                siteColumn.SiteActivityRuleID = _siteActivityRuleId;
            else
                siteColumn.SiteActivityRuleID = _newSiteActivityRuleId;
            siteColumn.Name = "Поле формы";
            siteColumn.CategoryID = dataManager.ColumnCategories.SelectAll(siteID).FirstOrDefault().ID;
            siteColumn.TypeID = (int)ColumnType.String;
            siteColumn.Code = RandomCharacters();
            dynamicSiteColumns.Add(siteColumn);

            RadTreeNode selectedNode = null;
            var siteActivityRuleLayout = new tbl_SiteActivityRuleLayout { ID = Guid.NewGuid(), SiteID = siteID, SiteColumnID = siteColumn.ID, Name = "Поле формы", LayoutType = (int)LayoutType.FormField };
            if (rtvFormTree.SelectedNode != null)
            {
                siteActivityRuleLayout.ParentID = Guid.Parse(rtvFormTree.SelectedNode.Value);
                selectedNode = rtvFormTree.SelectedNode;
            }
            dynamicRuleLayout.Add(siteActivityRuleLayout);

            BindTree();

            if (selectedNode != null)
                rtvFormTree.FindNodeByValue(selectedNode.Value).Selected = true;
        }



        /// <summary>
        /// Handles the OnClick event of the btnAddTextField control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnAddTextField_OnClick(object sender, EventArgs e)
        {
            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();

            if (rtvFormTree.SelectedNode != null)
            {
                var currentNode = dynamicRuleLayout.FirstOrDefault(a => a.ID == Guid.Parse(rtvFormTree.SelectedNode.Value));
                if (currentNode != null && (LayoutType)currentNode.LayoutType != LayoutType.GroupFields)
                    return;
            }

            RadTreeNode selectedNode = null;
            var siteActivityRuleLayout = new tbl_SiteActivityRuleLayout { ID = Guid.NewGuid(), SiteID = siteID, Name = "Текстовый блок", LayoutType = (int)LayoutType.TextBlock };
            if (rtvFormTree.SelectedNode != null)
            {
                siteActivityRuleLayout.ParentID = Guid.Parse(rtvFormTree.SelectedNode.Value);
                selectedNode = rtvFormTree.SelectedNode;
            }

            dynamicRuleLayout.Add(siteActivityRuleLayout);

            BindTree();

            if (selectedNode != null)
                rtvFormTree.FindNodeByValue(selectedNode.Value).Selected = true;
        }


        protected void btnAddImageField_OnClick(object sender, EventArgs e)
        {
            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();

            if (rtvFormTree.SelectedNode != null)
            {
                var currentNode = dynamicRuleLayout.FirstOrDefault(a => a.ID == Guid.Parse(rtvFormTree.SelectedNode.Value));
                if (currentNode != null && (LayoutType)currentNode.LayoutType != LayoutType.GroupFields)
                    return;
            }

            rblAlign.SelectedIndex = 0;

            RadTreeNode selectedNode = null;
            var siteActivityRuleLayout = new tbl_SiteActivityRuleLayout { ID = Guid.NewGuid(), SiteID = siteID, Name = "Изображение", LayoutType = (int)LayoutType.Image };
            if (rtvFormTree.SelectedNode != null)
            {
                siteActivityRuleLayout.ParentID = Guid.Parse(rtvFormTree.SelectedNode.Value);
                selectedNode = rtvFormTree.SelectedNode;
            }

            dynamicRuleLayout.Add(siteActivityRuleLayout);

            BindTree();

            if (selectedNode != null)
                rtvFormTree.FindNodeByValue(selectedNode.Value).Selected = true;
        }


        protected void ddlSystemLayouts_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlSystemLayouts.SelectedValue))
                return;

            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();

            if (rtvFormTree.SelectedNode != null)
            {
                var currentNode = dynamicRuleLayout.FirstOrDefault(a => a.ID == Guid.Parse(rtvFormTree.SelectedNode.Value));
                if (currentNode != null && (LayoutType)currentNode.LayoutType != LayoutType.GroupFields)
                    return;
            }

            RadTreeNode selectedNode = null;
            tbl_SiteActivityRuleLayout siteActivityRuleLayout = null;

            switch ((LayoutType)int.Parse(ddlSystemLayouts.SelectedValue))
            {
                case LayoutType.Feedback:
                    siteActivityRuleLayout = new tbl_SiteActivityRuleLayout { ID = Guid.NewGuid(), SiteID = siteID, Name = "Форма обратной связи", LayoutType = (int)LayoutType.Feedback };
                    break;
                case LayoutType.InviteFriend:
                    siteActivityRuleLayout = new tbl_SiteActivityRuleLayout { ID = Guid.NewGuid(), SiteID = siteID, Name = "Пригласить друга", LayoutType = (int)LayoutType.InviteFriend };
                    break;
            }

            if (rtvFormTree.SelectedNode != null)
            {
                siteActivityRuleLayout.ParentID = Guid.Parse(rtvFormTree.SelectedNode.Value);
                selectedNode = rtvFormTree.SelectedNode;
            }

            dynamicRuleLayout.Add(siteActivityRuleLayout);

            BindTree();

            if (selectedNode != null)
                rtvFormTree.FindNodeByValue(selectedNode.Value).Selected = true;

            ddlSystemLayouts.ClearSelection();
        }



        /// <summary>
        /// Handles the OnNodeClick event of the rtvFormTree control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeEventArgs"/> instance containing the event data.</param>
        protected void rtvFormTree_OnNodeClick(object sender, RadTreeNodeEventArgs e)
        {
            LoadProperties();
        }



        /// <summary>
        /// Handles the OnCommand event of the btnEditSiteColumn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void btnEditSiteColumn_OnCommand(object sender, CommandEventArgs e)
        {
            var dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();
            var dynamicSiteColumnValues = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValues"] ?? new List<tbl_SiteColumnValues>();
            var dynamicSiteColumnValuesEdit = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValuesEdit"] ?? new List<tbl_SiteColumnValues>();

            SiteColumnTooltip1.SiteColumnID = Guid.Parse(e.CommandArgument.ToString());
            SiteColumnTooltip1.SiteColumns = dynamicSiteColumns;
            SiteColumnTooltip1.SiteColumnValues = dynamicSiteColumnValues;
            SiteColumnTooltip1.SiteColumnValuesEdit = dynamicSiteColumnValuesEdit;
            SiteColumnTooltip1.BindData();

            rttSiteColumn.Show();
        }



        protected void SiteColumnTooltip1_OnSaved(object sender, EventArgs e)
        {
            ViewState["DynamicSiteColumns"] = SiteColumnTooltip1.SiteColumns;
            ViewState["DynamicSiteColumnValues"] = SiteColumnTooltip1.SiteColumnValues;
            ViewState["DynamicSiteColumnValuesEdit"] = SiteColumnTooltip1.SiteColumnValuesEdit;

            LoadProperties();
            //UpdatePanel1.Update();            
        }



        /// <summary>
        /// Loads the properties.
        /// </summary>
        protected void LoadProperties()
        {
            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();
            dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();
            //dynamicSiteColumns.AddRange(dataManager.SiteColumns.SelectAll(siteID).Where(a => a.SiteActivityRuleID == null));

            // Update ViewState
            //if (pFormProperties.Visible && ViewState["DynamicRuleLayoutCurrent"] != null && ViewState["DynamicRuleLayoutCurrent"].ToString() != rtvFormTree.SelectedValue)
            if (pFormProperties.Visible && ViewState["DynamicRuleLayoutCurrent"] != null)
            {
                var prevNode = dynamicRuleLayout.FirstOrDefault(a => a.ID == Guid.Parse(ViewState["DynamicRuleLayoutCurrent"].ToString()));
                if (prevNode != null)
                {
                    prevNode.Name = txtLayoutName.Text;
                    prevNode.SiteColumnID = null;
                    prevNode.IsRequired = cbIsRequired.Checked;
                    prevNode.IsExtraField = cbIsExtraField.Checked;
                    prevNode.IsAdmin = cbIsAdmin.Checked;
                    prevNode.CSSStyle = tbCSSStyle.Text;
                    prevNode.Orientation = int.Parse(ddlOrientation.SelectedValue);
                    prevNode.OutputFormat = int.Parse(ddlOutputFormat.SelectedValue);
                    prevNode.OutputFormatFields = int.Parse(ddlOutputFormatFields.SelectedValue);
                    prevNode.ColumnTypeExpressionID = null;

                    if ((LayoutType)prevNode.LayoutType == LayoutType.GroupFields)
                    {
                        prevNode.CSSStyle = txtGroupCSSStyle.Text;

                        if ((CurrentUser.Instance.AccessLevel == AccessLevel.Administrator || CurrentUser.Instance.AccessLevel == AccessLevel.SystemAdministrator) && CurrentUser.Instance.IsTemplateSite)
                        {
                            var layoutParams = new List<LayoutParams>();
                            layoutParams.Add(new LayoutParams() { Name = "IsUsedForAdditionalDetails", Value = chxIsUsedForAdditionalDetails.Checked.ToString() });
                            prevNode.LayoutParams = LayoutParams.Serialize(layoutParams);
                        }
                    }

                    if ((LayoutType)prevNode.LayoutType == LayoutType.ProfileField)
                    {
                        //prevNode.SiteColumnID = Guid.Parse(ddlSiteColumn.SelectedValue);

                        prevNode.Orientation = null;
                        prevNode.OutputFormat = null;
                        prevNode.OutputFormatFields = null;

                        Guid outSiteColumnId;
                        if (Guid.TryParse(ddlSiteColumn.SelectedValue, out outSiteColumnId))
                        {
                            prevNode.SiteColumnID = outSiteColumnId;
                            prevNode.Name = null;
                            prevNode.SysField = null;

                            switch ((ColumnType)dynamicSiteColumns.FirstOrDefault(a => a.ID == (Guid)prevNode.SiteColumnID).TypeID)
                            {
                                case ColumnType.String:
                                case ColumnType.Text:
                                case ColumnType.Number:
                                    prevNode.DefaultValue = tbDefaultValue.Text;
                                    break;
                                case ColumnType.Date:
                                    prevNode.DefaultValue = rdpDefaultValue.SelectedDate.ToString();
                                    break;
                                case ColumnType.Enum:
                                    prevNode.DefaultValue = ddlDefaultValue.SelectedValue;
                                    break;
                                case ColumnType.Logical:
                                    prevNode.DefaultValue = cbDefaultValue.Checked.ToString();
                                    break;
                            }
                        }
                        else
                        {
                            prevNode.SiteColumnID = null;
                            prevNode.SysField = ddlSiteColumn.SelectedValue;
                            prevNode.Name = contactData.GetNameBySysName(ddlSiteColumn.SelectedValue);
                        }

                        if (!string.IsNullOrEmpty(ddlFormatInputOutput.SelectedValue))
                            prevNode.ColumnTypeExpressionID = ddlFormatInputOutput.SelectedValue.ToGuid();
                    }

                    if ((LayoutType)prevNode.LayoutType == LayoutType.FormField)
                    {
                        prevNode.SiteColumnID = Guid.Parse(hdnSiteColumnID.Value);
                    }

                    //if ((LayoutType)prevNode.LayoutType == LayoutType.ProfileField || (LayoutType)prevNode.LayoutType == LayoutType.FormField)
                    if ((LayoutType)prevNode.LayoutType == LayoutType.FormField)
                    {
                        prevNode.Name = null;
                        prevNode.Orientation = null;
                        prevNode.OutputFormat = null;
                        prevNode.OutputFormatFields = null;

                        switch ((ColumnType)dynamicSiteColumns.FirstOrDefault(a => a.ID == (Guid)prevNode.SiteColumnID).TypeID)
                        {
                            case ColumnType.String:
                            case ColumnType.Text:
                            case ColumnType.Number:
                                prevNode.DefaultValue = tbDefaultValue.Text;
                                break;
                            case ColumnType.Date:
                                prevNode.DefaultValue = rdpDefaultValue.SelectedDate.ToString();
                                break;
                            case ColumnType.Enum:
                                prevNode.DefaultValue = ddlDefaultValue.SelectedValue;
                                break;
                            case ColumnType.Logical:
                                prevNode.DefaultValue = cbDefaultValue.Checked.ToString();
                                break;
                        }

                        if (!string.IsNullOrEmpty(ddlFormatInputOutput.SelectedValue))
                            prevNode.ColumnTypeExpressionID = ddlFormatInputOutput.SelectedValue.ToGuid();
                    }

                    if ((LayoutType)prevNode.LayoutType == LayoutType.TextBlock)
                    {
                        prevNode.SiteColumnID = null;
                        prevNode.Orientation = null;
                        prevNode.OutputFormat = null;
                        prevNode.OutputFormatFields = null;

                        prevNode.Name = txtLayoutName_TextBlock.Text;
                        prevNode.Description = tbDescription_TextBlock.Content;

                        var layoutParams = new List<LayoutParams>();

                        if ((CurrentUser.Instance.AccessLevel == AccessLevel.Administrator || CurrentUser.Instance.AccessLevel == AccessLevel.SystemAdministrator) && CurrentUser.Instance.IsTemplateSite)
                        {
                            layoutParams.Add(new LayoutParams() { Name = "ShowInMaster", Value = ddlShowTextBlockInMaster.SelectedValue });
                        }

                        layoutParams.Add(new LayoutParams() { Name = "IsUsedForErrorMessage", Value = chxIsUsedForErrorMessage.Checked.ToString() });
                        prevNode.LayoutParams = LayoutParams.Serialize(layoutParams);
                    }

                    if ((LayoutType)prevNode.LayoutType == LayoutType.Image)
                    {
                        prevNode.SiteColumnID = null;
                        prevNode.Orientation = null;
                        prevNode.OutputFormat = null;
                        prevNode.OutputFormatFields = null;

                        var layoutParams = new List<LayoutParams>();
                        layoutParams.Add(new LayoutParams { Name = "ImageHeight", Value = !string.IsNullOrEmpty(txtImageHeight.Text) ? txtImageHeight.Text : "" });
                        layoutParams.Add(new LayoutParams { Name = "ImageHeightPercent", Value = !string.IsNullOrEmpty(txtImageHeightPercent.Text) ? txtImageHeightPercent.Text : "" });
                        layoutParams.Add(new LayoutParams { Name = "ImageWidth", Value = !string.IsNullOrEmpty(txtImageWidth.Text) ? txtImageWidth.Text : "" });
                        layoutParams.Add(new LayoutParams { Name = "ImageWidthPercent", Value = !string.IsNullOrEmpty(txtImageWidthPercent.Text) ? txtImageWidthPercent.Text : "" });
                        layoutParams.Add(new LayoutParams { Name = "ImageAlign", Value = rblAlign.SelectedValue });
                        layoutParams.Add(new LayoutParams { Name = "ImageAlternativeText", Value = txtImageAlternativeText.Text });
                        prevNode.LayoutParams = LayoutParams.Serialize(layoutParams);
                    }

                    if ((LayoutType)prevNode.LayoutType == LayoutType.Feedback)
                    {
                        prevNode.SiteColumnID = null;
                        prevNode.Orientation = null;
                        prevNode.OutputFormat = null;
                        prevNode.OutputFormatFields = null;

                        prevNode.Name = txtLayoutName_TextBlock.Text;
                        prevNode.Description = string.Empty;

                        var layoutParams = new List<LayoutParams>();
                        layoutParams.Add(new LayoutParams() { Name = "step", Value = rblStep.SelectedValue });
                        layoutParams.Add(new LayoutParams() { Name = "kb", Value = rblKnowledgeBase.SelectedValue });
                        var publicationTypeValues = (from ListItem item in chxPublicationType.Items where item.Selected select item.Value).ToList();
                        layoutParams.Add(new LayoutParams() { Name = "pt", Value = string.Join(",", publicationTypeValues) });

                        prevNode.LayoutParams = LayoutParams.Serialize(layoutParams);
                    }

                    if ((LayoutType)prevNode.LayoutType == LayoutType.InviteFriend)
                    {
                        prevNode.SiteColumnID = null;
                        prevNode.Orientation = null;
                        prevNode.OutputFormat = null;
                        prevNode.OutputFormatFields = null;

                        prevNode.Name = txtLayoutName_TextBlock.Text;
                        prevNode.Description = string.Empty;

                        var layoutParams = new List<LayoutParams>();
                        layoutParams.Add(new LayoutParams()
                        {
                            Name = "WorkflowTemplateID",
                            Value = dcbWorkflowTemplate.SelectedIdNullable.HasValue ? dcbWorkflowTemplate.SelectedIdNullable.Value.ToString() : string.Empty
                        });
                        layoutParams.Add(new LayoutParams()
                        {
                            Name = "SiteActionTemplateID",
                            Value = ucPopupSiteActionTemplate.SiteActionTemplateId.ToString()
                        });

                        prevNode.LayoutParams = LayoutParams.Serialize(layoutParams);
                    }

                    /*if ((LayoutType)prevNode.LayoutType == LayoutType.FullName
                        || (LayoutType)prevNode.LayoutType == LayoutType.Email
                        || (LayoutType)prevNode.LayoutType == LayoutType.Phone
                        || (LayoutType)prevNode.LayoutType == LayoutType.Surname
                        || (LayoutType)prevNode.LayoutType == LayoutType.Name
                        || (LayoutType)prevNode.LayoutType == LayoutType.Patronymic)
                    {
                        prevNode.SiteColumnID = null;
                        prevNode.Orientation = null;
                        prevNode.OutputFormat = null;
                        prevNode.OutputFormatFields = null;

                        switch ((LayoutType)prevNode.LayoutType)
                        {
                            case LayoutType.FullName:
                                prevNode.Name = "Ф.И.О.";
                                break;
                            case LayoutType.Email:
                                prevNode.Name = "E-mail";
                                break;
                            case LayoutType.Phone:
                                prevNode.Name = "Телефон";
                                break;
                            case LayoutType.Surname:
                                prevNode.Name = "Фамилия";
                                break;
                            case LayoutType.Name:
                                prevNode.Name = "Имя";
                                break;
                            case LayoutType.Patronymic:
                                prevNode.Name = "Отчество";
                                break;
                        }
                    }*/
                }
            }
            // End Update ViewState

            if (rtvFormTree.SelectedValue == Guid.Empty.ToString())
            {
                btnAddGroupFields.Enabled = true;
                btnAddProfileField.Enabled = true;
                btnAddFormField.Enabled = true;
                btnAddTextField.Enabled = true;
                ddlSystemLayouts.Enabled = true;
                btnDeleteNode.Enabled = false;
                pFormProperties.Visible = false;
                BindTree();
                return;
            }

            pProfileField.Visible = false;
            pFormField.Visible = false;
            pFormProperties.Visible = false;
            pField.Visible = false;
            pGroupFields.Visible = false;
            pFormProperties.Visible = true;
            pTextBlock.Visible = false;
            pImage.Visible = false;
            pFeedbackForm.Visible = false;
            pInviteFriendForm.Visible = false;
            pDefaultValue.Visible = true;
            pnlFormatInputOutput.Visible = false;

            btnAddGroupFields.Enabled = false;
            btnAddProfileField.Enabled = false;
            btnAddFormField.Enabled = false;
            btnAddTextField.Enabled = false;
            ddlSystemLayouts.Enabled = false;
            btnDeleteNode.Enabled = true;

            var currentNode = dynamicRuleLayout.FirstOrDefault(a => a.ID == Guid.Parse(rtvFormTree.SelectedValue));
            txtLayoutName.Text = currentNode.Name;
            txtLayoutName_TextBlock.Text = currentNode.Name;
            cbIsRequired.Checked = currentNode.IsRequired;
            cbIsExtraField.Checked = currentNode.IsExtraField;
            cbIsAdmin.Checked = currentNode.IsAdmin;
            tbCSSStyle.Text = currentNode.CSSStyle;
            tbDescription_TextBlock.Content = currentNode.Description;

            ddlOrientation.ClearSelection();
            ddlOutputFormat.ClearSelection();
            ddlOutputFormatFields.ClearSelection();
            if (currentNode.Orientation != null) ddlOrientation.Items.FindByValue(currentNode.Orientation.ToString()).Selected = true;
            if (currentNode.OutputFormat != null) ddlOutputFormat.Items.FindByValue(currentNode.OutputFormat.ToString()).Selected = true;
            if (currentNode.OutputFormatFields != null) ddlOutputFormatFields.Items.FindByValue(currentNode.OutputFormatFields.ToString()).Selected = true;

            var columnTypesExpressions = new List<tbl_ColumnTypesExpression>();

            //if ((LayoutType)currentNode.LayoutType == LayoutType.ProfileField || (LayoutType)currentNode.LayoutType == LayoutType.FormField)
            if ((LayoutType)currentNode.LayoutType == LayoutType.FormField)
            {
                var layoutName = dynamicSiteColumns.FirstOrDefault(a => a.ID == (Guid)currentNode.SiteColumnID).Name;
                txtLayoutName.Text = layoutName;
                litLayoutName.Text = layoutName;

                RefreshDefaultValue(currentNode);

                pField.Visible = true;
            }

            if ((LayoutType)currentNode.LayoutType == LayoutType.ProfileField)
            {
                contactData.CollectionToDropDownList(ref ddlSiteColumn);

                /*ddlSiteColumn.DataSource = dataManager.SiteColumns.SelectAll(siteID).Where(a => a.SiteActivityRuleID == null);
                ddlSiteColumn.DataValueField = "ID";
                ddlSiteColumn.DataTextField = "Name";
                ddlSiteColumn.DataBind();

                ddlSiteColumn.Items.Insert(0, new ListItem("Телефон", "sys_phone"));
                ddlSiteColumn.Items.Insert(0, new ListItem("E-mail", "sys_email"));
                ddlSiteColumn.Items.Insert(0, new ListItem("Отчество", "sys_patronymic"));
                ddlSiteColumn.Items.Insert(0, new ListItem("Имя", "sys_name"));
                ddlSiteColumn.Items.Insert(0, new ListItem("Фамилия", "sys_surname"));
                ddlSiteColumn.Items.Insert(0, new ListItem("Ф.И.О.", "sys_fullname"));*/

                //ddlSiteColumn.Items.FindByValue(currentNode.SiteColumnID.ToString()).Selected = true;

                ddlSiteColumn.ClearSelection();
                if (currentNode.SiteColumnID != null)
                {
                    var layoutName = dynamicSiteColumns.FirstOrDefault(a => a.ID == (Guid)currentNode.SiteColumnID).Name;
                    txtLayoutName.Text = layoutName;
                    litLayoutName.Text = layoutName;

                    RefreshDefaultValue(currentNode);

                    pField.Visible = true;

                    ddlSiteColumn.Items.FindByValue(currentNode.SiteColumnID.ToString()).Selected = true;

                    columnTypesExpressions = dataManager.ColumnTypesExpression.SelectByColumnTypeId((int)contactData.GetFieldByValue(currentNode.SiteColumnID.ToString()).ColumnType).ToList();
                }
                else
                {
                    ddlSiteColumn.Items.FindByValue(currentNode.SysField).Selected = true;

                    pField.Visible = true;
                    pDefaultValue.Visible = false;

                    columnTypesExpressions = dataManager.ColumnTypesExpression.SelectByColumnTypeId((int)contactData.GetFieldByValue(currentNode.SysField).ColumnType).ToList();
                }

                pProfileField.Visible = true;
            }

            if ((LayoutType)currentNode.LayoutType == LayoutType.FormField)
            {
                hdnSiteColumnID.Value = currentNode.SiteColumnID.ToString();
                btnEditSiteColumn.CommandArgument = currentNode.SiteColumnID.ToString();
                pFormField.Visible = true;

                columnTypesExpressions =
                    dataManager.ColumnTypesExpression.SelectByColumnTypeId(
                        (int)contactData.GetFieldByValue(currentNode.SiteColumnID.ToString(),
                                                    siteActivityRuleId: _siteActivityRuleId == null || _siteActivityRuleId == Guid.Empty ? _newSiteActivityRuleId : _siteActivityRuleId,
                                                    siteColumns: dynamicSiteColumns.AsQueryable()).ColumnType).ToList();
            }

            if (columnTypesExpressions.Count > 0)
            {
                ddlFormatInputOutput.ClearSelection();
                ddlFormatInputOutput.DataSource = columnTypesExpressions;
                ddlFormatInputOutput.DataTextField = "Title";
                ddlFormatInputOutput.DataValueField = "ID";
                ddlFormatInputOutput.DataBind();
                ddlFormatInputOutput.Items.Insert(0, new ListItem(""));

                if (currentNode.ColumnTypeExpressionID.HasValue)
                    ddlFormatInputOutput.Items.FindByValue(currentNode.ColumnTypeExpressionID.ToString()).Selected = true;

                pnlFormatInputOutput.Visible = true;
            }

            if ((LayoutType)currentNode.LayoutType == LayoutType.GroupFields)
            {
                txtGroupCSSStyle.Text = currentNode.CSSStyle;

                pGroupFields.Visible = true;

                btnAddGroupFields.Enabled = true;
                btnAddProfileField.Enabled = true;
                btnAddFormField.Enabled = true;
                btnAddTextField.Enabled = true;

                if ((CurrentUser.Instance.AccessLevel == AccessLevel.Administrator || CurrentUser.Instance.AccessLevel == AccessLevel.SystemAdministrator) && CurrentUser.Instance.IsTemplateSite)
                {
                    if (!string.IsNullOrEmpty(currentNode.LayoutParams))
                    {
                        var layoutParams = LayoutParams.Deserialize(currentNode.LayoutParams);
                        chxIsUsedForAdditionalDetails.Checked = bool.Parse(layoutParams.GetValue("IsUsedForAdditionalDetails"));
                    }
                    else
                    {
                        chxIsUsedForAdditionalDetails.Checked = false;
                    }
                }
            }

            if ((LayoutType)currentNode.LayoutType == LayoutType.TextBlock)
            {
                pTextBlock.Visible = true;
                plIsUsedForErrorMessage.Visible = !string.IsNullOrEmpty(txtErrorMessage.Text);

                if ((CurrentUser.Instance.AccessLevel == AccessLevel.Administrator || CurrentUser.Instance.AccessLevel == AccessLevel.SystemAdministrator) && CurrentUser.Instance.IsTemplateSite)
                {
                    if (!string.IsNullOrEmpty(currentNode.LayoutParams))
                    {
                        var layoutParams = LayoutParams.Deserialize(currentNode.LayoutParams);
                        ddlShowTextBlockInMaster.SelectedIndex =
                            ddlShowTextBlockInMaster.FindItemIndexByValue(
                                layoutParams.GetValue("ShowInMaster"));
                    }
                    else
                    {
                        ddlShowTextBlockInMaster.ClearSelection();
                    }
                }

                if (!string.IsNullOrEmpty(currentNode.LayoutParams))
                {
                    var layoutParams = LayoutParams.Deserialize(currentNode.LayoutParams);
                    chxIsUsedForErrorMessage.Checked = layoutParams.GetBoolValue("IsUsedForErrorMessage");
                }
                else
                    chxIsUsedForErrorMessage.Checked = false;
            }

            if ((LayoutType)currentNode.LayoutType == LayoutType.Image)
            {
                pImage.Visible = true;
                var fullImageHeight = 0;
                var fullImageWidth = 0;

                if (HttpContext.Current.Cache.Get(_cacheKey + rtvFormTree.SelectedNode.Value) != null)
                {
                    rbiImage.DataValue = (byte[])HttpContext.Current.Cache.Get(_cacheKey + rtvFormTree.SelectedNode.Value);
                    var image = (byte[])HttpContext.Current.Cache.Get(_cacheKey + rtvFormTree.SelectedNode.Value);
                    Stream stream = new MemoryStream(image);

                    var bitmap = new Bitmap(stream);
                    fullImageHeight = bitmap.Height;
                    fullImageWidth = bitmap.Width;
                }
                else
                {
                    var fsp = new FileSystemProvider();
                    if (fsp.IsExist(siteID, "SiteActivityRule", rtvFormTree.SelectedNode.Value + ".png", FileType.Image))
                    {
                        var filePath = fsp.GetLink(siteID, "SiteActivityRule", rtvFormTree.SelectedNode.Value + ".png", FileType.Image);
                        rbiImage.ImageUrl = filePath;

                        var bitmap = new Bitmap(fsp.GetPhysicalPath(siteID, "SiteActivityRule", rtvFormTree.SelectedNode.Value + ".png", FileType.Image));
                        fullImageHeight = bitmap.Height;
                        fullImageWidth = bitmap.Width;
                    }
                    else
                        rbiImage.DataValue = new byte[0];
                }

                if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_ImageSize"))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_ImageSize", string.Format("fullImageHeight={0};fullImageWidth={1}", fullImageHeight, fullImageWidth), true);

                if (!string.IsNullOrEmpty(currentNode.LayoutParams))
                {
                    var layoutParams = LayoutParams.Deserialize(currentNode.LayoutParams);
                    txtImageHeight.Text = layoutParams.GetValue("ImageHeight");
                    txtImageHeightPercent.Text = layoutParams.GetValue("ImageHeightPercent");
                    txtImageWidth.Text = layoutParams.GetValue("ImageWidth");
                    txtImageWidthPercent.Text = layoutParams.GetValue("ImageWidthPercent");
                    if (rblAlign.Items.FindByValue(layoutParams.GetValue("ImageAlign")) != null)
                        rblAlign.SelectedIndex = rblAlign.Items.IndexOf(rblAlign.Items.FindByValue(layoutParams.GetValue("ImageAlign")));
                    else
                        rblAlign.SelectedIndex = 0;
                    txtImageAlternativeText.Text = layoutParams.GetValue("ImageAlternativeText");
                }
                else
                    txtImageHeight.Text = txtImageHeightPercent.Text = txtImageWidth.Text = txtImageWidthPercent.Text = txtImageAlternativeText.Text = "";
            }

            if ((LayoutType)currentNode.LayoutType == LayoutType.Feedback)
            {
                rblStep.Items.Clear();
                pFeedbackForm.Visible = true;
                foreach (var step in EnumHelper.EnumToList<FormFeedBackSteps>())
                    rblStep.Items.Add(new ListItem(EnumHelper.GetEnumDescription(step), ((int)step).ToString()));

                rblKnowledgeBase.Items.Clear();
                foreach (var knowledgeBase in EnumHelper.EnumToList<FormFeedBackKnowledgeBase>())
                    rblKnowledgeBase.Items.Add(new ListItem(EnumHelper.GetEnumDescription(knowledgeBase), ((int)knowledgeBase).ToString()));

                var publicationTypes = dataManager.PublicationType.SelectByPublicationKindID(CurrentUser.Instance.SiteID, (int)PublicationKind.Discussion);
                chxPublicationType.DataSource = publicationTypes;
                chxPublicationType.DataTextField = "Title";
                chxPublicationType.DataValueField = "ID";
                chxPublicationType.DataBind();

                if (!string.IsNullOrEmpty(currentNode.LayoutParams))
                {
                    var layoutParams = LayoutParams.Deserialize(currentNode.LayoutParams);
                    rblStep.SelectedIndex = rblStep.Items.IndexOf(rblStep.Items.FindByValue(layoutParams.GetValue("step")));
                    rblKnowledgeBase.SelectedIndex = rblKnowledgeBase.Items.IndexOf(rblKnowledgeBase.Items.FindByValue(layoutParams.GetValue("kb")));
                    var publicationTypeIds = layoutParams.GetValue("pt").Split(',');
                    foreach (var publicationTypeId in publicationTypeIds)
                        if (!string.IsNullOrEmpty(publicationTypeId) && chxPublicationType.Items.FindByValue(publicationTypeId) != null)
                            chxPublicationType.Items.FindByValue(publicationTypeId).Selected = true;
                }
                else
                {
                    rblStep.Items.FindByValue("1").Selected = true;
                    rblKnowledgeBase.Items.FindByValue("1").Selected = true;
                }
            }

            if ((LayoutType)currentNode.LayoutType == LayoutType.InviteFriend)
            {
                pInviteFriendForm.Visible = true;

                if (!string.IsNullOrEmpty(currentNode.LayoutParams))
                {
                    var layoutParams = LayoutParams.Deserialize(currentNode.LayoutParams);
                    if (!string.IsNullOrEmpty(layoutParams.GetValue("SiteActionTemplateID")))
                    {
                        ucPopupSiteActionTemplate.SiteActionTemplateId = Guid.Parse(layoutParams.GetValue("SiteActionTemplateID"));
                        ucPopupSiteActionTemplate.UpdateUI(dataManager.SiteActionTemplate.SelectById(ucPopupSiteActionTemplate.SiteActionTemplateId));
                    }
                    if (!string.IsNullOrEmpty(layoutParams.GetValue("WorkflowTemplateID")))
                    {
                        var workflowTemplate = dataManager.WorkflowTemplate.SelectById(CurrentUser.Instance.SiteID, Guid.Parse(layoutParams.GetValue("WorkflowTemplateID")));
                        dcbWorkflowTemplate.SelectedIdNullable = workflowTemplate.ID;
                        dcbWorkflowTemplate.SelectedText = workflowTemplate.Name;
                    }
                }
            }

            /*if ((LayoutType)currentNode.LayoutType == LayoutType.FullName
                || (LayoutType)currentNode.LayoutType == LayoutType.Email
                || (LayoutType)currentNode.LayoutType == LayoutType.Phone
                || (LayoutType)currentNode.LayoutType == LayoutType.Surname
                || (LayoutType)currentNode.LayoutType == LayoutType.Name
                || (LayoutType)currentNode.LayoutType == LayoutType.Patronymic)
            {
                contactData.CollectionToDropDownList(ref ddlSiteColumn);

                ddlSiteColumn.DataSource = dataManager.SiteColumns.SelectAll(siteID).Where(a => a.SiteActivityRuleID == null);
                ddlSiteColumn.DataValueField = "ID";
                ddlSiteColumn.DataTextField = "Name";
                ddlSiteColumn.DataBind();

                ddlSiteColumn.Items.Insert(0, new ListItem("Телефон", "sys_phone"));
                ddlSiteColumn.Items.Insert(0, new ListItem("E-mail", "sys_email"));
                ddlSiteColumn.Items.Insert(0, new ListItem("Отчество", "sys_patronymic"));
                ddlSiteColumn.Items.Insert(0, new ListItem("Имя", "sys_name"));
                ddlSiteColumn.Items.Insert(0, new ListItem("Фамилия", "sys_surname"));
                ddlSiteColumn.Items.Insert(0, new ListItem("Ф.И.О.", "sys_fullname"));

                switch ((LayoutType)currentNode.LayoutType)
                {
                    case LayoutType.FullName:
                        ddlSiteColumn.Items.FindByValue("sys_fullname").Selected = true;
                        break;
                    case LayoutType.Email:
                        ddlSiteColumn.Items.FindByValue("sys_email").Selected = true;
                        break;
                    case LayoutType.Phone:
                        ddlSiteColumn.Items.FindByValue("sys_phone").Selected = true;
                        break;
                    case LayoutType.Surname:
                        ddlSiteColumn.Items.FindByValue("sys_surname").Selected = true;
                        break;
                    case LayoutType.Name:
                        ddlSiteColumn.Items.FindByValue("sys_name").Selected = true;
                        break;
                    case LayoutType.Patronymic:
                        ddlSiteColumn.Items.FindByValue("sys_patronymic").Selected = true;
                        break;
                }

                pProfileField.Visible = true;
                pField.Visible = true;
                pDefaultValue.Visible = false;
            }*/

            ViewState["DynamicRuleLayoutCurrent"] = rtvFormTree.SelectedValue;
            var selectedValue = rtvFormTree.SelectedValue;
            BindTree();

            rtvFormTree.FindNodeByValue(selectedValue).Selected = true;
        }



        /// <summary>
        /// Handles the OnNodeDrop event of the rtvFormTree control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeDragDropEventArgs"/> instance containing the event data.</param>
        protected void rtvFormTree_OnNodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            RadTreeNode sourceNode = e.SourceDragNode;
            RadTreeNode destNode = e.DestDragNode;
            RadTreeViewDropPosition dropPosition = e.DropPosition;

            if (destNode != null) //drag&drop is performed between trees
            {
                if (sourceNode.TreeView.SelectedNodes.Count <= 1)
                {
                    PerformDragAndDrop(dropPosition, sourceNode, destNode);
                }
                else if (sourceNode.TreeView.SelectedNodes.Count > 1)
                {
                    foreach (RadTreeNode node in sourceNode.TreeView.SelectedNodes)
                    {
                        PerformDragAndDrop(dropPosition, node, destNode);
                    }
                }

                destNode.Expanded = true;
                BindTree();
                rtvFormTree.FindNodeByValue(sourceNode.Value).Selected = true;
                LoadProperties();
            }
        }


        /// <summary>
        /// Performs the drag and drop.
        /// </summary>
        /// <param name="dropPosition">The drop position.</param>
        /// <param name="sourceNode">The source node.</param>
        /// <param name="destNode">The dest node.</param>
        private void PerformDragAndDrop(RadTreeViewDropPosition dropPosition, RadTreeNode sourceNode, RadTreeNode destNode)
        {
            if (sourceNode.Equals(destNode) || sourceNode.IsAncestorOf(destNode))
                return;

            sourceNode.Owner.Nodes.Remove(sourceNode);

            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();

            switch (dropPosition)
            {
                case RadTreeViewDropPosition.Over:
                    // child
                    if (!sourceNode.IsAncestorOf(destNode))
                        destNode.Nodes.Add(sourceNode);
                    break;
                case RadTreeViewDropPosition.Above:
                    // sibling - above                    
                    destNode.InsertBefore(sourceNode);
                    break;
                case RadTreeViewDropPosition.Below:
                    // sibling - below
                    destNode.InsertAfter(sourceNode);
                    break;
            }
        }



        /// <summary>
        /// Reorders this instance.
        /// </summary>
        protected void Reorder()
        {
            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();
            var order = 0;

            var xml = XDocument.Parse(rtvFormTree.GetXml());
            var nodes = xml.Document.Descendants("Node");
            foreach (var node in nodes)
            {
                if (Guid.Parse(node.Attribute("Value").Value) != Guid.Empty)
                {
                    var ruleLayout = dynamicRuleLayout.FirstOrDefault(a => a.ID == Guid.Parse(node.Attribute("Value").Value));
                    if (ruleLayout != null)
                    {
                        ruleLayout.Order = order;
                        if (node.Parent != null)
                            dynamicRuleLayout.FirstOrDefault(a => a.ID == Guid.Parse(node.Attribute("Value").Value)).ParentID = Guid.Parse(node.Parent.Attribute("Value").Value);
                        order++;
                    }
                }
            }
        }



        /// <summary>
        /// Refreshes the default value.
        /// </summary>
        /// <param name="siteActivityRuleLayout">The site activity rule layout.</param>
        protected void RefreshDefaultValue(tbl_SiteActivityRuleLayout siteActivityRuleLayout)
        {
            dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();

            tbDefaultValue.Visible = false;
            rdpDefaultValue.Visible = false;
            ddlDefaultValue.Visible = false;
            cbDefaultValue.Visible = false;

            tbDefaultValue.Text = string.Empty;
            rdpDefaultValue.SelectedDate = null;
            cbDefaultValue.Checked = false;

            /*if ((LayoutType)siteActivityRuleLayout.LayoutType != LayoutType.FullName
                && (LayoutType)siteActivityRuleLayout.LayoutType != LayoutType.Email
                && (LayoutType)siteActivityRuleLayout.LayoutType != LayoutType.Phone
                && (LayoutType)siteActivityRuleLayout.LayoutType != LayoutType.Surname
                && (LayoutType)siteActivityRuleLayout.LayoutType != LayoutType.Name
                && (LayoutType)siteActivityRuleLayout.LayoutType != LayoutType.Patronymic)*/

            if (siteActivityRuleLayout.SiteColumnID.HasValue)
            {
                switch ((ColumnType)dynamicSiteColumns.FirstOrDefault(a => a.ID == siteActivityRuleLayout.SiteColumnID).TypeID)
                {
                    case ColumnType.String:
                    case ColumnType.Text:
                    case ColumnType.Number:
                        tbDefaultValue.Text = siteActivityRuleLayout.DefaultValue;
                        tbDefaultValue.Visible = true;
                        break;
                    case ColumnType.Date:
                        if (!string.IsNullOrEmpty(siteActivityRuleLayout.DefaultValue)) rdpDefaultValue.SelectedDate = DateTime.Parse(siteActivityRuleLayout.DefaultValue);
                        rdpDefaultValue.Visible = true;
                        break;
                    case ColumnType.Enum:
                        ddlDefaultValue.DataSource = dataManager.SiteColumnValues.SelectAll((Guid)siteActivityRuleLayout.SiteColumnID);
                        ddlDefaultValue.DataValueField = "ID";
                        ddlDefaultValue.DataTextField = "Value";
                        ddlDefaultValue.DataBind();
                        ddlDefaultValue.Items.Insert(0, new ListItem("", ""));

                        ddlDefaultValue.Items.FindByValue(siteActivityRuleLayout.DefaultValue ?? "").Selected = true;
                        ddlDefaultValue.Visible = true;
                        break;
                    case ColumnType.Logical:
                        cbDefaultValue.Checked = bool.Parse(siteActivityRuleLayout.DefaultValue ?? "false");
                        cbDefaultValue.Visible = true;
                        break;
                }
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlSiteColumn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlSiteColumn_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();
            var siteActivityRuleLayout = dynamicRuleLayout.FirstOrDefault(a => a.ID == Guid.Parse(rtvFormTree.SelectedNode.Value));
            var columnTypesExpressions = new List<tbl_ColumnTypesExpression>();

            Guid outSiteColumnId;
            if (Guid.TryParse(ddlSiteColumn.SelectedValue, out outSiteColumnId))
            {
                siteActivityRuleLayout.SiteColumnID = outSiteColumnId;
                siteActivityRuleLayout.SysField = null;
                columnTypesExpressions = dataManager.ColumnTypesExpression.SelectByColumnTypeId((int)contactData.GetFieldByValue(siteActivityRuleLayout.SiteColumnID.ToString()).ColumnType).ToList();
            }
            else
            {
                siteActivityRuleLayout.SiteColumnID = null;
                siteActivityRuleLayout.SysField = ddlSiteColumn.SelectedValue;
                columnTypesExpressions = dataManager.ColumnTypesExpression.SelectByColumnTypeId((int)contactData.GetFieldByValue(siteActivityRuleLayout.SysField).ColumnType).ToList();
            }

            if (columnTypesExpressions.Count > 0)
            {
                ddlFormatInputOutput.ClearSelection();
                ddlFormatInputOutput.DataSource = columnTypesExpressions;
                ddlFormatInputOutput.DataTextField = "Title";
                ddlFormatInputOutput.DataValueField = "ID";
                ddlFormatInputOutput.DataBind();
                ddlFormatInputOutput.Items.Insert(0, new ListItem(""));

                pnlFormatInputOutput.Visible = true;
            }

            /*switch (ddlSiteColumn.SelectedValue)
            {
                case "sys_fullname":
                    siteActivityRuleLayout.SiteColumnID = null;
                    siteActivityRuleLayout.LayoutType = (int)LayoutType.FullName;
                    break;
                case "sys_email":
                    siteActivityRuleLayout.LayoutType = (int)LayoutType.Email;
                    break;
                case "sys_phone":
                    siteActivityRuleLayout.LayoutType = (int)LayoutType.Phone;
                    break;
                case "sys_surname":
                    siteActivityRuleLayout.LayoutType = (int)LayoutType.Surname;
                    break;
                case "sys_name":
                    siteActivityRuleLayout.LayoutType = (int)LayoutType.Name;
                    break;
                case "sys_patronymic":
                    siteActivityRuleLayout.LayoutType = (int)LayoutType.Patronymic;
                    break;
                default:
                    siteActivityRuleLayout.LayoutType = (int)LayoutType.ProfileField;
                    siteActivityRuleLayout.SiteColumnID = Guid.Parse(ddlSiteColumn.SelectedValue);
                    break;
            }*/

            RefreshDefaultValue(siteActivityRuleLayout);
        }



        /// <summary>
        /// Handles the OnClick event of the btnDeleteNode control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnDeleteNode_OnClick(object sender, EventArgs e)
        {
            if (rtvFormTree.SelectedNode != null && Guid.Parse(rtvFormTree.SelectedNode.Value) != Guid.Empty)
            {
                RemoveNodeRecursive(Guid.Parse(rtvFormTree.SelectedNode.Value));
                BindTree();
                LoadProperties();
            }
        }



        /// <summary>
        /// Removes the node recursive.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void RemoveNodeRecursive(Guid value)
        {
            dynamicRuleLayout = (List<tbl_SiteActivityRuleLayout>)ViewState["DynamicRuleLayout"] ?? new List<tbl_SiteActivityRuleLayout>();
            var dynamicRuleLayoutRemoveIDs = (List<Guid>)ViewState["DynamicRuleLayoutRemoveIDs"] ?? new List<Guid>();
            //var dynamicSiteColumnsRemoveIDs = (List<Guid>)ViewState["DynamicSiteColumnsRemoveIDs"] ?? new List<Guid>();
            var siteActivityRuleLayout = dynamicRuleLayout.FirstOrDefault(a => a.ID == value);
            var layoutRemoveId = siteActivityRuleLayout.ID;
            /*if ((LayoutType)siteActivityRuleLayout.LayoutType == LayoutType.FormField)
            {
                if (siteActivityRuleLayout.SiteColumnID != null)
                    dynamicSiteColumnsRemoveIDs.Add((Guid)siteActivityRuleLayout.SiteColumnID);
            }*/
            dynamicRuleLayout.Remove(siteActivityRuleLayout);

            dynamicRuleLayoutRemoveIDs.Add(layoutRemoveId);
            var recursiveSiteActivityRuleLayout = dynamicRuleLayout.Where(a => a.ParentID == layoutRemoveId).ToList();
            foreach (var item in recursiveSiteActivityRuleLayout)
            {
                RemoveNodeRecursive(item.ID);
            }
        }



        public static string RandomCharacters()
        {
            string shorturl_chars_lcase = "abcdefgijkmnopqrstwxyz";
            string shorturl_chars_ucase = "ABCDEFGHJKLMNPQRSTWXYZ";
            string shorturl_chars_numeric = "23456789";

            // Create a local array containing supported short-url characters
            // grouped by types.
            char[][] charGroups = new char[][] 
            {
                shorturl_chars_lcase.ToCharArray(),
                shorturl_chars_ucase.ToCharArray(),
                shorturl_chars_numeric.ToCharArray()
            };

            // Use this array to track the number of unused characters in each
            // character group.
            int[] charsLeftInGroup = new int[charGroups.Length];

            // Initially, all characters in each group are not used.
            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;

            // Use this array to track (iterate through) unused character groups.
            int[] leftGroupsOrder = new int[charGroups.Length];

            // Initially, all character groups are not used.
            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;

            // Because we cannot use the default randomizer, which is based on the
            // current time (it will produce the same "random" number within a
            // second), we will use a random number generator to seed the
            // randomizer.

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                        randomBytes[1] << 16 |
                        randomBytes[2] << 8 |
                        randomBytes[3];

            // Now, this is real randomization.
            Random random = new Random(seed);

            // This array will hold short-url characters.
            char[] short_url = null;

            // Allocate appropriate memory for the short-url.
            short_url = new char[random.Next(5, 5)];

            // Index of the next character to be added to short-url.
            int nextCharIdx;

            // Index of the next character group to be processed.
            int nextGroupIdx;

            // Index which will be used to track not processed character groups.
            int nextLeftGroupsOrderIdx;

            // Index of the last non-processed character in a group.
            int lastCharIdx;

            // Index of the last non-processed group.
            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            // Generate short-url characters one at a time.
            for (int i = 0; i < short_url.Length; i++)
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list. To allow a special character to appear in the
                // first position, increment the second parameter of the Next
                // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0,
                                                         lastLeftGroupsOrderIdx);

                // Get the actual index of the character group, from which we will
                // pick the next character.
                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                // Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                // If only one unprocessed character is left, pick it; otherwise,
                // get a random character from the unused character list.
                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                // Add this character to the short-url.
                short_url[i] = charGroups[nextGroupIdx][nextCharIdx];

                // If we processed the last character in this group, start over.
                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                                              charGroups[nextGroupIdx].Length;
                // There are more unprocessed characters left.
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                                    charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    // Decrement the number of unprocessed characters in
                    // this group.
                    charsLeftInGroup[nextGroupIdx]--;
                }

                // If we processed the last group, start all over.
                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                // There are more unprocessed groups left.
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                    leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    // Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx--;
                }
            }

            // Convert password characters into a string and return the result.
            return new string(short_url);
        }

        protected void btnGetExternalForm_OnClick(object sender, EventArgs e)
        {
            var url = txtExternalFormUrl.Text;
            ViewState["DynamicExternalForms"] = new List<tbl_SiteActivityRuleExternalForms>();
            dynamicExternalForms = (List<tbl_SiteActivityRuleExternalForms>)ViewState["DynamicExternalForms"] ?? new List<tbl_SiteActivityRuleExternalForms>();
            ViewState["DynamicExternalFormFields"] = new List<tbl_SiteActivityRuleExternalFormFields>();
            dynamicExternalFormFields = (List<tbl_SiteActivityRuleExternalFormFields>)ViewState["DynamicExternalFormFields"] ?? new List<tbl_SiteActivityRuleExternalFormFields>();

            if (!string.IsNullOrEmpty(url))
            {
                if (!url.Contains("http://") && !url.Contains("https://"))
                    url = "http://" + url;

                HtmlDocument html = new HtmlDocument();

                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();
                var data = response.GetResponseStream();

                html.Load(data, Encoding.UTF8, true);

                var forms = html.DocumentNode.SelectNodes("//form");
                if (forms != null)
                {
                    foreach (var form in forms)
                    {
                        var formName = string.Empty;
                        if (form.Attributes["name"] != null && !string.IsNullOrEmpty(form.Attributes["name"].Value))
                            formName = form.Attributes["name"].Value;
                        else if (form.Attributes["id"] != null && !string.IsNullOrEmpty(form.Attributes["id"].Value))
                            formName = form.Attributes["id"].Value;
                        else
                            formName = "Неизвестная";

                        var externalForm = dataManager.SiteActivityRuleExternalForms.Select(formName, _siteActivityRuleId);
                        if (externalForm == null)
                            externalForm = new tbl_SiteActivityRuleExternalForms { ID = Guid.NewGuid(), Name = formName, SiteActivityRuleID = _siteActivityRuleId == Guid.Empty ? _newSiteActivityRuleId : _siteActivityRuleId };

                        dynamicExternalForms.Add(externalForm);

                        //var fields = form.SelectNodes("input[@type='text']");
                        var fields = form.SelectNodes(".//input[@type='text'] | .//select | .//textarea | .//input[@type='email']");
                        if (fields != null)
                        {
                            foreach (var field in fields)
                            {
                                var filedName = string.Empty;
                                if (field.Attributes["name"] != null && !string.IsNullOrEmpty(field.Attributes["name"].Value))
                                    filedName = field.Attributes["name"].Value;
                                else if (field.Attributes["id"] != null && !string.IsNullOrEmpty(field.Attributes["id"].Value))
                                    filedName = field.Attributes["id"].Value;
                                else
                                    filedName = "Неизвестная";

                                var externalFormField = dataManager.SiteActivityRuleExternalFormFields.Select(filedName, externalForm.ID);
                                if (externalFormField == null)
                                    externalFormField = new tbl_SiteActivityRuleExternalFormFields { ID = Guid.NewGuid(), SiteActivityRuleExternalFormID = externalForm.ID, Name = filedName };

                                if (field.Name.ToLower() == "input") externalFormField.FieldType = (int)FormFieldType.Input;
                                if (field.Name.ToLower() == "textarea") externalFormField.FieldType = (int)FormFieldType.Textarea;
                                if (field.Name.ToLower() == "select") externalFormField.FieldType = (int)FormFieldType.Select;

                                dynamicExternalFormFields.Add(externalFormField);
                            }
                        }
                    }
                }
            }

            lvExternalForms.DataSource = dynamicExternalForms;
            lvExternalForms.DataBind();

            rgExternalFormFields.Visible = false;
        }



        /// <summary>
        /// Handles the OnCommand event of the btnExternalForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void btnExternalForm_OnCommand(object sender, CommandEventArgs e)
        {
            dynamicExternalForms = (List<tbl_SiteActivityRuleExternalForms>)ViewState["DynamicExternalForms"] ?? new List<tbl_SiteActivityRuleExternalForms>();
            dynamicExternalFormFields = (List<tbl_SiteActivityRuleExternalFormFields>)ViewState["DynamicExternalFormFields"] ?? new List<tbl_SiteActivityRuleExternalFormFields>();

            UpdateExternalFormNames();

            if (e.CommandArgument != null)
            {
                var externalFormId = Guid.Parse(e.CommandArgument.ToString());

                rgExternalFormFields.DataSource = dynamicExternalFormFields.Where(a => a.SiteActivityRuleExternalFormID == externalFormId);
                rgExternalFormFields.DataBind();

                rgExternalFormFields.Visible = true;
            }
        }



        /// <summary>
        /// Handles the OnItemCreated event of the rgExternalFormFields control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgExternalFormFields_OnItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                var item = (tbl_SiteActivityRuleExternalFormFields)e.Item.DataItem;

                if (item != null)
                {
                    var ddlColumns = (DropDownList)e.Item.FindControl("ddlColumns");
                    ddlColumns.Items.Add(new ListItem("", ""));

                    var dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();
                    var columns = new List<tbl_SiteColumns>();
                    var siteActivityRuleId = _siteActivityRuleId == Guid.Empty ? _newSiteActivityRuleId : _siteActivityRuleId;

                    contactData.CollectionToDropDownList(ref ddlColumns, (FormFieldType)item.FieldType, siteActivityRuleId, dynamicSiteColumns.AsQueryable());

                    /*switch ((FormFieldType)item.FieldType)
                    {
                        case FormFieldType.Input:
                            columns = dynamicSiteColumns.Where(a => ((ColumnType)a.TypeID == ColumnType.String || (ColumnType)a.TypeID == ColumnType.Number || (ColumnType)a.TypeID == ColumnType.Date) && (a.SiteActivityRuleID == null || a.SiteActivityRuleID == siteActivityRuleId)).ToList();
                            ddlColumns.Items.Add(new ListItem("Ф.И.О.", "sys_fullname"));
                            ddlColumns.Items.Add(new ListItem("Фамилия", "sys_surname"));
                            ddlColumns.Items.Add(new ListItem("Имя", "sys_name"));
                            ddlColumns.Items.Add(new ListItem("Отчество", "sys_patronymic"));
                            ddlColumns.Items.Add(new ListItem("E-mail", "sys_email"));
                            ddlColumns.Items.Add(new ListItem("Телефон", "sys_phone"));
                            ddlColumns.Items.Add(new ListItem("Должность", "sys_jobtitle"));
                            ddlColumns.Items.Add(new ListItem("Комментарий", "sys_comment"));
                            ddlColumns.Items.Add(new ListItem("Url", "sys_refferurl"));
                            ddlColumns.Items.Add(new ListItem("Рекламная площадка", "sys_advertisingplatform"));
                            ddlColumns.Items.Add(new ListItem("Тип рекламы", "sys_advertisingtype"));
                            ddlColumns.Items.Add(new ListItem("Рекламная кампания", "sys_advertisingcampaign"));
                            ddlColumns.Items.Add(new ListItem("Ключевые слова", "sys_keywords"));
                            break;
                        case FormFieldType.Textarea:
                            columns = dynamicSiteColumns.Where(a => ((ColumnType)a.TypeID == ColumnType.Text) && (a.SiteActivityRuleID == null || a.SiteActivityRuleID == siteActivityRuleId)).ToList();
                            break;
                        case FormFieldType.Select:
                            columns = dynamicSiteColumns.Where(a => ((ColumnType)a.TypeID == ColumnType.Enum) && (a.SiteActivityRuleID == null || a.SiteActivityRuleID == siteActivityRuleId)).ToList();
                            break;
                    }*/

                    /*foreach (var column in columns)
                    {
                        ddlColumns.Items.Add(new ListItem(column.Name, column.ID.ToString()));
                    }*/
                    ddlColumns.Items.Add(new ListItem("Добавить поле формы...", "_add"));
                }
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgExternalFormFields control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgExternalFormFields_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                var item = (tbl_SiteActivityRuleExternalFormFields)e.Item.DataItem;
                ((Literal)e.Item.FindControl("lFieldType")).Text = Enum.GetName(typeof(FormFieldType), item.FieldType).ToLower();

                dynamicExternalFormFields = (List<tbl_SiteActivityRuleExternalFormFields>)ViewState["DynamicExternalFormFields"] ?? new List<tbl_SiteActivityRuleExternalFormFields>();
                var row = (GridDataItem)e.Item;
                var fieldId = Guid.Parse(row.OwnerTableView.DataKeyValues[row.ItemIndex]["ID"].ToString());
                var field = dynamicExternalFormFields.Where(a => a.ID == fieldId).FirstOrDefault();

                var ddlColumns = (DropDownList)e.Item.FindControl("ddlColumns");
                if (field.SiteColumnID != null)
                {
                    ddlColumns.Items.FindByValue(field.SiteColumnID.ToString()).Selected = true;

                    var dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();
                    var column = dynamicSiteColumns.Where(a => a.ID == field.SiteColumnID).FirstOrDefault();
                    var lbEditSiteColumn = (LinkButton)e.Item.FindControl("lbEditSiteColumn");
                    if (column != null && column.SiteActivityRuleID != null)
                    {
                        lbEditSiteColumn.CommandArgument = field.ID.ToString();
                        lbEditSiteColumn.Visible = true;
                    }
                    else
                        lbEditSiteColumn.Visible = false;
                }

                if (!string.IsNullOrEmpty(field.SysField))
                {
                    ddlColumns.Items.FindByValue(field.SysField).Selected = true;
                }
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlColumns control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlColumns_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            dynamicExternalFormFields = (List<tbl_SiteActivityRuleExternalFormFields>)ViewState["DynamicExternalFormFields"] ?? new List<tbl_SiteActivityRuleExternalFormFields>();

            var ddlColumns = (DropDownList)sender;
            var row = (GridDataItem)((DropDownList)sender).Parent.Parent;
            var fieldId = Guid.Parse(row.OwnerTableView.DataKeyValues[row.ItemIndex]["ID"].ToString());

            var field = dynamicExternalFormFields.Where(a => a.ID == fieldId).FirstOrDefault();

            if (ddlColumns.SelectedValue == "_add")
            {
                var dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();
                var dynamicSiteColumnValues = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValues"] ?? new List<tbl_SiteColumnValues>();
                var dynamicSiteColumnValuesEdit = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValuesEdit"] ?? new List<tbl_SiteColumnValues>();

                SiteColumnTooltip2.SiteColumnID = Guid.Empty;
                SiteColumnTooltip2.SiteColumns = dynamicSiteColumns;
                SiteColumnTooltip2.SiteColumnValues = dynamicSiteColumnValues;
                SiteColumnTooltip2.SiteColumnValuesEdit = dynamicSiteColumnValuesEdit;
                var additionalFields = new Dictionary<string, string>();
                additionalFields.Add("FieldID", field.ID.ToString());
                additionalFields.Add("FormID", field.SiteActivityRuleExternalFormID.ToString());
                if (_siteActivityRuleId != Guid.Empty)
                    additionalFields.Add("SiteActivityRuleID", _siteActivityRuleId.ToString());
                else
                    additionalFields.Add("SiteActivityRuleID", _newSiteActivityRuleId.ToString());
                additionalFields.Add("FormFieldType", field.FieldType.ToString());
                SiteColumnTooltip2.AdditionalFields = additionalFields;
                SiteColumnTooltip2.BindData();

                rrtSiteColumnExternalForms.Show();

                var lbEditSiteColumn = (LinkButton)(((DropDownList)sender).Parent).FindControl("lbEditSiteColumn");
                lbEditSiteColumn.Visible = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(ddlColumns.SelectedValue))
                {
                    Guid outSiteColumnId;

                    if (Guid.TryParse(ddlColumns.SelectedValue, out outSiteColumnId))
                    {
                        field.SiteColumnID = outSiteColumnId;
                        field.SysField = null;
                    }
                    else
                    {
                        field.SysField = ddlColumns.SelectedValue;
                        field.SiteColumnID = null;
                    }
                    /*switch (ddlColumns.SelectedValue)
                    {
                        case "sys_fullname":
                            field.SysField = "sys_fullname";
                            field.SiteColumnID = null;
                            break;
                        case "sys_email":
                            field.SysField = "sys_email";
                            field.SiteColumnID = null;
                            break;
                        case "sys_phone":
                            field.SysField = "sys_phone";
                            field.SiteColumnID = null;
                            break;
                        case "sys_surname":
                            field.SysField = "sys_surname";
                            field.SiteColumnID = null;
                            break;
                        case "sys_name":
                            field.SysField = "sys_name";
                            field.SiteColumnID = null;
                            break;
                        case "sys_patronymic":
                            field.SysField = "sys_patronymic";
                            field.SiteColumnID = null;
                            break;
                        case "sys_jobtitle":
                            field.SysField = "sys_jobtitle";
                            field.SiteColumnID = null;
                            break;
                        case "sys_comment":
                            field.SysField = "sys_comment";
                            field.SiteColumnID = null;
                            break;
                        case "sys_refferurl":
                            field.SysField = "sys_refferurl";
                            field.SiteColumnID = null;
                            break;
                        case "sys_advertisingplatform":
                            field.SysField = "sys_advertisingplatform";
                            field.SiteColumnID = null;
                            break;
                        case "sys_advertisingtype":
                            field.SysField = "sys_advertisingtype";
                            field.SiteColumnID = null;
                            break;
                        case "sys_advertisingcampaign":
                            field.SysField = "sys_advertisingcampaign";
                            field.SiteColumnID = null;
                            break;
                        case "sys_keywords":
                            field.SysField = "sys_keywords";
                            field.SiteColumnID = null;
                            break;
                        default:
                            field.SiteColumnID = Guid.Parse(ddlColumns.SelectedValue);
                            field.SysField = null;
                            break;
                    }*/
                }
                else
                {
                    field.SiteColumnID = null;
                    field.SysField = null;
                }

                var dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();
                var column = dynamicSiteColumns.Where(a => a.ID == field.SiteColumnID).FirstOrDefault();
                var lbEditSiteColumn = (LinkButton)(((DropDownList)sender).Parent).FindControl("lbEditSiteColumn");
                if (column != null && column.SiteActivityRuleID != null)
                {
                    lbEditSiteColumn.CommandArgument = field.ID.ToString();
                    lbEditSiteColumn.Visible = true;
                }
                else
                    lbEditSiteColumn.Visible = false;
            }
        }



        /// <summary>
        /// Handles the OnSaved event of the SiteColumnTooltip2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SiteColumnTooltip2_OnSaved(object sender, EventArgs e)
        {
            ViewState["DynamicSiteColumns"] = SiteColumnTooltip2.SiteColumns;
            ViewState["DynamicSiteColumnValues"] = SiteColumnTooltip2.SiteColumnValues;
            ViewState["DynamicSiteColumnValuesEdit"] = SiteColumnTooltip2.SiteColumnValuesEdit;

            dynamicExternalFormFields = (List<tbl_SiteActivityRuleExternalFormFields>)ViewState["DynamicExternalFormFields"] ?? new List<tbl_SiteActivityRuleExternalFormFields>();
            var field = dynamicExternalFormFields.Where(a => a.ID == Guid.Parse(SiteColumnTooltip2.AdditionalFields["FieldID"])).FirstOrDefault();
            field.SiteColumnID = SiteColumnTooltip2.SiteColumnID;

            dynamicExternalForms = (List<tbl_SiteActivityRuleExternalForms>)ViewState["DynamicExternalForms"] ?? new List<tbl_SiteActivityRuleExternalForms>();
            dynamicExternalFormFields = (List<tbl_SiteActivityRuleExternalFormFields>)ViewState["DynamicExternalFormFields"] ?? new List<tbl_SiteActivityRuleExternalFormFields>();

            if (SiteColumnTooltip2.AdditionalFields["FormID"] != null)
            {
                var externalFormId = Guid.Parse(SiteColumnTooltip2.AdditionalFields["FormID"]);

                rgExternalFormFields.DataSource = dynamicExternalFormFields.Where(a => a.SiteActivityRuleExternalFormID == externalFormId);
                rgExternalFormFields.DataBind();

                rgExternalFormFields.Visible = true;
            }

            //LoadProperties();
            //UpdatePanel1.Update();
        }



        protected void lbEditSiteColumn_OnCommand(object sender, CommandEventArgs e)
        {
            var dynamicSiteColumns = (List<tbl_SiteColumns>)ViewState["DynamicSiteColumns"] ?? new List<tbl_SiteColumns>();
            var dynamicSiteColumnValues = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValues"] ?? new List<tbl_SiteColumnValues>();
            var dynamicSiteColumnValuesEdit = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValuesEdit"] ?? new List<tbl_SiteColumnValues>();

            dynamicExternalFormFields = (List<tbl_SiteActivityRuleExternalFormFields>)ViewState["DynamicExternalFormFields"] ?? new List<tbl_SiteActivityRuleExternalFormFields>();
            var field = dynamicExternalFormFields.Where(a => a.ID == Guid.Parse(e.CommandArgument.ToString())).FirstOrDefault();
            SiteColumnTooltip2.SiteColumnID = (Guid)field.SiteColumnID;
            var additionalFields = new Dictionary<string, string>();
            additionalFields.Add("FieldID", field.ID.ToString());
            additionalFields.Add("FormID", field.SiteActivityRuleExternalFormID.ToString());
            if (_siteActivityRuleId != Guid.Empty)
                additionalFields.Add("SiteActivityRuleID", _siteActivityRuleId.ToString());
            else
                additionalFields.Add("SiteActivityRuleID", _newSiteActivityRuleId.ToString());
            additionalFields.Add("FormFieldType", field.FieldType.ToString());
            SiteColumnTooltip2.AdditionalFields = additionalFields;
            SiteColumnTooltip2.SiteColumns = dynamicSiteColumns;
            SiteColumnTooltip2.SiteColumnValues = dynamicSiteColumnValues;
            SiteColumnTooltip2.SiteColumnValuesEdit = dynamicSiteColumnValuesEdit;

            SiteColumnTooltip2.BindData();

            rrtSiteColumnExternalForms.Show();
        }

        protected void lvExternalForms_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (tbl_SiteActivityRuleExternalForms)e.Item.DataItem;
                if (_ruleTypeId == (int)RuleType.ExternalForm)
                    ((Literal)e.Item.FindControl("lActionURL")).Text = string.Format(WebConfigurationManager.AppSettings["externalFormUrl"], item.ID);
                else
                    ((Literal)e.Item.FindControl("lActionURL")).Text = (WebConfigurationManager.AppSettings["externalFormUrl"]).Replace("?id={0}", string.Empty);
            }
        }

        protected void RadAjaxPanel1_OnAjaxRequest(object sender, AjaxRequestEventArgs e)
        {
        }


        protected void GetWufooForm()
        {
            try
            {
                var client = new WufooClient(txtWufooName.Text, txtWufooAPIKey.Text);
                var wufooForm = client.GetAllForms().FirstOrDefault(o => o.Hash == txtCode.Text);

                WufooFileds = new List<KeyValuePair<string, string>>();

                var systemFields = new string[] { "DateCreated", "CreatedBy", "LastUpdated", "UpdatedBy", "EntryId" };

                var fields = client.GetFieldsByFormId(wufooForm.Hash, false).Where(o => !systemFields.Contains(o.Id)).ToList();

                var externalForm = dataManager.SiteActivityRuleExternalForms.Select(wufooForm.Hash, _siteActivityRuleId) ??
                                   new tbl_SiteActivityRuleExternalForms
                                   {
                                       ID = Guid.NewGuid(),
                                       Name = wufooForm.Hash,
                                       SiteActivityRuleID = _siteActivityRuleId
                                   };

                bool isFieldsAlreadyAdded = dynamicExternalFormFields.Any();

                if (!dynamicExternalForms.Any())
                    dynamicExternalForms.Add(externalForm);

                foreach (var field in fields)
                {
                    if (field.SubFields != null)
                    {
                        foreach (var subField in field.SubFields)
                        {
                            if (!isFieldsAlreadyAdded)
                            {
                                var externalFormField =
                                    dataManager.SiteActivityRuleExternalFormFields.Select(subField.Id, externalForm.ID) ??
                                    new tbl_SiteActivityRuleExternalFormFields
                                    {
                                        ID = Guid.NewGuid(),
                                        SiteActivityRuleExternalFormID = externalForm.ID,
                                        Name = subField.Id
                                    };
                                externalFormField.FieldType = (int)FormFieldType.Input;

                                dynamicExternalFormFields.Add(externalFormField);
                            }

                            WufooFileds.Add(new KeyValuePair<string, string>(subField.Id, subField.Label));
                        }
                    }
                    else
                    {
                        if (!isFieldsAlreadyAdded)
                        {
                            var externalFormField =
                                dataManager.SiteActivityRuleExternalFormFields.Select(field.Id, externalForm.ID) ??
                                new tbl_SiteActivityRuleExternalFormFields
                                {
                                    ID = Guid.NewGuid(),
                                    SiteActivityRuleExternalFormID = externalForm.ID,
                                    Name = field.Id
                                };

                            //"text" "select" "number" "textarea" "checkbox" "radio" 

                            switch (field.Type)
                            {
                                case "text":
                                    externalFormField.FieldType = (int)FormFieldType.Input;
                                    break;
                                case "select":
                                    externalFormField.FieldType = (int)FormFieldType.Select;
                                    break;
                                case "textarea":
                                    externalFormField.FieldType = (int)FormFieldType.Textarea;
                                    break;
                                default:
                                    externalFormField.FieldType = (int)FormFieldType.Input;
                                    break;
                            }

                            dynamicExternalFormFields.Add(externalFormField);
                        }

                        WufooFileds.Add(new KeyValuePair<string, string>(field.Id, field.Title));
                    }
                }

                rgExternalFormFields.DataSource = dynamicExternalFormFields;
                rgExternalFormFields.DataBind();

                rgExternalFormFields.Visible = true;
            }
            catch (Exception ex)
            {
                ucNotificationMessage.Text = "При получении данных с Wufoo возникли проблемы. Попробуйте позже.";
                Log.Error("GetWufooForm()", ex);
            }
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnLoadData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnLoadData_OnClick(object sender, EventArgs e)
        {
            try
            {
                var result = ExternalFormService.WufooLoadData(siteID, _siteActivityRuleId, rdtpLoadDataDate.SelectedDate.Value);

                if (string.IsNullOrEmpty(result.Message))
                {
                    rdtpRevisionDate.SelectedDate = result.RevisionDate;
                    ucLoadDataMessage.MessageType = NotificationMessageType.Success;
                    ucLoadDataMessage.Text = "Данные успешно загружены.";
                }
                else
                {
                    ucLoadDataMessage.MessageType = NotificationMessageType.Warning;
                    ucLoadDataMessage.Text = result.Message;
                }
            }
            catch (Exception ex)
            {
                ucLoadDataMessage.MessageType = NotificationMessageType.Warning;
                ucLoadDataMessage.Text = "Ошибка загрузки данных.";

                Log.Error("Ошибка загрузки данных.", ex);
            }
        }



        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        protected string GetLabel(string field)
        {
            var result = string.Empty;

            if (_ruleTypeId == (int)RuleType.LPgenerator)
                return result;

            if (WufooFileds == null)
                return field;

            if (WufooFileds.Any(o => o.Key == field))
                return WufooFileds.SingleOrDefault(o => o.Key == field).Value;

            return result;
        }


        /// <summary>
        /// Decodes the CSS.
        /// </summary>
        /// <param name="css">The CSS.</param>
        protected void DecodeCss(string css)
        {
            if (string.IsNullOrEmpty(css))
                return;

            var attributes = css.Split(new[] { ';' });
            foreach (var attribute in attributes)
            {
                var attr = attribute.Split(new[] { ':' });
                switch (attr[0])
                {
                    case "background-color":
                        rcpBackgroundColor.SelectedColor = ColorTranslator.FromHtml(attr[1]);
                        break;
                }
            }
        }



        /// <summary>
        /// Encodes the background CSS.
        /// </summary>
        /// <param name="rcpBackgroundColor">Color of the RCP background.</param>
        /// <returns></returns>
        protected string EncodeBackgroundCss(RadColorPicker rcpBackgroundColor)
        {
            var css = new StringBuilder();

            if (!rcpBackgroundColor.SelectedColor.IsEmpty)
                css.AppendFormat("background-color:{0};", ColorTranslator.ToHtml(rcpBackgroundColor.SelectedColor));

            if (css.ToString() == "")
                return null;

            return css.ToString();
        }

        protected void rauImageFile_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            using (var stream = e.File.InputStream)
            {
                var imageData = new byte[stream.Length];
                stream.Read(imageData, 0, (int)stream.Length);
                //rbiImage.DataValue = imageData;

                if (rtvFormTree.SelectedNode != null && Guid.Parse(rtvFormTree.SelectedNode.Value) != Guid.Empty)
                {
                    if (HttpContext.Current.Cache.Get(_cacheKey + rtvFormTree.SelectedNode.Value) != null)
                        HttpContext.Current.Cache.Remove(_cacheKey + rtvFormTree.SelectedNode.Value);
                    HttpContext.Current.Cache.Insert(_cacheKey + rtvFormTree.SelectedNode.Value, imageData);

                    stream.Position = 0;
                    var bitmap = new Bitmap(stream);
                    txtImageWidth.Text = bitmap.Width.ToString();
                    txtImageWidthPercent.Text = "100";
                    txtImageHeight.Text = bitmap.Height.ToString();
                    txtImageHeightPercent.Text = "100";

                    LoadProperties();
                }
            }
        }


        public void ClearCache()
        {
            var keys = new List<string>();
            // retrieve application Cache enumerator
            var enumerator = Cache.GetEnumerator();
            // copy all keys that currently exist in Cache
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().Contains(_cacheKey))
                    keys.Add(enumerator.Key.ToString());
            }
            // delete every key from cache
            for (int i = 0; i < keys.Count; i++)
                Cache.Remove(keys[i]);
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlActionOnFillForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlActionOnFillForm_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((ActionOnFillForm)ddlActionOnFillForm.SelectedValue.ToInt())
            {
                case ActionOnFillForm.Redirect:
                    pnlActionRedirect.Visible = true;
                    pnlActionMessage.Visible = false;
                    break;
                case ActionOnFillForm.PopupMessage:
                case ActionOnFillForm.Message:
                    pnlActionRedirect.Visible = false;
                    pnlActionMessage.Visible = true;
                    break;
            }
        }
    }
}