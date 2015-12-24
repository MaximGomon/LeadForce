using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations.Shipment;

namespace WebCounter.AdminPanel
{
    public partial class Shipments : LeadForceBasePage
    {
        public Access access;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Отгрузки - LeadForce";

            access = Access.Check();            

            rbAddShipment.NavigateUrl = UrlsData.AP_ShipmentAdd();                        
            gridShipments.SiteID = SiteId;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridShipments control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridShipments_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (access == null)
                    access = Access.Check();

                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var hlTitle = (HyperLink) item.FindControl("hlTitle");
                hlTitle.Text = string.Format("Отгрузка №{0} от {1}", data["tbl_Shipment_Number"], DateTime.Parse(data["tbl_Shipment_CreatedAt"].ToString()).ToString("dd/MM/yyyy"));
                ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = hlTitle.NavigateUrl = UrlsData.AP_ShipmentEdit(Guid.Parse(data["tbl_Shipment_ID"].ToString()));
                ((Literal)item.FindControl("lrlShipmentAmount")).Text = decimal.Parse(data["tbl_Shipment_ShipmentAmount"].ToString()).ToString("F");
                ((Literal) item.FindControl("lrlNote")).Text = data["tbl_Shipment_Note"].ToString();
                ((Literal)item.FindControl("lrlShipmentStatus")).Text = EnumHelper.GetEnumDescription((ShipmentStatus)int.Parse(data["tbl_Shipment_ShipmentStatusID"].ToString()));

                if (!string.IsNullOrEmpty(data["tbl_Company_Name"].ToString()))
                    ((Literal)item.FindControl("lrlCompany")).Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Company(Guid.Parse(data["tbl_Company_ID"].ToString())), data["tbl_Company_Name"]);

                if (!string.IsNullOrEmpty(data["tbl_CompanyLegalAccount_Title"].ToString()))
                    ((Literal)item.FindControl("lrlCompanyLegalAccount")).Text = data["tbl_CompanyLegalAccount_Title"].ToString();

                var lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                lbDelete.CommandArgument = data["ID"].ToString();
                lbDelete.Command += lbDelete_OnCommand;

                lbDelete.Visible = access.Delete;

                var shipment = DataManager.Shipment.SelectById(Guid.Parse(data["tbl_Shipment_ID"].ToString()));
                if (shipment.tbl_Invoice.Any())
                {
                    ((Literal)item.FindControl("lrlInvoices")).Text =
                        string.Format("<div class=\"span-url\">Счета: {0}</div>",
                                      string.Join(", ", shipment.tbl_Invoice.Select(
                                          o =>
                                          string.Format("<a href=\"{0}\">Счет №{1} от {2}</a>", UrlsData.AP_InvoiceEdit(o.ID), o.Number, o.CreatedAt.ToString("dd.MM.yyyy")))));
                }
            }
        }


        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {            
            DataManager.Shipment.DeleteById(SiteId, Guid.Parse(e.CommandArgument.ToString()));
            gridShipments.Rebind();
        }
    }
}