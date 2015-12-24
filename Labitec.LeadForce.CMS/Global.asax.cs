using System;
using System.Web.Routing;
using WebCounter.BusinessLogicLayer.Configuration;

namespace Labitec.LeadForce.CMS
{
    public class Global : System.Web.HttpApplication
    {
        protected void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.MapPageRoute("lfcms_home", "{webSiteId}", "~/Default.aspx", true, new RouteValueDictionary { { "webSiteId", null } }, new RouteValueDictionary { { "webSiteId", @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9‌​a-fA-F]{12}(\}){0,1}$" } });
            routes.MapPageRoute("lfcms_staticpage", "{link}/{webSiteId}/{*extrainfo}", "~/StaticPage.aspx", true, new RouteValueDictionary { { "webSiteId", null } }, new RouteValueDictionary { { "webSiteId", @"(^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9‌​a-fA-F]{12}(\}){0,1}$)|(^\s*$)" } });
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
            Log.Error("Application Error", objErr);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}