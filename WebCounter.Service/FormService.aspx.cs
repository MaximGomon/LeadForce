using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.WebSite;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;
using WebCounter.Service.UserControls;

namespace WebCounter.Service
{
    public partial class FormService : System.Web.UI.Page
    {
        private WebCounterServiceRepository repository = new WebCounterServiceRepository();
        public ContactData contactData;
        public List<KeyValuePair<string, string>> contactDataValues;
        public int counter;
        private int extraFieldCount = 0;

        private Guid siteID;
        private Guid contactID;
        private string activityCode;
        private string parameter;
        private bool register;
        private bool isAdmin;
        private List<string> checkboxes = new List<string>(); 


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            counter = 0;

            siteID = Guid.Parse(Request.QueryString["sid"]);
            contactID = Guid.Parse(Request.QueryString["cid"]);
            activityCode = Request.QueryString["code"];
            parameter = Request.QueryString["parameter"];
            register = bool.Parse(Request.QueryString["register"]);
            if (Request.QueryString["isadmin"] != null)
                isAdmin = bool.Parse(Request.QueryString["isadmin"]);

            contactData = new ContactData(siteID);
            contactDataValues = contactData.GetContactData(contactID);

            var siteActivityRule = repository.SiteActivityRules_SelectByCode(siteID, activityCode);
            var siteActivityRuleLayouts = repository.SiteActivityRuleLayout_SelectBySiteActivityRuleID(siteActivityRule.ID);


            //if (!string.IsNullOrEmpty(siteActivityRule.YandexGoals))
            //    lbtnSave.OnClientClick = string.Concat("if (!ValidatePage()) return false; ", siteActivityRule.YandexGoals, " return true;");

            //var contactColumnValues = repository.ContactColumnValues_SelectByContactID(contactID);
            var contactColumnValues = repository.GetContactColumnValues(contactID);

            if (siteActivityRule.FormWidth != null && (int)siteActivityRule.FormWidth > 0)
                pnlFormContainer.Width = new Unit((int)siteActivityRule.FormWidth, UnitType.Pixel);

            if (!string.IsNullOrEmpty(siteActivityRule.CSSForm))
                form1.Attributes.Add("style", siteActivityRule.CSSForm);

            if (!string.IsNullOrEmpty(siteActivityRule.ErrorMessage))
            {
                var LFErrorMessage = new Panel { ID = "LFErrorMessage", ClientIDMode = ClientIDMode.Static };
                LFErrorMessage.Attributes.Add("style", "display:none;");
                LFErrorMessage.Controls.Add(new Literal { Text = siteActivityRule.ErrorMessage });
                pnlFormContainer.Controls.Add(LFErrorMessage);
            }

            ProceedResources(siteActivityRule);

            BuildLayout(siteID, contactID, siteActivityRule, siteActivityRuleLayouts, null, contactColumnValues, register);

            if (!string.IsNullOrEmpty(siteActivityRule.TextButton))
                lbtnSave.Text = string.Format("<em>&nbsp;</em><span>{0}</span>", siteActivityRule.TextButton);

            if (!string.IsNullOrEmpty(siteActivityRule.CSSButton))
                lbtnSave.Attributes.Add("style", siteActivityRule.CSSButton);


        }

