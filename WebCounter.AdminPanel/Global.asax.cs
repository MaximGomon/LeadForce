using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel
{
    public class Global : System.Web.HttpApplication
    {
        protected void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute("ap_home", "LeadForce/Main/List", "~/Default.aspx");

            routes.MapPageRoute("ap_companies", "{tab}/Companies/List", "~/Companies.aspx");
            routes.MapPageRoute("ap_company", "{tab}/Companies/Edit/{id}", "~/Company.aspx");
            routes.MapPageRoute("ap_companyadd", "{tab}/Companies/Add", "~/Company.aspx");

            routes.MapPageRoute("ap_discussionportalsetting", "{tab}/Discussions/PortalSetting", "~/ModuleEditionAction.aspx");
            routes.MapPageRoute("ap_domainsettings", "{tab}/DomainSettings/List", "~/DomainSettings.aspx");

            routes.MapPageRoute("ap_contacts", "{tab}/Contacts/List", "~/Contacts.aspx");
            routes.MapPageRoute("ap_contact", "{tab}/Contacts/View/{id}", "~/Contact.aspx");
            routes.MapPageRoute("ap_contactadd", "{tab}/Contacts/Add", "~/ContactAdd.aspx");

            routes.MapPageRoute("ap_sitecolumns", "{tab}/SiteColumns/List", "~/SiteColumns.aspx");
            routes.MapPageRoute("ap_sitecolumn", "{tab}/SiteColumns/Edit/{id}", "~/SiteColumn.aspx");
            routes.MapPageRoute("ap_sitecolumnadd", "{tab}/SiteColumns/Add", "~/SiteColumn.aspx");

            routes.MapPageRoute("ap_siteactivityrules", "{tab}/SiteActivityRules/List/{ruletypeid}", "~/SiteActivityRules.aspx", true, new RouteValueDictionary { { "ruletypeid", "null" } });

            routes.MapPageRoute("ap_siteactivityrules_form", "{tab}/Form/List/{ruletypeid}", "~/SiteActivityRuleForms.aspx", true, new RouteValueDictionary { { "ruletypeid", "3" } });
            routes.MapPageRoute("ap_siteactivityrules_link", "{tab}/Link/List/", "~/Links.aspx");
            routes.MapPageRoute("ap_siteactivityrules_file", "{tab}/File/List/{ruletypeid}", "~/SiteActivityRuleFiles.aspx", true, new RouteValueDictionary { { "ruletypeid", "5" } });
            routes.MapPageRoute("ap_siteactivityrules_externalform", "{tab}/Form/List/{ruletypeid}", "~/SiteActivityRuleForms.aspx", true, new RouteValueDictionary { { "ruletypeid", "6" } });
            routes.MapPageRoute("ap_siteactivityrules_lpgenerator", "{tab}/Form/List/{ruletypeid}", "~/SiteActivityRuleForms.aspx", true, new RouteValueDictionary { { "ruletypeid", "8" } });            

            routes.MapPageRoute("ap_siteactivityrule_form", "{tab}/Form/Edit/{id}/{ruletypeid}", "~/SiteActivityRule.aspx", true, new RouteValueDictionary { { "ruletypeid", "3" } });
            routes.MapPageRoute("ap_siteactivityrule_link", "{tab}/Link/Edit/{id}", "~/Link.aspx");
            routes.MapPageRoute("ap_siteactivityrule_file", "{tab}/File/Edit/{id}/{ruletypeid}", "~/SiteActivityRule.aspx", true, new RouteValueDictionary { { "ruletypeid", "5" } });
            routes.MapPageRoute("ap_siteactivityrule_externalform", "{tab}/Form/Edit/{id}/6", "~/SiteActivityRule.aspx", true, new RouteValueDictionary { { "ruletypeid", "6" } });
            routes.MapPageRoute("ap_siteactivityrule_lpgenerator", "{tab}/Form/Edit/{id}/8", "~/SiteActivityRule.aspx", true, new RouteValueDictionary { { "ruletypeid", "8" } });
            routes.MapPageRoute("ap_siteactivityrule_wufooform", "{tab}/Form/Edit/{id}/7", "~/SiteActivityRule.aspx");

            routes.MapPageRoute("ap_siteactivityruleadd_form", "{tab}/Form/Add/{ruletypeid}", "~/SiteActivityRule.aspx", true, new RouteValueDictionary { { "ruletypeid", "3" } });
            routes.MapPageRoute("ap_siteactivityruleadd_link", "{tab}/Link/Add", "~/Link.aspx");
            routes.MapPageRoute("ap_siteactivityruleadd_file", "{tab}/File/Add/{ruletypeid}", "~/SiteActivityRule.aspx", true, new RouteValueDictionary { { "ruletypeid", "5" } });
            routes.MapPageRoute("ap_siteactivityrulewizard_form", "{tab}/Form/Wizard/{id}", "~/Wizards/FormsWizard.aspx", true, new RouteValueDictionary { { "id", "null" } });

            routes.MapPageRoute("ap_siteactivityruleadd_externalform", "{tab}/Form/Add/6", "~/SiteActivityRule.aspx", true, new RouteValueDictionary { { "ruletypeid", "6" } });

            routes.MapPageRoute("ap_siteactivityruleadd_lpgenerator", "{tab}/Form/Add/8", "~/SiteActivityRule.aspx", true, new RouteValueDictionary { { "ruletypeid", "8" } });

            routes.MapPageRoute("ap_contactactivity", "{tab}/ContactActivity/List/{id}", "~/ContactActivity.aspx", true, new RouteValueDictionary { { "id", "null" } });

            routes.MapPageRoute("ap_siteaction", "{tab}/SiteAction/List/{id}", "~/SiteAction.aspx", true, new RouteValueDictionary { { "id", "null" } });
            routes.MapPageRoute("ap_siteactionmessage", "{tab}/SiteAction/Message/{id}", "~/SiteActionMessage.aspx");

            routes.MapPageRoute("ap_siteeventtemplates", "{tab}/SiteEventTemplates/List", "~/SiteEventTemplates.aspx");
            routes.MapPageRoute("ap_siteeventtemplate", "{tab}/SiteEventTemplates/Edit/{id}", "~/SiteEventTemplate.aspx");
            routes.MapPageRoute("ap_siteeventtemplateadd", "{tab}/SiteEventTemplates/Add", "~/SiteEventTemplate.aspx");

            routes.MapPageRoute("ap_siteactiontemplates", "{tab}/SiteActionTemplates/List", "~/SiteActionTemplates.aspx");
            routes.MapPageRoute("ap_siteactiontemplate", "{tab}/SiteActionTemplates/Edit/{id}", "~/SiteActionTemplate.aspx");
            routes.MapPageRoute("ap_siteactiontemplateadd", "{tab}/SiteActionTemplates/Add", "~/SiteActionTemplate.aspx");

            routes.MapPageRoute("ap_contactsession", "{tab}/Contacts/Session/View/{id}", "~/ContactSession.aspx", true, new RouteValueDictionary { { "id", "null" } });

            routes.MapPageRoute("ap_settings", "{tab}/SystemSettings/List", "~/Settings.aspx");

            routes.MapPageRoute("ap_pssitesadd", "{tab}/Sites/PortalSettings/Add/{siteId}", "~/PortalSetting.aspx");
            routes.MapPageRoute("ap_pssitesedit", "{tab}/Sites/PortalSettings/Edit/{id}", "~/PortalSetting.aspx");

            routes.MapPageRoute("ap_pssettingsadd", "{tab}/SystemSettings/PortalSettings/Add/{siteId}", "~/PortalSetting.aspx");
            routes.MapPageRoute("ap_pssettingsedit", "{tab}/SystemSettings/PortalSettings/Edit/{id}", "~/PortalSetting.aspx");

            routes.MapPageRoute("ap_sdsitesadd", "{tab}/Sites/SiteDomains/Add/{siteId}", "~/SiteDomain.aspx");
            routes.MapPageRoute("ap_sdsitesedit", "{tab}/Sites/SiteDomains/Edit/{id}", "~/SiteDomain.aspx");

            routes.MapPageRoute("ap_sdsettingsadd", "{tab}/SystemSettings/SiteDomains/Add/{siteId}", "~/SiteDomain.aspx");
            routes.MapPageRoute("ap_sdsettingsedit", "{tab}/SystemSettings/SiteDomains/Edit/{id}", "~/SiteDomain.aspx");

            routes.MapPageRoute("ap_sites", "{tab}/Sites/List", "~/Sites.aspx");
            routes.MapPageRoute("ap_siteadd", "{tab}/Sites/Add", "~/SiteEdit.aspx");
            routes.MapPageRoute("ap_siteedit", "{tab}/Sites/Edit/{id}", "~/SiteEdit.aspx");

            routes.MapPageRoute("ap_sourcemonitorings", "{tab}/SourceMonitorings/List", "~/SourceMonitorings.aspx");
            routes.MapPageRoute("ap_sourcemonitoringadd", "{tab}/SourceMonitorings/Add", "~/SourceMonitoring.aspx");
            routes.MapPageRoute("ap_sourcemonitoringedit", "{tab}/SourceMonitorings/Edit/{id}", "~/SourceMonitoring.aspx");

            routes.MapPageRoute("ap_orders", "{tab}/Orders/List", "~/Orders.aspx");
            routes.MapPageRoute("ap_orderadd", "{tab}/Orders/Add", "~/Order.aspx");
            routes.MapPageRoute("ap_orderedit", "{tab}/Orders/Edit/{id}", "~/Order.aspx");

            routes.MapPageRoute("ap_tasks", "{tab}/Tasks/List", "~/Tasks.aspx");
            routes.MapPageRoute("ap_taskadd", "{tab}/Tasks/Add", "~/Task.aspx");
            routes.MapPageRoute("ap_taskedit", "{tab}/Tasks/Edit/{id}", "~/Task.aspx");

            routes.MapPageRoute("ap_dictionaries", "{tab}/Dictionaries/List", "~/Dictionaries.aspx");

            routes.MapPageRoute("ap_massmails", "{tab}/MassMails/List", "~/MassMails.aspx");
            routes.MapPageRoute("ap_massmail", "{tab}/MassMails/Edit/{id}", "~/MassMail.aspx");
            routes.MapPageRoute("ap_massmailadd", "{tab}/MassMails/Add", "~/MassMail.aspx");

            routes.MapPageRoute("ap_invoiceadd", "{tab}/Invoices/Add", "~/Invoice.aspx");
            routes.MapPageRoute("ap_invoices", "{tab}/Invoices/List", "~/Invoices.aspx");
            routes.MapPageRoute("ap_invoiceedit", "{tab}/Invoices/Edit/{id}", "~/Invoice.aspx");

            routes.MapPageRoute("ap_shipmentadd", "{tab}/Shipments/Add", "~/Shipment.aspx");
            routes.MapPageRoute("ap_shipments", "{tab}/Shipments/List", "~/Shipments.aspx");
            routes.MapPageRoute("ap_shipmentedit", "{tab}/Shipments/Edit/{id}", "~/Shipment.aspx");

            routes.MapPageRoute("ap_requestadd", "{tab}/Requests/Add", "~/Request.aspx");
            routes.MapPageRoute("ap_requests", "{tab}/Requests/List", "~/Requests.aspx");
            routes.MapPageRoute("ap_requestedit", "{tab}/Requests/Edit/{id}", "~/Request.aspx");

            routes.MapPageRoute("ap_requirementadd", "{tab}/Requirements/Add", "~/Requirement.aspx");
            routes.MapPageRoute("ap_requirements", "{tab}/Requirements/List", "~/Requirements.aspx");
            routes.MapPageRoute("ap_requirementedit", "{tab}/Requirements/Edit/{id}", "~/Requirement.aspx");

            routes.MapPageRoute("ap_productadd", "{tab}/Products/Add", "~/Product.aspx");
            routes.MapPageRoute("ap_products", "{tab}/Products/List/{categoryId}", "~/Products.aspx", true, new RouteValueDictionary { { "categoryId", null } });
            routes.MapPageRoute("ap_product", "{tab}/Products/Edit/{id}", "~/Product.aspx");


            routes.MapPageRoute("ap_imports", "{tab}/Imports/List", "~/Imports.aspx");
            routes.MapPageRoute("ap_importadd", "{tab}/Imports/Add", "~/ImportEdit.aspx");
            routes.MapPageRoute("ap_importedit", "{tab}/Imports/Edit/{id}", "~/ImportEdit.aspx");

            routes.MapPageRoute("ap_users", "{tab}/Users/List", "~/Users.aspx");
            routes.MapPageRoute("ap_useradd", "{tab}/Users/Add", "~/UserEdit.aspx");
            routes.MapPageRoute("ap_useredit", "{tab}/Users/Edit/{id}", "~/UserEdit.aspx");

            routes.MapPageRoute("ap_accessprofiles", "{tab}/AccessProfiles/List", "~/AccessProfiles.aspx");
            routes.MapPageRoute("ap_profilesites", "{tab}/ProfileSites/List", "~/ProfileSites.aspx");

            routes.MapPageRoute("ap_publicationadd", "{tab}/Publications/Add", "~/Publication.aspx");
            routes.MapPageRoute("ap_publications", "{tab}/Publications/List", "~/Publications.aspx");
            routes.MapPageRoute("ap_publication", "{tab}/Publications/Edit/{id}", "~/Publication.aspx");

            routes.MapPageRoute("ap_knowledgebaseadd", "{tab}/KnowledgeBase/Add", "~/KnowledgeBase.aspx");
            routes.MapPageRoute("ap_knowledgebases", "{tab}/KnowledgeBase/List/{categoryId}", "~/KnowledgeBases.aspx", true, new RouteValueDictionary { { "categoryId", null } });
            routes.MapPageRoute("ap_knowledgebase", "{tab}/KnowledgeBase/Edit/{id}", "~/KnowledgeBase.aspx");

            routes.MapPageRoute("ap_discussions", "{tab}/Discussions/List", "~/Discussions.aspx");

            routes.MapPageRoute("ap_workflowtemplates", "{tab}/WorkflowTemplates/List", "~/WorkflowTemplates.aspx");
            routes.MapPageRoute("ap_workflowtemplateadd", "{tab}/WorkflowTemplates/Add", "~/WorkflowTemplate.aspx");
            routes.MapPageRoute("ap_workflowtemplate", "{tab}/WorkflowTemplates/Edit/{id}", "~/WorkflowTemplate.aspx");
            routes.MapPageRoute("ap_workflowtemplatewizard", "{tab}/WorkflowTemplates/Wizard/{id}", "~/Wizards/WorkflowTemplateWizard.aspx", true, new RouteValueDictionary { { "id", "null" } });

            routes.MapPageRoute("ap_workflows", "{tab}/Workflows/List", "~/Workflows.aspx");
            routes.MapPageRoute("ap_workflow", "{tab}/Workflows/Edit/{id}", "~/Workflow.aspx");

            routes.MapPageRoute("ap_massworkflows", "{tab}/MassWorkflows/List", "~/MassWorkflows.aspx");
            routes.MapPageRoute("ap_massworkflowadd", "{tab}/MassWorkflows/Add", "~/MassWorkflow.aspx");
            routes.MapPageRoute("ap_massworkflow", "{tab}/MassWorkflows/Edit/{id}", "~/MassWorkflow.aspx");

            routes.MapPageRoute("ap_analytics", "{tab}/Analytics/List", "~/Analytics.aspx");

            routes.MapPageRoute("ap_payments", "{tab}/Payments/List", "~/Payments.aspx");
            routes.MapPageRoute("ap_paymentadd", "{tab}/Payments/Add", "~/Payment.aspx");
            routes.MapPageRoute("ap_payment", "{tab}/Payments/Edit/{id}", "~/Payment.aspx");
            routes.MapPageRoute("ap_paymentservices", "{tab}/PaymentServices/List", "~/PaymentServices.aspx");

            routes.MapPageRoute("ap_contactsegments", "{tab}/ContactSegments/List", "~/ContactSegments.aspx");
            routes.MapPageRoute("ap_contactsegmentadd", "{tab}/ContactSegments/Add", "~/ContactSegment.aspx");
            routes.MapPageRoute("ap_contactsegment", "{tab}/ContactSegments/Edit/{id}", "~/ContactSegment.aspx");

            routes.MapPageRoute("ap_websites", "{tab}/WebSites/List", "~/WebSites.aspx");
            routes.MapPageRoute("ap_websiteadd", "{tab}/WebSites/Add", "~/WebSite.aspx");
            routes.MapPageRoute("ap_website", "{tab}/WebSites/Edit/{id}", "~/WebSite.aspx");
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