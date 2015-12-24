using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;
using System.Xml.Linq;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class CounterServiceHelper
    {
        //private static DataManager dataManager = new DataManager();
        private static WebCounterServiceRepository repository = new WebCounterServiceRepository();



        /// <summary>
        /// Adds the site user.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The user ID.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <param name="activityCode">The activity code.</param>
        /// <param name="refferURL">The reffer URL.</param>
        /// <param name="resolution">The resolution.</param>
        public static void AddContact(Guid siteID, Guid contactID, ActivityType activityType, string activityCode, string refferURL, string resolution)
        {
            AddContact(siteID, contactID, activityType, activityCode, refferURL, resolution, null, null);
        }



        /// <summary>
        /// Adds the site user.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="contactID">The user ID.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <param name="activityCode">The activity code.</param>
        /// <param name="refferURL">The reffer URL.</param>
        /// <param name="resolution">The resolution.</param>
        /// <param name="enterPointUrl">The enter point URL.</param>
        /// <param name="source">The source.</param>
        /// <param name="medium">The medium.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="campaign">The campaign.</param>
        /// <param name="content">The content.</param>
        public static void AddContact(Guid siteID, Guid contactID, ActivityType activityType, string activityCode, string refferURL, string resolution, SessionSource sessionSource, Guid? refferID)
        {
            /*if (dataManager.SiteUser.GetCount(SiteID, UserID) == 0)
            {
                var siteUser = new tbl_SiteUser { ID = UserID, SiteID = SiteID, CreatedAt = DateTime.Now, RefferURL = RefferURL, UserIP = GetUserIP(), StatusID = dataManager.Status.SelectDefault(SiteID).ID, ReadyToSellID = dataManager.ReadyToSell.SelectByScore(SiteID, 0).ID, PriorityID = dataManager.Priorities.SelectByScore(SiteID, 0).ID };
                dataManager.SiteUser.Add(siteUser);
            }*/

            //var siteUser = new tbl_SiteUser { ID = UserID, SiteID = SiteID, CreatedAt = DateTime.Now, RefferURL = RefferURL, UserIP = GetUserIP() };
            //repository.SiteUser_Add(siteUser);
            var userAgent = HttpContext.Current.Request.UserAgent;
            var browser = GetBrowser(userAgent);
            var os = GetOperatingSystem(userAgent);
            var userParams = new AddContactParams { ID = contactID,
                                   SiteID = siteID,
                                   RefferURL = refferURL,
                                   UserIP = GetUserIP(),
                                   UserAgent = userAgent,
                                   BrowserName = browser.Name,
                                   BrowserVersion = browser.Version,
                                   OperatingSystemName = os.Name,
                                   OperatingSystemVersion = os.Version,
                                   Resolution = resolution,
                                   MobileDevice = (os.IsMobile) ? GetMobileDevice(userAgent) : null,
                                   ActivityTypeID = activityType,
                                   ActivityCode = activityCode,
                                   SessionSource = sessionSource,
                                   RefferID = refferID
            };

            repository.Contact_Add(userParams);

            //CheckEvent(siteID, contactID);
        }



        public static LinkProcessingMap LinkProcessing(Guid siteActionLinkID)
        {
            return repository.LinkProcessing(siteActionLinkID);
        }



        /// <summary>
        /// Selects the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public static List<CounterServiceDomainAccessMap> SelectSiteDomains(Guid siteId)
        {
            return repository.SiteDomain_SelectBySiteId(siteId);
        }


        /// <summary>
        /// Adds the site user session.
        /// </summary>
        /// <param name="SiteID">The site ID.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="RefferURL">The reffer URL.</param>
        /// <param name="Resolution">The resolution.</param>
        /// <returns></returns>
        /*public static Guid AddSiteUserSession(Guid SiteID, Guid UserID, string RefferURL, string Resolution)
        {
            var siteUserSession = new tbl_SiteUserSessions();

            string userAgent = string.Empty;
            try
            {
                if (HttpContext.Current != null)
                {
                    userAgent = HttpContext.Current.Request.UserAgent;

                    var _browser = GetBrowser(userAgent);
                    var browser = repository.Browsers_Select(SiteID, _browser.Name, _browser.Version);
                    if (browser == null)
                    {
                        browser = new tbl_Browsers { SiteID = SiteID, Name = _browser.Name, Version = _browser.Version };
                        repository.Browsers_Add(browser);
                    }
                    if (_browser.Name == "Не определен")
                        Log.Warn(string.Format("Unknown browser name. User-agent: {0}", userAgent));

                    var _platform = GetPlatform(userAgent);
                    var operatingSystem = repository.OperatingSystems_Select(SiteID, _platform.Name, _platform.Version);
                    if (operatingSystem == null)
                    {
                        operatingSystem = new tbl_OperatingSystems { SiteID = SiteID, Name = _platform.Name, Version = _platform.Version };
                        repository.OperatingSystems_Add(operatingSystem);
                    }
                    if (_platform.Name == "Не определена")
                        Log.Warn(string.Format("Unknown platform name. User-agent: {0}", userAgent));

                    var resolution = repository.Resolutions_Select(SiteID, Resolution);
                    if (resolution == null)
                    {
                        resolution = new tbl_Resolutions { SiteID = SiteID, Value = Resolution };
                        repository.Resolutions_Add(resolution);
                    }

                    tbl_MobileDevices mobileDevice = null;
                    if (_platform.IsMobile)
                    {
                        var _mobileDevice = GetMobileDevice(userAgent);
                        mobileDevice = repository.MobileDevices_Select(SiteID, _mobileDevice);
                        if (mobileDevice == null)
                        {
                            mobileDevice = new tbl_MobileDevices { SiteID = SiteID, Name = _mobileDevice };
                            repository.MobileDevices_Add(mobileDevice);
                        }
                        if (_mobileDevice == "Не определено")
                            Log.Warn(string.Format("Unknown mobile device name. User-agent: {0}", userAgent));
                    }

                    siteUserSession = repository.SiteUserSessions_SelectLastSession(SiteID, UserID);
                    if (siteUserSession == null || (siteUserSession.UserIP != GetUserIP() || siteUserSession.BrowserID != browser.ID || siteUserSession.OperatingSystemID != operatingSystem.ID || siteUserSession.ResolutionID != resolution.ID || siteUserSession.MobileDeviceID != (mobileDevice != null ? mobileDevice.ID : (Guid?)null)))
                    {
                        siteUserSession = new tbl_SiteUserSessions
                        {
                            SiteID = SiteID,
                            SiteUserID = UserID,
                            SessionDate = DateTime.Now,
                            RefferURL = RefferURL,
                            UserIP = CounterServiceHelper.GetUserIP(),
                            BrowserID = browser.ID,
                            OperatingSystemID = operatingSystem.ID,
                            ResolutionID = resolution.ID,
                            UserAgent = userAgent
                        };
                        siteUserSession.MobileDeviceID = mobileDevice != null ? mobileDevice.ID : (Guid?)null;
                        siteUserSession = repository.SiteUserSessions_Add(siteUserSession);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("User-agent: {0}", userAgent), ex);
            }

            return siteUserSession.ID;
        }*/



        /// <summary>
        /// Gets the user IP.
        /// </summary>
        /// <returns></returns>
        private static string GetUserIP()
        {
            /*var userIP = "0.0.0.0";

            var context = OperationContext.Current;
            var messageProperties = context.IncomingMessageProperties;
            var endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpointProperty != null)
                userIP = endpointProperty.Address;*/

            return HttpContext.Current.Request.UserHostAddress;
        }



        /// <summary>
        /// Gets the OS.
        /// </summary>
        /// <param name="userAgent">The user agent.</param>
        /// <returns></returns>
        private static OperatingSystem GetOperatingSystem(string userAgent)
        {            
            var os = new OperatingSystem();
            os.Name = "Не определено";


            if (string.IsNullOrEmpty(userAgent))
                return os;

            var xmlUserAgent = XElement.Load(HttpContext.Current.Server.MapPath("~/UserAgent.xml"));

            var query = (from item in
                             (from item in xmlUserAgent.Descendants("platform")
                              select item).FirstOrDefault().Descendants("item")
                         where userAgent.Contains(item.Attribute("pattern").Value)
                         select item).FirstOrDefault();

            if (query != null)
            {
                os.Name = query.Element("name").Value;
                if (query.Element("version") != null)
                    os.Version = query.Element("version").Value;
                if (query.Element("isMobile") != null && bool.Parse(query.Element("isMobile").Value) == true)
                    os.IsMobile = true;
            }

            return os;
        }



        /// <summary>
        /// Gets the mobile device.
        /// </summary>
        /// <param name="userAgent">The user agent.</param>
        /// <returns></returns>
        private static Broswer GetBrowser(string userAgent)
        {            
            var browser = new Broswer();
            browser.Name = "Не определено";

            if (string.IsNullOrEmpty(userAgent))
                return browser;

            var xmlUserAgent = XElement.Load(HttpContext.Current.Server.MapPath("~/UserAgent.xml"));

            var query = (from item in
                             (from item in xmlUserAgent.Descendants("browsers")
                              select item).FirstOrDefault().Descendants("item")
                         where userAgent.Contains(item.Attribute("pattern").Value)
                         select item).FirstOrDefault();



            if (query != null)
            {
                browser.Name = query.Element("name").Value;

                var regexes = from item in query.Descendants("regex")
                              select item;

                foreach (var xElement in regexes)
                {
                    if (!string.IsNullOrEmpty(xElement.Value))
                    {
                        var regex = new Regex(xElement.Value, RegexOptions.IgnoreCase);
                        var match = regex.Match(userAgent);
                        if (match.Success)
                        {
                            browser.Version = match.Groups[1].Value;
                            break;
                        }
                    }
                }
            }

            return browser;
        }



        /// <summary>
        /// Gets the mobile device.
        /// </summary>
        /// <param name="userAgent">The user agent.</param>
        /// <returns></returns>
        private static string GetMobileDevice(string userAgent)
        {
            var xmlUserAgent = XElement.Load(HttpContext.Current.Server.MapPath("~/UserAgent.xml"));

            var mobileDevice = "Не определено";

            if (string.IsNullOrEmpty(userAgent))
                return mobileDevice;

            var query = (from item in
                             (from item in xmlUserAgent.Descendants("mobiles")
                              select item).FirstOrDefault().Descendants("item")
                         where userAgent.Contains(item.Attribute("pattern").Value)
                         select item).FirstOrDefault();

            if (query != null)
            {
                mobileDevice = query.Element("name").Value;
            }

            return mobileDevice;
        }

        public static void AddContactActivity(Guid siteID, Guid contactID, ActivityType activityType, string activityCode = "", string userIP = "")
        {
            AddContactActivity(siteID, contactID, activityType, DateTime.Now, activityCode, userIP);
        }

        public static void AddContactActivity(Guid siteID, Guid contactID, ActivityType activityType, DateTime activityDate, string activityCode = "", string userIP = "")
        {
            var userAgent = string.IsNullOrEmpty(userIP) ? HttpContext.Current.Request.UserAgent : string.Empty;
            var browser = GetBrowser(userAgent);
            var os = GetOperatingSystem(userAgent);
            var userParams = new AddContactParams
            {
                ID = contactID,
                SiteID = siteID,
                RefferURL = null,
                UserIP = string.IsNullOrEmpty(userIP) ? GetUserIP() : userIP,
                UserAgent = userAgent,
                BrowserName = browser.Name,
                BrowserVersion = browser.Version,
                OperatingSystemName = os.Name,
                OperatingSystemVersion = os.Version,
                Resolution = null,
                MobileDevice = (os.IsMobile) ? GetMobileDevice(userAgent) : null,
                ActivityTypeID = activityType,
                ActivityCode = activityCode
            };

            repository.ContactActivity_Add(userParams, activityDate);

            //CheckEvent(siteID, contactID);
        }

        /// <summary>
        /// Adds the site user activity.
        /// </summary>
        /// <param name="SiteID">The site ID.</param>
        /// <param name="UserID">The user ID.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <param name="sessionId">The session id.</param>
        /// <param name="activityCode">The activity code.</param>
        /*public static void AddSiteUserActivity(Guid SiteID, Guid UserID, ActivityType activityType, string activityCode = "", Guid? sessionId = null)
        {
            if (!sessionId.HasValue)
            {
                var lastSession = repository.SiteUserSessions_SelectLastSession(SiteID, UserID);
                if (lastSession != null)
                    sessionId = lastSession.ID;
            }

            var siteUserActivity = new tbl_SiteUserActivity { ID = Guid.NewGuid(), SiteID = SiteID, UserID = UserID, CreatedAt = DateTime.Now, ActivityTypeID = (int)activityType, ActivityCode = activityCode, SiteUserSessionID = sessionId };
            repository.SiteUserActivity_Add(siteUserActivity);

            var siteUser = repository.SiteUser_SelectById(SiteID, UserID);
            siteUser.LastActivityAt = DateTime.Now;
            siteUser.CharacteristicsScore = repository.SiteUserColumnValues_GetSumByCharacteristicsScore(SiteID, UserID);
            siteUser.Score = siteUser.BehaviorScore + siteUser.CharacteristicsScore;

            siteUser.ReadyToSellID = repository.ReadyToSell_SelectByScore(SiteID, siteUser.BehaviorScore).ID;
            siteUser.PriorityID = repository.Priorities_SelectByScore(SiteID, siteUser.CharacteristicsScore).ID;

            repository.SiteUser_Update(siteUser);

            CheckEvent(SiteID, UserID);
        }*/



        /// <summary>
        /// Checks the event.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The user id.</param>
        public static void CheckEvent(Guid siteId, Guid contactId)
        {
            var siteEventTemplates = repository.SiteEventTemplates_SelectAll(siteId);
            var contactActivities = repository.ContactActivity_Select(siteId, contactId);

            // Перебираем все шаблоны событий
            foreach (var siteEventTemplate in siteEventTemplates)
            {
                var conditionCount = 0;

                var contactActivity = contactActivities.OrderByDescending(a => a.CreatedAt).FirstOrDefault(a => a.ActivityCode == siteEventTemplate.ID.ToString());
                // Проверка на текущее событие и период вызова
                if (contactActivity != null && siteEventTemplate.FrequencyPeriod > 0)
                {
                    /*var ts = DateTime.Now - siteUserActivity.CreatedAt;
                    if (ts.Days <= siteEventTemplate.FrequencyPeriod)
                        break;*/
                    if (contactActivity.CreatedAt.AddDays(siteEventTemplate.FrequencyPeriod) < DateTime.Now)
                        break;
                }

                // Перебираем все условия
                var siteEventTemplateActivities = repository.SiteEventTemplateActivity_SelectAll(siteId, siteEventTemplate.ID);
                /*if (siteEventTemplateActivities == null)
                    break;*/
                
                if (siteEventTemplateActivities.Count == 0)
                    break;

                foreach (var siteEventTemplateActivity in siteEventTemplateActivities)
                {
                    IEnumerable<tbl_ContactActivity> actualContactActivities = null;
                    var checkCondition = false;

                    // Проверка какого типа условие "Действие", "Значение реквизита", "Баллы"
                    switch ((EventCategory)siteEventTemplateActivity.EventCategoryID)
                    {
                        // Действия
                        case EventCategory.Action:
                            actualContactActivities = contactActivities;
                            if (contactActivity != null)
                                actualContactActivities = actualContactActivities.Where(a => a.CreatedAt > contactActivity.CreatedAt);
                            switch ((ActivityType)siteEventTemplateActivity.ActivityTypeID)
                            {
                                case ActivityType.Link:
                                case ActivityType.OpenForm:
                                case ActivityType.FillForm:
                                case ActivityType.OpenLandingPage:
                                case ActivityType.CancelForm:
                                case ActivityType.DownloadFile:                                
                                case ActivityType.ReturnMessage:
                                    actualContactActivities = actualContactActivities.Where(a => a.ActivityTypeID == siteEventTemplateActivity.ActivityTypeID && a.ActivityCode == siteEventTemplateActivity.ActivityCode);
                                    break;
                                case ActivityType.InboxMessage:
                                case ActivityType.ViewPage:
                                    actualContactActivities = actualContactActivities.Where(a => a.ActivityTypeID == siteEventTemplateActivity.ActivityTypeID && a.ActivityCode.Contains(siteEventTemplateActivity.ActivityCode));
                                    break;
                            }
                            if (siteEventTemplateActivity.ActualPeriod != 0)
                                actualContactActivities = actualContactActivities.Where(a => a.CreatedAt.AddDays((int)siteEventTemplateActivity.ActualPeriod) >= DateTime.Now);
                            break;

                        // Значение реквизита
                        case EventCategory.ColumnValue:
                            var contactColumnValue = repository.ContactColumnValues_Select(contactId, Guid.Parse(siteEventTemplateActivity.Option));
                            if (contactColumnValue != null)
                            {
                                switch ((ColumnType)contactColumnValue.tbl_SiteColumns.TypeID)
                                {
                                    case ColumnType.Number:
                                    case ColumnType.String:
                                    case ColumnType.Text:
                                        if (((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.StartWith && contactColumnValue.StringValue.StartsWith(siteEventTemplateActivity.Value))
                                            || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.Mask && contactColumnValue.StringValue.Contains(siteEventTemplateActivity.Value)))
                                            checkCondition = true;
                                        break;
                                    case ColumnType.Date:
                                        if (((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.Less && contactColumnValue.DateValue < DateTime.Parse(siteEventTemplateActivity.Value))
                                            || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.LessOrEqual && contactColumnValue.DateValue <= DateTime.Parse(siteEventTemplateActivity.Value))
                                            || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.Greater && contactColumnValue.DateValue > DateTime.Parse(siteEventTemplateActivity.Value))
                                            || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.GreaterOrEqual && contactColumnValue.DateValue >= DateTime.Parse(siteEventTemplateActivity.Value)))
                                            checkCondition = true;
                                        break;
                                    case ColumnType.Enum:
                                        if (((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.Empty && (contactColumnValue.SiteColumnValueID == null || contactColumnValue.SiteColumnValueID == Guid.Empty))
                                            || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.NotEmpty && (contactColumnValue.SiteColumnValueID != null && contactColumnValue.SiteColumnValueID != Guid.Empty))
                                            || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.SelectFromList && contactColumnValue.SiteColumnValueID == Guid.Parse(siteEventTemplateActivity.Value)))
                                            checkCondition = true;
                                        break;
                                }
                            }
                            break;

                        // Балл по поведению
                        case EventCategory.ScoreByType:
                            var contactActivityScore = repository.ContactActivityScore_SelectByScoreType(siteId, contactId, Guid.Parse(siteEventTemplateActivity.Option));
                            if (contactActivityScore != null)
                            {
                                if (((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.Less && contactActivityScore.Score < int.Parse(siteEventTemplateActivity.Value))
                                    || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.LessOrEqual && contactActivityScore.Score <= int.Parse(siteEventTemplateActivity.Value))
                                    || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.Greater && contactActivityScore.Score > int.Parse(siteEventTemplateActivity.Value))
                                    || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.GreaterOrEqual && contactActivityScore.Score >= int.Parse(siteEventTemplateActivity.Value)))
                                    checkCondition = true;
                            }
                            break;

                        // Балл по характеристикам
                        case EventCategory.ScoreByCharacteristics:
                            var contact = repository.Contact_SelectById(siteId, contactId);
                            if (((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.Less && contact.CharacteristicsScore < int.Parse(siteEventTemplateActivity.Value))
                                || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.LessOrEqual && contact.CharacteristicsScore <= int.Parse(siteEventTemplateActivity.Value))
                                || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.Greater && contact.CharacteristicsScore > int.Parse(siteEventTemplateActivity.Value))
                                || ((FormulaType)siteEventTemplateActivity.FormulaID == FormulaType.GreaterOrEqual && contact.CharacteristicsScore >= int.Parse(siteEventTemplateActivity.Value)))
                                checkCondition = true;
                            break;
                    }

                    if (actualContactActivities != null && actualContactActivities.ToList().Count > 0)
                    {
                        if ((LogicConditionType)siteEventTemplate.LogicConditionID == LogicConditionType.Or || (LogicConditionType)siteEventTemplate.LogicConditionID == LogicConditionType.And)
                            conditionCount++;
                        if ((LogicConditionType)siteEventTemplate.LogicConditionID == LogicConditionType.NEvents)
                            conditionCount += actualContactActivities.ToList().Count;
                    }

                    if (checkCondition)
                        conditionCount++;

                    // Если Формула "ИЛИ" и выполнилось хотя бы одно условие
                    if ((LogicConditionType)siteEventTemplate.LogicConditionID == LogicConditionType.Or && conditionCount > 0)
                        break;
                }

                if (((LogicConditionType)siteEventTemplate.LogicConditionID == LogicConditionType.Or && conditionCount > 0)
                    || ((LogicConditionType)siteEventTemplate.LogicConditionID == LogicConditionType.And && conditionCount == siteEventTemplateActivities.Count)
                    || ((LogicConditionType)siteEventTemplate.LogicConditionID == LogicConditionType.NEvents && conditionCount >= siteEventTemplate.ActionCount))
                {
                    ////AddContactActivity(siteId, contactId, ActivityType.Event, siteEventTemplate.ID.ToString());
                    //repository.EventProcessing(siteId, contactId, siteEventTemplate.ID);

                    /*AddSiteAction(siteId, userId, siteEventTemplate.ID);
                    RecalculateScores(siteId, userId, siteEventTemplate.ID);
                    ChangeColumns(siteId, userId, siteEventTemplate.ID);*/
                }
            }
        }


        /*
        /// <summary>
        /// Adds the site action.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="siteEventTemplateId">The site event template id.</param>
        public static void AddSiteAction(Guid siteId, Guid userId, Guid siteEventTemplateId)
        {
            var siteEventActionTemplates = repository.SiteEventActionTemplate_Select(siteId, siteEventTemplateId);
            if (siteEventActionTemplates != null && siteEventActionTemplates.Count > 0)
            {
                foreach (var item in siteEventActionTemplates)
                {
                    var siteAction = new tbl_SiteAction();
                    siteAction.SiteID = siteId;
                    siteAction.SiteActionTemplateID = item.SiteActionTemplateID;
                    siteAction.SiteUserID = userId;
                    siteAction.ActionStatusID = (int)ActionStatus.Scheduled;
                    DateTime actionDate;
                    switch ((StartAfterType)item.StartAfterTypeID)
                    {
                        case StartAfterType.Minutes:
                            actionDate = DateTime.Now.AddMinutes(item.StartAfter);
                            break;
                        case StartAfterType.Hours:
                            actionDate = DateTime.Now.AddHours(item.StartAfter);
                            break;
                        default:
                            actionDate = DateTime.Now.AddDays(item.StartAfter);
                            break;
                    }
                    siteAction.ActionDate = actionDate;
                    repository.SiteAction_Add(siteAction);
                }
            }
        }



        /// <summary>
        /// Recalculates the scores.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="siteEventTemplateId">The site event template id.</param>
        public static void RecalculateScores(Guid siteId, Guid userId, Guid siteEventTemplateId)
        {
            var siteEventTemplateScores = repository.SiteEventTemplateScore_Select(siteId, siteEventTemplateId);
            if (siteEventTemplateScores != null && siteEventTemplateScores.Count > 0)
            {
                foreach (var item in siteEventTemplateScores)
                {
                    var siteUserActivityScore = repository.SiteUserActivityScore_SelectByScoreType(siteId, userId, item.SiteActivityScoreTypeID);
                    var isNew = false;
                    if (siteUserActivityScore == null)
                    {
                        siteUserActivityScore = new tbl_SiteUserActivityScore();
                        siteUserActivityScore.SiteID = siteId;
                        siteUserActivityScore.SiteActivityScoreTypeID = item.SiteActivityScoreTypeID;
                        siteUserActivityScore.SiteUserID = userId;
                        siteUserActivityScore.Score = 0;
                        isNew = true;
                    }

                    switch ((OperationType)item.OperationID)
                    {
                        case OperationType.Assignment:
                            siteUserActivityScore.Score = item.Score;
                            break;
                        case OperationType.Increase:
                            siteUserActivityScore.Score += item.Score;
                            break;
                        case OperationType.Decrease:
                            siteUserActivityScore.Score -= item.Score;
                            break;
                    }

                    if (siteUserActivityScore.Score < 0)
                        siteUserActivityScore.Score = 0;
                    if (siteUserActivityScore.Score > 100)
                        siteUserActivityScore.Score = 100;

                    if (isNew)
                        repository.SiteUserActivityScore_Add(siteUserActivityScore);
                    else
                        repository.SiteUserActivityScore_Update(siteUserActivityScore);

                    var siteUser = repository.SiteUser_SelectById(siteId, userId);
                    siteUser.BehaviorScore = siteUserActivityScore.Score ?? 0;
                    siteUser.Score = siteUser.BehaviorScore + siteUser.CharacteristicsScore;

                    siteUser.ReadyToSellID = repository.ReadyToSell_SelectByScore(siteId, siteUser.BehaviorScore).ID;
                    siteUser.PriorityID = repository.Priorities_SelectByScore(siteId, siteUser.CharacteristicsScore).ID;

                    repository.SiteUser_Update(siteUser);
                }
            }
        }



        /// <summary>
        /// Changes the columns.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="siteEventTemplateId">The site event template id.</param>
        public static void ChangeColumns(Guid siteId, Guid userId, Guid siteEventTemplateId)
        {
            var siteActionTemplateUserColumns = repository.SiteActionTemplateUserColumn_SelectByEventTemplateId(siteId, siteEventTemplateId);
            if (siteActionTemplateUserColumns != null && siteActionTemplateUserColumns.Count > 0)
            {
                foreach (var siteActionTemplateUserColumn in siteActionTemplateUserColumns)
                {
                    var siteUserColumnValues = repository.SiteUserColumnValues_Select(userId, siteActionTemplateUserColumn.SiteColumnID);
                    if (siteUserColumnValues != null)
                    {
                        siteUserColumnValues.StringValue = siteActionTemplateUserColumn.StringValue;
                        siteUserColumnValues.DateValue = siteActionTemplateUserColumn.DateValue;
                        siteUserColumnValues.SiteColumnValueID = siteActionTemplateUserColumn.SiteColumnValueID;
                        repository.SiteUserColumnValues_Update(siteUserColumnValues);
                    }
                    else
                    {
                        siteUserColumnValues = new tbl_SiteUserColumnValues();
                        siteUserColumnValues.SiteUserID = userId;
                        siteUserColumnValues.SiteColumnID = siteActionTemplateUserColumn.SiteColumnID;
                        siteUserColumnValues.StringValue = siteActionTemplateUserColumn.StringValue;
                        siteUserColumnValues.DateValue = siteActionTemplateUserColumn.DateValue;
                        siteUserColumnValues.SiteColumnValueID = siteActionTemplateUserColumn.SiteColumnValueID;
                        repository.SiteUserColumnValues_Add(siteUserColumnValues);
                    }
                }
            }
        }
        */
    }





    public class OperatingSystem
    {
        private string _name = "Не определена";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _version = null;
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public bool IsMobile { get; set; }
    }

    public class Broswer
    {
        private string _name = "Не определен";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _version = null;
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }
    }
}