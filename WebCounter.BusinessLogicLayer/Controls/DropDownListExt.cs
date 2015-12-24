using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCounter.BusinessLogicLayer.Controls
{
    public class DropDownListExt : DropDownList
    {
        #region Members
        //private RequiredFieldValidator requiredFieldValidator;
        #endregion



        #region Propeties
        public string PlaceHolderText { get; set; }
        //public bool Required { get; set; }

        /*private string _requiredErrorMessage = "*";
        public string RequiredErrorMessage
        {
            get { return _requiredErrorMessage; }
            set { _requiredErrorMessage = value; }
        }*/

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

            /*if (Required)
            {
                requiredFieldValidator = new RequiredFieldValidator();
                requiredFieldValidator.ControlToValidate = this.ID;
                requiredFieldValidator.ErrorMessage = RequiredErrorMessage;
                requiredFieldValidator.ValidationGroup = this.ValidationGroup;
                requiredFieldValidator.EnableClientScript = true;
                //requiredFieldValidator.Display = ValidatorDisplay.Dynamic;
                requiredFieldValidator.Text = "*";
                Controls.Add(requiredFieldValidator);
            }*/

            if (!string.IsNullOrEmpty(PlaceHolderText) && this.Items.FindByText(PlaceHolderText) == null)
                this.Items.Insert(0, new ListItem(PlaceHolderText, ""));
        }



        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!string.IsNullOrEmpty(PlaceHolderText) && this.Items.FindByText(PlaceHolderText) != null && this.Items.FindByText(PlaceHolderText).Selected)
                this.CssClass = this.CssClass + " placeholder ";
            else
                this.CssClass = this.CssClass.Replace("placeholder", "");
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);

            /*if (Required)
                requiredFieldValidator.RenderControl(writer);*/

            var css = new StringBuilder();
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
            writer.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
            writer.RenderBeginTag(HtmlTextWriterTag.Style);
            css.AppendFormat("#{0}.placeholder {{ color: {1}; }} select.placeholder option {{ color: #222; }}", this.ClientID, PlaceHolderColor);
            writer.Write(css.ToString());
            writer.RenderEndTag();
            writer.WriteLine();

            var sm = ScriptManager.GetCurrent(Page);
            var js = new StringBuilder();
            js.AppendFormat("if (document.getElementById('{0}').addEventListener) {{ document.getElementById('{0}').addEventListener('change', function () {{ if (document.getElementById('{0}').options[document.getElementById('{0}').selectedIndex].text == '{1}') this.className = this.className + ' placeholder '; else this.className = this.className.replace(/placeholder/g, ''); }}, false); }} else if (document.getElementById('{0}').attachEvent) {{ document.getElementById('{0}').attachEvent('onchange', function () {{ if (document.getElementById('{0}').options[document.getElementById('{0}').selectedIndex].text == '{1}') document.getElementById('{0}').className = this.className + ' placeholder '; else document.getElementById('{0}').className = document.getElementById('{0}').className.replace(/placeholder/g, ''); }}); }}", this.ClientID, PlaceHolderText);
            if (sm != null && sm.IsInAsyncPostBack)
            {
                if (!string.IsNullOrEmpty(PlaceHolderText))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(TextBoxExt), UniqueID, js.ToString(), true);
                    ScriptManager.RegisterStartupScript(Page, typeof(TextBoxExt), UniqueID + "_1", string.Format("if (document.getElementById('{0}').options[document.getElementById('{0}').selectedIndex].text == '{1}') document.getElementById('{0}').className = document.getElementById('{0}').className + ' placeholder '", this.ClientID, PlaceHolderText), true);
                }
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

                    if (this.Items.FindByText(PlaceHolderText).Selected)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                        writer.RenderBeginTag(HtmlTextWriterTag.Script);
                        writer.Write(string.Format("if (document.getElementById('{0}').addEventListener) {{ window.addEventListener('load', function() {{ document.getElementById('{0}').className = document.getElementById('{0}').className + ' placeholder '; }}, false); }} else if (document.getElementById('{0}').attachEvent) {{ window.attachEvent('onload', function() {{ document.getElementById('{0}').className = document.getElementById('{0}').className + ' placeholder '; }}); }}", this.ClientID));
                        writer.RenderEndTag();
                        writer.WriteLine();
                    }
                }
            }
        }

    }
}
