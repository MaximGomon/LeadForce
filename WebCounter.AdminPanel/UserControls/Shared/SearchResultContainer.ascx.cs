using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls.Shared
{
    public partial class SearchResultContainer : System.Web.UI.UserControl
    {
        /// <summary>
        /// Gets or sets the kind of the publication.
        /// </summary>
        /// <value>
        /// The kind of the publication.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public PublicationKind? PublicationKind
        {
            get { return (PublicationKind?) ViewState["PublicationKind"]; }
            set { ViewState["PublicationKind"] = value; }
        }



        /// <summary>
        /// Gets or sets a value indicating whether this instance is select answer.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is select answer; otherwise, <c>false</c>.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool IsSelectAnswer
        {
            get
            {
                if (ViewState["IsSelectAnswer"] == null)
                    ViewState["IsSelectAnswer"] = false;

                return (bool) ViewState["IsSelectAnswer"];
            }
            set { ViewState["IsSelectAnswer"] = value; }
        }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PublicationKind.HasValue)
                hfPublicationKind.Value = ((int) PublicationKind.Value).ToString();            

            if (IsSelectAnswer)            
                plIsSelectAnswer.Visible = true;            
            else            
                plNotSelectAnswer.Visible = true;            
        }
    }
}