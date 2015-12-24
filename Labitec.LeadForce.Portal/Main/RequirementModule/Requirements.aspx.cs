using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace Labitec.LeadForce.Portal.Main.RequirementModule
{
    public partial class Requirements : LeadForcePortalBasePage
    {        
        protected const string OpenQuery = @"
(RequirementStatusID IN 
(SELECT ID 
FROM tbl_RequirementStatus
WHERE IsLast <> 1))";

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(All, gridRequirements);            
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(Open, gridRequirements);

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(chxByResponsible, gridRequirements, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucResponsible, gridRequirements, null, UpdatePanelRenderMode.Inline);
            
            var accessCheck = Access.Check(TblUser, "Requirements");
            if (!accessCheck.Read)
                Response.Redirect(UrlsData.LFP_Home(PortalSettingsId));

            Title = "Требования";

            gridRequirements.ModuleName = "Requirements";

            gridRequirements.SiteID = SiteId;
            gridRequirements.Actions.Add(new GridAction { Text = "Карточка требования", NavigateUrl = "~/" + PortalSettingsId + "/Main/Requirements/Edit/{0}", ImageUrl = "~/App_Themes/Default/images/icoView.png" });

            ucResponsible.SelectedValue = CurrentUser.Instance.ContactID;

            if (!Page.IsPostBack)
            {
                gridRequirements.Where.Add(new GridWhere() { CustomQuery = OpenQuery });
                AddSystemWhere();
            }

        }



        /// <summary>
        /// Handles the OnCheckedChanged event of the filters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void filters_OnCheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();            
        }



        /// <summary>
        /// Adds the system where.
        /// </summary>
        private void AddSystemWhere()
        {            
            if (CurrentUser.Instance.CompanyID.HasValue)
            {
                gridRequirements.Where.Add(new GridWhere()
                                                {
                                                    CustomQuery =
                                                        string.Format(
                                                            "(tbl_Requirement.CompanyID = '{0}' OR ResponsibleID = '{1}' OR ContactID = '{1}' OR tbl_Requirement.CompanyID IN (SELECT tbl_ServiceLevelClient.ClientID FROM tbl_ServiceLevelContact LEFT JOIN tbl_ServiceLevelClient ON tbl_ServiceLevelContact.ServiceLevelClientID = tbl_ServiceLevelClient.ID WHERE ContactID = '{1}'))",
                                                            CurrentUser.Instance.CompanyID.Value,
                                                            CurrentUser.Instance.ContactID.Value)
                                                });
            }
            else
            {
                gridRequirements.Where.Add(new GridWhere()
                {
                    CustomQuery =
                        string.Format(
                            "(ResponsibleID = '{0}' OR ContactID = '{0}' OR tbl_Requirement.CompanyID IN (SELECT tbl_ServiceLevelClient.ClientID FROM tbl_ServiceLevelContact LEFT JOIN tbl_ServiceLevelClient ON tbl_ServiceLevelContact.ServiceLevelClientID = tbl_ServiceLevelClient.ID WHERE ContactID = '{0}'))",
                            CurrentUser.Instance.ContactID.Value)
                });
            }            
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
        /// Handles the OnCheckedChanged event of the chxByResponsible control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void chxByResponsible_OnCheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }



        /// <summary>
        /// Applies the filter.
        /// </summary>
        private void ApplyFilter()
        {
            gridRequirements.Where.Clear();
            
            if (Open.Checked)
                gridRequirements.Where.Add(new GridWhere() { CustomQuery = OpenQuery });

            if (chxByResponsible.Checked && ucResponsible.SelectedValue.HasValue)
                gridRequirements.Where.Add(new GridWhere
                {
                    CustomQuery =
                        string.Format("(tbl_Requirement.ResponsibleID = '{0}')",
                                      ucResponsible.SelectedValue.ToString())
                });

            AddSystemWhere();

            gridRequirements.Rebind();
        }
    }
}