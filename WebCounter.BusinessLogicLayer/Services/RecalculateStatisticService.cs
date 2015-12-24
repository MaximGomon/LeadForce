using System;
using System.Configuration;
using System.Data.SqlClient;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Invoice;


namespace WebCounter.BusinessLogicLayer.Services
{
    public class RecalculateStatisticService
    {
        /// <summary>
        /// Contacts the category known count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        public static void ContactCategoryKnownCount(Guid siteId, string code)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();                
                string query = string.Format(@"
DECLARE @Value int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE (Category = {0} OR Category = {1} OR Category = {2}) AND SiteID = @SiteID
EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, NULL", (int)ContactCategory.Known, (int)ContactCategory.Active, (int)ContactCategory.ActiveAboveTariff);
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Contacts the category known count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        public static void ContactCategoryActiveCount(Guid siteId, string code)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE (Category = {0} OR Category = {1}) AND SiteID = @SiteID
EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, NULL", (int)ContactCategory.Active, (int)ContactCategory.ActiveAboveTariff);
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Contacts the category anonymous count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        public static void ContactCategoryAnonymousCount(Guid siteId, string code)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE Category = {0} AND SiteID = @SiteID
EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, NULL", (int)ContactCategory.Anonymous);
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Contacts the category total count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        public static void ContactCategoryTotalCount(Guid siteId, string code)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE Category <> {0} AND SiteID = @SiteID
EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, NULL", (int)ContactCategory.Deleted);
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Contacts the category deleted count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        public static void ContactCategoryDeletedCount(Guid siteId, string code)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE Category = {0} AND SiteID = @SiteID
EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, NULL", (int)ContactCategory.Deleted);
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Visitors the source new anonymous total.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="previousStartDate">The previous start date.</param>
        /// <param name="previousEndDate">The previous end date.</param>
        public static void VisitorSourceNewAnonymousTotal(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE Category = {0} AND SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate
SELECT @PreviousValue = COUNT(*) FROM tbl_Contact WHERE Category = {0} AND SiteID = @SiteID AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate
EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue", (int)ContactCategory.Anonymous);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Visitors the source new anonymous reffer.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="previousStartDate">The previous start date.</param>
        /// <param name="previousEndDate">The previous end date.</param>
        public static void VisitorSourceNewAnonymousReffer(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                const string query = @"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE RefferID IS NOT NULL AND SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate
SELECT @PreviousValue = COUNT(*) FROM tbl_Contact WHERE RefferID IS NOT NULL AND SiteID = @SiteID AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate
EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Visitors the source new anonymous direct.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="previousStartDate">The previous start date.</param>
        /// <param name="previousEndDate">The previous end date.</param>
        public static void VisitorSourceNewAnonymousDirect(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                const string query = @"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE (RefferURL IS NULL OR RefferURL = '') AND SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate
SELECT @PreviousValue = COUNT(*) FROM tbl_Contact WHERE (RefferURL IS NULL OR RefferURL = '') AND SiteID = @SiteID AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate
EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Visitors the source repeated anonymous.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="previousStartDate">The previous start date.</param>
        /// <param name="previousEndDate">The previous end date.</param>
        public static void VisitorSourceRepeatedAnonymous(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                const string query = @"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(DISTINCT ContactID) FROM tbl_ContactSessions WHERE UserSessionNumber > 1 AND SiteID = @SiteID AND SessionDate >= @StartDate AND SessionDate <= @EndDate
SELECT @PreviousValue = COUNT(DISTINCT ContactID) FROM tbl_ContactSessions WHERE UserSessionNumber > 1 AND SiteID = @SiteID AND SessionDate >= @PreviousStartDate AND SessionDate <= @PreviousEndDate
EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue";

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Clients the base statistic active count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="code">The code.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="previousStartDate">The previous start date.</param>
        /// <param name="previousEndDate">The previous end date.</param>
        public static void ClientBaseStatisticActiveCount(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE SiteID = @SiteID AND (Category = {0} OR Category = {1} OR Category = {2}) AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate
AND ID IN (SELECT ContactID FROM tbl_ContactActivity WHERE SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate)

SELECT @PreviousValue = COUNT(*) FROM tbl_Contact WHERE SiteID = @SiteID AND (Category = {0} OR Category = {1} OR Category = {2}) AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate
AND ID IN (SELECT ContactID FROM tbl_ContactActivity WHERE SiteID = @SiteID AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate)

EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue", (int)ContactCategory.Known, (int)ContactCategory.Active, (int)ContactCategory.ActiveAboveTariff);

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }

        /*dataManager.StatisticData.ClientBaseStatisticCountInBase.DbPreviousValue =
                    dataManager.Contact.SelectAll(site.ID).Count(
                        o => o.CreatedAt <= previousEndDate &&
                        (o.Category == (int)ContactCategory.Active ||
                         o.Category == (int)ContactCategory.ActiveAboveTariff ||
                         o.Category == (int)ContactCategory.Known));*/

        public static void ClientBaseStatisticCountInBase(Guid siteId, string code, DateTime endDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE SiteID = @SiteID AND (Category = {0} OR Category = {1} OR Category = {2}) AND CreatedAt <= @EndDate
SELECT @PreviousValue = COUNT(*) FROM tbl_Contact WHERE SiteID = @SiteID AND (Category = {0} OR Category = {1} OR Category = {2}) AND CreatedAt <= @PreviousEndDate

EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue", (int)ContactCategory.Known, (int)ContactCategory.Active, (int)ContactCategory.ActiveAboveTariff);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static void ClientBaseGrowthNewTotal(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE SiteID = @SiteID AND (Category = {0} OR Category = {1} OR Category = {2}) AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate

SELECT @PreviousValue = COUNT(*) FROM tbl_Contact WHERE SiteID = @SiteID AND (Category = {0} OR Category = {1} OR Category = {2}) AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate

EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue", (int)ContactCategory.Known, (int)ContactCategory.Active, (int)ContactCategory.ActiveAboveTariff);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }



        public static void ClientBaseGrowthNewRegistered(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE SiteID = @SiteID AND (Category = {0} OR Category = {1} OR Category = {2}) AND RegistrationSourceID = {3} AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate

SELECT @PreviousValue = COUNT(*) FROM tbl_Contact WHERE SiteID = @SiteID AND (Category = {0} OR Category = {1} OR Category = {2}) AND RegistrationSourceID = {3} AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate

EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue", (int)ContactCategory.Known, (int)ContactCategory.Active, (int)ContactCategory.ActiveAboveTariff, (int)RegistrationSource.CounterService);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static void ClientBaseGrowthNewImported(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(*) FROM tbl_Contact WHERE SiteID = @SiteID AND RegistrationSourceID = {0} AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate

SELECT @PreviousValue = COUNT(*) FROM tbl_Contact WHERE SiteID = @SiteID AND RegistrationSourceID = {0} AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate

EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue", (int)RegistrationSource.Import);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static void LoyaltyManagementUnansweredRequest(Guid siteId, string code)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                const string query = @"
DECLARE @Value int
SELECT @Value = COUNT(*) FROM tbl_Requirement AS R LEFT JOIN tbl_RequirementStatus AS RS ON R.RequirementStatusID = RS.ID WHERE R.SiteID = @SiteID AND RS.IsLast <> 1
EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, NULL";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.ExecuteNonQuery();
                }
            }
        }

        

        public static void NewSalesPotentialClientCount(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(DISTINCT C.ID) 
FROM tbl_Contact AS C LEFT JOIN tbl_Invoice AS I ON I.BuyerContactID = C.ID
WHERE C.SiteID = @SiteID AND C.CreatedAt >= @StartDate AND C.CreatedAt <= @EndDate AND
(I.InvoiceStatusID IS NULL OR I.InvoiceStatusID <> {0}) AND
C.ID IN (
        SELECT CAST([Value] as uniqueidentifier)
        FROM tbl_WorkflowParameter AS WP 
        LEFT JOIN tbl_Workflow AS W ON WP.WorkflowID = W.ID 
        WHERE [Value] IS NOT NULL AND [Value] <> '' AND W.SiteID = @SiteID)

SELECT @PreviousValue = COUNT(DISTINCT C.ID) 
FROM tbl_Contact AS C LEFT JOIN tbl_Invoice AS I ON I.BuyerContactID = C.ID
WHERE C.SiteID = @SiteID AND C.CreatedAt >= @PreviousStartDate AND C.CreatedAt <= @PreviousEndDate AND
(I.InvoiceStatusID IS NULL OR I.InvoiceStatusID <> {0}) AND
C.ID IN (
        SELECT CAST([Value] as uniqueidentifier)
        FROM tbl_WorkflowParameter AS WP 
        LEFT JOIN tbl_Workflow AS W ON WP.WorkflowID = W.ID 
        WHERE [Value] IS NOT NULL AND [Value] <> '' AND W.SiteID = @SiteID)


EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue", (int)InvoiceStatus.Paid);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static void NewSalesPotentialClientActiveCount(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(DISTINCT C.ID) 
FROM tbl_Contact AS C LEFT JOIN tbl_Invoice AS I ON I.BuyerContactID = C.ID 
LEFT JOIN tbl_ContactActivity AS CA ON CA.ContactID = C.ID
WHERE C.SiteID = @SiteID AND C.CreatedAt >= @StartDate AND C.CreatedAt <= @EndDate AND 
(I.InvoiceStatusID IS NULL OR I.InvoiceStatusID <> {0}) AND
CA.SiteID = @SiteID AND CA.CreatedAt >= @StartDate AND CA.CreatedAt <= @EndDate AND 
C.ID IN (
        SELECT CAST([Value] as uniqueidentifier)
        FROM tbl_WorkflowParameter AS WP 
        LEFT JOIN tbl_Workflow AS W ON WP.WorkflowID = W.ID 
        WHERE [Value] IS NOT NULL AND [Value] <> '' AND W.SiteID = @SiteID)

SELECT @PreviousValue = COUNT(DISTINCT C.ID) 
FROM tbl_Contact AS C LEFT JOIN tbl_Invoice AS I ON I.BuyerContactID = C.ID 
LEFT JOIN tbl_ContactActivity AS CA ON CA.ContactID = C.ID
WHERE C.SiteID = @SiteID AND C.CreatedAt >= @PreviousStartDate AND C.CreatedAt <= @PreviousEndDate AND 
(I.InvoiceStatusID IS NULL OR I.InvoiceStatusID <> {0}) AND
CA.SiteID = @SiteID AND CA.CreatedAt >= @PreviousStartDate AND CA.CreatedAt <= @PreviousEndDate AND 
C.ID IN (
        SELECT CAST([Value] as uniqueidentifier)
        FROM tbl_WorkflowParameter AS WP 
        LEFT JOIN tbl_Workflow AS W ON WP.WorkflowID = W.ID 
        WHERE [Value] IS NOT NULL AND [Value] <> '' AND W.SiteID = @SiteID)

EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue", (int)InvoiceStatus.Paid);

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static void NewSalesInvoiceForPayment(Guid siteId, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int

DECLARE @AmountValue decimal(18,2)
DECLARE @AmountPreviousValue decimal(18,2)

SELECT @Value = COUNT(*), @AmountValue = SUM(CASE WHEN InvoiceAmount IS NULL THEN 0 ELSE InvoiceAmount END)
FROM tbl_Invoice WHERE SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate AND (InvoiceStatusID = {0} OR InvoiceStatusID = {1})
AND BuyerContactID NOT IN (SELECT BuyerContactID FROM tbl_Invoice WHERE SiteID = @SiteID AND BuyerContactID IS NOT NULL AND InvoiceStatusID = {2})

SELECT @PreviousValue = COUNT(*), @AmountPreviousValue = SUM(CASE WHEN InvoiceAmount IS NULL THEN 0 ELSE InvoiceAmount END)
FROM tbl_Invoice WHERE SiteID = @SiteID AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate AND (InvoiceStatusID = {0} OR InvoiceStatusID = {1})
AND BuyerContactID NOT IN (SELECT BuyerContactID FROM tbl_Invoice WHERE SiteID = @SiteID AND BuyerContactID IS NOT NULL AND InvoiceStatusID = {2})

EXEC [Statistic_Recalculate] @SiteID, 'NewSalesInvoiceForPaymentCount', @Value, @PreviousValue
EXEC [Statistic_Recalculate] @SiteID, 'NewSalesInvoiceForPaymentAmount', @AmountValue, @AmountPreviousValue", (int)InvoiceStatus.PendingPayment, (int)InvoiceStatus.PartialPaid, (int)InvoiceStatus.Paid);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);                    
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static void NewSalesInvoiceForPaymentExposed(Guid siteId, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int

DECLARE @AmountValue decimal(18,2)
DECLARE @AmountPreviousValue decimal(18,2)

SELECT @Value = COUNT(*), @AmountValue = SUM(CASE WHEN InvoiceAmount IS NULL THEN 0 ELSE InvoiceAmount END)
FROM tbl_Invoice AS I WHERE SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate AND InvoiceStatusID = {0}
AND 1 = (SELECT COUNT(*) FROM tbl_Invoice WHERE SiteID = @SiteID AND BuyerContactID IS NOT NULL AND InvoiceStatusID = {0} AND BuyerContactID = I.BuyerContactID)

SELECT @PreviousValue = COUNT(*), @AmountPreviousValue = SUM(CASE WHEN InvoiceAmount IS NULL THEN 0 ELSE InvoiceAmount END)
FROM tbl_Invoice AS I WHERE SiteID = @SiteID AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate AND InvoiceStatusID = {0}
AND 1 = (SELECT COUNT(*) FROM tbl_Invoice WHERE SiteID = @SiteID AND BuyerContactID IS NOT NULL AND InvoiceStatusID = {0} AND BuyerContactID = I.BuyerContactID)

EXEC [Statistic_Recalculate] @SiteID, 'NewSalesInvoiceForPaymentExposedCount', @Value, @PreviousValue
EXEC [Statistic_Recalculate] @SiteID, 'NewSalesInvoiceForPaymentExposedAmount', @AmountValue, @AmountPreviousValue", (int)InvoiceStatus.Paid);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);                    
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static void NewSalesInvoiceForPaymentPayed(Guid siteId, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int

DECLARE @AmountValue decimal(18,2)
DECLARE @AmountPreviousValue decimal(18,2)

SELECT @Value = COUNT(*), @AmountValue = SUM(CASE WHEN InvoiceAmount IS NULL THEN 0 ELSE InvoiceAmount END)
FROM tbl_Invoice AS I WHERE ID = (SELECT TOP 1 ID FROM tbl_Invoice WHERE 
SiteID = @SiteID AND PaymentDateActual IS NOT NULL AND PaymentDateActual >= @StartDate AND PaymentDateActual <= @EndDate AND InvoiceStatusID = {0} AND
BuyerContactID IS NOT NULL AND BuyerContactID = I.BuyerContactID
ORDER BY PaymentDateActual)

SELECT @PreviousValue = COUNT(*), @AmountPreviousValue = SUM(CASE WHEN InvoiceAmount IS NULL THEN 0 ELSE InvoiceAmount END)
FROM tbl_Invoice AS I WHERE ID = (SELECT TOP 1 ID FROM tbl_Invoice WHERE 
SiteID = @SiteID AND PaymentDateActual IS NOT NULL AND PaymentDateActual >= @PreviousStartDate AND PaymentDateActual <= @PreviousEndDate AND InvoiceStatusID = {0} AND
BuyerContactID IS NOT NULL AND BuyerContactID = I.BuyerContactID
ORDER BY PaymentDateActual)

EXEC [Statistic_Recalculate] @SiteID, 'NewSalesInvoiceForPaymentPayedCount', @Value, @PreviousValue
EXEC [Statistic_Recalculate] @SiteID, 'NewSalesInvoiceForPaymentPayedAmount', @AmountValue, @AmountPreviousValue",
(int)InvoiceStatus.Paid);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }



        public static void RepeatSalesPotentialClientCount(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(*) FROM tbl_Contact AS C WHERE SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate AND
        EXISTS (SELECT * FROM tbl_Invoice WHERE SiteID = @SiteID AND InvoiceStatusID = {0} AND BuyerContactID = C.ID)
        AND
        C.ID IN (
        SELECT CAST([Value] as uniqueidentifier)
        FROM tbl_WorkflowParameter AS WP 
        LEFT JOIN tbl_Workflow AS W ON WP.WorkflowID = W.ID 
        WHERE [Value] IS NOT NULL AND [Value] <> '' AND W.SiteID = @SiteID)

SELECT @PreviousValue = COUNT(*) FROM tbl_Contact AS C WHERE SiteID = @SiteID AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate AND
        EXISTS (SELECT * FROM tbl_Invoice WHERE SiteID = @SiteID AND InvoiceStatusID = {0} AND BuyerContactID = C.ID)
        AND
        C.ID IN (
        SELECT CAST([Value] as uniqueidentifier)
        FROM tbl_WorkflowParameter AS WP 
        LEFT JOIN tbl_Workflow AS W ON WP.WorkflowID = W.ID 
        WHERE [Value] IS NOT NULL AND [Value] <> '' AND W.SiteID = @SiteID)

EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue", (int)InvoiceStatus.Paid);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static void RepeatSalesPotentialClientActiveCount(Guid siteId, string code, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int
SELECT @Value = COUNT(*) FROM tbl_Contact AS C WHERE SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate AND
        EXISTS (SELECT * FROM tbl_Invoice WHERE SiteID = @SiteID AND InvoiceStatusID = {0} AND BuyerContactID = C.ID)
        AND
        C.ID IN (
        SELECT CAST([Value] as uniqueidentifier)
        FROM tbl_WorkflowParameter AS WP 
        LEFT JOIN tbl_Workflow AS W ON WP.WorkflowID = W.ID 
        WHERE [Value] IS NOT NULL AND [Value] <> '' AND W.SiteID = @SiteID)
        AND C.ID IN (SELECT ContactID FROM tbl_ContactActivity WHERE SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate)

SELECT @PreviousValue = COUNT(*) FROM tbl_Contact AS C WHERE SiteID = @SiteID AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate AND
        EXISTS (SELECT * FROM tbl_Invoice WHERE SiteID = @SiteID AND InvoiceStatusID = {0} AND BuyerContactID = C.ID)
        AND
        C.ID IN (
        SELECT CAST([Value] as uniqueidentifier)
        FROM tbl_WorkflowParameter AS WP 
        LEFT JOIN tbl_Workflow AS W ON WP.WorkflowID = W.ID 
        WHERE [Value] IS NOT NULL AND [Value] <> '' AND W.SiteID = @SiteID)
        AND C.ID IN (SELECT ContactID FROM tbl_ContactActivity WHERE SiteID = @SiteID AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate)

EXEC [Statistic_Recalculate] @SiteID, @Code, @Value, @PreviousValue", (int)InvoiceStatus.Paid);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void RepeatSalesInvoiceForPayment(Guid siteId, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                string query = string.Format(@"
DECLARE @Value int
DECLARE @PreviousValue int

DECLARE @AmountValue decimal(18,2)
DECLARE @AmountPreviousValue decimal(18,2)

SELECT @Value = COUNT(*), @AmountValue = SUM(CASE WHEN InvoiceAmount IS NULL THEN 0 ELSE InvoiceAmount END)
FROM tbl_Invoice AS I WHERE SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate AND (InvoiceStatusID = {0} OR InvoiceStatusID = {1})
AND EXISTS (SELECT BuyerContactID FROM tbl_Invoice WHERE SiteID = @SiteID AND BuyerContactID IS NOT NULL AND InvoiceStatusID = {2} AND BuyerContactID = I.BuyerContactID)

SELECT @PreviousValue = COUNT(*), @AmountPreviousValue = SUM(CASE WHEN InvoiceAmount IS NULL THEN 0 ELSE InvoiceAmount END)
FROM tbl_Invoice AS I WHERE SiteID = @SiteID AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate AND (InvoiceStatusID = {0} OR InvoiceStatusID = {1})
AND EXISTS (SELECT BuyerContactID FROM tbl_Invoice WHERE SiteID = @SiteID AND BuyerContactID IS NOT NULL AND InvoiceStatusID = {2} AND BuyerContactID = I.BuyerContactID)

EXEC [Statistic_Recalculate] @SiteID, 'RepeatSalesInvoiceForPaymentCount', @Value, @PreviousValue
EXEC [Statistic_Recalculate] @SiteID, 'RepeatSalesInvoiceForPaymentAmount', @AmountValue, @AmountPreviousValue", (int)InvoiceStatus.PendingPayment, (int)InvoiceStatus.PartialPaid, (int)InvoiceStatus.Paid);

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }

        //return _dataContext.tbl_Invoice.Where(
        //        o => o.SiteID == siteId &&
        //             o.CreatedAt >= startDate && o.CreatedAt <= endDate &&
        //             o.InvoiceStatusID == (int) InvoiceStatus.Paid &&
        //             _dataContext.tbl_Invoice.Count(
        //                 x => x.InvoiceStatusID == (int) InvoiceStatus.Paid && x.BuyerContactID == o.BuyerContactID) > 1
        //             &&
        //             o.ID ==
        //             _dataContext.tbl_Invoice.Where(
        //                 x => x.InvoiceStatusID == (int) InvoiceStatus.Paid && x.BuyerContactID == o.BuyerContactID).
        //                 OrderBy(x => x.CreatedAt).Skip(1).Take(1).FirstOrDefault().ID);

//        public static void RepeatSalesInvoiceForPaymentExposed(Guid siteId, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
//        {
//            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
//            {
//                connection.Open();
//                string query = string.Format(@"
//DECLARE @Value int
//DECLARE @PreviousValue int
//
//DECLARE @AmountValue decimal(18,2)
//DECLARE @AmountPreviousValue decimal(18,2)
//
//SELECT @Value = COUNT(*), @AmountValue = SUM(CASE WHEN InvoiceAmount IS NULL THEN 0 ELSE InvoiceAmount END)
//FROM tbl_Invoice AS I WHERE SiteID = @SiteID AND CreatedAt >= @StartDate AND CreatedAt <= @EndDate AND InvoiceStatusID = {0}
//AND (SELECT COUNT(*) FROM tbl_Invoice WHERE InvoiceStatusID = {0} AND BuyerContactID IS NOT NULL AND BuyerContactID = I.BuyerContactID) > 1
//AND ID = 
//
//SELECT @PreviousValue = COUNT(*), @AmountPreviousValue = SUM(CASE WHEN InvoiceAmount IS NULL THEN 0 ELSE InvoiceAmount END)
//FROM tbl_Invoice AS I WHERE SiteID = @SiteID AND CreatedAt >= @PreviousStartDate AND CreatedAt <= @PreviousEndDate AND (InvoiceStatusID = {0} OR InvoiceStatusID = {1})
//AND EXISTS (SELECT BuyerContactID FROM tbl_Invoice WHERE SiteID = @SiteID AND BuyerContactID IS NOT NULL AND InvoiceStatusID = {2} AND BuyerContactID = I.BuyerContactID)
//
//EXEC [Statistic_Recalculate] @SiteID, 'RepeatSalesInvoiceForPaymentExposedCount', @Value, @PreviousValue
//EXEC [Statistic_Recalculate] @SiteID, 'RepeatSalesInvoiceForPaymentExposedAmount', @AmountValue, @AmountPreviousValue", (int)InvoiceStatus.Paid);

//                using (var command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.AddWithValue("@SiteID", siteId);
//                    command.Parameters.AddWithValue("@StartDate", startDate);
//                    command.Parameters.AddWithValue("@EndDate", endDate);
//                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
//                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
//                    command.ExecuteNonQuery();
//                }
//            }
//        }

        /// <summary>
        /// Visitors the source new anonymous advertising platform.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="previousStartDate">The previous start date.</param>
        /// <param name="previousEndDate">The previous end date.</param>
        public static void VisitorSourceNewAnonymousAdvertisingPlatform(Guid siteId, DateTime startDate, DateTime endDate, DateTime previousStartDate, DateTime previousEndDate)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                const string query = @"
UPDATE tbl_StatisticData
SET Value = 0, PreviousValue = 0
WHERE SiteID = @SiteID AND Code LIKE 'VisitorSourceNewAnonymousAdvertisingPlatform_%'

DECLARE @CurrentCode nvarchar(256)
DECLARE @Value int
DECLARE @AdvertisingPlatformStats TABLE
(
    Code nvarchar(256), 
    Cnt int
)

INSERT INTO @AdvertisingPlatformStats
SELECT 'VisitorSourceNewAnonymousAdvertisingPlatform_' + CAST(AP.ID AS varchar(50)) AS Code, COUNT(*) AS Cnt
FROM tbl_AdvertisingPlatform AS AP 
LEFT JOIN tbl_Contact AS C ON AP.ID = C.AdvertisingPlatformID
WHERE AP.SiteID = @SiteID AND C.CreatedAt >= @StartDate
 AND C.CreatedAt <= @EndDate
GROUP BY AP.ID

WHILE EXISTS(SELECT * FROM @AdvertisingPlatformStats)
BEGIN

SELECT TOP 1 @CurrentCode = Code, @Value = Cnt
FROM @AdvertisingPlatformStats

EXEC [Statistic_Recalculate] @SiteID, @CurrentCode, @Value, NULL

DELETE FROM @AdvertisingPlatformStats
WHERE Code = @CurrentCode

END

INSERT INTO @AdvertisingPlatformStats
SELECT 'VisitorSourceNewAnonymousAdvertisingPlatform_' + CAST(AP.ID AS varchar(50)) AS Code, COUNT(*) AS Cnt
FROM tbl_AdvertisingPlatform AS AP 
LEFT JOIN tbl_Contact AS C ON AP.ID = C.AdvertisingPlatformID
WHERE AP.SiteID = @SiteID AND C.CreatedAt >= @PreviousStartDate
 AND C.CreatedAt <= @PreviousEndDate
GROUP BY AP.ID

WHILE EXISTS(SELECT * FROM @AdvertisingPlatformStats)
BEGIN

SELECT TOP 1 @CurrentCode = Code, @Value = Cnt
FROM @AdvertisingPlatformStats

EXEC [Statistic_Recalculate] @SiteID, @CurrentCode, NULL, @Value

DELETE FROM @AdvertisingPlatformStats
WHERE Code = @CurrentCode

END";

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 300;
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    command.Parameters.AddWithValue("@PreviousStartDate", previousStartDate);
                    command.Parameters.AddWithValue("@PreviousEndDate", previousEndDate);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
