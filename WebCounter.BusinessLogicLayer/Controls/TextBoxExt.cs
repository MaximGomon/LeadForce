using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Controls
{
    public class TextBoxExt : TextBox
    {
        #region Members
        private RequiredFieldValidator requiredFieldValidator;
        private CompareValidator compareValidator;
        #endregion
        


        #region Propeties
        public string PlaceHolderText { get; set; }
        public bool Required { get; set; }

        private string _requiredErrorMessage = "*";
        public string RequiredErrorMessage
        {
            get { return _requiredErrorMessage; }
            set { _requiredErrorMessage = value; }
        }

        private string _placeHolderColor = "#8A8A8A";
        public string PlaceHolderColor
        {
            get { return _placeHolderColor; }
            set { _placeHolderColor = value; }
        }
        #endregion        


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Required)
            {
                requiredFieldValidator = new RequiredFieldValidator();
                requiredFieldValidator.ControlToValidate = this.ID;
                requiredFieldValidator.ErrorMessage = RequiredErrorMessage;
                requiredFieldValidator.ValidationGroup = this.ValidationGroup;
                requiredFieldValidator.EnableClientScript = true;
                //requiredFieldValidator.Display = ValidatorDisplay.Dynamic;
                requiredFieldValidator.Text = "*";
                Controls.Add(requiredFieldValidator);
            }

            if (Required && !string.IsNullOrEmpty(PlaceHolderText))
            {
                compareValidator = new CompareValidator();
                compareValidator.ControlToValidate = this.ID;
                compareValidator.ValueToCompare = PlaceHolderText;
                compareValidator.Operator = ValidationCompareOperator.NotEqual;
                compareValidator.ErrorMessage = RequiredErrorMessage;
                compareValidator.ValidationGroup = this.ValidationGroup;
                compareValidator.EnableClientScript = true;
                //compareValidator.Display = ValidatorDisplay.Dynamic;
                compareValidator.Text = "*";
                Controls.Add(compareValidator);
            }
        }


        protected override void OnPreRender(EventArgs e)
        {
            if (!string.IsNullOrEmpty(PlaceHolderText) && (string.IsNullOrEmpty(this.Text) || PlaceHolderText == this.Text))
            {
                this.Text = PlaceHolderText;
                this.CssClass = this.CssClass + " placeholder ";
            }
            else
                this.CssClass = this.CssClass.Replace("placeholder", "");

            base.OnPreRender(e);
        }



        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);

            if (Required)
                requiredFieldValidator.RenderControl(writer);

            if (Required && !string.IsNullOrEmpty(PlaceHolderText))
                compareValidator.RenderControl(writer);

            var css = new StringBuilder();
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
            writer.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
            writer.RenderBeginTag(HtmlTextWriterTag.Style);
            css.AppendFormat("#{0}.placeholder {{ color: {1}; }}", this.ClientID, PlaceHolderColor);
            writer.Write(css.ToString());
            writer.RenderEndTag();
            writer.WriteLine();

            var sm = ScriptManager.GetCurrent(Page);
            var js = new StringBuilder();
            js.AppendFormat("if (document.getElementById('{0}').addEventListener) {{ document.getElementById('{0}').addEventListener('focus', function () {{ if (this.value == '{1}') {{ this.value = ''; this.className = this.className.replace(/placeholder/g, ''); }} }}, false); }} else if (document.getElementById('{0}').attachEvent) {{ document.getElementById('{0}').attachEvent('onfocus', function () {{ if (document.getElementById('{0}').value == '{1}') {{ document.getElementById('{0}').value = ''; document.getElementById('{0}').className = document.getElementById('{0}').className.replace(/placeholder/g, ''); }} }}); }}", this.ClientID, PlaceHolderText);
            js.AppendFormat("if (document.getElementById('{0}').addEventListener) {{ document.getElementById('{0}').addEventListener('blur', function () {{ if (this.value == '') {{ this.value = '{1}'; this.className = this.className + ' placeholder '; }} }}, false); }} else if (document.getElementById('{0}').attachEvent) {{ document.getElementById('{0}').attachEvent('onblur', function () {{ if (document.getElementById('{0}').value == '') {{ document.getElementById('{0}').value = '{1}'; document.getElementById('{0}').className = document.getElementById('{0}').className + ' placeholder '; }} }}); }}", this.ClientID, PlaceHolderText);

            if (sm != null && sm.IsInAsyncPostBack)
            {
                if (!string.IsNullOrEmpty(PlaceHolderText))
                    ScriptManager.RegisterStartupScript(Page, typeof(TextBoxExt), UniqueID, js.ToString(), true);
            }
            else
            {
                if (!string.IsNullOrEmpty(PlaceHolderText))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                    writer.RenderBeginTag(HtmlTextWriterTag.Script);
                    writer.Write(js.ToString());
                    writer.RenderEndTag();
                    writer.WriteLine();
                }
            }
        }
    }
}