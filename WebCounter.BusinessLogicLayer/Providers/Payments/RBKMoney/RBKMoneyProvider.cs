using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Providers.Payments.RBKMoney
{
    public class RBKMoneyProvider
    {
        public static string EshopId = "2014918";
        public static string SecretKey = "";

        public static string Pay(tbl_Payment payment, tbl_Sites site)
        {
            string formName = "rbkmoneyform";

            var dataManager = new DataManager();

            var user = dataManager.User.SelectById(site.MainUserID.Value);

            var sb = new StringBuilder();
            sb.Append(string.Format("<form name='{0}' method='post' action='https://rbkmoney.ru/acceptpurchase.aspx'>",
                                    formName));
            sb.Append(string.Format("<input type='hidden' name='eshopId' value='{0}' />", EshopId));
            sb.Append(string.Format("<input type='hidden' name='user_email' value='{0}' />", user.Login));
            sb.Append(string.Format("<input type='hidden' name='orderId' value='{0}' />", payment.ID));
            sb.Append(string.Format("<input type='hidden' name='serviceName' value='{0}' />", payment.Assignment));
            sb.Append(string.Format("<input type='hidden' name='recipientAmount' value='{0}' />", payment.Total));
            sb.Append("<input type='hidden' name='recipientCurrency' value='RUR' />");
            sb.Append("</form>");

            var result = string.Format("$('body').append(\"{0}\");document.{1}.submit();", sb.ToString(), formName);

            return result;
        }



        /// <summary>
        /// Successes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Success(HttpContext context)
        {
            var sbParams = new StringBuilder();

            foreach (string key in context.Request.Params.AllKeys)
                sbParams.AppendLine(string.Format("{0} = {1}", key, context.Request[key]));

            Log.Error("Request params : " + sbParams);        

            var response = RBKMoneyRequest.Parse(context);
            var dataManager = new DataManager();
            var payment = dataManager.Payment.SelectById(response.orderId);
            var toHash =
                string.Format("{0}::{1}::{2}::{3}::{4}::{5}::{6}::{7}::{8}::{9}::{10}", EshopId, payment.ID,
                              payment.Assignment,
                              response.eshopAccount, payment.Total.ToString("##.##").Replace(",", "."), "RUR", response.paymentStatus,
                              response.userName, response.userEmail,
                              response.paymentData.ToString("yyyy-MM-dd HH:mm:ss"), SecretKey).ToLower();

            Log.Debug("Строка для контрольной суммы : " + toHash);
    
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, toHash);
                if (response.hash != hash)
                {
                    Log.Error("Контрольные суммы не совпадают");
                }
                else
                {
                    //response.paymentStatus == 5
                    //payment.StatusID = 
                }
            }            

            context.Response.StatusCode = 200;
        }



        /// <summary>
        /// Fails the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Fail(HttpContext context)
        {
            var sbParams = new StringBuilder();

            foreach (string key in context.Request.Params.AllKeys)
                sbParams.AppendLine(string.Format("{0} = {1}", key, context.Request[key]));

            Log.Error("Ошибка оплаты: " + sbParams.ToString());

            context.Response.StatusCode = 200;
        }



        /// <summary>
        /// Gets the MD5 hash.
        /// </summary>
        /// <param name="md5Hash">The MD5 hash.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        protected static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }



    public class RBKMoneyRequest
    {
        public string eshopId { get; set; }
        public Guid orderId { get; set; }
        public string serviceName { get; set; }
        public string eshopAccount { get; set; }
        public decimal recipientAmount { get; set; }
        public string recipientCurrency { get; set; }
        public string hash { get; set; }
        public int paymentStatus { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public DateTime paymentData { get; set; }
        public string MESSAGE { get; set; }
        public string ERR { get; set; }



        /// <summary>
        /// Parses the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static RBKMoneyRequest Parse(HttpContext context)
        {
            var result = new RBKMoneyRequest
                             {
                                 eshopId = context.Request["eshopId"],
                                 orderId = Guid.Parse(context.Request["orderId"]),
                                 serviceName = context.Request["serviceName"],
                                 eshopAccount = context.Request["eshopAccount"],
                                 recipientAmount = decimal.Parse(context.Request["recipientAmount"]),
                                 recipientCurrency = context.Request["recipientCurrency"],
                                 paymentStatus = int.Parse(context.Request["paymentStatus"]),
                                 userName = context.Request["userName"],
                                 userEmail = context.Request["userEmail"],
                                 paymentData = DateTime.Parse(context.Request["paymentData"]),
                                 hash = context.Request["hash"]
                             };

            if (context.Request["MESSAGE"] != null)
                result.MESSAGE = context.Request["MESSAGE"];

            if (context.Request["ERR"] != null)
                result.ERR = context.Request["ERR"];

            return result;
        }        
    }
}
//Номер сайта Участника ( eshopId );
//Внутренний номер операции Участника ( orderId );
//Описание операции ( serviceName );
//Идентификатор учетной записи Участника ( eshopAccount );
//Сумма операции ( recipientAmount );
//Валюта операции ( recipientCurrency );
//Статус операции ( paymentStatus );
//Имя Пользователя ( userName );
//Email Пользователя ( userEmail );
//Дата и время выполнения операции ( paymentData );
//Секретный ключ ( secretKey ) (Передается, если URL Оповещение о Переводе обеспечивает секретность );