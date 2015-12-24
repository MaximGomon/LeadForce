using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Drawing;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class CssEditor : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Css
        {
            get
            {
                object o = ViewState["Css"];
                return (o == null ? string.Empty : (string)o);
            }
            set
            {
                ViewState["Css"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            rcbFontFamily.Items.Clear();
            rcbFontSize.Items.Clear();

            var fontFamilys = new[] { "По умолчанию", "Verdana", "Arial", "Courier", "Times New Roman" };
            foreach (var fontFamily in fontFamilys)
            {
                if (fontFamily == "По умолчанию")
                    rcbFontFamily.Items.Add(new RadComboBoxItem { Text = fontFamily, Value = "" });
                else
                    rcbFontFamily.Items.Add(new RadComboBoxItem { Text = fontFamily, Value = fontFamily });
            }


            var fontSizes = new[] { "По умолчанию", "8px", "10px", "12px", "14px" };
            foreach (var fontSize in fontSizes)
            {
                if (fontSize == "По умолчанию")
                    rcbFontSize.Items.Add(new RadComboBoxItem { Text = fontSize, Value = "" });
                else
                    rcbFontSize.Items.Add(new RadComboBoxItem { Text = fontSize, Value = fontSize });
            }


            DecodeCss(Css);
        }



        /// <summary>
        /// Encodes the CSS.
        /// </summary>
        /// <returns></returns>
        protected string EncodeCss()
        {
            var css = new StringBuilder();

            if (!string.IsNullOrEmpty(rcbFontFamily.SelectedValue))
                css.AppendFormat("font-family:{0};", rcbFontFamily.SelectedValue);

            if (!string.IsNullOrEmpty(rcbFontSize.SelectedValue))
                css.AppendFormat("font-size:{0};", rcbFontSize.SelectedValue);

            if (cbBold.Checked)
                css.Append("font-weight:bold;");

            if (!rcpColor.SelectedColor.IsEmpty)
                css.AppendFormat("color:{0};", ColorTranslator.ToHtml(rcpColor.SelectedColor));

            if (!rcpBackgroundColor.SelectedColor.IsEmpty)
                css.AppendFormat("background-color:{0};", ColorTranslator.ToHtml(rcpBackgroundColor.SelectedColor));

            if (css.ToString() == "")
                return null;

            return css.ToString();
        }



        /// <summary>
        /// Decodes the CSS.
        /// </summary>
        /// <param name="css">The CSS.</param>
        protected void DecodeCss(string css)
        {
            var attributes = css.Split(new[] {';'});
            foreach (var attribute in attributes)
            {
                var attr = attribute.Split(new[] {':'});
                switch (attr[0])
                {
                    case "font-family":
                        rcbFontFamily.FindItemByValue(attr[1]).Selected = true;
                        break;
                    case "font-size":
                        rcbFontSize.FindItemByValue(attr[1]).Selected = true;
                        break;
                    case "font-weight":
                        cbBold.Checked = true;
                        break;
                    case "color":
                        rcpColor.SelectedColor = ColorTranslator.FromHtml(attr[1]);
                        break;
                    case "background-color":
                        rcpBackgroundColor.SelectedColor = ColorTranslator.FromHtml(attr[1]);
                        break;
                }
            }
        }


        public string GetCss()
        {
            return EncodeCss();
        }


        /*public void SetCSS(string css)
        {
            if (!string.IsNullOrEmpty(css))
                DecodeCSS(css);
        }*/
    }
}