using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Task;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Order
{
    public partial class OrderProductsComboBox : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid OrderId
        {
            get
            {
                object o = ViewState["OrderId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["OrderId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? ProductId
        {
            get { return (Guid?)ViewState["ProductId"]; }
            set { ViewState["ProductId"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Unit Width
        {
            get { return rcbOrderProducts.Width; }
            set { rcbOrderProducts.Width = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool Enabled
        {
            get { return rcbOrderProducts.Enabled; }
            set { rcbOrderProducts.Enabled = value; }
        }


        public Guid? SelectedValue
        {
            get
            {
                if (!string.IsNullOrEmpty(rcbOrderProducts.SelectedValue))
                    return Guid.Parse(rcbOrderProducts.SelectedValue);

                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    var orderProduct = DataManager.OrderProducts.SelectById((Guid)value);
                    if (orderProduct != null)
                    {
                        rcbOrderProducts.Items.Add(new RadComboBoxItem(orderProduct.tbl_Product.Title, orderProduct.ID.ToString()) { Selected = true });
                    }
                }
                else
                {
                    rcbOrderProducts.Items.Clear();
                    rcbOrderProducts.SelectedIndex = -1;
                    rcbOrderProducts.Text = string.Empty;
                }
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rcbOrderProducts.EmptyMessage = "Выберите значение";
        }


        private static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id)
                return root;

            return (from Control c in root.Controls select FindControlRecursive(c, id)).FirstOrDefault(rc => rc != null);
        }



        /// <summary>
        /// Handles the ItemsRequested event of the rcbOrderProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs"/> instance containing the event data.</param>
        protected void rcbOrderProducts_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var ctrl = FindControlRecursive(Page, "ucTaskMembers");

            var taskMembers = new List<TaskMemberMap>();
            var taskMembersProducts = new List<Guid>();

            if (ctrl != null && ctrl is TaskMembers)
            {
                var taskMembersControl = (TaskMembers) ctrl;                
                taskMembers = taskMembersControl.TaskMembersList.Where(o => o.OrderProductsID.HasValue).ToList();
                if (taskMembers.Any())
                {
                    var existsTaskMembers = DataManager.TaskMember.SelectAll(taskMembers[0].TaskID);
                    taskMembers = taskMembers.Where(o => !existsTaskMembers.Select(x => x.ID).Contains(o.ID)).ToList();
                }

                taskMembersProducts = taskMembers.Select(o => (Guid)o.OrderProductsID).ToList();
            }

            rcbOrderProducts.Items.Clear();

            var orderProducts = DataManager.OrderProducts.SelectAllForCombobox(OrderId);

            if (ProductId.HasValue)
                orderProducts = orderProducts.Where(o => o.ProductID == ProductId.Value);

            if (taskMembers.Any())
                orderProducts = orderProducts.Where(o => o.Quantity > (taskMembersProducts.Count(x => x == o.ID)) + o.tbl_TaskMember.Count);

            if (!string.IsNullOrEmpty(e.Text))
                orderProducts = orderProducts.Where(c => c.tbl_Product.Title.ToLower().StartsWith(e.Text.ToLower()));

            var listOfOrderProducts = new List<tbl_OrderProducts>();           

            foreach (var orderProduct in orderProducts)
            {
                if (!orderProduct.ParentOrderProductID.HasValue && DataManager.OrderProducts.StockQuantity(orderProduct.ID, orderProduct.ProductID) == 0)
                    continue;                                    

                if (listOfOrderProducts.SingleOrDefault(o => o.ProductID == orderProduct.ProductID) == null)
                    listOfOrderProducts.Add(orderProduct);
            }

            var count = listOfOrderProducts.Count;
            int itemOffset = e.NumberOfItems;
            int endOffset = Math.Min(itemOffset + 15, count);
            e.EndOfItems = endOffset == count;

            for (int i = itemOffset; i < endOffset; i++)
                rcbOrderProducts.Items.Add(new RadComboBoxItem(listOfOrderProducts[i].tbl_Product.Title, listOfOrderProducts[i].ID.ToString()));

            e.Message = GetStatusMessage(endOffset, count);
        }



        /// <summary>
        /// Gets the status message.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="total">The total.</param>
        /// <returns></returns>
        private static string GetStatusMessage(int offset, int total)
        {
            if (total <= 0)
                return "Пусто";

            return String.Format("Элементы <b>1</b>-<b>{0}</b> из <b>{1}</b>", offset, total);
        }
    }
}