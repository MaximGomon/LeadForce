using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using WebCounter.BusinessLogicLayer.Configuration;

namespace Labitec.LeadForce.Portal
{
    public class Global : System.Web.HttpApplication
    {
        protected void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");            
            routes.MapPageRoute("lfp_home", "{portalSettingsId}", "~/Default.aspx");
            routes.MapPageRoute("lfp_discussions", "{portalSettingsId}/{tab}/Discussions", "~/Main/Discussions/ActivityRibbon.aspx");
            routes.MapPageRoute("lfp_knowledgebase", "{portalSettingsId}/{tab}/KnowledgeBase/{categoryId}", "~/Main/KnowledgeBase/KnowledgeBase.aspx", true, new RouteValueDictionary { { "categoryId", null } });
            //PublicationRepository, метод SearchPublication использует KnowledgeBase/Article
            routes.MapPageRoute("lfp_article", "{portalSettingsId}/{tab}/KnowledgeBase/Article/{id}", "~/Main/KnowledgeBase/Article.aspx");
            routes.MapPageRoute("lfp_discussion", "{portalSettingsId}/{tab}/Discussions/View/{id}", "~/Main/Discussions/Discussion.aspx");
            routes.MapPageRoute("lfp_tasks", "{portalSettingsId}/{tab}/Tasks", "~/Main/TaskModule/Tasks.aspx");
            routes.MapPageRoute("lfp_taskadd", "{portalSettingsId}/{tab}/Tasks/Add", "~/Main/TaskModule/Task.aspx");
            routes.MapPageRoute("lfp_taskedit", "{portalSettingsId}/{tab}/Tasks/Edit/{id}", "~/Main/TaskModule/Task.aspx");
            routes.MapPageRoute("lfp_taskpublic", "{portalSettingsId}/{tab}/Tasks/Public/{id}", "~/Main/TaskModule/PublicTask.aspx");
            routes.MapPageRoute("lfp_requests", "{portalSettingsId}/{tab}/Requests", "~/Main/RequestModule/Requests.aspx");
            routes.MapPageRoute("lfp_requestadd", "{portalSettingsId}/{tab}/Requests/Add", "~/Main/RequestModule/Request.aspx");
            routes.MapPageRoute("lfp_requestedit", "{portalSettingsId}/{tab}/Requests/Edit/{id}", "~/Main/RequestModule/Request.aspx");
            routes.MapPageRoute("lfp_requirements", "{portalSettingsId}/{tab}/Requirements", "~/Main/RequirementModule/Requirements.aspx");
            routes.MapPageRoute("lfp_requirementadd", "{portalSettingsId}/{tab}/Requirements/Add", "~/Main/RequirementModule/Requirement.aspx");
            //RequestNotificationTagsReplacer.cs, метод AddRequirementRow использует Requirements/Edit
            routes.MapPageRoute("lfp_requirementedit", "{portalSettingsId}/{tab}/Requirements/Edit/{id}", "~/Main/RequirementModule/Requirement.aspx");
            routes.MapPageRoute("lfp_invoices", "{portalSettingsId}/{tab}/Invoices", "~/Main/InvoiceModule/Invoices.aspx");
            routes.MapPageRoute("lfp_invoiceadd", "{portalSettingsId}/{tab}/Invoices/Add", "~/Main/InvoiceModule/Invoice.aspx");
            routes.MapPageRoute("lfp_invoiceedit", "{portalSettingsId}/{tab}/Invoices/Edit/{id}", "~/Main/InvoiceModule/Invoice.aspx");
            routes.MapPageRoute("lfp_invoiceprint", "{portalSettingsId}/{tab}/Invoices/Print/{id}", "~/Main/InvoiceModule/PrintInvoice.aspx");
            routes.MapPageRoute("lfp_agreement", "{portalSettingsId}/Agreement", "~/StaticPages/Agreement.aspx");
            routes.MapPageRoute("lfp_accessdenied", "{portalSettingsId}/AccessDenied", "~/StaticPages/AccessDenied.aspx");
            routes.MapPageRoute("lfp_activate", "{portalSettingsId}/Activate/{id}", "~/Handlers/ActivateUser.aspx");
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