using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Mapping;
using System.Data;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Import
{
    public partial class ImportTag : System.Web.UI.UserControl
    {
        DataManager _dataManager = new DataManager();
        private Guid _siteId = CurrentUser.Instance.SiteID;


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid ImportId
        {
            get
            {
                if (ViewState["ImportId"] == null)
                    ViewState["ImportId"] = Guid.Empty;

                return (Guid)ViewState["ImportId"];
            }
            set
            {
                ViewState["ImportId"] = value;
            }
        }


        public List<ImportTagMap> ImportTagsList
        {
            get { return (List<ImportTagMap>)ViewState["ImportTags"]; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            if (ImportId != Guid.Empty)
            {
                ViewState["ImportTags"] =
                    _dataManager.ImportTag.SelectAll(ImportId).Select(
                        a =>
                        new ImportTagMap { ID = a.ID, ImportID = a.ImportID, SiteTagID = a.SiteTagID, Operation = a.Operation }).ToList
                        ();
            }
            else
                ViewState["ImportTags"] = new List<ImportTagMap>(); 
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="importTagId">The import tag id.</param>
        /// <param name="item">The item.</param>
        protected void SaveToViewState(Guid importTagId, GridEditableItem item)
        {
            var importTag = ((List<ImportTagMap>)ViewState["ImportTags"]).FirstOrDefault(a => a.ID == importTagId) ?? new ImportTagMap();

            var siteTagId = ((DictionaryOnDemandComboBox)item.FindControl("dcbSiteTags")).SelectedIdNullable;
            var siteTagText = ((DictionaryOnDemandComboBox)item.FindControl("dcbSiteTags")).SelectedText;

            if (siteTagId.HasValue)
                importTag.SiteTagID = ((DictionaryOnDemandComboBox)item.FindControl("dcbSiteTags")).SelectedId;
            else
            {
                var contactObjectTypeId = _dataManager.ObjectTypes.SelectByName("tbl_Contact").ID;
                var newSiteTagId = Guid.NewGuid();
                _dataManager.SiteTags.Add(new tbl_SiteTags
                {
                    ID = newSiteTagId,
                    UserID = CurrentUser.Instance.ID,
                    SiteID = _siteId,
                    Name = siteTagText,
                    ObjectTypeID = contactObjectTypeId
                });
                importTag.SiteTagID = newSiteTagId;
            }
            importTag.Operation = ((RadioButtonList)item.FindControl("rblOperation")).SelectedValue.ToInt();

            if (importTag.ID == Guid.Empty)
            {
                importTag.ID = Guid.NewGuid();
                ((List<ImportTagMap>)ViewState["ImportTags"]).Add(importTag);
            }
        }



        /// <summary>
        /// Handles the OnNeedDataSource event of the rgImportTags control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgImportTags_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgImportTags.DataSource = ViewState["ImportTags"];
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgImportTags control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgImportTags_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;

                var dcbSiteTags = ((DictionaryOnDemandComboBox)gridEditFormItem.FindControl("dcbSiteTags"));
                dcbSiteTags.SiteID = _siteId;
                var contactObjectTypeId = _dataManager.ObjectTypes.SelectByName("tbl_Contact").ID;
                var filter = new DictionaryOnDemandComboBox.DictionaryFilterColumn
                {
                    Name = "ObjectTypeID",
                    DbType = DbType.Int32,
                    Value = contactObjectTypeId.ToString()
                };
                dcbSiteTags.Filters.Add(filter);
                dcbSiteTags.BindData();

                var item = e.Item as GridEditableItem;

                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var importTag = (ImportTagMap)item.DataItem;
                    ((DictionaryOnDemandComboBox)item.FindControl("dcbSiteTags")).SelectedId = importTag.SiteTagID;
                    ((DictionaryOnDemandComboBox)item.FindControl("dcbSiteTags")).SelectedText = _dataManager.SiteTags.SelectById(importTag.SiteTagID).Name;
                    ((RadioButtonList)item.FindControl("rblOperation")).Items.FindByValue(importTag.Operation.ToString()).Selected = true;
                }
            }
            else if (e.Item is GridDataItem)
            {
                var importTag = e.Item.DataItem as ImportTagMap;
                if (importTag != null)
                {
                    var siteTag = _dataManager.SiteTags.SelectById(importTag.SiteTagID);
                    if (siteTag != null)
                    {
                        ((Literal)e.Item.FindControl("litName")).Text = siteTag.Name;
                        ((Literal)e.Item.FindControl("litOperation")).Text = importTag.Operation == 0 ? "Исключить" : "Добавить";
                    }
                }
            }
        }



        /// <summary>
        /// Handles the OnDeleteCommand event of the rgImportTags control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgImportTags_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<ImportTagMap>)ViewState["ImportTags"]).Remove(((List<ImportTagMap>)ViewState["ImportTags"]).FirstOrDefault(s => s.ID == id));
        }



        /// <summary>
        /// Handles the OnInsertCommand event of the rgImportTags control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgImportTags_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgImportTags.MasterTableView.IsItemInserted = false;
            rgImportTags.Rebind();
        }



        /// <summary>
        /// Handles the OnUpdateCommand event of the rgImportTags control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgImportTags_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }


        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            _dataManager.ImportTag.Save(ImportTagsList, ImportId);
        }
    }
}