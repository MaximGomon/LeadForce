using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.WebSite;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls.Shared
{
    public partial class ExternalResources : System.Web.UI.UserControl
    {
        private DataManager _dataManager;
        public event EventHandler ExternalResourceChanged;
        protected RadAjaxManager radAjaxManager = null;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? DestinationId
        {
            get
            {
                object o = ViewState["DestinationId"];
                return (o == null ? null : (Guid?)o);
            }
            set
            {
                ViewState["DestinationId"] = value;
            }
        }


        /// <summary>
        /// Gets the web site external resource list.
        /// </summary>
        public List<ExternalResourceMap> ExternalResourceList
        {
            get
            {
                if (ViewState["ExternalResourceList"] == null)
                    ViewState["ExternalResourceList"] = new List<ExternalResourceMap>();

                return (List<ExternalResourceMap>) ViewState["ExternalResourceList"];
            }
            set { ViewState["ExternalResourceList"] = value; }
        }




        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataManager = ((LeadForceBasePage)Page).DataManager;

            rgExternalResource.Culture = new CultureInfo("ru-RU");

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (DestinationId != null)
                ExternalResourceList = _dataManager.ExternalResource.SelectAll((Guid)DestinationId).Select(o => new ExternalResourceMap()
                {
                    ID = o.ID,
                    DestinationID = o.DestinationID,
                    Title = o.Title,
                    ResourcePlaceID = o.ResourcePlaceID,
                    ExternalResourceTypeID = o.ExternalResourceTypeID,
                    File = o.File,
                    Text = o.Text,
                    Url = o.Url
                }).ToList();            
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgExternalResource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgExternalResource_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgExternalResource.DataSource = ExternalResourceList;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgExternalResource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgExternalResource_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;

                var ddlResourcePlace = (DropDownList) gridEditFormItem.FindControl("ddlResourcePlace");
                EnumHelper.EnumToDropDownList<ResourcePlace>(ref ddlResourcePlace, false);
                var ddlExternalResourceType = (DropDownList)gridEditFormItem.FindControl("ddlExternalResourceType");
                EnumHelper.EnumToDropDownList<ExternalResourceType>(ref ddlExternalResourceType, false);

                var item = e.Item as GridEditableItem;

                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var externalResource = (ExternalResourceMap)item.DataItem;

                    ((TextBox)item.FindControl("txtTitle")).Text = externalResource.Title;
                    ((DropDownList) item.FindControl("ddlResourcePlace")).SelectedIndex =
                        ((DropDownList) item.FindControl("ddlResourcePlace")).FindItemIndexByValue(externalResource.ResourcePlaceID.ToString());
                    ((DropDownList) item.FindControl("ddlExternalResourceType")).SelectedIndex =
                        ((DropDownList)item.FindControl("ddlExternalResourceType")).FindItemIndexByValue(externalResource.ExternalResourceTypeID.ToString());

                    if (!string.IsNullOrEmpty(externalResource.File))
                    {
                        var fsp = new FileSystemProvider();                     
                        var link = fsp.GetLink(CurrentUser.Instance.SiteID, "ExternalResource", externalResource.File, FileType.Attachment);
                        ((Literal)item.FindControl("lrlFileName")).Text = string.Format("<a href='{0}' target='_blank'>{1}</a>", link, externalResource.File);                        
                    }                    
                    ((TextBox) item.FindControl("txtResourceText")).Text = externalResource.Text;
                    ((TextBox)item.FindControl("txtResourceUrl")).Text = externalResource.Url;                    
                }
            }
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgExternalResource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgExternalResource_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());

            var externalResource = ExternalResourceList.FirstOrDefault(s => s.ID == id);

            if (externalResource != null && !string.IsNullOrEmpty(externalResource.File))
            {
                var fsp = new FileSystemProvider();
                fsp.Delete(CurrentUser.Instance.SiteID, "ExternalResource", externalResource.File, FileType.Attachment);
            }

            ExternalResourceList.Remove(ExternalResourceList.FirstOrDefault(s => s.ID == id));

            if (ExternalResourceChanged != null)
                ExternalResourceChanged(this, new EventArgs());
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgExternalResource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgExternalResource_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgExternalResource.MasterTableView.IsItemInserted = false;
            rgExternalResource.Rebind();
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgExternalResource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgExternalResource_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }




        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="orderProductId">The order product id.</param>
        /// <param name="item">The item.</param>
        private void SaveToViewState(Guid orderProductId, GridEditableItem item)
        {
            var externalResource = ExternalResourceList.FirstOrDefault(s => s.ID == orderProductId) ?? new ExternalResourceMap();

            externalResource.Title = ((TextBox) item.FindControl("txtTitle")).Text;
            externalResource.ResourcePlaceID = int.Parse(((DropDownList) item.FindControl("ddlResourcePlace")).SelectedValue);
            externalResource.ExternalResourceTypeID = int.Parse(((DropDownList) item.FindControl("ddlExternalResourceType")).SelectedValue);
            externalResource.Text = ((TextBox) item.FindControl("txtResourceText")).Text;
            externalResource.Url = ((TextBox)item.FindControl("txtResourceUrl")).Text;

            var rauResourceFile = (RadAsyncUpload) item.FindControl("rauResourceFile");
            if (rauResourceFile.UploadedFiles.Count > 0)
            {
                var fsp = new FileSystemProvider();

                if (externalResource.ID != Guid.Empty && !string.IsNullOrEmpty(externalResource.File))                
                    fsp.Delete(CurrentUser.Instance.SiteID, "ExternalResource",externalResource.File, FileType.Attachment);                
                
                var fileName = fsp.Upload(CurrentUser.Instance.SiteID, "ExternalResource", rauResourceFile.UploadedFiles[0].FileName, rauResourceFile.UploadedFiles[0].InputStream, FileType.Attachment);
                externalResource.File = fileName;
            }

            if (externalResource.ID == Guid.Empty)
            {
                externalResource.ID = Guid.NewGuid();                
                ExternalResourceList.Add(externalResource);
            }

            if (ExternalResourceChanged != null)
                ExternalResourceChanged(this, new EventArgs());
        }
    }
}