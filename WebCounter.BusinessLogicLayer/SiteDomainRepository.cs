using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using HtmlAgilityPack;
using Microsoft.Web.Administration;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SiteDomainRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteDomainRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SiteDomainRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteid">The siteid.</param>
        /// <param name="siteDomainId">The site domain id.</param>
        /// <returns></returns>
        public tbl_SiteDomain SelectById(Guid siteid, Guid siteDomainId)
        {
            return _dataContext.tbl_SiteDomain.SingleOrDefault(c => c.SiteID == siteid && c.ID == siteDomainId);
        }



        /// <summary>
        /// Selects the by domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns></returns>
        public tbl_SiteDomain SelectByDomain(string domain)
        {
            return _dataContext.tbl_SiteDomain.SingleOrDefault(o => o.Domain == domain) ?? new tbl_SiteDomain();
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteid">The siteid.</param>
        /// <returns></returns>
        public tbl_SiteDomain SelectBySiteId(Guid siteid)
        {
            return _dataContext.tbl_SiteDomain.FirstOrDefault(c => c.SiteID == siteid);
        }



        /// <summary>
        /// Selects the domains by site id.
        /// </summary>
        /// <param name="siteid">The siteid.</param>
        /// <returns></returns>
        public IQueryable<tbl_SiteDomain> SelectDomainsBySiteId(Guid siteid)
        {
            return _dataContext.tbl_SiteDomain.Where(c => c.SiteID == siteid);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteDomainId">The site domain id.</param>
        /// <returns></returns>
        public tbl_SiteDomain SelectById(Guid siteDomainId)
        {
            return _dataContext.tbl_SiteDomain.SingleOrDefault(c => c.ID == siteDomainId);
        }



        /// <summary>
        /// Adds the specified site domain.
        /// </summary>
        /// <param name="siteDomain">The site domain.</param>
        /// <returns></returns>
        public tbl_SiteDomain Add(tbl_SiteDomain siteDomain)
        {
            siteDomain.ID = Guid.NewGuid();

            if (string.IsNullOrEmpty(siteDomain.Note))
                siteDomain.Note = string.Empty;

            _dataContext.tbl_SiteDomain.AddObject(siteDomain);
            _dataContext.SaveChanges();

            return siteDomain;
        }



        /// <summary>
        /// Saves the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="aliases">The aliases.</param>
        /// <param name="isSimple">if set to <c>true</c> [is simple].</param>
        /// <param name="siteDomainId">The site domain id.</param>
        /// <returns></returns>
        public string Save(Guid? siteId, string domain, string aliases, bool isSimple, ref Guid siteDomainId)
        {            
            tbl_SiteDomain siteDomain = null;

            if (isSimple && siteId.HasValue)
                siteDomain = SelectBySiteId(siteId.Value) ?? new tbl_SiteDomain();
            else
            {
                if (CurrentUser.Instance.AccessLevel == AccessLevel.SystemAdministrator)
                    siteDomain = SelectById(siteDomainId) ?? new tbl_SiteDomain();
                else if (siteId.HasValue)
                    siteDomain = SelectById(siteId.Value, siteDomainId) ?? new tbl_SiteDomain();
            }

            if (siteDomain == null)
                return "Ошибка изменения домена. Обратитесь к системному администратору.";
            
            siteDomain.Domain = domain.Trim();

            if (!Regex.IsMatch(siteDomain.Domain, @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$"))
                siteDomain.Domain = string.Format("http://{0}", siteDomain.Domain);

            if (!string.IsNullOrEmpty(aliases.Trim()))
            {
                var aliasesList = new List<string>();

                foreach (string alias in aliases.Split(';'))
                    aliasesList.Add(alias.Trim());

                siteDomain.Aliases = string.Join(";", aliasesList);
            }
            else
                siteDomain.Aliases = string.Empty;

            if (SelectByDomain(siteDomain.Domain).ID != Guid.Empty && SelectByDomain(siteDomain.Domain).ID != siteDomain.ID)
            {
                return "Такой домен уже зарегистрирован в системе.";
            }
            
            if (siteDomain.ID == Guid.Empty)
            {
                siteDomain.StatusID = (int) SiteDomainStatus.Added;

                if (CurrentUser.Instance.AccessLevel == AccessLevel.SystemAdministrator && siteId.HasValue)
                    siteDomain.SiteID = siteId.Value;
                else
                    siteDomain.SiteID = CurrentUser.Instance.SiteID;

                siteDomain.Note = string.Empty;

                var site = _dataContext.tbl_Sites.SingleOrDefault(o => o.ID == siteDomain.SiteID);
                if (site!= null && site.AccessProfileID.HasValue)
                {
                    var accessProfile = _dataContext.tbl_AccessProfile.SingleOrDefault(o => o.ID == site.AccessProfileID);
                    if ( accessProfile != null && accessProfile.DomainsCount.HasValue)
                    {
                        if (site.tbl_SiteDomain.Count + 1 > accessProfile.DomainsCount )
                        {
                            return "У вас исчерпан лимит добавления доменов.";
                        }
                    }                    
                }

                Add(siteDomain);
            }
            else
            {
                siteDomain.StatusID = (int)SiteDomainStatus.Added;

                Update(siteDomain);
            }

            siteDomainId = siteDomain.ID;

            return string.Empty;
        }



        /// <summary>
        /// Updates the specified site domain.
        /// </summary>
        /// <param name="siteDomain">The site domain.</param>
        public void Update(tbl_SiteDomain siteDomain)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Selects the domain from contact activity.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public List<string> SelectDomainFromContactActivity(Guid siteId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                var result = new List<string>();

                connection.Open();

                using (var command = new SqlCommand("GetDomainsFromContactActivity", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SiteID", siteId);                    

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(reader["Domain"] != DBNull.Value ? (string)reader["Domain"] : string.Empty);
                            }
                            reader.NextResult();
                        }
                    }
                }
                return result;
            }
        }



        /// <summary>
        /// Deletes the specified site domain.
        /// </summary>
        /// <param name="siteDomain">The site domain.</param>
        public void Delete(tbl_SiteDomain siteDomain)
        {
            _dataContext.tbl_SiteDomain.DeleteObject(siteDomain);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Checks all.
        /// </summary>
        /// <param name="siteDomain">The site domain.</param>
        /// <returns></returns>
        public List<CheckSiteStatus> CheckAll(tbl_SiteDomain siteDomain)
        {
            Uri siteUrl = GetDomainUrl(siteDomain.Domain);

            try
            {                
                if (IsLeadForceDomain(siteUrl))
                {                    
                    using (var mgr = new ServerManager())
                    {
                        var isChanged = false;
                        var site = mgr.Sites.FirstOrDefault(o => o.Name == Settings.CMSSiteHost);
                        if (site != null && !site.Bindings.Any(o => o.Host == siteUrl.Host))
                        {
                            var host = siteUrl.Host.Replace("www.", string.Empty);
                            if (!host.ToLower().StartsWith("xn--"))
                            {
                                Binding binding = site.Bindings.CreateElement();
                                binding.Protocol = "http";
                                binding.BindingInformation = "*:80:" + host;
                                site.Bindings.Add(binding);

                                binding = site.Bindings.CreateElement();
                                binding.Protocol = "http";
                                binding.BindingInformation = "*:80:www." + host;
                                site.Bindings.Add(binding);

                                isChanged = true;
                            }

                        }

                        if (site != null && !string.IsNullOrEmpty(siteDomain.Aliases))
                        {
                            foreach (string alias in siteDomain.Aliases.Split(';'))
                            {
                                var aliasUrl = GetDomainUrl(alias.Trim());
                                if (!site.Bindings.Any(o => o.Host == aliasUrl.Host))
                                {
                                    var host = aliasUrl.Host.Replace("www.", string.Empty);
                                    if (!host.ToLower().StartsWith("xn--"))
                                    {
                                        Binding binding = site.Bindings.CreateElement();
                                        binding.Protocol = "http";
                                        binding.BindingInformation = "*:80:" + host;
                                        site.Bindings.Add(binding);

                                        binding = site.Bindings.CreateElement();
                                        binding.Protocol = "http";
                                        binding.BindingInformation = "*:80:www." + host;
                                        site.Bindings.Add(binding);
                                        isChanged = true;
                                    }
                                }
                            }
                        }

                        if (isChanged)
                        {
                            mgr.CommitChanges();
                            Thread.Sleep(2000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Ошибка операции bindings", ex);
            }            

            var result = new List<CheckSiteStatus>();

            var counter = new CheckSiteStatus() { SuccessLinks = new List<string>(), ErrorLinks = new List<string>() };
            var aliases = new CheckSiteStatus() { SuccessLinks = new List<string>(), ErrorLinks = new List<string>() };
            var formsExceptCallOnClosing = new CheckSiteStatus() { SuccessLinks = new List<string>(), ErrorLinks = new List<string>() };
            var formsCallOnClosing = new CheckSiteStatus() { SuccessLinks = new List<string>(), ErrorLinks = new List<string>() };
            var formCodes = new List<string>();            

            var htmlPage = GetPageFromUrl(siteUrl.ToString());

            if (htmlPage.Contains("#LFSiteNotFound#"))
            {
                GenerateCounterResult(ref counter, siteDomain, true);
                result.Add(counter);
                return result;
            }

            ProceedPage(siteDomain.SiteID, siteUrl.ToString(), htmlPage, ref counter, ref formsExceptCallOnClosing, ref formsCallOnClosing, ref formCodes);

            if (!string.IsNullOrEmpty(siteDomain.Aliases))
            {
                foreach (string alias in siteDomain.Aliases.Split(';'))
                {
                    try
                    {
                        var aliasPage = GetPageFromUrl(alias.StartsWith("http") ? alias : "http://" + alias);
                        if (aliasPage.Replace(alias, string.Empty) != htmlPage.Replace(siteDomain.Domain, string.Empty))
                            aliases.ErrorLinks.Add(alias);
                        else
                            aliases.SuccessLinks.Add(alias);
                    }
                    catch
                    {
                        aliases.ErrorLinks.Add(alias);
                    }
                }
            }

            var links = DocumentLinks(htmlPage);

            foreach (string link in links)
            {
                var tmpLink = link;

                if (!tmpLink.StartsWith("http"))
                    tmpLink = siteUrl.AbsoluteUri + link;

                tmpLink = HttpUtility.HtmlDecode(tmpLink);

                while (tmpLink.IndexOf("//") != -1)
                    tmpLink = tmpLink.Replace("//", "/");

                tmpLink = tmpLink.Replace(":/", "://");

                if (tmpLink.StartsWith("http") && tmpLink.ToLower().Contains(siteUrl.Host) && !tmpLink.ToLower().Contains("javascript")
                     && !tmpLink.ToLower().Contains("mailto:"))
                {
                    try
                    {
                        htmlPage = GetPageFromUrl(tmpLink);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(string.Format("Ошибка обработки ссылки {0}", tmpLink), ex);
                        htmlPage = string.Empty;
                    }

                    ProceedPage(siteDomain.SiteID, tmpLink, htmlPage, ref counter, ref formsExceptCallOnClosing, ref formsCallOnClosing, ref formCodes);
                }
            }

            GenerateCounterResult(ref counter, siteDomain);
            result.Add(counter);

            if (!string.IsNullOrEmpty(siteDomain.Aliases))
            {
                GenerateAliasesResult(ref aliases);
                result.Add(aliases);

                siteDomain.Aliases = string.Join(";", aliases.SuccessLinks);
            }

            GenerateFormsExceptCallOnClosingResult(ref formsExceptCallOnClosing);
            result.Add(formsExceptCallOnClosing);
            GenerateFormsCallOnClosingResult(ref formsCallOnClosing);
            result.Add(formsCallOnClosing);

            siteDomain.TotalPageCount = counter.SuccessLinks.Count + counter.ErrorLinks.Count;
            siteDomain.PageCountWithCounter = counter.SuccessLinks.Count;
            siteDomain.PageCountWithForms = formsExceptCallOnClosing.SuccessLinks.Count;
            siteDomain.PageCountWithOnClosingForms = formsCallOnClosing.SuccessLinks.Count;

            ProceedFormCodes(formCodes, siteDomain);

            if (result.Sum(o => o.Score) == 100)
                siteDomain.Note = "Подключение домена выполнено полностью";
            else
                siteDomain.Note = string.Format("Статус подключения домена: {0} из 100%", result.Sum(o => o.Score));

            Update(siteDomain);

            return result;
        }



        /// <summary>
        /// Proceeds the form codes.
        /// </summary>
        /// <param name="formCodes">The form codes.</param>
        /// <param name="siteDomain">The site domain.</param>
        private void ProceedFormCodes(List<string> formCodes, tbl_SiteDomain siteDomain)
        {
            var dataManager = new DataManager();
            var wizardForms = new List<tbl_SiteActivityRules>();
            var otherForms = new List<tbl_SiteActivityRules>();
            
            var inviteFriendCount = 0;

            dataManager.StatisticData.SiteId = HttpContext.Current != null
                                                   ? CurrentUser.Instance.SiteID
                                                   : siteDomain.SiteID;

            foreach (var formCode in formCodes)
            {
                var siteActivityRule = dataManager.SiteActivityRules.SelectFormByCode(siteDomain.SiteID, formCode);
                if (siteActivityRule == null)
                    continue;

                if (siteActivityRule.tbl_SiteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.InviteFriend))
                    inviteFriendCount++;

                if (siteActivityRule.TemplateID.HasValue)
                    wizardForms.Add(siteActivityRule);
                else
                    otherForms.Add(siteActivityRule);
            }

            var groupedForms = wizardForms.GroupBy(o => o.TemplateID).Select(o => new {ID = o.Key, Count = o.Count()});

            foreach (var groupedForm in groupedForms)
            {
                var templateForm = dataManager.SiteActivityRules.SelectById(groupedForm.ID.Value);
                var statisticDataValue = dataManager.StatisticData.GetStatisticDataValueByCode(string.Format("ClientBaseGrowthTemplateForm_{0}", templateForm.ID));
                statisticDataValue.DbValue = groupedForm.Count;                
                statisticDataValue.Update(siteDomain.SiteID);
            }

            dataManager.StatisticData.ClientBaseOtherFormsCount.DbValue = otherForms.Count;
            dataManager.StatisticData.ClientBaseOtherFormsCount.Update(siteDomain.SiteID);

            dataManager.StatisticData.LoyaltyManagementInviteFriendFormCount.DbValue = inviteFriendCount;
            dataManager.StatisticData.LoyaltyManagementInviteFriendFormCount.Update(siteDomain.SiteID);
        }



        /// <summary>
        /// Proceeds the page.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="url">The URL.</param>
        /// <param name="htmlPage">The HTML page.</param>
        /// <param name="counter">The counter.</param>
        /// <param name="formsExceptCallOnClosing">The forms except call on closing.</param>
        /// <param name="formsCallOnClosing">The forms call on closing.</param>
        protected void ProceedPage(Guid siteId, string url, string htmlPage, ref CheckSiteStatus counter, ref CheckSiteStatus formsExceptCallOnClosing, ref CheckSiteStatus formsCallOnClosing, ref List<string> formCodes)
        {            
            var counterCode1 = string.Format("_lfq.push(['WebCounter.LG_Counter', '{0}']);", siteId);
            var counterCode2 = string.Format("_lfq.push(['WebCounter.LG_Counter','{0}']);", siteId);

            var formsCallOnClosingSuccessLinks = new List<string>();
            var formsExceptCallOnClosingSuccessLinks = new List<string>();

            if (!htmlPage.Contains(counterCode1) && !htmlPage.Contains(counterCode2))
                counter.ErrorLinks.Add(HttpUtility.UrlDecode(url));
            else
                counter.SuccessLinks.Add(HttpUtility.UrlDecode(url));

            var forms = GetAllForms(htmlPage);
            formCodes.AddRange(GetFormCodes(forms));
            var lfqPushForms = forms.Where(o => o.Contains("_lfq")).ToList();
            var simpleForms = forms.Where(o => !o.Contains("_lfq")).ToList();
            if (simpleForms.Any())
                formsExceptCallOnClosingSuccessLinks.Add(url);

            foreach (string lfqPushForm in lfqPushForms)
            {
                var formParts = lfqPushForm.Replace(" ", string.Empty).Replace("'", string.Empty).Split(',');
                if (int.Parse(formParts[2]) == (int)FormMode.CallOnClosing)
                    formsCallOnClosingSuccessLinks.Add(url);
                else
                    formsExceptCallOnClosingSuccessLinks.Add(url);
            }

            if (!formsCallOnClosingSuccessLinks.Any())
                formsCallOnClosing.ErrorLinks.Add(HttpUtility.UrlDecode(url));
            else
                formsCallOnClosing.SuccessLinks.Add(HttpUtility.UrlDecode(url));


            if (!formsExceptCallOnClosingSuccessLinks.Any())
                formsExceptCallOnClosing.ErrorLinks.Add(HttpUtility.UrlDecode(url));
            else
                formsExceptCallOnClosing.SuccessLinks.Add(HttpUtility.UrlDecode(url));

        }

        private List<string> GetFormCodes(IEnumerable<string> forms)
        {
            var regex = new Regex(".*LG_Form.*?'", RegexOptions.IgnoreCase);

            return forms.Select(form => regex.Replace(form, string.Empty).Split(',').First(o => !string.IsNullOrEmpty(o.Trim())).Replace("'", string.Empty)).ToList();
        }



        /// <summary>
        /// Gets all forms.
        /// </summary>
        /// <param name="htmlPage">The HTML page.</param>
        /// <returns></returns>
        protected List<string> GetAllForms(string htmlPage)
        {
            var regexForms = new Regex(@"_lfq.push\(\['WebCounter.LG_Form'.*?\);");
            var regexLinkForms = new Regex(@"javascript:WebCounter.LG_Form\(.*?\)");
            var matches = regexForms.Matches(htmlPage);
            var result = (from Match match in matches select match.Value).ToList();
            matches = regexLinkForms.Matches(htmlPage);
            result.AddRange(from Match match in matches select match.Value);

            return result;
        }



        /// <summary>
        /// Gets the page from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        protected string GetPageFromUrl(string url)
        {
            using (var webClient = new WebClient())
            {
                return webClient.DownloadString(url);
            }
        }



        /// <summary>
        /// Documents the links.
        /// </summary>
        /// <param name="sourceHtml">The source HTML.</param>
        /// <returns></returns>
        private IEnumerable<string> DocumentLinks(string sourceHtml)
        {
            var sourceDocument = new HtmlDocument();
            sourceDocument.LoadHtml(sourceHtml);
            if (sourceDocument.DocumentNode.SelectNodes("//a[@href!='#']") != null)
                return sourceDocument.DocumentNode.SelectNodes("//a[@href!='#']").Select(n => n.GetAttributeValue("href", ""));

            return new List<string>();
        }



        /// <summary>
        /// Generates the aliases result.
        /// </summary>
        /// <param name="aliases">The aliases.</param>
        private void GenerateAliasesResult(ref CheckSiteStatus aliases)
        {
            if (!aliases.ErrorLinks.Any())
            {
                aliases.ErrorTypeClass = "success";
                aliases.Message = "Псевдонимы корректны";
            }
            else
            {
                aliases.ErrorTypeClass = "warning";
                aliases.Message = string.Format("Псевдонимы ({0}) не корректны и отключены.", string.Join(",", aliases.ErrorLinks));
            }
        }



        /// <summary>
        /// Generates the forms call on closing result.
        /// </summary>
        /// <param name="formsCallOnClosing">The forms call on closing.</param>
        private void GenerateFormsCallOnClosingResult(ref CheckSiteStatus formsCallOnClosing)
        {
            if (formsCallOnClosing.SuccessLinks.Any())
            {
                formsCallOnClosing.ErrorTypeClass = "success";
                formsCallOnClosing.Message = "Перехват уходящих клиентов подключен";
                formsCallOnClosing.Score = 20;
            }
            else
            {
                formsCallOnClosing.ErrorTypeClass = "warning";
                formsCallOnClosing.Message = @"<p>Вы не используете форму для возврата уходящих клиентов. Используйте все шансы!</p>
<p>Если клиент зашел к Вам на сайт, значит у него был интерес к Вашей тематике. Предложите спец предложение, подарок или просто подписку уходящему клиенту.</p>
";
            }
        }



        /// <summary>
        /// Generates the forms except call on closing result.
        /// </summary>
        /// <param name="formsExceptCallOnClosing">The forms except call on closing.</param>
        private void GenerateFormsExceptCallOnClosingResult(ref CheckSiteStatus formsExceptCallOnClosing)
        {
            if (!formsExceptCallOnClosing.SuccessLinks.Any())
            {
                formsExceptCallOnClosing.ErrorTypeClass = "error";
                formsExceptCallOnClosing.Message =
                    @"<p>Формы заказа не найдены! Вам необходимо на каждой странице предусмотреть возможность для клиента обратиться к Вам для увеличения отклика.</p>
<p>Вы можете добавить форму заказа звонка, презентации или просто оформления подписки. Если Вы можете предложить что-либо ценное, предложите заполнить соответствующую форму для доступа.</p>
<p>Для добавления форм Вам необходимо в модуле Опросные формы добавить соответствующую форму, сформировать код действием [Получить скрипт] и включить его на Ваш сайт.</p>";
            }
            else if (!formsExceptCallOnClosing.ErrorLinks.Any() && formsExceptCallOnClosing.SuccessLinks.Any())
            {
                formsExceptCallOnClosing.ErrorTypeClass = "success";
                formsExceptCallOnClosing.Message = "Формы заказа подключены";
                formsExceptCallOnClosing.Score = 30;
            }
            else
            {
                var percents = (decimal)(formsExceptCallOnClosing.SuccessLinks.Count + formsExceptCallOnClosing.ErrorLinks.Count) / 100;
                var successPercents = formsExceptCallOnClosing.SuccessLinks.Count / percents;
                var errorPercents = formsExceptCallOnClosing.ErrorLinks.Count / percents;
                if (errorPercents >= 25)
                    formsExceptCallOnClosing.ErrorTypeClass = "error";
                else
                {
                    formsExceptCallOnClosing.ErrorTypeClass = "warning";
                    formsExceptCallOnClosing.Score = 15;
                }

                formsExceptCallOnClosing.Message = string.Format(@"<p>Формы заказа найдены только на {0}% страниц! Мы рекомендуем предусмотреть на каждой странице возможность для клиента обратиться к Вам для увеличения отклика.</p>
<p>Вы можете добавить форму заказа звонка, презентации или просто оформления подписки. Если Вы можете предложить что-либо ценное, предложите заполнить соответствующую форму для доступа.</p>
<p>Для добавления форм Вам необходимо в модуле Опросные формы добавить соответствующую форму, сформировать код действием [Получить скрипт] и включить его на Ваш сайт.</p>
", successPercents.ToString("F"));
            }
        }



        /// <summary>
        /// Generates the counter result.
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <param name="siteDomain">The site domain.</param>
        private void GenerateCounterResult(ref CheckSiteStatus counter, tbl_SiteDomain siteDomain, bool isNotFound = false)
        {
            if (!counter.ErrorLinks.Any())
            {
                counter.Score = 50;
                counter.ErrorTypeClass = "success";
                
                try
                {
                    Uri siteUrl = GetDomainUrl(siteDomain.Domain);                    
                    if (IsLeadForceDomain(siteUrl))
                    {
                        siteDomain.StatusID = (int)SiteDomainStatus.LeadForceDomain;
                        if (isNotFound)
                            counter.Message = "Домен найден, но не привязан к мини сайту";
                        else
                            counter.Message = string.Format("Счетчик найден. Проверено {0} страниц сайта.", counter.SuccessLinks.Count);
                    }
                    else
                    {
                        siteDomain.StatusID = (int) SiteDomainStatus.Checking;
                        counter.Message = string.Format("Счетчик найден. Проверено {0} страниц сайта.", counter.SuccessLinks.Count);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Ошибка проверки домена LeadForce", ex);
                    siteDomain.StatusID = (int)SiteDomainStatus.Checking;
                }                

                Update(siteDomain);
            }
            else
            {
                siteDomain.StatusID = (int)SiteDomainStatus.CheckingFailed;
                Update(siteDomain);

                counter.ErrorTypeClass = "error";
                counter.Message =
                    string.Format(
                        @"Счетчик не найден на {0} страницах из {1} проверенных. <a href=""javascript:;"" onclick=""$('.checkedLinks').show();"">Подробнее...</a><div style=""display:none"" class=""checkedLinks""><br/>{2}<br/></div><br/>Для установки счетчика Вам необходимо подключить к Вашему сайту код:
<div class=""row"">
    <label>Код счетчика (часть 1):</label><br />
    <span class=""note"">(размещать в &lt;head&gt; или в начале документа)</span>
    <table class=""tbl-counter-code"">
        <tr>
            <td><pre><code>{3}</code></pre></td>
        </tr>
    </table>
</div>
<div class=""row"">
    <label>Код счетчика (часть 2):</label><br />
    <span class=""note"">(размещать в конце файла перед &lt;/body&gt;)</span>
    <table class=""tbl-counter-code"">
        <tr>
            <td><pre><code>{4}</code></pre></td>
        </tr>
    </table>
</div>",
                        counter.ErrorLinks.Count,
                        counter.ErrorLinks.Count + counter.SuccessLinks.Count,
                        string.Join("<br/> ", counter.ErrorLinks.Select(o => string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", o))),
                        HttpUtility.HtmlEncode(ScriptTemplates.Counter(siteDomain.SiteID)),
                        HttpUtility.HtmlEncode(ScriptTemplates.Script(true)));
            }
        }



        /// <summary>
        /// Gets the domain URL.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns></returns>
        public Uri GetDomainUrl(string domain)
        {
            Uri siteUrl = null;
            Uri.TryCreate(domain.StartsWith("http") ? domain : "http://" + domain, UriKind.Absolute, out siteUrl);

            return siteUrl;
        }



        /// <summary>
        /// Determines whether [is lead force domain] [the specified site URL].
        /// </summary>
        /// <param name="siteUrl">The site URL.</param>
        /// <returns>
        ///   <c>true</c> if [is lead force domain] [the specified site URL]; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsLeadForceDomain(Uri siteUrl)
        {
            var serverIP = Dns.Resolve(Dns.GetHostName()).AddressList[0];
            var siteHostAddress = Dns.GetHostAddresses(siteUrl.Host);
            return siteHostAddress.FirstOrDefault(ipAddress => ipAddress.ToString() == serverIP.ToString()) != null;
        }
    }


    public class CheckSiteStatus
    {        
        public string ErrorTypeClass { get; set; }
        public string Message { get; set; }
        public int Score { get; set; }
        public List<string> SuccessLinks { get; set; }
        public List<string> ErrorLinks { get; set; }
    }
}