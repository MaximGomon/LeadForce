using System;
using System.Web;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Portal.Shared.UserControls
{
    public partial class MainMenu : System.Web.UI.UserControl
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshMenu();
        }



        /// <summary>
        /// Builds the menu.
        /// </summary>
        public void RefreshMenu(Guid? userId = null)
        {
            rmMainMenu.Items.Clear();

            var menuItem = new RadMenuItem("Главная", UrlsData.LFP_Home(((LeadForcePortalBasePage)Page).PortalSettingsId)) { Value = "main" };
            rmMainMenu.Items.Add(menuItem);

            menuItem = new RadMenuItem("Обсуждения", UrlsData.LFP_Discussions(((LeadForcePortalBasePage)Page).PortalSettingsId)) { Value = "discussions" };
            rmMainMenu.Items.Add(menuItem);

            menuItem = new RadMenuItem("База знаний", UrlsData.LFP_KnowledgeBase(((LeadForcePortalBasePage)Page).PortalSettingsId)) { Value = "knowledge" };
            rmMainMenu.Items.Add(menuItem);

            if (HttpContext.Current.User.Identity.IsAuthenticated || userId.HasValue)
            {
                var dataManager = new DataManager();
                tbl_User user = null;
                if (CurrentUser.Instance != null)
                    user = dataManager.User.SelectById(CurrentUser.Instance.ID);
                else if (userId.HasValue)
                    user = dataManager.User.SelectById((Guid)userId);

                if (user != null)
                {
                    var accessCheck = Access.Check(user, "Requests");
                    if (accessCheck.Read)
                    {
                        menuItem = new RadMenuItem("Запросы", UrlsData.LFP_Requests(((LeadForcePortalBasePage) Page).PortalSettingsId)) {Value = "requests"};
                        rmMainMenu.Items.Add(menuItem);
                    }
                    accessCheck = Access.Check(user, "Requirements");
                    if (accessCheck.Read)
                    {
                        menuItem = new RadMenuItem("Требования", UrlsData.LFP_Requirements(((LeadForcePortalBasePage)Page).PortalSettingsId)) { Value = "requirements" };
                        rmMainMenu.Items.Add(menuItem);
                    }
                    accessCheck = Access.Check(user, "Tasks");
                    if (accessCheck.Read)
                    {
                        menuItem = new RadMenuItem("Задачи", UrlsData.LFP_Tasks(((LeadForcePortalBasePage) Page).PortalSettingsId)){Value = "tasks"};
                        rmMainMenu.Items.Add(menuItem);
                    }
                    accessCheck = Access.Check(user, "Invoices");
                    if (accessCheck.Read)
                    {
                        menuItem = new RadMenuItem("Счета", UrlsData.LFP_Invoices(((LeadForcePortalBasePage)Page).PortalSettingsId)) { Value = "invoices" };
                        rmMainMenu.Items.Add(menuItem);
                    }
                }
            }

            var currentItem = rmMainMenu.FindItemByUrl(Request.Url.PathAndQuery);

            if (currentItem != null)
                currentItem.Selected = true;

            if (currentItem == null)
            {
                SelectItem(ref currentItem, "knowledge");
                SelectItem(ref currentItem, "requests");
                SelectItem(ref currentItem, "requirements");
                SelectItem(ref currentItem, "tasks");
                SelectItem(ref currentItem, "discussions");
                SelectItem(ref currentItem, "invoices");
            }

            if (currentItem == null)
                rmMainMenu.Items[0].Selected = true;
        }


        /// <summary>
        /// Selects the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="value">The value.</param>
        protected void SelectItem(ref RadMenuItem item, string value)
        {
            if (Request.Url.PathAndQuery.ToLower().Contains(value))
            {
                item = rmMainMenu.FindItemByValue(value);
                if (item != null)
                    item.Selected = true;
            }
        }
    }
}