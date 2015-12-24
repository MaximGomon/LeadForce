using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Portal.Main.TaskModule.UserControls
{
    public partial class RefusedComment : System.Web.UI.UserControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid TaskId
        {
            get
            {
                var o = ViewState["TaskId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["TaskId"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Handles the Click event of the lnkSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var dataManager = new DataManager();

            var refusedComment = dataManager.TaskPersonalComment.SelectByTaskContactId(TaskId, (Guid)CurrentUser.Instance.ContactID) ?? new tbl_TaskPersonalComment();
            refusedComment.RefusedComment = txtRefusedComment.Text;

            if (refusedComment.ID == Guid.Empty)
            {
                refusedComment.TaskID = TaskId;
                refusedComment.ContactID = (Guid) CurrentUser.Instance.ContactID;
                dataManager.TaskPersonalComment.Add(refusedComment);
            }
            else
                dataManager.TaskPersonalComment.Update(refusedComment);

            if (!Page.ClientScript.IsStartupScriptRegistered("refusedCommentSave"))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "refusedCommentSave", "CloseRefusedCommentRadWindow();", true);
        }
    }
}