        protected void BuildLayout(Guid siteID, Guid contactID, tbl_SiteActivityRules siteActivityRule, List<SiteActivityRuleLayoutParams> siteActivityRuleLayouts, SiteActivityRuleLayoutParams parentLayout, List<ContactColumnValueMap> contactColumnValues, bool register)
        {
            var skin = ((Skin)siteActivityRule.Skin).ToString();
            FormDecorator.Skin = skin;

            List<SiteActivityRuleLayoutParams> layouts;
            if (parentLayout == null)
                layouts = siteActivityRuleLayouts.Where(a => a.ParentID == null).OrderBy(a => a.Order).ToList();
            else
                layouts = siteActivityRuleLayouts.Where(a => a.ParentID == parentLayout.ID).OrderBy(a => a.Order).ToList();


            foreach (var layout in layouts)
            {
                var controlToInsert = new Control();
                var container = new Panel();
                var fieldWrapper = new Panel { CssClass = "field-wrapper" };

                if (parentLayout == null)
                    controlToInsert = pnlFormContainer;
                else
                    controlToInsert = pnlFormContainer.FindControl(string.Format("csf-{0}", layout.ParentID.ToString().Replace("-", "__")));

                switch ((LayoutType)layout.LayoutType)
                {
                    case LayoutType.GroupFields:
                        if ((OutputFormat)layout.OutputFormat == OutputFormat.Header)
                        {
                            var hdr = new Panel { CssClass = string.Format("hdr-{0}", layout.ID) };
                            var linkHdr = new LinkButton
                                              {
                                                  Text = layout.Name,
                                                  OnClientClick = string.Format("toggleForm('csf-{0}'); return false;", layout.ID),
                                                  CssClass = "hdr"
                                              };
                            hdr.Controls.Add(linkHdr);
                            controlToInsert.Controls.Add(hdr);
                        }

                        var pnl = new Panel { ID = string.Format("csf-{0}", layout.ID.ToString().Replace("-", "__")), CssClass = string.Format("csf-{0}", layout.ID) };
                        pnl.Attributes.Add("style", "width: 100%; display: table;" + layout.CSSStyle);
                        controlToInsert.Controls.Add(pnl);
                        break;

                    case LayoutType.TextBlock:
                        var textBlock = new Panel { CssClass = string.Format("csf-{0}", layout.ID), ClientIDMode = ClientIDMode.Static };

                        textBlock.Controls.Add(new Literal { Text = layout.Description });

                        if (!string.IsNullOrEmpty(layout.CSSStyle) && layout.CSSStyle.Trim() != string.Empty)
                            textBlock.Attributes.Add("style", layout.CSSStyle);

                        if (!string.IsNullOrEmpty(siteActivityRule.ErrorMessage) && !string.IsNullOrEmpty(layout.LayoutParams))
                        {
                            var layoutParams = LayoutParams.Deserialize(layout.LayoutParams);
                            if (layoutParams.GetBoolValue("IsUsedForErrorMessage"))
                            {
                                if (textBlock.Attributes["style"] != null)
                                    textBlock.Attributes["style"] += ";display:none";
                                else
                                    textBlock.Attributes.Add("style", "display:none;");

                                textBlock.Attributes["style"] += "padding: 5px 0;color:#b63306";
                                textBlock.ID = "LFErrorMessageTextBlock";
                            }
                        }

                        container.Controls.Add(textBlock);
                        controlToInsert.Controls.Add(container);
                        break;

                    case LayoutType.Image:
                        var binaryImage = new RadBinaryImage { CssClass = string.Format("csf-{0}", layout.ID), ClientIDMode = ClientIDMode.Static };

                        var fsp = new FileSystemProvider();
                        var filePath = fsp.GetRemoteLink(siteID, "SiteActivityRule", layout.ID.ToString() + ".png", FileType.Image);
                        binaryImage.ImageUrl = filePath;

                        if (!string.IsNullOrEmpty(layout.LayoutParams))
                        {
                            var layoutParams = LayoutParams.Deserialize(layout.LayoutParams);
                            if (!string.IsNullOrEmpty(layoutParams.GetValue("ImageWidth")))
                                binaryImage.Width = new Unit(int.Parse(layoutParams.GetValue("ImageWidth")), UnitType.Pixel);
                            if (!string.IsNullOrEmpty(layoutParams.GetValue("ImageHeight")))
                                binaryImage.Height = new Unit(int.Parse(layoutParams.GetValue("ImageHeight")), UnitType.Pixel);
                            if (!string.IsNullOrEmpty(layoutParams.GetValue("ImageAlign")))
                                container.Attributes.CssStyle.Add("text-align", layoutParams.GetValue("ImageAlign"));
                            if (!string.IsNullOrEmpty(layoutParams.GetValue("ImageAlternativeText")))
                                binaryImage.AlternateText = layoutParams.GetValue("ImageAlternativeText");
                        }

                        container.Controls.Add(binaryImage);
                        controlToInsert.Controls.Add(container);
                        break;

                    case LayoutType.ProfileField:
                    case LayoutType.FormField:
                        if (register && layout.IsAdmin) continue;

                        //var controlId = string.Format("LF{0}___{1}___", counter++, layout.SiteColumnID.HasValue ? ((Guid)layout.SiteColumnID).ToString() : layout.SysField);
                        var controlId = string.Format("LF{0}___{1}___", counter++, layout.SiteColumnID.HasValue ? ((Guid)layout.SiteColumnID).ToString().Replace("-", "__") : layout.SysField);

                        var field = contactData.GetFieldByValue(layout.SiteColumnID.HasValue ? ((Guid)layout.SiteColumnID).ToString() : layout.SysField, siteActivityRule.ID);

                        var label = new Label { AssociatedControlID = controlId, Text = layout.SiteColumnID.HasValue ? layout.SiteColumnName : field.Name, CssClass = "label"};
                        var fieldClass = string.Empty;
                        if (parentLayout != null)
                        {
                            if ((OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Top || (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Left)
                                container.Controls.Add(label);
                            if ((OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Top)
                                container.Controls.Add(new LiteralControl("<br />"));

                            fieldClass = GetFieldClass(parentLayout);
                            if (!string.IsNullOrEmpty(fieldClass))
                                container.CssClass = fieldClass;
                        }

                        //var contactDataValues = contactData.GetContactData(contactID);

                        //RadDatePicker datePicker = new RadDatePicker();

                        var dataValue = contactDataValues.FirstOrDefault(a => a.Key == field.Value);
                        switch (field.ColumnType)
                        {
                            case ColumnType.String:
                                var textBox = new RadTextBox { ID = controlId, Width = new Unit(98, UnitType.Percentage), Skin = skin };

                                if (!string.IsNullOrEmpty(dataValue.Key))
                                    textBox.Text = dataValue.Value;
                                else if (field.IsAdditional && !string.IsNullOrEmpty(layout.DefaultValue))
                                        textBox.Text = layout.DefaultValue;

                                if (!string.IsNullOrEmpty(layout.CSSStyle))
                                    textBox.Attributes.Add("style", ClearBackground(layout.CSSStyle));

                                textBox.Attributes.Add("onfocus", "TextboxFocus(this);");

                                if ((OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.InElement)
                                    textBox.EmptyMessage = layout.SiteColumnID.HasValue ? layout.SiteColumnName : field.Name;

                                fieldWrapper.Controls.Add(textBox);
                                container.Controls.Add(fieldWrapper);
                                break;
                            case ColumnType.Text:
                                var text = new RadTextBox { ID = controlId, Width = new Unit(98, UnitType.Percentage), TextMode = InputMode.MultiLine, Skin = skin };

                                if (!string.IsNullOrEmpty(dataValue.Key))
                                    text.Text = !string.IsNullOrEmpty(dataValue.Value) ? dataValue.Value.Replace("<br />", "\n") : null;
                                else if (field.IsAdditional && !string.IsNullOrEmpty(layout.DefaultValue))
                                    text.Text = !string.IsNullOrEmpty(layout.DefaultValue) ? layout.DefaultValue.Replace("<br />", "\n") : null;

                                if (!string.IsNullOrEmpty(layout.CSSStyle))
                                    text.Attributes.Add("style", ClearBackground(layout.CSSStyle));

                                text.Attributes.Add("onfocus", "TextboxFocus(this);");

                                if ((OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.InElement)
                                    text.EmptyMessage = layout.SiteColumnID.HasValue ? layout.SiteColumnName : field.Name;

                                fieldWrapper.Controls.Add(text);
                                container.Controls.Add(fieldWrapper);
                                break;
                            case ColumnType.Number:
                                var numericTextBox = new RadNumericTextBox { ID = controlId, Width = new Unit(98, UnitType.Percentage), AutoCompleteType = AutoCompleteType.Disabled, Type = NumericType.Number, Skin = skin.ToString() };
                                numericTextBox.NumberFormat.GroupSeparator = "";
                                numericTextBox.NumberFormat.DecimalDigits = 0;

                                if (!string.IsNullOrEmpty(dataValue.Key))
                                    numericTextBox.Text = dataValue.Value;
                                else if (field.IsAdditional && !string.IsNullOrEmpty(layout.DefaultValue))
                                    numericTextBox.Text = layout.DefaultValue;

                                if (!string.IsNullOrEmpty(layout.CSSStyle))
                                    numericTextBox.Attributes.Add("style", ClearBackground(layout.CSSStyle));

                                numericTextBox.Attributes.Add("onfocus", "TextboxFocus(this);");

                                if ((OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.InElement)
                                    numericTextBox.EmptyMessage = layout.SiteColumnID.HasValue ? layout.SiteColumnName : field.Name;

                                fieldWrapper.Controls.Add(numericTextBox);
                                container.Controls.Add(fieldWrapper);
                                break;
                            case ColumnType.Date:
                                var datePicker = new RadDateTimePicker { ID = controlId, Width = new Unit(98, UnitType.Percentage), ShowPopupOnFocus = true, Skin = skin };
                                datePicker.TimePopupButton.Visible = false;
                                datePicker.DateInput.DateFormat = "dd/MM/yyyy";

                                if (layout.ColumnTypeExpressionID.HasValue)
                                {
                                    var columnTypesExpression = repository.ColumnTypesExpression_SelectById((Guid) layout.ColumnTypeExpressionID);
                                    datePicker.DateInput.DateFormat = columnTypesExpression.Expression;
                                    switch (columnTypesExpression.Expression)
                                    {
                                        case "dd/MM/yyyy hh:mm":
                                            datePicker.TimePopupButton.Visible = true;
                                            break;
                                        case "hh:mm":
                                            datePicker.TimePopupButton.Visible = true;
                                            datePicker.DatePopupButton.Visible = false;
                                            datePicker.ShowPopupOnFocus = false;
                                            break;
                                        /*case "dd/MM/yyyy":
                                        default:
                                            datePicker.DateInput.DateFormat = columnTypesExpression.Expression;
                                            break;*/
                                    }
                                }
                                

                                if (!string.IsNullOrEmpty(dataValue.Key))
                                {
                                    if (!string.IsNullOrEmpty(dataValue.Value))
                                        datePicker.SelectedDate = DateTime.Parse(dataValue.Value);
                                }
                                else if (field.IsAdditional && !string.IsNullOrEmpty(layout.DefaultValue))
                                    datePicker.SelectedDate = DateTime.Parse(layout.DefaultValue);

                                if (!string.IsNullOrEmpty(layout.CSSStyle))
                                    datePicker.DateInput.Attributes.Add("style", ClearBackground(layout.CSSStyle));

                                datePicker.ClientEvents.OnPopupOpening = "PopupOpening";

                                fieldWrapper.Controls.Add(datePicker);
                                container.Controls.Add(fieldWrapper);
                                break;
                            case ColumnType.Enum:
                                var dictionaryComboBox = (DictionaryComboBox)LoadControl("~/UserControls/DictionaryComboBox.ascx");
                                dictionaryComboBox.ID = controlId;
                                dictionaryComboBox.DataValueField = "ID";
                                dictionaryComboBox.Skin = skin;

                                if (field.IsAdditional)
                                {
                                    dictionaryComboBox.DictionaryName = "tbl_SiteColumnValues";
                                    dictionaryComboBox.DataTextField = "Value";
                                    dictionaryComboBox.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn { DbType = DbType.Guid, Name = "SiteColumnID", Value = field.Value });

                                    if (!string.IsNullOrEmpty(dataValue.Key) && !string.IsNullOrEmpty(dataValue.Value))
                                        dictionaryComboBox.SelectedId = Guid.Parse(dataValue.Value);
                                    else if (field.IsAdditional && !string.IsNullOrEmpty(layout.DefaultValue))
                                        dictionaryComboBox.SelectedId = Guid.Parse(layout.DefaultValue);
                                }
                                else
                                {
                                    var order = new List<DictionaryComboBox.DictionaryOrderColumn>();
                                    switch (field.Value)
                                    {
                                        case "sys_joblevel":
                                            dictionaryComboBox.SiteID = siteID;
                                            dictionaryComboBox.DictionaryName = "tbl_ContactJobLevel";
                                            dictionaryComboBox.DataTextField = "Name";
                                            order.Add(new DictionaryComboBox.DictionaryOrderColumn { Name = "Name", Direction = "ASC" });
                                            break;
                                        case "sys_functionincompany":
                                            dictionaryComboBox.SiteID = siteID;
                                            dictionaryComboBox.DictionaryName = "tbl_ContactFunctionInCompany";
                                            dictionaryComboBox.DataTextField = "Name";
                                            order.Add(new DictionaryComboBox.DictionaryOrderColumn { Name = "Name", Direction = "ASC" });
                                            break;
                                        case "sys_country":
                                            dictionaryComboBox.DictionaryName = "tbl_Country";
                                            dictionaryComboBox.DataTextField = "Name";
                                            order.Add(new DictionaryComboBox.DictionaryOrderColumn { Name = "Name", Direction = "ASC" });
                                            break;
                                        case "sys_city":
                                            dictionaryComboBox.DictionaryName = "tbl_City";
                                            dictionaryComboBox.DataTextField = "Name";
                                            order.Add(new DictionaryComboBox.DictionaryOrderColumn { Name = "Name", Direction = "ASC" });
                                            break;
                                        case "sys_advertisingplatform":
                                            dictionaryComboBox.SiteID = siteID;
                                            dictionaryComboBox.DictionaryName = "tbl_AdvertisingPlatform";
                                            dictionaryComboBox.DataTextField = "Title";
                                            order.Add(new DictionaryComboBox.DictionaryOrderColumn { Name = "Title", Direction = "ASC" });
                                            break;
                                        case "sys_advertisingtype":
                                            dictionaryComboBox.SiteID = siteID;
                                            dictionaryComboBox.DictionaryName = "tbl_AdvertisingType";
                                            dictionaryComboBox.DataTextField = "Title";
                                            order.Add(new DictionaryComboBox.DictionaryOrderColumn { Name = "Title", Direction = "ASC" });
                                            break;
                                        case "sys_advertisingcampaign":
                                            dictionaryComboBox.SiteID = siteID;
                                            dictionaryComboBox.DictionaryName = "tbl_AdvertisingCampaign";
                                            dictionaryComboBox.DataTextField = "Title";
                                            order.Add(new DictionaryComboBox.DictionaryOrderColumn { Name = "Title", Direction = "ASC" });
                                            break;
                                    }
                                    dictionaryComboBox.Order = order;
                                    
                                    if (!string.IsNullOrEmpty(dataValue.Value))
                                        dictionaryComboBox.SelectedId = Guid.Parse(dataValue.Value);
                                }

                                if (layout.IsRequired)
                                {
                                    dictionaryComboBox.ComboBox.OnClientSelectedIndexChanged = "ClientSelectedIndexChanged";
                                    dictionaryComboBox.ValidationGroup = "vg";
                                }

                                if (!string.IsNullOrEmpty(layout.CSSStyle))
                                {
                                    dictionaryComboBox.ComboBox.Attributes.Add("style", ClearBackground(layout.CSSStyle));
                                    if (IsContainsBackground(layout.CSSStyle))
                                        dictionaryComboBox.Skin = "Transparent";
                                }

                                if ((OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.InElement)
                                    dictionaryComboBox.EmptyItemText = layout.SiteColumnID.HasValue ? layout.SiteColumnName : field.Name;

                                fieldWrapper.Controls.Add(dictionaryComboBox);
                                container.Controls.Add(fieldWrapper);
                                break;

                            case ColumnType.Logical:
                                var checkBox = new CheckBox { ID = controlId, Width = new Unit(16, UnitType.Pixel) };
                                if (!checkboxes.Contains(controlId))
                                    checkboxes.Add(controlId);

                                if (!string.IsNullOrEmpty(dataValue.Key))
                                    checkBox.Checked = !string.IsNullOrEmpty(dataValue.Value) ? bool.Parse(dataValue.Value) : false;
                                else if (field.IsAdditional && !string.IsNullOrEmpty(layout.DefaultValue))
                                    checkBox.Checked = !string.IsNullOrEmpty(layout.DefaultValue) ? bool.Parse(layout.DefaultValue) : false;

                                /*if (!string.IsNullOrEmpty(layout.CSSStyle))
                                    text.Attributes.Add("style", ClearBackground(layout.CSSStyle));*/

                                fieldWrapper.Controls.Add(checkBox);
                                container.Controls.Add(fieldWrapper);
                                break;
                        }

                        /*if (!string.IsNullOrEmpty(layout.CSSStyle) && layout.CSSStyle.Trim() != string.Empty)
                            fieldWrapper.Attributes.Add("style", layout.CSSStyle);*/

                        if (layout.IsRequired && field.ColumnType != ColumnType.Enum)
                            fieldWrapper.Controls.Add(new RequiredFieldValidator { ControlToValidate = controlId, ErrorMessage = "Поле обязательное для заполнения", Display = ValidatorDisplay.None, ValidationGroup = "vg" });

                        if (layout.ColumnTypeExpressionID.HasValue)
                        {
                            if (field.ColumnType != ColumnType.Date)
                            {
                                var columnTypesExpression = repository.ColumnTypesExpression_SelectById((Guid)layout.ColumnTypeExpressionID);
                                fieldWrapper.Controls.Add(new RegularExpressionValidator { ValidationExpression = columnTypesExpression.Expression, ControlToValidate = controlId, ErrorMessage = "Неправильный формат поля", Display = ValidatorDisplay.None, ValidationGroup = "vg" });   
                            }
                        }
                            
                        if (!layout.IsExtraField || (layout.IsExtraField && (siteActivityRule.CountExtraFields == null || siteActivityRule.CountExtraFields > extraFieldCount) && string.IsNullOrEmpty(dataValue.Value)))
                        {
                            controlToInsert.Controls.Add(container);
                            extraFieldCount++;
                        }
                        break;
                }

                BuildLayout(siteID, contactID, siteActivityRule, siteActivityRuleLayouts, layout, contactColumnValues, register);
            }
        }


        protected void OnClick(object sender, EventArgs e)
        {
            var values = GetValues();
            var contactID = Guid.Parse(Request.QueryString["cid"]);
            contactData.SaveForm(contactID, values);

            var siteActivityRule = repository.SiteActivityRules_SelectByCode(siteID, activityCode);
            if (siteActivityRule != null)
            {
                if (register || !isAdmin)
                {
                    var ActivityCodeWithParameter = activityCode;
                    if (!string.IsNullOrEmpty(parameter))
                        ActivityCodeWithParameter = ActivityCodeWithParameter + "#" + parameter;

                    CounterServiceHelper.AddContact(siteID, contactID, ActivityType.FillForm, ActivityCodeWithParameter, null, null);
                }

                if (!string.IsNullOrEmpty(siteActivityRule.YandexGoals))
                    hdnYandexGoals.Value = siteActivityRule.YandexGoals;

                switch ((ActionOnFillForm)siteActivityRule.ActionOnFillForm)
                {
                    case ActionOnFillForm.Redirect:
                        if (!string.IsNullOrEmpty(siteActivityRule.URL))
                        {
                            hdnUrl.Value = siteActivityRule.URL;
                            if (siteActivityRule.SendFields)
                            {
                                hdnUrl.Value += siteActivityRule.URL.Contains("?") ? "&" : "?";
                                foreach (var val in values)
                                    hdnUrl.Value += string.Format("{0}={1}&", val.Key, val.Value);
                                hdnUrl.Value = hdnUrl.Value.TrimEnd(new [] {'&'});
                            }
                        }
                        else
                            hdnUrl.Value = "reload";
                        break;
                    case ActionOnFillForm.PopupMessage:
                        hdnSuccessMessage.Value = siteActivityRule.SuccessMessage;
                        break;
                    case ActionOnFillForm.Message:
                        pnlFormContainer.Controls.Clear();
                        pnlFormContainer.Controls.Add(new Literal { Text = siteActivityRule.SuccessMessage });
                        lbtnSave.Visible = false;
                        break;
                }    
            }
        }


        public List<KeyValuePair<string, string>> GetValues()
        {
            var values = new List<KeyValuePair<string, string>>();

            foreach (var key in Request.Form.AllKeys)
            {
                var match = Regex.Match(key, @"^LF[\d]{1,}___(?<name>.*?)___($|(\$rcbDictionary))", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var control = pnlFormContainer.FindControl(key.Substring(0, key.IndexOf("$") != -1 ? key.IndexOf("$") : key.Length));
                    var value = match.Groups["name"].Value.Replace("__", "-");

                    if (control is RadTextBox)
                        values.Add(new KeyValuePair<string, string>(value, ((RadTextBox)control).Text));

                    if (control is RadNumericTextBox)
                        values.Add(new KeyValuePair<string, string>(value, ((RadNumericTextBox)control).Text));

                    if (control is RadDateTimePicker)
                        values.Add(new KeyValuePair<string, string>(value, ((RadDateTimePicker)control).SelectedDate.ToString()));

                    if (control is DictionaryComboBox)
                    {
                        if (((DictionaryComboBox)control).SelectedIdNullable != null && ((DictionaryComboBox)control).SelectedIdNullable != Guid.Empty)
                            values.Add(new KeyValuePair<string, string>(value, ((DictionaryComboBox)control).SelectedIdNullable.ToString()));
                        else if (((DictionaryComboBox)control).Text != ((DictionaryComboBox)control).EmptyItemText)
                            values.Add(new KeyValuePair<string, string>(value, ((DictionaryComboBox)control).Text));
                        else
                            values.Add(new KeyValuePair<string, string>(value, ""));
                    }
                }
            }

            foreach (var checkbox in checkboxes)
            {
                var match = Regex.Match(checkbox, @"^LF[\d]{1,}___(?<name>.*?)___($|(\$rcbDictionary))", RegexOptions.IgnoreCase);
                var value = match.Groups["name"].Value.Replace("__", "-");
                values.Add(new KeyValuePair<string, string>(value, (!string.IsNullOrEmpty(Request.Form[checkbox]) && match.Success).ToString()));
            }

            return values;
        }


        protected string GetFieldClass(tbl_SiteActivityRuleLayout parentLayout)
        {
            var fieldClass = string.Empty;
            if ((BusinessLogicLayer.Orientation)parentLayout.Orientation == BusinessLogicLayer.Orientation.Horizontal && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Left)
                fieldClass = fieldClass + "hor-left";
            if ((BusinessLogicLayer.Orientation)parentLayout.Orientation == BusinessLogicLayer.Orientation.Horizontal && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Top)
                fieldClass = fieldClass + "hor-top";
            if ((BusinessLogicLayer.Orientation)parentLayout.Orientation == BusinessLogicLayer.Orientation.Vertical && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Left)
                fieldClass = fieldClass + "ver-left";
            if ((BusinessLogicLayer.Orientation)parentLayout.Orientation == BusinessLogicLayer.Orientation.Vertical && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.Top)
                fieldClass = fieldClass + "ver-top";
            if ((BusinessLogicLayer.Orientation)parentLayout.Orientation == BusinessLogicLayer.Orientation.Horizontal && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.None)
                fieldClass = fieldClass + "hor-none";
            if ((BusinessLogicLayer.Orientation)parentLayout.Orientation == BusinessLogicLayer.Orientation.Vertical && (OutputFormatFields)parentLayout.OutputFormatFields == OutputFormatFields.None)
                fieldClass = fieldClass + "ver-none";

            return fieldClass;
        }


        protected string ClearBackground(string cssStyle)
        {
            if (cssStyle.ToLower().Contains("background-color"))
                return "background:none;" + cssStyle;
            return cssStyle;
        }



        protected bool IsContainsBackground(string cssStyle)
        {
            return cssStyle.ToLower().Contains("background-color");
        }


        public void ProceedResources(tbl_SiteActivityRules siteActivityRules)
        {
            var externalResources = repository.ExternalResource_SelectByDestinationID(siteActivityRules.ID);

            if (!externalResources.Any())
                return;

            foreach (var resource in externalResources)
            {
                switch ((ExternalResourceType)resource.ExternalResourceTypeID)
                {
                    case ExternalResourceType.JavaScript:
                        var result = string.Empty;
                        if (!string.IsNullOrEmpty(resource.File))
                        {
                            var fsp = new FileSystemProvider();
                            var filePath = fsp.GetRemoteLink(siteActivityRules.SiteID, "ExternalResource", resource.File, FileType.Attachment);
                            result += string.Format(@"<script type=""text/javascript"" src=""{0}""></script>", filePath);
                        }
                        if (!string.IsNullOrEmpty(resource.Url))
                            result += string.Format(@"<script type=""text/javascript"" src=""{0}""></script>", resource.Url);
                        if (!string.IsNullOrEmpty(resource.Text))
                        {
                            if (!resource.Text.Contains("<script"))
                                result += string.Format(@"<script type=""text/javascript"">{0}</script>", resource.Text);
                            else
                                result += resource.Text;
                        }

                        PutToPlace((ResourcePlace)resource.ResourcePlaceID, result);
                        break;
                    case ExternalResourceType.CSS:
                        var css = string.Empty;
                        if (!string.IsNullOrEmpty(resource.File))
                        {
                            var fsp = new FileSystemProvider();
                            var filePath = fsp.GetRemoteLink(siteActivityRules.SiteID, "ExternalResource", resource.File, FileType.Attachment);
                            css += string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}"" />", filePath);
                        }
                        if (!string.IsNullOrEmpty(resource.Url))
                            css += string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}"" />", resource.Url);
                        if (!string.IsNullOrEmpty(resource.Text))
                        {
                            if (!resource.Text.Contains("<style"))
                                css += string.Format(@"<style type=""text/css"">{0}</style>", resource.Text);
                            else
                                css += resource.Text;
                        }

                        PutToPlace((ResourcePlace)resource.ResourcePlaceID, css);
                        break;
                }
            }
        }



        /// <summary>
        /// Puts to place.
        /// </summary>
        /// <param name="place">The place.</param>
        /// <param name="resource">The resource.</param>
        protected void PutToPlace(ResourcePlace place, string resource)
        {
            switch (place)
            {
                case ResourcePlace.Header:
                    lrlResourcesHead.Text += resource;
                    break;
                case ResourcePlace.Body:
                    lrlResourcesBody.Text += resource;
                    break;
            }
        }
    }
}