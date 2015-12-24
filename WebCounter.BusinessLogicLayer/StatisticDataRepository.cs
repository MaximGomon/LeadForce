using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class StatisticDataRepository
    {
        private WebCounterEntities _dataContext;

        public Guid SiteId;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticDataRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public StatisticDataRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by code.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public tbl_StatisticData SelectByCode(Guid siteId, string code)
        {
            return _dataContext.tbl_StatisticData.SingleOrDefault(o => o.Code == code && o.SiteID == siteId);
        }




        /// <summary>
        /// Selects the by code start with.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public IQueryable<tbl_StatisticData> SelectByCodeStartWith(Guid siteId, string code)
        {
            return _dataContext.tbl_StatisticData.Where(o => o.SiteID == siteId && o.Code.StartsWith(code));
        }



        /// <summary>
        /// Updates the specified statistic data.
        /// </summary>
        /// <param name="statisticData">The statistic data.</param>
        public void Update(tbl_StatisticData statisticData)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        protected tbl_StatisticData Add(string code)
        {
            var statisticData = new tbl_StatisticData()
                                    {
                                        ID = Guid.NewGuid(),
                                        SiteID = HttpContext.Current != null ? CurrentUser.Instance.SiteID : SiteId,
                                        Code = code,
                                        Value = 0,
                                        RecalculateDate = null
                                    };            

            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();

                var query = @"INSERT INTO tbl_StatisticData ([ID], [SiteID], [Code], [Value], [RecalculateDate], [PreviousValue]) 
                            VALUES(newid(),@SiteID, @Code, @Value, null, 0)";

                using (var command = new SqlCommand(query, connection))
                {                    
                    command.Parameters.AddWithValue("@SiteID", statisticData.SiteID);
                    command.Parameters.AddWithValue("@Code", statisticData.Code);
                    command.Parameters.AddWithValue("@Value", statisticData.Value);

                    command.ExecuteNonQuery();
                }
            }

            //_dataContext.tbl_StatisticData.AddObject(statisticData);
            //_dataContext.SaveChanges();

            return statisticData;
        }


        #region Visitor Source

        private StatisticDataValue _visitorSourceNewAnonymousTotal;        
        public StatisticDataValue VisitorSourceNewAnonymousTotal
        {
            get
            {
                if (_visitorSourceNewAnonymousTotal == null)
                    _visitorSourceNewAnonymousTotal = GetStatisticDataValueByCode("VisitorSourceNewAnonymousTotal");
                return _visitorSourceNewAnonymousTotal;
            }
        }

        
        public StatisticDataValue VisitorSourceNewAnonymousSum
        {
            get
            {
                return
                    GetStatisticDataValue("VisitorSourceNewAnonymousSum",
                        VisitorSourceNewAnonymousTotal.DbValue + VisitorSourceRepeatedAnonymous.DbValue, VisitorSourceNewAnonymousTotal.DbPreviousValue + VisitorSourceRepeatedAnonymous.DbPreviousValue);
            }
        }



        /// <summary>
        /// Gets the visitor source advertising platform.
        /// </summary>
        public Dictionary<string, StatisticDataValue> VisitorSourceNewAnonymousAdvertisingPlatform
        {
            get
            {
                const string code = "VisitorSourceNewAnonymousAdvertisingPlatform";
                var statisticDataList = SelectByCodeStartWith(HttpContext.Current != null ? CurrentUser.Instance.SiteID : SiteId, code).OrderByDescending(o => o.Value);
                if (statisticDataList.Any())
                    return statisticDataList.Select(o => new {o.Code, o.Value, o.PreviousValue}).ToDictionary(o=>o.Code, o=> GetStatisticDataValue(o.Code, o.Value, o.PreviousValue));

                return new Dictionary<string, StatisticDataValue>();
            }
        }



        private StatisticDataValue _visitorSourceNewAnonymousReffer;
        public StatisticDataValue VisitorSourceNewAnonymousReffer
        {
            get
            {
                if (_visitorSourceNewAnonymousReffer == null)
                    _visitorSourceNewAnonymousReffer = GetStatisticDataValueByCode("VisitorSourceNewAnonymousReffer");

                return _visitorSourceNewAnonymousReffer;
            }
        }


        private StatisticDataValue _visitorSourceNewAnonymousDirect;
        public StatisticDataValue VisitorSourceNewAnonymousDirect
        {
            get
            {
                if (_visitorSourceNewAnonymousDirect == null)
                    _visitorSourceNewAnonymousDirect = GetStatisticDataValueByCode("VisitorSourceNewAnonymousDirect");

                return _visitorSourceNewAnonymousDirect;
            }
        }



        private StatisticDataValue _visitorSourceRepeatedAnonymous;
        public StatisticDataValue VisitorSourceRepeatedAnonymous
        {
            get
            {
                if (_visitorSourceRepeatedAnonymous == null)
                    _visitorSourceRepeatedAnonymous = GetStatisticDataValueByCode("VisitorSourceRepeatedAnonymous");

                return _visitorSourceRepeatedAnonymous;
            }
        }

        #endregion

        #region Client Base Statistic


        private StatisticDataValue _clientBaseStatisticCountInBase;
        public StatisticDataValue ClientBaseStatisticCountInBase
        {
            get
            {
                if (_clientBaseStatisticCountInBase == null)
                    _clientBaseStatisticCountInBase = GetStatisticDataValueByCode("ClientBaseStatisticCountInBase");

                return _clientBaseStatisticCountInBase;
            }
        }


        private StatisticDataValue _clientBaseStatisticActiveCount;
        public StatisticDataValue ClientBaseStatisticActiveCount
        {
            get
            {
                if (_clientBaseStatisticActiveCount == null)
                    _clientBaseStatisticActiveCount = GetStatisticDataValueByCode("ClientBaseStatisticActiveCount");

                return _clientBaseStatisticActiveCount;
            }
        }

        #endregion

        #region Client Base Growth

        private StatisticDataValue _clientBaseGrowthNewTotal;
        public StatisticDataValue ClientBaseGrowthNewTotal
        {
            get
            {
                if (_clientBaseGrowthNewTotal == null)
                    _clientBaseGrowthNewTotal = GetStatisticDataValueByCode("ClientBaseGrowthNewTotal");

                return _clientBaseGrowthNewTotal;
            }
        }

        private StatisticDataValue _clientBaseGrowthNewRegistered;
        public StatisticDataValue ClientBaseGrowthNewRegistered
        {
            get
            {
                if (_clientBaseGrowthNewRegistered == null)
                    _clientBaseGrowthNewRegistered = GetStatisticDataValueByCode("ClientBaseGrowthNewRegistered");

                return _clientBaseGrowthNewRegistered;
            }
        }


        private StatisticDataValue _clientBaseGrowthNewImported;
        public StatisticDataValue ClientBaseGrowthNewImported
        {
            get
            {
                if (_clientBaseGrowthNewImported == null)
                    _clientBaseGrowthNewImported = GetStatisticDataValueByCode("ClientBaseGrowthNewImported");

                return _clientBaseGrowthNewImported;
            }
        }

        public Dictionary<string, StatisticDataValue> ClientBaseGrowthTemplateForm
        {
            get
            {
                const string code = "ClientBaseGrowthTemplateForm";
                var statisticDataList = SelectByCodeStartWith(HttpContext.Current != null ? CurrentUser.Instance.SiteID : SiteId, code).OrderByDescending(o => o.Value);
                if (statisticDataList.Any())
                    return statisticDataList.Select(o => new { o.Code, o.Value, o.PreviousValue }).ToDictionary(o => o.Code, o => GetStatisticDataValue(o.Code, o.Value, o.PreviousValue));

                return new Dictionary<string, StatisticDataValue>();
            }
        }

        private StatisticDataValue _clientBaseOtherFormsCount;
        public StatisticDataValue ClientBaseOtherFormsCount
        {
            get
            {
                if (_clientBaseOtherFormsCount == null)
                    _clientBaseOtherFormsCount = GetStatisticDataValueByCode("ClientBaseOtherFormsCount");

                return _clientBaseOtherFormsCount;
            }
        }        

        #endregion

        #region Loyalty Management

        private StatisticDataValue _loyaltyManagementInviteFriendFormCount;
        public StatisticDataValue LoyaltyManagementInviteFriendFormCount
        {
            get
            {
                if (_loyaltyManagementInviteFriendFormCount == null)
                    _loyaltyManagementInviteFriendFormCount = GetStatisticDataValueByCode("LoyaltyManagementInviteFriendFormCount");

                return _loyaltyManagementInviteFriendFormCount;
            }
        }

        private StatisticDataValue _loyaltyManagementUnansweredRequest;
        public StatisticDataValue LoyaltyManagementUnansweredRequest
        {
            get
            {
                if (_loyaltyManagementUnansweredRequest == null)
                    _loyaltyManagementUnansweredRequest = GetStatisticDataValueByCode("LoyaltyManagementUnansweredRequest");

                return _loyaltyManagementUnansweredRequest;
            }
        }


        private StatisticDataValue _loyaltyManagementIsExistPortal;
        public StatisticDataValue LoyaltyManagementIsExistPortal
        {
            get
            {
                if (_loyaltyManagementIsExistPortal == null)
                    _loyaltyManagementIsExistPortal = GetStatisticDataValueByCode("LoyaltyManagementIsExistPortal");

                return _loyaltyManagementIsExistPortal;
            }
        }


        #endregion

        #region New Sales Potential Client

        private StatisticDataValue _newSalesPotentialClientCount;
        public StatisticDataValue NewSalesPotentialClientCount
        {
            get
            {
                if (_newSalesPotentialClientCount == null)
                    _newSalesPotentialClientCount = GetStatisticDataValueByCode("NewSalesPotentialClientCount");

                return _newSalesPotentialClientCount;
            }
        }


        private StatisticDataValue _newSalesPotentialClientActiveCount;
        public StatisticDataValue NewSalesPotentialClientActiveCount
        {
            get
            {
                if (_newSalesPotentialClientActiveCount == null)
                    _newSalesPotentialClientActiveCount = GetStatisticDataValueByCode("NewSalesPotentialClientActiveCount");

                return _newSalesPotentialClientActiveCount;
            }
        }

        #endregion

        #region New Sales Invoice For Payment

        private StatisticDataValue _newSalesInvoiceForPaymentCount;
        public StatisticDataValue NewSalesInvoiceForPaymentCount
        {
            get
            {
                if (_newSalesInvoiceForPaymentCount== null)
                    _newSalesInvoiceForPaymentCount = GetStatisticDataValueByCode("NewSalesInvoiceForPaymentCount");

                return _newSalesInvoiceForPaymentCount;
            }
        }


        private StatisticDataValue _newSalesInvoiceForPaymentAmount;
        public StatisticDataValue NewSalesInvoiceForPaymentAmount
        {
            get
            {
                if (_newSalesInvoiceForPaymentAmount == null)
                    _newSalesInvoiceForPaymentAmount = GetStatisticDataValueByCode("NewSalesInvoiceForPaymentAmount");

                return _newSalesInvoiceForPaymentAmount;
            }
        }


        private StatisticDataValue _newSalesInvoiceForPaymentExposedCount;
        public StatisticDataValue NewSalesInvoiceForPaymentExposedCount
        {
            get
            {
                if (_newSalesInvoiceForPaymentExposedCount == null)
                    _newSalesInvoiceForPaymentExposedCount = GetStatisticDataValueByCode("NewSalesInvoiceForPaymentExposedCount");

                return _newSalesInvoiceForPaymentExposedCount;
            }
        }



        private StatisticDataValue _newSalesInvoiceForPaymentExposedAmount;
        public StatisticDataValue NewSalesInvoiceForPaymentExposedAmount
        {
            get
            {
                if (_newSalesInvoiceForPaymentExposedAmount == null)
                    _newSalesInvoiceForPaymentExposedAmount = GetStatisticDataValueByCode("NewSalesInvoiceForPaymentExposedAmount");

                return _newSalesInvoiceForPaymentExposedAmount;
            }
        }


        private StatisticDataValue _newSalesInvoiceForPaymentPayedCount;
        public StatisticDataValue NewSalesInvoiceForPaymentPayedCount
        {
            get
            {
                if (_newSalesInvoiceForPaymentPayedCount == null)
                    _newSalesInvoiceForPaymentPayedCount = GetStatisticDataValueByCode("NewSalesInvoiceForPaymentPayedCount");

                return _newSalesInvoiceForPaymentPayedCount;
            }
        }


        private StatisticDataValue _newSalesInvoiceForPaymentPayedAmount;
        public StatisticDataValue NewSalesInvoiceForPaymentPayedAmount
        {
            get
            {
                if (_newSalesInvoiceForPaymentPayedAmount == null)
                    _newSalesInvoiceForPaymentPayedAmount = GetStatisticDataValueByCode("NewSalesInvoiceForPaymentPayedAmount");

                return _newSalesInvoiceForPaymentPayedAmount;
            }
        }

        #endregion

        #region Repeat Sales Potential Client

        private StatisticDataValue _repeatSalesPotentialClientCount;
        public StatisticDataValue RepeatSalesPotentialClientCount
        {
            get
            {
                if (_repeatSalesPotentialClientCount == null)
                    _repeatSalesPotentialClientCount = GetStatisticDataValueByCode("RepeatSalesPotentialClientCount");

                return _repeatSalesPotentialClientCount;
            }
        }

        private StatisticDataValue _repeatSalesPotentialClientActiveCount;
        public StatisticDataValue RepeatSalesPotentialClientActiveCount
        {
            get
            {
                if (_repeatSalesPotentialClientActiveCount == null)
                    _repeatSalesPotentialClientActiveCount = GetStatisticDataValueByCode("RepeatSalesPotentialClientActiveCount");

                return _repeatSalesPotentialClientActiveCount;
            }
        }

        #endregion

        #region Repeat Sales Invoice For Payment

        private StatisticDataValue _repeatSalesInvoiceForPaymentCount;
        public StatisticDataValue RepeatSalesInvoiceForPaymentCount
        {
            get
            {
                if (_repeatSalesInvoiceForPaymentCount == null)
                    _repeatSalesInvoiceForPaymentCount = GetStatisticDataValueByCode("RepeatSalesInvoiceForPaymentCount");

                return _repeatSalesInvoiceForPaymentCount;
            }
        }


        private StatisticDataValue _repeatSalesInvoiceForPaymentAmount;
        public StatisticDataValue RepeatSalesInvoiceForPaymentAmount
        {
            get
            {
                if (_repeatSalesInvoiceForPaymentAmount == null)
                    _repeatSalesInvoiceForPaymentAmount = GetStatisticDataValueByCode("RepeatSalesInvoiceForPaymentAmount");

                return _repeatSalesInvoiceForPaymentAmount;
            }
        }


        private StatisticDataValue _repeatSalesInvoiceForPaymentExposedCount;
        public StatisticDataValue RepeatSalesInvoiceForPaymentExposedCount
        {
            get
            {
                if (_repeatSalesInvoiceForPaymentExposedCount == null)
                    _repeatSalesInvoiceForPaymentExposedCount = GetStatisticDataValueByCode("RepeatSalesInvoiceForPaymentExposedCount");

                return _repeatSalesInvoiceForPaymentExposedCount;
            }
        }

        private StatisticDataValue _repeatSalesInvoiceForPaymentExposedAmount;
        public StatisticDataValue RepeatSalesInvoiceForPaymentExposedAmount
        {
            get
            {
                if (_repeatSalesInvoiceForPaymentExposedAmount == null)
                    _repeatSalesInvoiceForPaymentExposedAmount = GetStatisticDataValueByCode("RepeatSalesInvoiceForPaymentExposedAmount");

                return _repeatSalesInvoiceForPaymentExposedAmount;
            }
        }


        private StatisticDataValue _repeatSalesInvoiceForPaymentPayedCount;
        public StatisticDataValue RepeatSalesInvoiceForPaymentPayedCount
        {
            get
            {
                if (_repeatSalesInvoiceForPaymentPayedCount == null)
                    _repeatSalesInvoiceForPaymentPayedCount = GetStatisticDataValueByCode("RepeatSalesInvoiceForPaymentPayedCount");

                return _repeatSalesInvoiceForPaymentPayedCount;
            }
        }


        private StatisticDataValue _repeatSalesInvoiceForPaymentPayedAmount;
        public StatisticDataValue RepeatSalesInvoiceForPaymentPayedAmount
        {
            get
            {
                if (_repeatSalesInvoiceForPaymentPayedAmount == null)
                    _repeatSalesInvoiceForPaymentPayedAmount = GetStatisticDataValueByCode("RepeatSalesInvoiceForPaymentPayedAmount");

                return _repeatSalesInvoiceForPaymentPayedAmount;
            }
        }

        #endregion

        #region Contact Category

        private StatisticDataValue _contactCategoryKnownCount;
        public StatisticDataValue ContactCategoryKnownCount
        {
            get
            {
                if (_contactCategoryKnownCount == null)
                    _contactCategoryKnownCount = GetStatisticDataValueByCode("ContactCategoryKnownCount");

                return _contactCategoryKnownCount;
            }            
        }

        private StatisticDataValue _contactCategoryActiveCount;
        public StatisticDataValue ContactCategoryActiveCount
        {
            get
            {
                if (_contactCategoryActiveCount == null)
                    _contactCategoryActiveCount = GetStatisticDataValueByCode("ContactCategoryActiveCount");

                return _contactCategoryActiveCount;
            }
        }


        private StatisticDataValue _contactCategoryAnonymousCount;
        public StatisticDataValue ContactCategoryAnonymousCount
        {
            get
            {
                if (_contactCategoryAnonymousCount == null)
                    _contactCategoryAnonymousCount = GetStatisticDataValueByCode("ContactCategoryAnonymousCount");

                return _contactCategoryAnonymousCount;
            }
        }


        private StatisticDataValue _contactCategoryTotalCount;
        public StatisticDataValue ContactCategoryTotalCount
        {
            get
            {
                if (_contactCategoryTotalCount == null)
                    _contactCategoryTotalCount = GetStatisticDataValueByCode("ContactCategoryTotalCount");

                return _contactCategoryTotalCount;
            }
        }


        private StatisticDataValue _contactCategoryDeletedCount;
        public StatisticDataValue ContactCategoryDeletedCount
        {
            get
            {
                if (_contactCategoryDeletedCount == null)
                    _contactCategoryDeletedCount = GetStatisticDataValueByCode("ContactCategoryDeletedCount");

                return _contactCategoryDeletedCount;
            }
        }

        #endregion


        /// <summary>
        /// Gets the statistic data value by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public StatisticDataValue GetStatisticDataValueByCode(string code)
        {
            var statisticData = SelectByCode(HttpContext.Current != null ? CurrentUser.Instance.SiteID : SiteId, code) ?? Add(code);
            return GetStatisticDataValue(code, statisticData.Value, statisticData.PreviousValue);
        }



        /// <summary>
        /// Gets the statistic data value.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="value">The value.</param>
        /// <param name="previousValue">The previous value.</param>
        /// <returns></returns>
        public StatisticDataValue GetStatisticDataValue(string code, decimal value, decimal previousValue)
        {
            var result = new StatisticDataValue {Code = code, DbValue = value, DbPreviousValue = previousValue};

            var prevPercents = previousValue/100;
            if (prevPercents > 0)
            {
                result.Value = value/prevPercents - 100;
                if (result.Value > 0)
                    result.Type = Increase.Positive;
                if (result.Value < 0)
                    result.Type = Increase.Negative;
                if (result.Value == 0)
                    result.Type = Increase.None;
            }
            else
            {
                result.Value = value;
                if (value != 0)
                    result.Type = Increase.Positive;
                else
                    result.Type = Increase.None;                
            }

            result.HtmlValue = string.Format("<b>{0}</b> <span class='{1}'>({2}%)</span>", result.DbValue.ToString("F"),result.Type == Increase.Positive ? "positive"
                : result.Type == Increase.Negative ? "negative" : string.Empty, (result.Value > 0 ? "+" : string.Empty) + result.Value.ToString("F"));

            return result;
        }        
    }    


    public class StatisticDataValue
    {
        public string Code { get; set; }
        public decimal Value { get; set; }
        public decimal DbValue { get; set; }
        public decimal DbPreviousValue { get; set; }
        public string HtmlValue { get; set; }
        public Increase Type { get; set; }

        public void Update(Guid siteId)
        {            
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();

                var query = "UPDATE tbl_StatisticData SET Value=@Value, PreviousValue=@PreviousValue, RecalculateDate=getdate() WHERE Code=@Code AND SiteID = @SiteID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Value", DbValue);
                    command.Parameters.AddWithValue("@PreviousValue", DbPreviousValue);
                    command.Parameters.AddWithValue("@Code", Code);
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.ExecuteNonQuery();
                }
            }
        }        
    }

    public enum Increase
    {
        None,
        Negative,
        Positive
    }
}