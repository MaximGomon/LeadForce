using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls.WorkflowTemplate
{
    public partial class WorkflowTemplateMaterial : System.Web.UI.UserControl
    {
        private DataManager _dataManager;
        private tbl_User _currentUser;


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid WorkflowTemplateId
        {
            get
            {
                object o = ViewState["WorkflowTemplateId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["WorkflowTemplateId"] = value;
            }
        }


        public List<MaterialMap> MaterialList
        {
            get
            {
                if (ViewState["Materials"] == null)
                    ViewState["Materials"] = new List<MaterialMap>();
                return (List<MaterialMap>)ViewState["Materials"];
            }
            set { ViewState["Materials"] = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataManager = ((LeadForceBasePage)Page).DataManager;
            _currentUser = _dataManager.User.SelectById(CurrentUser.Instance.ID);

            if (!Page.IsPostBack)
                BindData();
        }


        public void BindData()
        {
            if (WorkflowTemplateId != Guid.Empty)
            {
                ViewState["Materials"] =
                    _dataManager.Material.SelectByWorkflowTemplateId(_currentUser.SiteID, WorkflowTemplateId).Select(
                        a =>
                        new MaterialMap
                            {
                                ID = a.ID,
                                SiteID = a.SiteID,
                                Name = a.Name,
                                Type = a.Type,
                                Description = a.Description,
                                Value = a.Value,
                                WorkflowTemplateID = a.WorkflowTemplateID
                            }).ToList();
            }
            else
                ViewState["Materials"] = new List<MaterialMap>();

            rgMaterials.Rebind();
        }


        /// <summary>
        /// Handles the OnNeedDataSource event of the rgMaterials control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgMaterials_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgMaterials.DataSource = MaterialList;
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the rblType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rblType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var rblType = (RadioButtonList)sender;

            var editFormTableCell = FindControlParent(rblType, typeof(GridEditFormItem.EditFormTableCell)) as GridEditFormItem.EditFormTableCell;
            var litValue = (Literal)editFormTableCell.FindControl("litValue");
            var pnlUrl = (Panel)editFormTableCell.FindControl("pnlUrl");
            var pnlFile = (Panel)editFormTableCell.FindControl("pnlFile");
            var pnlForm = (Panel)editFormTableCell.FindControl("pnlForm");

            pnlUrl.Visible = false;
            pnlFile.Visible = false;
            pnlForm.Visible = false;

            litValue.Text = EnumHelper.GetEnumDescription((MaterialType)rblType.SelectedValue.ToInt()) + ":";

            switch (rblType.SelectedValue.ToEnum<MaterialType>())
            {
                case MaterialType.Url:
                    pnlUrl.Visible = true;
                    break;
                case MaterialType.File:
                    pnlFile.Visible = true;
                    break;
                case MaterialType.Form:
                    pnlForm.Visible = true;
                    break;
            }
        }



        private static Control FindControlParent(Control control, Type type)
        {
            Control ctrlParent = control;
            while ((ctrlParent = ctrlParent.Parent) != null)
            {
                if (ctrlParent.GetType() == type)
                {
                    return ctrlParent;
                }
            }
            return null;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgMaterials control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgMaterials_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem) e.Item;
                var data = (MaterialMap) e.Item.DataItem;

                switch ((MaterialType)data.Type)
                {
                    case MaterialType.Url:
                        ((Literal)item.FindControl("litName")).Text = data.Name;
                        item.FindControl("hlName").Visible = false;
                        ((Literal)item.FindControl("litValue")).Text = data.Value;
                        break;
                    case MaterialType.File:
                        var file = _dataManager.Links.SelectById(data.Value.ToGuid());
                        ((HyperLink)item.FindControl("hlName")).Text = data.Name;
                        ((HyperLink)item.FindControl("hlName")).NavigateUrl = UrlsData.AP_SiteActivityRule(file.ID, (int)RuleType.File);
                        item.FindControl("litName").Visible = false;
                        ((Literal)item.FindControl("litValue")).Text = file.Name;
                        break;
                    case MaterialType.Form:
                        var siteActivityRule = _dataManager.SiteActivityRules.SelectById(data.Value.ToGuid());
                        ((HyperLink)item.FindControl("hlName")).Text = data.Name;
                        ((HyperLink)item.FindControl("hlName")).NavigateUrl = UrlsData.AP_SiteActivityRule(siteActivityRule.ID, (MaterialType)data.Type == MaterialType.File ? (int)RuleType.File : (int)RuleType.Form);
                        item.FindControl("litName").Visible = false;
                        ((Literal)item.FindControl("litValue")).Text = siteActivityRule.Name;
                        break;
                }

                ((Literal)item.FindControl("litType")).Text = EnumHelper.GetEnumDescription((MaterialType)data.Type);
            }

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;

                var rblType = (RadioButtonList)gridEditFormItem.FindControl("rblType");
                foreach (var materialType in EnumHelper.EnumToList<MaterialType>())
                    rblType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(materialType), ((int)materialType).ToString()));
                rblType.SelectedIndex = 0;
                rblType.Items.RemoveAt(3); // Удаление Шаблона сообщения

                var dcbFile = ((DictionaryOnDemandComboBox)gridEditFormItem.FindControl("dcbFile"));
                dcbFile.SiteID = _currentUser.SiteID;
                dcbFile.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn { Name = "RuleTypeID", DbType = DbType.Int32, Value = ((int)RuleType.File).ToString() });
                dcbFile.DataBind();

                var dcbForm = ((DictionaryOnDemandComboBox)gridEditFormItem.FindControl("dcbForm"));
                dcbForm.SiteID = _currentUser.SiteID;
                dcbForm.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn { Name = "RuleTypeID", DbType = DbType.Int32, Value = ((int)RuleType.Form).ToString() });
                dcbForm.DataBind();

                var rauFile = ((RadAsyncUpload)gridEditFormItem.FindControl("rauFile"));
                rauFile.OnClientFileUploaded = "fileUploaded";
                rauFile.OnClientFileUploadRemoved = "fileUploadRemoved";

                var data = e.Item.DataItem as MaterialMap;
                if (data != null)
                {
                    rblType.SelectedValue = data.Type.ToString();

                    gridEditFormItem.FindControl("pnlUrl").Visible = false;
                    gridEditFormItem.FindControl("pnlFile").Visible = false;
                    gridEditFormItem.FindControl("pnlForm").Visible = false;

                    switch ((MaterialType)data.Type)
                    {
                        case MaterialType.Url:
                            gridEditFormItem.FindControl("pnlUrl").Visible = true;
                            break;
                        case MaterialType.File:
                            var siteActivityRuleFile = _dataManager.Links.SelectById(data.Value.ToGuid());
                            dcbFile.SelectedId = siteActivityRuleFile.ID;
                            dcbFile.SelectedText = siteActivityRuleFile.Name;

                            gridEditFormItem.FindControl("pnlFile").Visible = true;
                            break;
                        case MaterialType.Form:
                            var siteActivityRuleForm = _dataManager.SiteActivityRules.SelectById(data.Value.ToGuid());
                            dcbForm.SelectedId = siteActivityRuleForm.ID;
                            dcbForm.SelectedText = siteActivityRuleForm.Name;

                            gridEditFormItem.FindControl("pnlForm").Visible = true;
                            break;
                    }
                }
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbAddFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbAddFile_OnClick(object sender, EventArgs e)
        {
            var btnAddFile = (RadButton)sender;
            var editFormTableCell = FindControlParent(btnAddFile, typeof(GridEditFormItem.EditFormTableCell)) as GridEditFormItem.EditFormTableCell;
            var rauFile = (RadAsyncUpload)editFormTableCell.FindControl("rauFile");

            var siteId = CurrentUser.Instance.SiteID;
            var fsp = new FileSystemProvider();
            if (rauFile.UploadedFiles.Count > 0)
            {
                string fileName = null;
                if (rauFile.UploadedFiles.Count > 0)
                {                    
                    IFileProvider fileProvider = new FSProvider();
                    fileName = fileProvider.GetFilename(siteId, rauFile.UploadedFiles[0].FileName);
                    fsp.Upload(siteId, fileName, rauFile.UploadedFiles[0].InputStream);

                }
                var link = new tbl_Links();
                link.SiteID = siteId;
                link.Name = fileName;
                link.RuleTypeID = (int)RuleType.File;
                link.URL = fileName;
                link.FileSize = rauFile.UploadedFiles[0].InputStream.Length;
                string code = String.Format("file_[{0}]_[{1}]", DateTime.Now.ToString("ddMMyyyy"), DateTime.Now.ToString("mmss"));
                int maxCode = _dataManager.Links.SelectByCode(siteId, code);
                if (maxCode != 0) maxCode++;
                link.Code = code + (maxCode != 0 ? String.Format("[{0}]", maxCode >= 10 ? maxCode.ToString() : "0" + maxCode.ToString()) : "");

                _dataManager.Links.Add(link);

                ((DictionaryOnDemandComboBox)editFormTableCell.FindControl("dcbFile")).BindData();
            }
        }



        /// <summary>
        /// Handles the OnInsertCommand event of the rgMaterials control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgMaterials_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgMaterials.MasterTableView.IsItemInserted = false;
            rgMaterials.Rebind();
        }



        /// <summary>
        /// Handles the OnUpdateCommand event of the rgMaterials control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgMaterials_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }



        /// <summary>
        /// Handles the OnDeleteCommand event of the rgMaterials control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgMaterials_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = (e.Item as GridDataItem).GetDataKeyValue("ID").ToString().ToGuid();
            ((List<MaterialMap>)ViewState["Materials"]).Remove(((List<MaterialMap>)ViewState["Materials"]).FirstOrDefault(s => s.ID == id));
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="materialId">The material id.</param>
        /// <param name="item">The item.</param>
        protected void SaveToViewState(Guid materialId, GridEditableItem item)
        {
            var material = ((List<MaterialMap>)ViewState["Materials"]).FirstOrDefault(s => s.ID == materialId) ?? new MaterialMap();

            material.SiteID = _currentUser.SiteID;
            material.Name = ((TextBox)item.FindControl("txtName")).Text;
            material.Type = ((RadioButtonList)item.FindControl("rblType")).SelectedValue.ToInt();
            material.Description = ((TextBox)item.FindControl("txtDescription")).Text;

            switch ((MaterialType)material.Type)
            {
                case MaterialType.Url:
                    material.Value = ((TextBox)item.FindControl("txtValue")).Text;
                    break;
                case MaterialType.File:
                    material.Value = ((DictionaryOnDemandComboBox)item.FindControl("dcbFile")).SelectedId.ToString();
                    break;
                case MaterialType.Form:
                    material.Value = ((DictionaryOnDemandComboBox)item.FindControl("dcbForm")).SelectedId.ToString();
                    break;
            }

            if (material.ID == Guid.Empty)
            {
                material.ID = Guid.NewGuid();
                ((List<MaterialMap>)ViewState["Materials"]).Add(material);
            }
        }



        /// <summary>
        /// Saves the specified workflow template id.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Save(Guid workflowTemplateId)
        {
            _dataManager.Material.Save(MaterialList, workflowTemplateId);
        }
    }
}