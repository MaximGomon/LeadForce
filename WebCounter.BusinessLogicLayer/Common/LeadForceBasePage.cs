using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class LeadForceBasePage : System.Web.UI.Page
    {
        public Guid SiteId
        {
            get { return CurrentUser.Instance.SiteID; }
        }

        public DataManager DataManager = new DataManager();

        public string CurrentTab
        {
            get { return Page.RouteData.Values["tab"] as string; }
        }


        public string Code
        {
            get
            {
                var path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Split(new[] { '/' });
                if (path.Length == 3)
                {
                    var module = path[1];
                    var moduleEditionAction = path[2];
                    return module + "_" + moduleEditionAction;                    
                }
                if (path.Length >= 4)
                {
                    var module = path[2];
                    var moduleEditionAction = path[3];
                    return module + "_" + moduleEditionAction;
                }
                return "";
            }
        }



        /// <summary>
        /// Gets the current module id.
        /// </summary>
        public Guid CurrentModuleId
        {
            get
            {             
                var path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Split(new[] { '/' });
                if (path.Length >= 4)
                {
                    var module = DataManager.Module.SelectByName(path[2]);
                    if (module != null)
                        return module.ID;
                }

                return Guid.Empty;
            }
        }



        /// <summary>
        /// Gets a value indicating whether this instance is mail edition.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is mail edition; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefaultEdition
        {
            get
            {
                var path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Split(new[] { '/' });
                if (path.Length >= 4)
                {
                    var module = DataManager.Module.SelectByName(path[2]);

                    if (CurrentUser.Instance.SiteAccessProfileID != null && CurrentUser.Instance.SiteAccessProfileID != Guid.Empty)
                    {
                        var siteAccessProfile = DataManager.AccessProfile.SelectById((Guid)CurrentUser.Instance.SiteAccessProfileID);
                        if (siteAccessProfile != null)
                        {
                            var accessProfileModule = siteAccessProfile.tbl_AccessProfileModule.SingleOrDefault(a => a.ModuleID == module.ID);
                            if (accessProfileModule.ModuleEditionID != null)
                                return false;
                        }
                    }
                }

                return true;
            }
        }


        /// <summary>
        /// Gets the current module edition action user control.
        /// </summary>
        public tbl_ModuleEditionAction CurrentModuleEditionAction
        {
            get
            {
                var path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Split(new[] { '/' });
                if (path.Length >= 4)
                {
                    var module = DataManager.Module.SelectByName(path[2]);
                    var moduleEditionAction = DataManager.ModuleEditionAction.Select(module.ID, path[3], CurrentUser.Instance.SiteAccessProfileID);

                    return moduleEditionAction;                    
                }

                return null;
            }
        }

        public void ProcessModuleEdition(Panel plPageContainer)
        {
            if (CurrentModuleEditionAction != null && !string.IsNullOrEmpty(CurrentModuleEditionAction.UserControl))
            {
                plPageContainer.Controls.Clear();
                var control = LoadControl(CurrentModuleEditionAction.UserControl);
                control.ID = CurrentModuleEditionAction.SystemName;
                plPageContainer.Controls.Add(control);
            }
        }

        public List<tbl_ModuleEditionOption> CurrentModuleEditionOptions
        {
            get
            {
                var path = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Split(new[] { '/' });
                if (path.Length >= 4)
                {
                    var module = DataManager.Module.SelectByName(path[2]);

                    if (CurrentUser.Instance.SiteAccessProfileID != null && CurrentUser.Instance.SiteAccessProfileID != Guid.Empty)
                    {
                        var siteAccessProfile = DataManager.AccessProfile.SelectById((Guid)CurrentUser.Instance.SiteAccessProfileID);
                        if (siteAccessProfile != null)
                        {
                            var accessProfileModule = siteAccessProfile.tbl_AccessProfileModule.SingleOrDefault(a => a.ModuleID == module.ID);
                            if (accessProfileModule != null)
                                return accessProfileModule.tbl_ModuleEditionOption.ToList();
                        }
                    }
                }

                return new List<tbl_ModuleEditionOption>();
            }
        }
    }
}
