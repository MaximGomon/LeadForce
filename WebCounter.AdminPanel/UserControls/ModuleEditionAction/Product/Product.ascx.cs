using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;
using Labitec.UI.Photo.Controls;
using Labitec.UI.Photo;
using System.Configuration;
using System.Drawing;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel.UserControls.ModuleEditionAction.Product
{
    public partial class Product : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();
        protected tbl_Product productData = null;

        protected List<tbl_ProductPhoto> UploadedProductPhoto = new List<tbl_ProductPhoto>();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid ProductId
        {
            get
            {
                object o = ViewState["ProductId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["ProductId"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            Guid SiteId = CurrentUser.Instance.SiteID;
           

            productData = _dataManager.Product.SelectById(SiteId, ProductId);

            if (productData != null)
            {

                txtSKU.Text = productData.SKU;
                txtTitle.Text = productData.Title;
                txtDescription.Text = productData.Description;
                txtPrice.Text = productData.Price == null ? "0,00" : ((decimal)productData.Price).ToString("F");

            }

        }





        protected void lbUpdateFile_OnClick(object sender, EventArgs e)
        {
            productData = _dataManager.Product.SelectById(CurrentUser.Instance.SiteID, ProductId);

            productData = _dataManager.Product.Save(
                          CurrentUser.Instance.SiteID,
                          productData == null ? Guid.Empty : productData.ID,
                          txtTitle.Text,
                          txtSKU.Text,
                          null,
                          null,
                          txtPrice.Text == "" ? null : (decimal?)(decimal.Parse(txtPrice.Text)),
                          null,
                          null,
                          null,
                          null,
                          null,
                          null,
                          null,
                          txtDescription.Text,
                          CurrentUser.Instance.ID,
                          null
                          );
            ((Labitec.UI.BaseWorkspace.Grid)FindControl("gridProducts", Page.Controls)).Rebind();
        }


        public override Control FindControl(string id)
        {
            var ctrl = base.FindControl(id);

            if (ctrl == null)
                ctrl = FindControl(id, Controls);

            return ctrl;
        }


        public static Control FindControl(string id, ControlCollection col)
        {
            foreach (Control c in col)
            {
                Control child = FindControlRecursive(c, id);
                if (child != null)
                    return child;
            }
            return null;
        }


        private static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control rc = FindControlRecursive(c, id);
                if (rc != null)
                    return rc;
            }
            return null;
        }
    }
}