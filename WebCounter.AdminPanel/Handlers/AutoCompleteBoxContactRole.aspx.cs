using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.Handlers
{
    public partial class AutoCompleteBoxContactRole : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        [WebMethod]
        public static AutoCompleteBoxData GetData(object context)
        {
           var  dataManager = new DataManager();
            var siteId = CurrentUser.Instance.SiteID;

            var searchString = ((Dictionary<string, object>)context)["Text"].ToString();

            var roles = dataManager.ContactRole.SelectAll(siteId).Where(a => a.Title.Contains(searchString));
            var contacts =
                dataManager.Contact.SelectAll(siteId).Where(
                    a =>
                    !string.IsNullOrEmpty(a.Email) &&
                    (a.UserFullName.Contains(searchString) || a.Email.Contains(searchString)));

            var result = new List<AutoCompleteBoxItemData>();

            foreach (var role in roles)
            {
                var node = new AutoCompleteBoxItemData { Text = role.Title, Value = "Role|" + role.ID.ToString() };
                result.Add(node);
            }

            foreach (var contact in contacts)
            {
                var node = new AutoCompleteBoxItemData { Value = "Contact|" + contact.ID.ToString() };
                node.Text = !string.IsNullOrEmpty(contact.UserFullName)
                            ? string.Format("{0} &lt;{1}&gt;", contact.UserFullName, contact.Email)
                            : string.Format("&lt;{0}&gt;", contact.Email);
                result.Add(node);
            }

            var res = new AutoCompleteBoxData();
            res.Items = result.ToArray();

            return res;
        }
    }
}