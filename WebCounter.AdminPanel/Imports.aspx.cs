using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class Imports : LeadForceBasePage
    {
        private DataManager _dataManager = new DataManager();
        public Access access;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Импорт - LeadForce";

            access = Access.Check();

            gridImports.AddNavigateUrl = UrlsData.AP_ImportAdd();
//            gridImports.Actions.Add(new GridAction { Text = "Редактировать", NavigateUrl = string.Format("~/{0}/Imports/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridImports.SiteID = SiteId;
        }

        protected void gridImports_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (access == null)
                    access = Access.Check();

                var item = (GridDataItem) e.Item;
                var data = (DataRowView) e.Item.DataItem;

                ((HyperLink)item.FindControl("spanName")).Text = data["tbl_Import_Name"].ToString();
                ((HyperLink)item.FindControl("spanName")).NavigateUrl = "#";
                ((Literal)item.FindControl("lType")).Text = EnumHelper.GetEnumDescription((ImportType)int.Parse(data["tbl_Import_Type"].ToString()));
                ((Literal)item.FindControl("lImportTable")).Text = EnumHelper.GetEnumDescription((ImportTable)int.Parse(data["tbl_Import_ImportTable"].ToString()));

                var lbEdit = (LinkButton)e.Item.FindControl("lbEdit");
                lbEdit.CommandArgument = data["ID"].ToString();
                lbEdit.Command += new CommandEventHandler(lbEdit_OnCommand);


                var lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                lbDelete.CommandArgument = data["tbl_Import_ID"].ToString();
                lbDelete.Command += new CommandEventHandler(lbDelete_OnCommand);
                lbDelete.Visible = access.Delete;
            }
        }

        protected void lbEdit_OnCommand(object sender, CommandEventArgs e)
        {
            Response.Redirect(UrlsData.AP_ImportEdit(Guid.Parse(e.CommandArgument.ToString())));
        }

        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            _dataManager.Import.DeleteByID(CurrentUser.Instance.SiteID,Guid.Parse(e.CommandArgument.ToString()));
            gridImports.Rebind();
        }

        protected void lbAdd_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(UrlsData.AP_ImportAdd());
        }


    }
}