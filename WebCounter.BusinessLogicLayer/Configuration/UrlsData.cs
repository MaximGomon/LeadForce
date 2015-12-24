using System;
using System.Web;
using System.Web.UI;
using System.Web.Routing;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.BusinessLogicLayer.Configuration
{
    public static class UrlsData
    {
        public static string GetCurrentTab()
        {            
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Split(new[] {'/'})[1];
        }


        #region Admin Panel

        /// <summary>
        /// Admin Panel home page.
        /// </summary>
        /// <returns>
        /// url to admin panel home page
        /// </returns>
        public static string AP_Home()
        {
            return GetRouteUrl(null, "ap_home");
        }


        public static string AP_DomainSettings()
        {
            return GetRouteUrl(null, "ap_domainsettings");
        }        
                
        

        /// <summary>
        /// Company list page.
        /// </summary>
        /// <returns></returns>
        public static string AP_Companies(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_companies", new { tab });
        }



        /// <summary>
        /// Company details url.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static string AP_Company(Guid ID, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_company", new { id = ID, tab });
        }



        /// <summary>
        /// Add company page.
        /// </summary>
        /// <returns></returns>
        public static string AP_CompanyAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_companyadd", new { tab });
        }



        /// <summary>
        /// Get contacts list page
        /// </summary>
        /// <returns>        
        /// url to admin panel site user page
        /// </returns>
        public static string AP_Contacts(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_contacts", new { tab });
        }



        /// <summary>
        /// Get contact info page
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static string AP_Contact(Guid ID, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_contact", new { id = ID, tab });
        }



        /// <summary>
        /// As the p_ site user add.
        /// </summary>
        /// <returns></returns>
        public static string AP_ContactAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_contactadd", new { tab });
        }




        /// <summary>
        /// As the p_ site user session.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static string AP_ContactSession(Guid ID, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_contactsession", new { id = ID, tab });
        }



        /// <summary>
        /// Admin Panel Site columns page.
        /// </summary>
        /// <returns></returns>
        public static string AP_SiteColumns(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_sitecolumns", new { tab });
        }



        /// <summary>
        /// As the p_ site column.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static string AP_SiteColumn(Guid ID, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_sitecolumn", new { id = ID, tab });
        }



        /// <summary>
        /// Admin Panel Site column add page.
        /// </summary>
        /// <returns></returns>
        public static string AP_SiteColumnAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_sitecolumnadd", new { tab });
        }
        


        /// <summary>
        /// As the p_ site activity rules.
        /// </summary>
        /// <returns></returns>
        public static string AP_SiteActivityRules(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteactivityrules", new { tab });
        }



        /// <summary>
        /// Get form wizard form url
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_FormWizard(Guid? id = null, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteactivityrulewizard_form", new { id, tab });
        }


        /// <summary>
        /// As the p_ site activity rules.
        /// </summary>
        /// <param name="ruleTypeId">The rule type id.</param>
        /// <returns></returns>
        public static string AP_SiteActivityRules(int ruleTypeId, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            switch ((RuleType)ruleTypeId)
            {
                case RuleType.Form:
                    return GetRouteUrl(null, "ap_siteactivityrules_form", new { tab });
                case RuleType.WufooForm:
                    return GetRouteUrl(null, "ap_siteactivityrules_form", new { tab });
                case RuleType.File:
                    return GetRouteUrl(null, "ap_siteactivityrules_file", new { tab });
                case RuleType.Link:
                    return GetRouteUrl(null, "ap_siteactivityrules_link", new { tab });
                case RuleType.ExternalForm:
                    return GetRouteUrl(null, "ap_siteactivityrules_externalform", new { tab });
                case RuleType.LPgenerator:
                    return GetRouteUrl(null, "ap_siteactivityrules_lpgenerator", new { tab });
            }

            return GetRouteUrl(null, "ap_siteactivityrules", new { ruletypeid = ruleTypeId, tab });
        }




        /// <summary>
        /// As the p_ site activity rule add.
        /// </summary>
        /// <param name="ruleTypeId">The rule type id.</param>
        /// <returns></returns>
        public static string AP_SiteActivityRuleAdd(int ruleTypeId, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            switch ((RuleType)ruleTypeId)
            {
                case RuleType.Form:
                    return GetRouteUrl(null, "ap_siteactivityruleadd_form", new { tab });
                case RuleType.File:
                    return GetRouteUrl(null, "ap_siteactivityruleadd_file", new { tab });
                case RuleType.Link:
                    return GetRouteUrl(null, "ap_siteactivityruleadd_link", new { tab });
                case RuleType.ExternalForm:
                    return GetRouteUrl(null, "ap_siteactivityruleadd_externalform", new { tab });
                case RuleType.LPgenerator:
                    return GetRouteUrl(null, "ap_siteactivityruleadd_lpgenerator", new { tab });
            }

            return string.Empty;
        }



        /// <summary>
        /// As the p_ site activity rule.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <param name="ruleTypeId">The rule type id.</param>
        /// <returns></returns>
        public static string AP_SiteActivityRule(Guid ID, int ruleTypeId, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            switch ((RuleType)ruleTypeId)
            {
                case RuleType.WufooForm:
                    return GetRouteUrl(null, "ap_siteactivityrule_wufooform", new { id = ID, tab });
                case RuleType.Form:
                    return GetRouteUrl(null, "ap_siteactivityrule_form", new { id = ID, tab });
                case RuleType.File:
                    return GetRouteUrl(null, "ap_siteactivityrule_file", new { id = ID, tab });
                case RuleType.Link:
                    return GetRouteUrl(null, "ap_siteactivityrule_link", new { id = ID, tab });
                case RuleType.ExternalForm:
                    return GetRouteUrl(null, "ap_siteactivityrule_externalform", new { id = ID, tab });
                case RuleType.LPgenerator:
                    return GetRouteUrl(null, "ap_siteactivityrule_lpgenerator", new { id = ID, tab });
            }

            return string.Empty;
        }





        /// <summary>
        /// Portal settings add.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_PortalSettingsAdd(string module, Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            switch (module)
            {
                case "Settings":
                    return GetRouteUrl(null, "ap_pssettingsadd", new { tab, siteId = id });
                case "Sites":
                    return GetRouteUrl(null, "ap_pssitesadd", new { tab, siteId = id });
            }

            return string.Empty;
        }



        /// <summary>
        /// Portal settings edit.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="id">The id.</param>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_PortalSettingsEdit(string module, Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            switch (module)
            {
                case "Settings":
                    return GetRouteUrl(null, "ap_pssettingsedit", new { tab, id });
                case "Sites":
                    return GetRouteUrl(null, "ap_pssitesedit", new { tab, id });
            }

            return string.Empty;
        }


        /// <summary>
        /// Site domain add.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_SiteDomainsAdd(string module, Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            switch (module)
            {
                case "Settings":
                    return GetRouteUrl(null, "ap_sdsettingsadd", new { tab, siteId = id });
                case "Sites":
                    return GetRouteUrl(null, "ap_sdsitesadd", new { tab, siteId = id });
            }

            return string.Empty;
        }



        /// <summary>
        /// Site domains edit.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="id">The id.</param>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_SiteDomainsEdit(string module, Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            switch (module)
            {
                case "Settings":
                    return GetRouteUrl(null, "ap_sdsettingsedit", new { tab, id });
                case "Sites":
                    return GetRouteUrl(null, "ap_sdsitesedit", new { tab, id });
            }

            return string.Empty;
        }



        /// <summary>
        /// As the p_ site user activity.
        /// </summary>
        /// <returns></returns>
        public static string AP_ContactActivity(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_contactactivity", new { tab });
        }



        /// <summary>
        /// As the p_ site user activity.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static string AP_ContactActivity(Guid ID, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_contactactivity", new { id = ID, tab });
        }



        /// <summary>
        /// As the p_ site user activity.
        /// </summary>
        /// <returns></returns>
        public static string AP_SiteAction(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteaction", new { tab });
        }



        /// <summary>
        /// As the p_ site user activity.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static string AP_SiteAction(Guid ID, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteaction", new { id = ID, tab });
        }



        /// <summary>
        /// Get site action message page url.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_SiteActionMessage(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteaction,essage", new { id, tab });
        }



        /// <summary>
        /// As the p_ site event templates.
        /// </summary>
        /// <returns></returns>
        public static string AP_SiteEventTemplates(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteeventtemplates", new { tab });
        }



        /// <summary>
        /// As the p_ site event template add.
        /// </summary>
        /// <returns></returns>
        public static string AP_SiteEventTemplateAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteeventtemplateadd", new { tab });
        }



        /// <summary>
        /// As the p_ site event template.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static string AP_SiteEventTemplate(Guid ID, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteeventtemplate", new { id = ID, tab });
        }



        /// <summary>
        /// As the p_ site action templates.
        /// </summary>
        /// <returns></returns>
        public static string AP_SiteActionTemplates(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteactiontemplates", new { tab });
        }



        /// <summary>
        /// As the p_ site action template add.
        /// </summary>
        /// <returns></returns>
        public static string AP_SiteActionTemplateAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteactiontemplateadd", new { tab });
        }



        /// <summary>
        /// As the p_ site action template.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static string AP_SiteActionTemplate(Guid ID, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteactiontemplate", new { id = ID, tab });            
        }



        /// <summary>
        /// As the p_ settings.
        /// </summary>
        /// <returns></returns>
        public static string AP_Settings(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_settings", new { tab });
        }




        /// <summary>
        /// As the p_ mass mail add.
        /// </summary>
        /// <returns></returns>
        public static string AP_MassMailAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_massmailadd", new { tab });
        }



        /// <summary>
        /// As the p_ mass mail.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static string AP_MassMail(Guid ID, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_massmail", new { id = ID, tab });
        }



        /// <summary>
        /// As the p_ mass mail.
        /// </summary>
        /// <returns></returns>
        public static string AP_MassMails(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_massmails", new { tab });
        }



        /// <summary>
        /// Get dictionaries page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Dictionaries(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_dictionaries", new { tab });
        }



        /// <summary>
        /// Get site list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Sites(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_sites", new { tab });
        }



        /// <summary>
        /// Get site add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_SiteAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteadd", new { tab });
        }



        /// <summary>
        /// Get site edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_SiteEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_siteedit", new { id, tab });
        }



        /// <summary>
        /// Get source monitoring list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_SourceMonitorings(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_sourcemonitorings", new { tab });
        }



        /// <summary>
        /// Get source monitoring add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_SourceMonitoringAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_sourcemonitoringadd", new { tab });
        }



        /// <summary>
        /// Get source monitoring edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_SourceMonitoringEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_sourcemonitoringedit", new { id, tab });
        }


        /// <summary>
        /// Get orders list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Orders(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_orders", new { tab });
        }



        /// <summary>
        /// Get order add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_OrderAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_orderadd", new { tab });
        }



        /// <summary>
        /// Get order edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_OrderEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_orderedit", new { id, tab });
        }


        /// <summary>
        /// Get invoices list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Invoices(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_invoices", new { tab });
        }



        /// <summary>
        /// Get invoice add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_InvoiceAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_invoiceadd", new { tab });
        }



        /// <summary>
        /// Get invoice edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_InvoiceEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_invoiceedit", new { id, tab });
        }


        /// <summary>
        /// Get shipments list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Shipments(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_shipments", new { tab });
        }



        /// <summary>
        /// Get shipment add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_ShipmentAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_shipmentadd", new { tab });
        }



        /// <summary>
        /// Get shipment edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_ShipmentEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_shipmentedit", new { id, tab });
        }



        /// <summary>
        /// Get requests list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Requests(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_requests", new { tab });
        }



        /// <summary>
        /// Get request add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_RequestAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_requestadd", new { tab });
        }



        /// <summary>
        /// Get request edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_RequestEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_requestedit", new { id, tab });
        }



        /// <summary>
        /// Get requirements list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Requirements(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_requirements", new { tab });
        }



        /// <summary>
        /// Get requirement add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_RequirementAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_requirementadd", new { tab });
        }



        /// <summary>
        /// Get requirement edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_RequirementEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_requirementedit", new { id, tab });
        }



        /// <summary>
        /// Get tasks list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Tasks(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_tasks", new { tab });
        }



        /// <summary>
        /// Get task add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_TaskAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_taskadd", new { tab });
        }



        /// <summary>
        /// Get order edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_TaskEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_taskedit", new { id, tab });
        }



        /// <summary>
        /// Get product list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Products(Guid categoryId, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_products", new { categoryId, tab });
        }



        /// <summary>
        /// Get product list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Products(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_products", new { tab });
        }



        /// <summary>
        /// Get product add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_ProductAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_productadd", new { tab });
        }



        /// <summary>
        /// Get product edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_ProductEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_product", new { id, tab });
        }



        /// <summary>
        /// Get import list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Imports(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_imports", new { tab });
        }



        /// <summary>
        /// Get import add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_ImportAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_importadd", new { tab });
        }



        /// <summary>
        /// Get import edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_ImportEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_importedit", new { id, tab });
        }


        /// <summary>
        /// Get user list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Users(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_users", new { tab });
        }



        /// <summary>
        /// Get user add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_UserAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_useradd", new { tab });
        }



        /// <summary>
        /// Get user edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_UserEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_useredit", new { id, tab });
        }



        /// <summary>
        /// Get publication list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Publications(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_publications", new { tab });
        }



        /// <summary>
        /// Get publication add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_PublicationAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_publicationadd", new { tab });
        }



        /// <summary>
        /// Get publication edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_PublicationEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_publication", new { id, tab });
        }



        /// <summary>
        /// Get Knowledge Base list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_KnowledgeBase(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_knowledgebases", new { tab });
        }



        /// <summary>
        /// Get Knowledge Base list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_KnowledgeBase(Guid categoryId, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_knowledgebases", new { categoryId, tab });
        }



        /// <summary>
        /// Get Knowledge Base add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_KnowledgeBaseAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_knowledgebaseadd", new { tab });
        }



        /// <summary>
        /// Get Knowledge Base edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_KnowledgeBaseEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_knowledgebase", new { id, tab });
        }


        /// <summary>
        /// Get discussions list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Discussions(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_discussions", new { tab });
        }



        /// <summary>
        /// Get Workflow Templates list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_WorkflowTemplates(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_workflowtemplates", new { tab });
        }



        /// <summary>
        /// Get Workflow Templates add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_WorkflowTemplateAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_workflowtemplateadd", new { tab });
        }



        /// <summary>
        /// Get Workflow Templates edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_WorkflowTemplateEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_workflowtemplate", new { id, tab });
        }



        /// <summary>
        /// As the p_ workflow template wizard.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_WorkflowTemplateWizard(Guid? id = null, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_workflowtemplatewizard", new { id, tab });
        }



        /// <summary>
        /// Get Mass Workflows list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_MassWorkflows(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_massworkflows", new { tab });
        }



        /// <summary>
        /// Get Mass Workflows add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_MassWorkflowAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_massworkflowadd", new { tab });
        }



        /// <summary>
        /// Get Mass Workflows edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_MassWorkflowEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_massworkflow", new { id, tab });
        }



        /// <summary>
        /// Get Workflow list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Workflows(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_workflows", new { tab });
        }



        /// <summary>
        /// Get Workflow Templates edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_WorkflowEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_workflow", new { id, tab });
        }


        /// <summary>
        /// Get payment list page url
        /// </summary>
        /// <returns></returns>
        public static string AP_Payments(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_payments", new { tab });
        }



        /// <summary>
        /// Get payment add page url
        /// </summary>
        /// <returns></returns>
        public static string AP_PaymentAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_paymentadd", new { tab });
        }



        /// <summary>
        /// Get payment edit page url
        /// </summary>
        /// <returns></returns>
        public static string AP_PaymentsEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_payment", new { id, tab });
        }



        /// <summary>
        /// Get websites list page url
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_WebSites(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_websites", new { tab });
        }



        /// <summary>
        /// Get website add page url
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_WebSiteAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_websiteadd", new { tab });
        }



        /// <summary>
        /// Get website edit page url
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_WebSiteEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_website", new { id, tab });
        }



        /// <summary>
        /// Get Contact Segment list page url
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_ContactSegments(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_contactsegments", new { tab });
        }



        /// <summary>
        /// Get Contact Segment add page url
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_ContactSegmentAdd(string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_contactsegmentadd", new { tab });
        }



        /// <summary>
        /// Get Contact Segment edit page url
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="tab">The tab.</param>
        /// <returns></returns>
        public static string AP_ContactSegmentEdit(Guid id, string tab = null)
        {
            if (string.IsNullOrEmpty(tab)) tab = GetCurrentTab();
            return GetRouteUrl(null, "ap_contactsegment", new { id, tab });
        }

        #endregion

        #region LeadForce Portal

        /// <summary>
        /// Get leadfroce portal home page url.
        /// </summary>
        /// <returns></returns>
        public static string LFP_Home(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_home", new { portalSettingsId });
        }



        /// <summary>
        /// Get leadfroce portal discussions page url.
        /// </summary>
        /// <returns></returns>
        public static string LFP_Discussions(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_discussions", new { tab = "Main", portalSettingsId });
        }



        /// <summary>
        /// Get leadfroce portal knowledge base page url.
        /// </summary>
        /// <returns></returns>
        public static string LFP_KnowledgeBase(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_knowledgebase", new { tab = "Main", portalSettingsId });
        }


        /// <summary>
        /// Get leadfroce portal knowledge base page url.
        /// </summary>
        /// <returns></returns>
        public static string LFP_KnowledgeBase(Guid portalSettingsId, Guid categoryId)
        {
            return GetRouteUrl(null, "lfp_knowledgebase", new { tab = "Main", portalSettingsId, categoryId });
        }


        /// <summary>
        /// Get leadfroce portal article page url.
        /// </summary>
        /// <returns></returns>
        public static string LFP_Article(Guid portalSettingsId, Guid id)
        {
            return GetRouteUrl(null, "lfp_article", new { tab = "Main", portalSettingsId, id });
        }



        /// <summary>
        /// Get leadfroce portal discussion page url.
        /// </summary>
        /// <param name="portalSettingsId">The portal settings id.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static string LFP_Discussion(Guid portalSettingsId, Guid id)
        {
            return GetRouteUrl(null, "lfp_discussion", new { tab = "Main", portalSettingsId, id });
        }        



        /// <summary>
        /// Get tasks list page url
        /// </summary>
        /// <returns></returns>
        public static string LFP_Tasks(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_tasks", new { tab = "Main", portalSettingsId });
        }



        /// <summary>
        /// Get task edit page url
        /// </summary>
        /// <returns></returns>
        public static string LFP_TaskEdit(Guid id, Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_taskedit", new { id, tab = "Main", portalSettingsId });
        }


        /// <summary>
        /// Get agreement page url
        /// </summary>
        /// <returns></returns>
        public static string LFP_Agreement(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_agreement", new { portalSettingsId });
        }


        /// <summary>
        /// Get requests list page url
        /// </summary>
        /// <returns></returns>
        public static string LFP_Requests(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_requests", new { tab = "Main", portalSettingsId });
        }


        /// <summary>
        /// Get request add page url
        /// </summary>
        /// <returns></returns>
        public static string LFP_RequestAdd(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_requestadd", new { tab = "Main", portalSettingsId });
        }


        /// <summary>
        /// Get requirements list page url
        /// </summary>
        /// <returns></returns>
        public static string LFP_Requirements(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_requirements", new { tab = "Main", portalSettingsId });
        }


        /// <summary>
        /// Get requirement add page url
        /// </summary>
        /// <returns></returns>
        public static string LFP_RequirementAdd(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_requirementadd", new { tab = "Main", portalSettingsId });
        }



        /// <summary>
        /// Get invoices list page url
        /// </summary>
        /// <returns></returns>
        public static string LFP_Invoices(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_invoices", new { tab = "Main", portalSettingsId });
        }


        /// <summary>
        /// Get invoice add page url
        /// </summary>
        /// <returns></returns>
        public static string LFP_InvoiceAdd(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_invoiceadd", new { tab = "Main", portalSettingsId });
        }


        /// <summary>
        /// Get invoice edit page url
        /// </summary>
        /// <returns></returns>
        public static string LFP_InvoiceEdit(Guid id, Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_invoiceedit", new { id, tab = "Main", portalSettingsId });                    
        }



        /// <summary>
        /// LFs the p_ invoice print.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="portalSettingsId">The portal settings id.</param>
        /// <returns></returns>
        public static string LFP_InvoicePrint(Guid id, Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_invoiceprint", new { id, tab = "Main", portalSettingsId });
        }



        /// <summary>
        /// Get access denied page url
        /// </summary>
        /// <returns></returns>
        public static string LFP_AccessDenied(Guid portalSettingsId)
        {
            return GetRouteUrl(null, "lfp_accessdenied", new { portalSettingsId });
        }

        #endregion

        #region Get route Url

        /// <summary>
        /// Gets the route URL.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="name">The name.</param>
        /// <returns>route url</returns>
        public static string GetRouteUrl(this Page page, string name)
        {
            return GetRouteUrl(page, name, new { });
        }



        /// <summary>
        /// Gets the route URL.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="name">The name.</param>
        /// <param name="values">The values.</param>
        /// <returns>route url</returns>
        public static string GetRouteUrl(this Page page, string name, object values)
        {
            return GetRouteUrl(page, name, new RouteValueDictionary(values));
        }



        /// <summary>
        /// Gets the route URL.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="name">The name.</param>
        /// <param name="values">The values.</param>
        /// <returns>route url</returns>
        public static string GetRouteUrl(this Page page, string name, RouteValueDictionary values)
        {
            return RouteTable.Routes.GetVirtualPath(null, name, values).VirtualPath;
        }

        #endregion
    }
}