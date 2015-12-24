using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Labitec.UI.Dictionary;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class Dictionaries : LeadForceBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Справочники";
            
            if (!Page.IsPostBack)
            {                
                
                var dictionaries = new DataManager().Dictionary.SelectAll(CurrentUser.Instance.AccessLevelID);

                Guid? accessProfileId = null;
                accessProfileId = CurrentUser.Instance.AccessProfileID;

                if (!accessProfileId.HasValue)
                    accessProfileId = DataManager.Sites.SelectById(CurrentUser.Instance.SiteID).AccessProfileID;

                if (accessProfileId.HasValue)
                {
                    var accessProfileModules = DataManager.AccessProfileModule.SelectByAccessProfileID((Guid)accessProfileId).Where(apm => apm.Read && apm.Write);
                    if (accessProfileModules.Any())
                    {
                        var availableModules = accessProfileModules.Select(apr => apr.ModuleID).ToList();
                        dictionaries =
                            dictionaries.Where(
                                d => (
                                    d.tbl_DictionaryGroup != null && d.tbl_DictionaryGroup.ModuleID.HasValue &&
                                availableModules.Contains((Guid)d.tbl_DictionaryGroup.ModuleID)) 
                                || d.tbl_DictionaryGroup == null 
                                || !d.tbl_DictionaryGroup.ModuleID.HasValue);
                    }
                }
                else if (CurrentUser.Instance.AccessLevelID != (int)AccessLevel.SystemAdministrator && CurrentUser.Instance.AccessLevelID != (int)AccessLevel.Administrator)
                {
                    dictionaries = dictionaries.Where(d => (d.tbl_DictionaryGroup == null || !d.tbl_DictionaryGroup.ModuleID.HasValue));
                }


                lbcDictionaries.AvailableDictionaries = dictionaries.Select(
                        d =>
                        new DictionaryItem()
                            {
                                Name = d.Title,
                                Dataset = d.DataSet,
                                GroupId = d.DictionaryGroupID,
                                GroupTitle = d.tbl_DictionaryGroup.Title,
                                EditFormUserControl = d.EditFormUserControl
                            }).OrderBy(d => d.Name).ToList();

                var filters = new List<FilterColumn> { new FilterColumn() { Name = "SiteID", DbType = DbType.Guid, Value = SiteId.ToString() } };
                lbcDictionaries.Filters = filters;
                lbcDictionaries.UploadImagesAbsoluteFilePath = BusinessLogicLayer.Configuration.Settings.LabitecDictionaryUploadImagesAbsoluteFilePath.Replace("$siteId", SiteId.ToString());
                lbcDictionaries.ViewImagesVirtualFilePath = BusinessLogicLayer.Configuration.Settings.LabitecDictionaryViewImagesVirtualFilePath.Replace("$siteId", SiteId.ToString());
            }
        }
    }
}