using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class PublicationCommentDetail : System.Web.UI.UserControl
    {

        public string PublicationCommentID
        {
            get
            {
                if (ViewState["PublicationCommentID"] == null)
                {
                    return "";
                }
                return (string)ViewState["PublicationCommentID"];
            }
            set
            {

                ViewState["PublicationCommentID"] = value;

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}