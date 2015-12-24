using System;
using System.Collections.Generic;
using System.Data;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using System.Web.UI.WebControls;
using System.Linq;
namespace WebCounter.AdminPanel
{
    public partial class MassMails : LeadForceBasePage
    {
        private DataManager _dataManager = new DataManager();
        public Access access;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Рассылки - LeadForce";

            access = Access.Check();

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(Plan, gridMassMails);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(Complete, gridMassMails);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(Cancel, gridMassMails);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(All, gridMassMails);


            //gridMassMails.AddNavigateUrl = UrlsData.AP_MassMailAdd();
//            gridMassMails.Actions.Add(new GridAction { Text = "Редактировать", NavigateUrl = string.Format("~/{0}/MassMails/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridMassMails.Where.Add(new GridWhere() { CustomQuery = "(MassMailStatusID IS NOT NULL AND MassMailStatusID = 0)" });
            gridMassMails.DefaultOrderBy = "MailDate";
            gridMassMails.DefaultOrderSort = GridOrderSort.ASC;
            gridMassMails.SiteID = SiteId;
            var showTagsAndFilters = ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "ShowTagsAndFilters");
            if (showTagsAndFilters == null && !((LeadForceBasePage)Page).IsDefaultEdition)
            {
                RadPanelBar1.Visible = false;
            }            

        }

        protected void filters_OnCheckedChanged(object sender, EventArgs e)
        {
            gridMassMails.Where.Clear();
            gridMassMails.DefaultOrderBy = "MailDate";
            gridMassMails.DefaultOrderSort = GridOrderSort.DESC;
            var radioButton = (RadioButton)sender;
            switch (radioButton.ID)
            {
                case "Plan":
                    gridMassMails.Where.Add(new GridWhere() { CustomQuery = "(MassMailStatusID IS NOT NULL AND MassMailStatusID = 0)" });
                    gridMassMails.DefaultOrderBy = "MailDate";
                    gridMassMails.DefaultOrderSort = GridOrderSort.ASC;
                    break;
                case "Complete":
                    gridMassMails.Where.Add(new GridWhere() { CustomQuery = "(MassMailStatusID IS NOT NULL AND MassMailStatusID = 1)" });
                    break;
                case "Cancel":
                    gridMassMails.Where.Add(new GridWhere() { CustomQuery = "(MassMailStatusID IS NOT NULL AND MassMailStatusID = 2)" });
                    break;
            }

            gridMassMails.Rebind();
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridMassMails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void gridMassMails_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (access == null)
                    access = Access.Check();

                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var litStatus = (Literal)item.FindControl("litStatus");
                litStatus.Text = EnumHelper.GetEnumDescription((MassMailStatus)data["tbl_MassMail_MassMailStatusID"].ToString().ToInt());

                ((HyperLink)item.FindControl("spanName")).Text = data["tbl_MassMail_Name"].ToString();
                ((HyperLink)item.FindControl("spanName")).NavigateUrl = UrlsData.AP_MassMail(Guid.Parse(data["ID"].ToString()));
                ((Literal)item.FindControl("lMailDate")).Text = data["tbl_MassMail_MailDate"].ToString();

                var lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                lbEdit.CommandArgument = data["ID"].ToString();
                lbEdit.Command += new CommandEventHandler(lbEdit_OnCommand);


                var lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                lbDelete.CommandArgument = data["ID"].ToString();
                lbDelete.Command += new CommandEventHandler(lbDelete_OnCommand);
                lbDelete.Visible = access.Delete;
            }
        }

        protected void lbEdit_OnCommand(object sender, CommandEventArgs e)
        {
            Response.Redirect(UrlsData.AP_MassMail(Guid.Parse(e.CommandArgument.ToString())));
        }

        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            _dataManager.MassMail.DeleteByID(CurrentUser.Instance.SiteID, Guid.Parse(e.CommandArgument.ToString()));
            gridMassMails.Rebind();
        }

        protected void lbAdd_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(UrlsData.AP_MassMailAdd());
        }

    }
}