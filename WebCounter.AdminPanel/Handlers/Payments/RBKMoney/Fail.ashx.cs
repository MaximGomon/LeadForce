using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Providers.Payments.RBKMoney;

namespace WebCounter.AdminPanel.Handlers.Payments.RBKMoney
{
    /// <summary>
    /// Summary description for Fail
    /// </summary>
    public class Fail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            RBKMoneyProvider.Fail(context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}