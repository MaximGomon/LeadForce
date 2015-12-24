using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class Access
    {
        private bool _read = true;
        public bool Read
        {
            get { return _read; }
            set { _read = value; }
        }

        private bool _write = true;
        public bool Write
        {
            get { return _write; }
            set { _write = value; }
        }

        private bool _delete = true;
        public bool Delete
        {
            get { return _delete; }
            set { _delete = value; }
        }



        /// <summary>
        /// Checks the specified owner id.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        public static Access Check(Guid? ownerId = null, Guid? companyId = null)
        {
            var DataManager = new DataManager();

            var access = new Access();

            var accessProfileModule = new tbl_AccessProfileModule();

            // Проверка доступа модулей на уровне сайта
            var site = DataManager.Sites.SelectById(CurrentUser.Instance.SiteID);
            if (site.AccessProfileID != null)
            {
                var accessProfile = DataManager.AccessProfile.SelectById((Guid)site.AccessProfileID);
                {
                    var modules = DataManager.Module.SelectAll();
                    foreach (var module in modules)
                    {
                        Match match = Regex.Match(HttpContext.Current.Request.RawUrl, string.Format("/{0}($|/)", module.Name), RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            accessProfileModule = accessProfile.tbl_AccessProfileModule.Where(a => a.ModuleID == module.ID).SingleOrDefault();
                            if (accessProfileModule != null && !accessProfileModule.Write)
                            {
                                access.Read = accessProfileModule.Read;
                                access.Write = accessProfileModule.Write;
                                access.Delete = accessProfileModule.Delete;
                            }
                        }
                    }
                }
            }

            // Проверка доступа модулей на уровне пользователя            
            var user = CurrentUser.Instance;
            if (user.AccessProfileID != null)
            {
                var accessProfile = DataManager.AccessProfile.SelectById((Guid)user.AccessProfileID);
                {
                    var modules = DataManager.Module.SelectAll();
                    foreach (var module in modules)
                    {
                        Match match = Regex.Match(HttpContext.Current.Request.RawUrl, string.Format("/{0}($|/)", module.Name), RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            accessProfileModule = accessProfile.tbl_AccessProfileModule.Where(a => a.ModuleID == module.ID).SingleOrDefault();
                            if (accessProfileModule != null && !accessProfileModule.Write)
                            {
                                access.Read = accessProfileModule.Read;
                                access.Write = accessProfileModule.Write;
                                access.Delete = accessProfileModule.Delete;
                            }
                        }
                    }
                }
            }            

            if (user.AccessProfileID != null)
            {
                var path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Split(new[] { '/' });
                var accessProfileRecords = DataManager.AccessProfileRecord.SelectByAccessProfileID((Guid)user.AccessProfileID).Where(a => a.tbl_Module.Name == path[2]).ToList();
                if (accessProfileRecords.Count > 0)
                {
                    var readList = new List<bool>();
                    var writeList = new List<bool>();
                    var deleteList = new List<bool>();

                    foreach (var accessProfileRecord in accessProfileRecords)
                    {
                        switch ((AccessProfileRecordRule)accessProfileRecord.CompanyRuleID)
                        {
                            case AccessProfileRecordRule.SelfValue:
                                if (user.CompanyID == companyId)
                                {
                                    readList.Add(accessProfileRecord.Read);
                                    writeList.Add(accessProfileRecord.Write);
                                    deleteList.Add(accessProfileRecord.Delete);
                                }
                                else
                                {
                                    readList.Add(false);
                                    writeList.Add(false);
                                    deleteList.Add(false);
                                }
                                break;
                            case AccessProfileRecordRule.SpecificValue:
                                if (accessProfileRecord.CompanyID == companyId)
                                {
                                    readList.Add(accessProfileRecord.Read);
                                    writeList.Add(accessProfileRecord.Write);
                                    deleteList.Add(accessProfileRecord.Delete);
                                }
                                else
                                {
                                    readList.Add(false);
                                    writeList.Add(false);
                                    deleteList.Add(false);
                                }
                                break;
                        }
                        switch ((AccessProfileRecordRule)accessProfileRecord.OwnerRuleID)
                        {
                            case AccessProfileRecordRule.SelfValue:
                                if (user.ContactID == ownerId)
                                {
                                    readList.Add(accessProfileRecord.Read);
                                    writeList.Add(accessProfileRecord.Write);
                                    deleteList.Add(accessProfileRecord.Delete);
                                }
                                else
                                {
                                    readList.Add(false);
                                    writeList.Add(false);
                                    deleteList.Add(false);
                                }
                                break;
                            case AccessProfileRecordRule.SpecificValue:
                                if (accessProfileRecord.OwnerID == ownerId)
                                {
                                    readList.Add(accessProfileRecord.Read);
                                    writeList.Add(accessProfileRecord.Write);
                                    deleteList.Add(accessProfileRecord.Delete);
                                }
                                else
                                {
                                    readList.Add(false);
                                    writeList.Add(false);
                                    deleteList.Add(false);
                                }
                                break;
                        }
                    }

                    if (readList.Count > 0 && readList.IndexOf(false) != -1)
                        access.Read = false;
                    if (writeList.Count > 0 && writeList.IndexOf(false) != -1)
                        access.Write = false;
                    if (deleteList.Count > 0 && deleteList.IndexOf(false) != -1)
                        access.Delete = false;
                }
            }
            

            return access;
        }



        /// <summary>
        /// Checks the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        public static Access Check(tbl_User user, string moduleName, Guid? ownerId = null, Guid? companyId = null)
        {
            var dataManager = new DataManager();

            var access = new Access();

            tbl_AccessProfileModule accessProfileModule;

            // Проверка доступа модулей на уровне сайта
            var site = dataManager.Sites.SelectById(user.SiteID);
            if (site.AccessProfileID != null)
            {
                var accessProfile = dataManager.AccessProfile.SelectById((Guid) site.AccessProfileID);
                {
                    var modules = dataManager.Module.SelectAll();
                    foreach (var module in modules)
                    {                        
                        if (module.Name.ToLower() == moduleName.ToLower())
                        {
                            accessProfileModule =
                                accessProfile.tbl_AccessProfileModule.SingleOrDefault(a => a.ModuleID == module.ID);
                            if (accessProfileModule != null && !accessProfileModule.Write)
                            {
                                access.Read = accessProfileModule.Read;
                                access.Write = accessProfileModule.Write;
                                access.Delete = accessProfileModule.Delete;
                            }
                        }
                    }
                }
            }

            // Проверка доступа модулей на уровне пользователя                        
            if (user.AccessProfileID != null)
            {
                var accessProfile = dataManager.AccessProfile.SelectById((Guid) user.AccessProfileID);
                {
                    var modules = dataManager.Module.SelectAll();
                    foreach (var module in modules)
                    {
                        if (module.Name.ToLower() == moduleName.ToLower())
                        {
                            accessProfileModule =
                                accessProfile.tbl_AccessProfileModule.SingleOrDefault(a => a.ModuleID == module.ID);
                            if (accessProfileModule != null && !accessProfileModule.Write)
                            {
                                access.Read = accessProfileModule.Read;
                                access.Write = accessProfileModule.Write;
                                access.Delete = accessProfileModule.Delete;
                            }
                        }
                    }
                }
            }

            if (user.AccessProfileID != null && (ownerId.HasValue || companyId.HasValue))
            {
                var path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Split(new[] { '/' });
                var accessProfileRecords = dataManager.AccessProfileRecord.SelectByAccessProfileID((Guid)user.AccessProfileID).Where(a => a.tbl_Module.Name == path[2]).ToList();
                if (accessProfileRecords.Count > 0)
                {
                    var readList = new List<bool>();
                    var writeList = new List<bool>();
                    var deleteList = new List<bool>();

                    foreach (var accessProfileRecord in accessProfileRecords)
                    {
                        switch ((AccessProfileRecordRule)accessProfileRecord.CompanyRuleID)
                        {
                            case AccessProfileRecordRule.SelfValue:
                                if (user.tbl_Contact != null && user.tbl_Contact.CompanyID == companyId)
                                {
                                    readList.Add(accessProfileRecord.Read);
                                    writeList.Add(accessProfileRecord.Write);
                                    deleteList.Add(accessProfileRecord.Delete);
                                }
                                else
                                {
                                    readList.Add(false);
                                    writeList.Add(false);
                                    deleteList.Add(false);
                                }
                                break;
                            case AccessProfileRecordRule.SpecificValue:
                                if (accessProfileRecord.CompanyID == companyId)
                                {
                                    readList.Add(accessProfileRecord.Read);
                                    writeList.Add(accessProfileRecord.Write);
                                    deleteList.Add(accessProfileRecord.Delete);
                                }
                                else
                                {
                                    readList.Add(false);
                                    writeList.Add(false);
                                    deleteList.Add(false);
                                }
                                break;
                        }
                        switch ((AccessProfileRecordRule)accessProfileRecord.OwnerRuleID)
                        {
                            case AccessProfileRecordRule.SelfValue:
                                if (user.ContactID == ownerId)
                                {
                                    readList.Add(accessProfileRecord.Read);
                                    writeList.Add(accessProfileRecord.Write);
                                    deleteList.Add(accessProfileRecord.Delete);
                                }
                                else
                                {
                                    readList.Add(false);
                                    writeList.Add(false);
                                    deleteList.Add(false);
                                }
                                break;
                            case AccessProfileRecordRule.SpecificValue:
                                if (accessProfileRecord.OwnerID == ownerId)
                                {
                                    readList.Add(accessProfileRecord.Read);
                                    writeList.Add(accessProfileRecord.Write);
                                    deleteList.Add(accessProfileRecord.Delete);
                                }
                                else
                                {
                                    readList.Add(false);
                                    writeList.Add(false);
                                    deleteList.Add(false);
                                }
                                break;
                        }
                    }

                    if (readList.Count > 0 && readList.IndexOf(false) != -1)
                        access.Read = false;
                    if (writeList.Count > 0 && writeList.IndexOf(false) != -1)
                        access.Write = false;
                    if (deleteList.Count > 0 && deleteList.IndexOf(false) != -1)
                        access.Delete = false;
                }
            }

            return access;
        }



        /// <summary>
        /// Selects the available modules.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static List<Guid> SelectAvailableModules(Guid userId)
        {
            var dataManager = new DataManager();
            var modules = dataManager.Module.SelectAll();
            var user = dataManager.User.SelectById(userId);

            return (from module in modules where Check(user, module.Name).Read select module.ID).ToList();
        }
    }
}