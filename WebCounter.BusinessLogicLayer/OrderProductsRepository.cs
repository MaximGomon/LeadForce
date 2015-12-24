using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class OrderProductsRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public OrderProductsRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="orderProductId">The order product id.</param>
        /// <returns></returns>
        public tbl_OrderProducts SelectById(Guid orderProductId)
        {
            return _dataContext.tbl_OrderProducts.SingleOrDefault(o => o.ID == orderProductId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <param name="showHidden">if set to <c>true</c> [show hidden].</param>
        /// <returns></returns>
        public IQueryable<tbl_OrderProducts> SelectAll(Guid orderId, bool showHidden = false)
        {            
            if (!showHidden)
                return _dataContext.tbl_OrderProducts.Where(o => o.OrderID == orderId && !o.ParentOrderProductID.HasValue);            

            return _dataContext.tbl_OrderProducts.Where(o => o.OrderID == orderId);
        }



        /// <summary>
        /// Selects all for combobox.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <returns></returns>
        public IQueryable<tbl_OrderProducts> SelectAllForCombobox(Guid orderId)
        {            
            return _dataContext.tbl_OrderProducts.Where(o => o.OrderID == orderId && o.Quantity > o.tbl_TaskMember.Count);
        }



        /// <summary>
        /// Stocks the quantity.
        /// </summary>
        /// <param name="orderProductId">The order product id.</param>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public decimal StockQuantity(Guid orderProductId, Guid? productId = null)
        {
            var orderProduct = _dataContext.tbl_OrderProducts.SingleOrDefault(o => o.ID == orderProductId);

            if (orderProduct == null)
            {
                return (decimal)_dataContext.tbl_ProductComplectation.Where(o => o.BaseProductID == productId).Sum(o => o.Quantity);
            }

            decimal quantity = 0;

            var orderProducts = _dataContext.tbl_OrderProducts.Where(o => o.ParentOrderProductID == orderProduct.ID).ToList();

            if (orderProducts.Any())
            {
                var taskProductsIncludedToTask = orderProducts.Sum(o => o.tbl_TaskMember.Count);
                quantity = orderProducts.Sum(o => o.Quantity) - taskProductsIncludedToTask;
            }
            else
            {
                quantity = orderProduct.Quantity - orderProduct.tbl_TaskMember.Count;
            }

            return quantity;
        }



        /// <summary>
        /// Adds the specified order product.
        /// </summary>
        /// <param name="orderProduct">The order product.</param>
        /// <returns></returns>
        public tbl_OrderProducts Add(tbl_OrderProducts orderProduct)
        {
            orderProduct.ID = Guid.NewGuid();
            _dataContext.tbl_OrderProducts.AddObject(orderProduct);
            _dataContext.SaveChanges();

            return orderProduct;        
        }



        /// <summary>
        /// Updates the specified to update order products.
        /// </summary>
        /// <param name="toUpdateOrderProducts">To update order products.</param>
        /// <param name="orderId">The order id.</param>
        public void Update(List<OrderProductsMap> toUpdateOrderProducts, Guid orderId)
        {
            var existsOrderProducts = SelectAll(orderId).ToList();
            
            foreach (var updateOrderProduct in toUpdateOrderProducts)
            {
                var existOrderProduct = existsOrderProducts.SingleOrDefault(op => op.ID == updateOrderProduct.ID);

                if (existOrderProduct == null)
                {
                    _dataContext.tbl_OrderProducts.AddObject(new tbl_OrderProducts()
                    {
                        ID = updateOrderProduct.ID,
                        OrderID = orderId,
                        ProductID = updateOrderProduct.ProductID,
                        AnyProductName = updateOrderProduct.AnyProductName,
                        SerialNumber = updateOrderProduct.SerialNumber,
                        PriceListID = updateOrderProduct.PriceListID,
                        Quantity = updateOrderProduct.Quantity,
                        UnitID = updateOrderProduct.UnitID,
                        CurrencyID = updateOrderProduct.CurrencyID,
                        Rate = updateOrderProduct.Rate,
                        CurrencyPrice = updateOrderProduct.CurrencyPrice,
                        CurrencyAmount = updateOrderProduct.CurrencyAmount,
                        Price = updateOrderProduct.Price,
                        Amount = updateOrderProduct.Amount,
                        SpecialOfferPriceListID = updateOrderProduct.SpecialOfferPriceListID,
                        Discount = updateOrderProduct.Discount,
                        CurrencyDiscountAmount = updateOrderProduct.CurrencyDiscountAmount,
                        DiscountAmount = updateOrderProduct.DiscountAmount,
                        CurrencyTotalAmount = updateOrderProduct.CurrencyTotalAmount,
                        TotalAmount = updateOrderProduct.TotalAmount
                    });

                    AddProductComplectation(updateOrderProduct, orderId);
                }
                else
                {
                    if (existOrderProduct.ProductID != updateOrderProduct.ProductID)
                    {
                        RemoveProductComplectation(existOrderProduct);
                        AddProductComplectation(updateOrderProduct, orderId);
                    }

                    existOrderProduct.ProductID = updateOrderProduct.ProductID;
                    existOrderProduct.AnyProductName = updateOrderProduct.AnyProductName;
                    existOrderProduct.SerialNumber = updateOrderProduct.SerialNumber;
                    existOrderProduct.PriceListID = updateOrderProduct.PriceListID;
                    existOrderProduct.Quantity = updateOrderProduct.Quantity;
                    existOrderProduct.UnitID = updateOrderProduct.UnitID;
                    existOrderProduct.CurrencyID = updateOrderProduct.CurrencyID;
                    existOrderProduct.Rate = updateOrderProduct.Rate;
                    existOrderProduct.CurrencyPrice = updateOrderProduct.CurrencyPrice;
                    existOrderProduct.CurrencyAmount = updateOrderProduct.CurrencyAmount;
                    existOrderProduct.Price = updateOrderProduct.Price;
                    existOrderProduct.Amount = updateOrderProduct.Amount;
                    existOrderProduct.SpecialOfferPriceListID = updateOrderProduct.SpecialOfferPriceListID;
                    existOrderProduct.Discount = updateOrderProduct.Discount;
                    existOrderProduct.CurrencyDiscountAmount = updateOrderProduct.CurrencyDiscountAmount;
                    existOrderProduct.DiscountAmount = updateOrderProduct.DiscountAmount;
                    existOrderProduct.CurrencyTotalAmount = updateOrderProduct.CurrencyTotalAmount;
                    existOrderProduct.TotalAmount = updateOrderProduct.TotalAmount;
                }
            }

            foreach (var existsOrderProduct in existsOrderProducts)
            {
                if (toUpdateOrderProducts.SingleOrDefault(op => op.ID == existsOrderProduct.ID) == null)
                {
                    _dataContext.tbl_OrderProducts.DeleteObject(existsOrderProduct);
                    RemoveProductComplectation(existsOrderProduct);
                }
            }

            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the product complectation.
        /// </summary>
        /// <param name="updateOrderProduct">The update order product.</param>
        /// <param name="orderId">The order id.</param>
        protected void AddProductComplectation(OrderProductsMap updateOrderProduct, Guid orderId)
        {
            var productComplectation = _dataContext.tbl_ProductComplectation.Where(o => o.BaseProductID == updateOrderProduct.ProductID);
            if (productComplectation.Any())
            {
                foreach (var complectation in productComplectation)
                {                                      
                    _dataContext.tbl_OrderProducts.AddObject(new tbl_OrderProducts()
                    {
                        ID = Guid.NewGuid(),
                        OrderID = orderId,
                        ProductID = complectation.ProductID,
                        PriceListID = updateOrderProduct.PriceListID,
                        Quantity = (decimal)complectation.Quantity,
                        UnitID = updateOrderProduct.UnitID,
                        CurrencyID = updateOrderProduct.CurrencyID,
                        Rate = 1,
                        CurrencyPrice = complectation.Price,
                        CurrencyAmount = complectation.Price,
                        Price = complectation.Price,
                        Amount = complectation.Price,
                        CurrencyTotalAmount = complectation.Price,
                        TotalAmount = complectation.Price,
                        ParentOrderProductID = updateOrderProduct.ID
                    });
                }
            }
        }



        /// <summary>
        /// Removes the product complectation.
        /// </summary>
        /// <param name="updateOrderProduct">The update order product.</param>
        protected void RemoveProductComplectation(tbl_OrderProducts updateOrderProduct)
        {
            var productComplectation = _dataContext.tbl_ProductComplectation.Where(o => o.BaseProductID == updateOrderProduct.ProductID);
            if (productComplectation.Any())
            {
                var orderProducts =
                    _dataContext.tbl_OrderProducts.Where(o => o.ParentOrderProductID == updateOrderProduct.ID).ToList();

                foreach (var orderProduct in orderProducts)
                {
                    _dataContext.tbl_OrderProducts.DeleteObject(orderProduct);
                }
            }
        }
    }
}