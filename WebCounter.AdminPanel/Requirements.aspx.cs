using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class Requirements : LeadForceBasePage
    {
        protected const string RequirementWidgetFilter = "WebCounter.AdminPanel.RequirementWidgetFilter";

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Требования - LeadForce";

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(chxByResponsible, gridRequirements);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(chxCurrentRequirements, gridRequirements);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucResponsible, gridRequirements);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(chxToPay, gridRequirements);

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(chxByResponsible, rtlRequirements);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(chxCurrentRequirements, rtlRequirements);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucResponsible, rtlRequirements);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(chxToPay, rtlRequirements);

            gridRequirements.AddNavigateUrl = UrlsData.AP_RequirementAdd();
            gridRequirements.Actions.Add(new GridAction { Text = "Карточка требования", NavigateUrl = string.Format("~/{0}/Requirements/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridRequirements.SiteID = SiteId;

            dcbCompany.SiteID = SiteId;            
            
            if (!Page.IsPostBack)
            {
                InitFilter();
                rdpStartDate.SelectedDate = DateTimeHelper.GetFirstDayOfWeek(DateTime.Now);
                rdpEndDate.SelectedDate = DateTime.Now;
            }
        }



        /// <summary>
        /// Inits the filter.
        /// </summary>
        private void InitFilter()
        {
            var userSettings = DataManager.UserSettings.SelectByClassName(CurrentUser.Instance.ID, RequirementWidgetFilter);
            if (userSettings != null && !string.IsNullOrEmpty(userSettings.UserSettings))
            {
                var settings = new RequirementFilterSettings();
                settings.Deserialize(userSettings.UserSettings);

                if (settings.ToPay)
                    gridRequirements.Where.Add(new GridWhere { CustomQuery = "(tbl_Requirement.Quantity > 0 AND tbl_Requirement.InvoiceID IS NULL)" });

                if (settings.CurrentRequirements)
                    gridRequirements.Where.Add(new GridWhere { CustomQuery = string.Format("(tbl_RequirementStatus.IsLast = {0})", settings.CurrentRequirements ? 0 : 1) });

                if (settings.ByResponsible && settings.ResponsibleId.HasValue)
                    gridRequirements.Where.Add(new GridWhere
                    {
                        CustomQuery =
                            string.Format("(tbl_Requirement.ResponsibleID = '{0}')",
                                          settings.ResponsibleId.ToString())
                    });

                chxToPay.Checked = settings.ToPay;
                chxByResponsible.Checked = settings.ByResponsible;
                chxCurrentRequirements.Checked = settings.CurrentRequirements;
                ucResponsible.SelectedValue = settings.ResponsibleId;
            }
            else
            {
                ucResponsible.SelectedValue = CurrentUser.Instance.ContactID;
                gridRequirements.Where.Add(new GridWhere { CustomQuery = "tbl_RequirementStatus.IsLast = 0" });
            }                
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            BindTreeView();
        }


        /// <summary>
        /// Binds the tree view.
        /// </summary>
        protected void BindTreeView()
        {
            var requirements = gridRequirements.GetDataFromGrid();

            if (requirements != null)
            {                
                rtlRequirements.DataSource = requirements;                
                rtlRequirements.DataBind();
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridRequirements control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridRequirements_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var lrlCompanyName = (Literal)item.FindControl("lrlCompanyName");
                var lrlUserFullName = (Literal)item.FindControl("lrlUserFullName");
                
                if (!string.IsNullOrEmpty(data["tbl_Company_Name"].ToString()))
                    lrlCompanyName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Company(Guid.Parse(data["tbl_Company_ID"].ToString())), data["tbl_Company_Name"]);

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lrlUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), data["tbl_Contact_UserFullName"]);
            }
        }



        /// <summary>
        /// Handles the OnCheckedChanged event of the chxFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void chxFilter_OnCheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ucResponsible control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucResponsible_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ApplyFilter();
        }



        /// <summary>
        /// Applies the filter.
        /// </summary>
        private void ApplyFilter()
        {
            gridRequirements.Where.Clear();

            if (chxToPay.Checked)
                gridRequirements.Where.Add(new GridWhere { CustomQuery = "(tbl_Requirement.Quantity > 0 AND tbl_Requirement.InvoiceID IS NULL)" });

            if (chxCurrentRequirements.Checked)
                gridRequirements.Where.Add(new GridWhere { CustomQuery = "(tbl_RequirementStatus.IsLast = 0)" });

            if (chxByResponsible.Checked && ucResponsible.SelectedValue.HasValue)
                gridRequirements.Where.Add(new GridWhere
                                               {
                                                   CustomQuery =
                                                       string.Format("(tbl_Requirement.ResponsibleID = '{0}')",
                                                                     ucResponsible.SelectedValue.ToString())
                                               });

            var userSettings = DataManager.UserSettings.SelectByClassName(CurrentUser.Instance.ID,
                                                           RequirementWidgetFilter) ?? new tbl_UserSettings();
            var filter = new RequirementFilterSettings
                             {
                                 CurrentRequirements = chxCurrentRequirements.Checked,
                                 ResponsibleId = ucResponsible.SelectedValue,
                                 ByResponsible = chxByResponsible.Checked,
                                 ToPay = chxToPay.Checked
                             };

            userSettings.UserID = CurrentUser.Instance.ID;
            userSettings.ClassName = RequirementWidgetFilter;
            userSettings.UserSettings = filter.Serialize();
            DataManager.UserSettings.Save(userSettings);

            gridRequirements.Rebind();            
        }



        /// <summary>
        /// Handles the OnItemDrop event of the rtlRequirements control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.TreeListItemDragDropEventArgs"/> instance containing the event data.</param>
        protected void rtlRequirements_OnItemDrop(object sender, TreeListItemDragDropEventArgs e)
        {
            var destinationItem = e.DestinationDataItem as TreeListDataItem;
            var draggedItem = e.DraggedItems[0] as TreeListDataItem;
            var draggedItemId = Guid.Parse(draggedItem.GetDataKeyValue("tbl_Requirement_ID").ToString());
            Guid? destinationItemId = null;

            if (destinationItem != null)            
                destinationItemId = Guid.Parse(destinationItem.GetDataKeyValue("tbl_Requirement_ID").ToString());

            var requirement = DataManager.Requirement.SelectById(SiteId, draggedItemId);
            requirement.ParentID = destinationItemId;
            DataManager.Requirement.Update(requirement);
            

            gridRequirements.Rebind();            
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSendReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnGenerateReport_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("{0}?companyId={1}&startDate={2}&endDate={3}", ResolveUrl("~/Reports/RequiremetReports.aspx"), dcbCompany.SelectedIdNullable,
                                            rdpStartDate.SelectedDate, rdpEndDate.SelectedDate));

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseSendReportRadWindow"))
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof (Page), "CloseSendReportRadWindow", "CloseSendReportRadWindow();", true);
        }
    }

    public class RequirementFilterSettings
    {
        public bool CurrentRequirements { get; set; }
        public Guid? ResponsibleId { get; set; }
        public bool ByResponsible { get; set; }
        public bool ToPay { get; set; }


        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            return string.Format("{0}${1}${2}${3}", this.CurrentRequirements, this.ResponsibleId, this.ByResponsible, this.ToPay);
        }



        /// <summary>
        /// Deserializes the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public void Deserialize(string filter)
        {
            var values = filter.Split('$');
            if (values.Length >= 1 && !string.IsNullOrEmpty(values[0]))
                this.CurrentRequirements = bool.Parse(values[0]);
            if (values.Length >= 2 && !string.IsNullOrEmpty(values[1]))
                this.ResponsibleId = string.IsNullOrEmpty(values[1]) ? null : (Guid?) Guid.Parse(values[1]);
            if (values.Length >= 3 && !string.IsNullOrEmpty(values[2]))
                this.ByResponsible = bool.Parse(values[2]);
            if (values.Length >= 4 && !string.IsNullOrEmpty(values[3]))
                this.ToPay = bool.Parse(values[3]);
        }
    }
}