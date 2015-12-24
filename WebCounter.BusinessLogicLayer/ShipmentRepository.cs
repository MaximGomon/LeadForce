using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ShipmentRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ShipmentRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }




        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public tbl_Shipment SelectById(Guid siteId, Guid shipmentId)
        {
            return _dataContext.tbl_Shipment.SingleOrDefault(i => i.SiteID == siteId && i.ID == shipmentId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Shipment>  SelectAll(Guid siteId)
        {
            return _dataContext.tbl_Shipment.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by buyer company id.
        /// </summary>
        /// <param name="buyerCompanyId">The buyer company id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Shipment> SelectByBuyerCompanyId(Guid buyerCompanyId)
        {
            return _dataContext.tbl_Shipment.Where(o => o.BuyerCompanyID.HasValue && o.BuyerCompanyID == buyerCompanyId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="shipmentId">The shipment id.</param>
        /// <returns></returns>
        public tbl_Shipment SelectById(Guid shipmentId)
        {
            return _dataContext.tbl_Shipment.SingleOrDefault(i => i.ID == shipmentId);
        }


        public IQueryable<tbl_Shipment> SelectForInvoice(decimal amount, Guid? buyerCompanyLegalAccountId, Guid? executorCompanyLegalAccountId)
        {
            return
                _dataContext.tbl_Shipment.Where(
                    o =>
                    o.ShipmentAmount == amount && o.BuyerCompanyLegalAccountID == buyerCompanyLegalAccountId &&
                    o.ExecutorCompanyLegalAccountID == executorCompanyLegalAccountId);
        }


        /// <summary>
        /// Adds the specified shipment.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        /// <returns></returns>
        public tbl_Shipment Add(tbl_Shipment shipment)
        {
            shipment.ID = Guid.NewGuid();
            shipment.CreatedAt = DateTime.Now;
            _dataContext.tbl_Shipment.AddObject(shipment);
            _dataContext.SaveChanges();

            AddHistory(new DataManager(), shipment);            

            return shipment;
        }



        /// <summary>
        /// Updates the specified shipment.
        /// </summary>
        /// <param name="shipment">The shipment.</param>
        public void Update(tbl_Shipment shipment)
        {
            var dataManager = new DataManager();
            var shipmentInDataBase = dataManager.Shipment.SelectById(shipment.SiteID, shipment.ID);
            shipment.ModifiedAt = DateTime.Now;
            _dataContext.SaveChanges();

            if (shipment.SendDate != shipmentInDataBase.SendDate ||
                shipment.ShipmentAmount != shipmentInDataBase.ShipmentAmount || shipment.ShipmentStatusID != shipmentInDataBase.ShipmentStatusID || 
                shipment.Note != shipmentInDataBase.Note)
            {
                AddHistory(dataManager, shipment);
            }

        //    if (shipment.ShipmentStatusID != shipmentInDataBase.ShipmentStatusID && shipment.ShipmentStatusID == (int)ShipmentStatus.PendingPayment)
        //        ShipmentNotificationService.PendingPayment(shipment.ID);
        }



        /// <summary>
        /// Adds the history.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="shipment">The shipment.</param>
        protected void AddHistory(DataManager dataManager, tbl_Shipment shipment)
        {
            var shipmentHistory = new tbl_ShipmentHistory
            {
                ShipmentID = shipment.ID,
                AuthorID = (Guid)CurrentUser.Instance.ContactID,
                SendDate = shipment.SendDate,                
                ShipmentAmount = shipment.ShipmentAmount,
                ShipmentStatusID = shipment.ShipmentStatusID,
                Note = shipment.Note
            };

            dataManager.ShipmentHistory.Add(shipmentHistory);
        }



        /// <summary>
        /// Deletes the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="shipmentId">The shipment id.</param>
        public void DeleteById(Guid siteId, Guid shipmentId)
        {
            var shipment = SelectById(siteId, shipmentId);
            if (shipment != null)
            {
                _dataContext.tbl_Shipment.DeleteObject(shipment);
                _dataContext.SaveChanges();
            }
        }
    }
}