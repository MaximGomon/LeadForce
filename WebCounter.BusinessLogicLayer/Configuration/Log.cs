using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using log4net;

namespace WebCounter.BusinessLogicLayer.Configuration
{
    /// <summary>
    /// Logging class
    /// </summary>
    public class Log
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);



        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Error(object message)
        {
            Error(message, null);
        }



        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public static void Error(object message, Exception ex)
        {
            using (ThreadContext.Stacks["RequestURL"].Push(HttpContext.Current != null ? HttpContext.Current.Request.Url.ToString() : ""))
            using (ThreadContext.Stacks["ReferrerURL"].Push(HttpContext.Current != null && HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer.ToString() : string.Empty))
            using (ThreadContext.Stacks["CurrentUserID"].Push(HttpContext.Current != null && HttpContext.Current.Session != null && CurrentUser.Instance != null ? CurrentUser.Instance.ID.ToString() : string.Empty))
            using (ThreadContext.Stacks["UserIP"].Push(GetUserIP()))
            {
                if (log.IsErrorEnabled)
                    log.Error(message, ex);
            }
        }



        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Debug(object message)
        {
            Debug(message, null);
        }



        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public static void Debug(object message, Exception ex)
        {
            using (ThreadContext.Stacks["RequestURL"].Push(HttpContext.Current != null ? HttpContext.Current.Request.Url.ToString() : ""))
            using (ThreadContext.Stacks["ReferrerURL"].Push(HttpContext.Current != null && HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer.ToString() : string.Empty))
            using (ThreadContext.Stacks["CurrentUserID"].Push(HttpContext.Current != null && HttpContext.Current.Session != null && CurrentUser.Instance != null ? CurrentUser.Instance.ID.ToString() : string.Empty))
            using (ThreadContext.Stacks["UserIP"].Push(GetUserIP()))
            {
                if (log.IsDebugEnabled)
                    log.Debug(message, ex);
            }
        }



        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Info(object message)
        {
            Info(message, null);
        }



        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public static void Info(object message, Exception ex)
        {
            using (ThreadContext.Stacks["RequestURL"].Push(HttpContext.Current != null ? HttpContext.Current.Request.Url.ToString() : string.Empty))
            using (ThreadContext.Stacks["ReferrerURL"].Push(HttpContext.Current != null && HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer.ToString() : string.Empty))
            using (ThreadContext.Stacks["CurrentUserID"].Push(HttpContext.Current != null && HttpContext.Current.Session != null && CurrentUser.Instance != null ? CurrentUser.Instance.ID.ToString() : string.Empty))
            using (ThreadContext.Stacks["UserIP"].Push(GetUserIP()))
            {
                if (log.IsInfoEnabled)
                    log.Info(message, ex);
            }
        }



        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Warn(object message)
        {
            Warn(message, null);
        }



        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public static void Warn(object message, Exception ex)
        {
            using (ThreadContext.Stacks["RequestURL"].Push(HttpContext.Current != null ? HttpContext.Current.Request.Url.ToString() : string.Empty))
            using (ThreadContext.Stacks["ReferrerURL"].Push(HttpContext.Current != null && HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer.ToString() : string.Empty))
            using (ThreadContext.Stacks["CurrentUserID"].Push(HttpContext.Current != null && HttpContext.Current.Session != null && CurrentUser.Instance != null ? CurrentUser.Instance.ID.ToString() : string.Empty))
            using (ThreadContext.Stacks["UserIP"].Push(GetUserIP()))
            {
                if (log.IsWarnEnabled)
                    log.Warn(message, ex);
            }
        }



        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Fatal(object message)
        {
            Fatal(message, null);
        }



        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public static void Fatal(object message, Exception ex)
        {
            using (ThreadContext.Stacks["RequestURL"].Push(HttpContext.Current != null ? HttpContext.Current.Request.Url.ToString() : ""))
            using (ThreadContext.Stacks["ReferrerURL"].Push(HttpContext.Current != null && HttpContext.Current.Request.UrlReferrer != null ? HttpContext.Current.Request.UrlReferrer.ToString() : string.Empty))
            using (ThreadContext.Stacks["CurrentUserID"].Push(HttpContext.Current != null && HttpContext.Current.Session != null && CurrentUser.Instance != null ? CurrentUser.Instance.ID.ToString() : string.Empty))
            using (ThreadContext.Stacks["UserIP"].Push(GetUserIP()))
            {
                if (log.IsFatalEnabled)
                    log.Fatal(message, ex);
            }
        }



        /// <summary>
        /// Gets the user IP.
        /// </summary>
        /// <returns></returns>
        private static string GetUserIP()
        {
            if (HttpContext.Current == null)
                return string.Empty;

            string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddress == null || ipAddress.ToLower() == "unknown")
                ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            return ipAddress;
        }

    }
}