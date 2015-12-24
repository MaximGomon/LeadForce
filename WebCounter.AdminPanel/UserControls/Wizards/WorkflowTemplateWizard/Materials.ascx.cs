using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using System.Data;

namespace WebCounter.AdminPanel.UserControls.Wizards.WorkflowTemplateWizard
{
    public partial class Materials : WorkflowTemplateWizardStep
    {
        private List<MaterialMap> MaterialList
        {
            get
            {
                if (Session["Materials"] == null)
                    Session["Materials"] = new List<MaterialMap>();
                return (List<MaterialMap>)(Session["Materials"]);
            }
            set
            {
                Session["Materials"] = value;
            }
        }



        private List<tbl_SiteActionTemplate> SiteActionTemplateList
        {
            get
            {
                if (Session["SiteActionTemplates"] == null)
                    Session["SiteActionTemplates"] = new List<tbl_SiteActionTemplate>();
                return (List<tbl_SiteActionTemplate>)(Session["SiteActionTemplates"]);
            }
            set
            {
                Session["SiteActionTemplates"] = value;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            pnlValdationForm.Visible = false;
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public override void BindData()
        {
            base.BindData();

            var workflowTemplateId = Guid.Empty;

            if (!IsEditMode)
                workflowTemplateId = CurrentWorkflowTemplate;
            else if (EditObjectId.HasValue)
                workflowTemplateId = EditObjectId.Value;

            if (MaterialList.Count == 0)
            {
                Session["Materials"] = DataManager.Material.SelectByWorkflowTemplateId(workflowTemplateId).
                    Select(
                        a =>
                        new MaterialMap
                            {
                                ID = a.ID,
                                SiteID = a.SiteID,
                                Name = a.Name,
                                Type = a.Type,
                                Description = a.Description,
                                OldValue = a.Value,
                                Value = ((MaterialType)a.Type == MaterialType.Form && !IsEditMode) ? null : a.Value,
                                WorkflowTemplateID = a.WorkflowTemplateID
                            }).ToList();
            }


            var workflowTemplateElements = DataManager.WorkflowTemplateElement.SelectAll(workflowTemplateId).Where(a => a.ElementType == (int)WorkflowTemplateElementType.Message).ToList();
            if (SiteActionTemplateList.Count == 0)
            {
                foreach (var workflowTemplateElement in workflowTemplateElements)
                {
                    var workflowTemplateElementParameter = DataManager.WorkflowTemplateElementParameter.SelectByElementId(workflowTemplateElement.ID).FirstOrDefault(a => a.Name == "SiteActionTemplateID");
                    var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(workflowTemplateElementParameter.Value.ToGuid());
                    SiteActionTemplateList.Add(siteActionTemplate);

                    MaterialList.Add(new MaterialMap
                                         {
                                             ID = siteActionTemplate.ID,
                                             SiteID = CurrentUser.Instance.SiteID,
                                             Name = workflowTemplateElement.Name,
                                             OldValue = siteActionTemplate.ID.ToString(),
                                             Value = siteActionTemplate.ID.ToString(),
                                             Type = (int)MaterialType.ActionTemplate
                                         });
                }
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void lbtnNext_OnClick(object sender, EventArgs e)
        {
            if (MaterialList.Any(material => (MaterialType)material.Type == MaterialType.Form && material.Value == null))
            {
                pnlValdationForm.Visible = true;
                return;
            }

            ((WorkflowTemplateWizard)FindControlRecursive(Page, "ucWorkflowTemplateWizard")).Save(CurrentWorkflowTemplate);
            CurrentWorkflowTemplate = Guid.Empty;
        }



        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        private static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control rc = FindControlRecursive(c, id);
                if (rc != null)
                    return rc;
            }
            return null;
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
        /// Handles the OnItemDataBound event of the rgMaterials control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgMaterials_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (MaterialMap)e.Item.DataItem;

                if (!string.IsNullOrEmpty(data.Description))
                    ((Literal) item.FindControl("litDescription")).Text = data.Description + "<br />";

                if (item.FindControl("pnlUrl") != null)
                    item.FindControl("pnlUrl").Visible = false;
                if (item.FindControl("pnlFile") != null)
                    item.FindControl("pnlFile").Visible = false;
                if (item.FindControl("pnlForm") != null)
                    item.FindControl("pnlForm").Visible = false;

                ((Literal)item.FindControl("litType")).Text = EnumHelper.GetEnumDescription((MaterialType)data.Type);

                switch ((MaterialType)data.Type)
                {
                    case MaterialType.Url:
                        ((Literal)item.FindControl("litName")).Text = data.Name;

                        if ((Literal)item.FindControl("litValue") != null)
                            ((Literal)item.FindControl("litValue")).Text = data.Value;

                        if ((TextBox)item.FindControl("txtValue") != null)
                            ((TextBox)item.FindControl("txtValue")).Text = data.Value;
                        if (item.FindControl("pnlUrl") != null)
                            item.FindControl("pnlUrl").Visible = true;
                        break;
                    case MaterialType.File:
                        var siteActivityRuleFile = DataManager.Links.SelectById(data.Value.ToGuid());

                        ((Literal)item.FindControl("litName")).Text = data.Name;

                        if ((Literal)item.FindControl("litValue") != null)
                            ((Literal)item.FindControl("litValue")).Text = siteActivityRuleFile.Name;

                        var dcbFile = ((DictionaryOnDemandComboBox)item.FindControl("dcbFile"));
                        if (dcbFile != null)
                        {
                            dcbFile.SiteID = CurrentUser.Instance.SiteID;
                            dcbFile.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn { Name = "RuleTypeID", DbType = DbType.Int32, Value = ((int)RuleType.File).ToString() });
                            dcbFile.DataBind();

                            if (IsEditMode)
                                dcbFile.SelectedId = siteActivityRuleFile.ID;
                            dcbFile.SelectedText = siteActivityRuleFile.Name;
                        }
                        if (item.FindControl("pnlFile") != null)
                            item.FindControl("pnlFile").Visible = true;
                        break;
                    case MaterialType.Form:
                        ((Literal)item.FindControl("litName")).Text = data.Name;

                        tbl_SiteActivityRules siteActivityRuleForm = null;
                        if (data.Value != null)
                        {
                            siteActivityRuleForm = DataManager.SiteActivityRules.SelectById(data.Value.ToGuid());

                            if ((Literal)item.FindControl("litValue") != null)
                                ((Literal)item.FindControl("litValue")).Text = siteActivityRuleForm.Name;
                        }
                        else
                        {
                            if ((Literal)item.FindControl("litValue") != null)
                                ((Literal)item.FindControl("litValue")).Text = "Выберите форму";
                        }


                        var dcbForm = ((DictionaryOnDemandComboBox)item.FindControl("dcbForm"));
                        if (dcbForm != null)
                        {
                            dcbForm.SiteID = CurrentUser.Instance.SiteID;
                            dcbForm.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn { Name = "RuleTypeID", DbType = DbType.Int32, Value = ((int)RuleType.Form).ToString() });
                            dcbForm.DataBind();

                            if (siteActivityRuleForm != null)
                            {
                                if (IsEditMode)
                                    dcbForm.SelectedId = siteActivityRuleForm.ID;
                                dcbForm.SelectedText = siteActivityRuleForm.Name;
                            }
                        }

                        if (item.FindControl("pnlForm") != null)
                            item.FindControl("pnlForm").Visible = true;
                        break;
                    case MaterialType.ActionTemplate:
                        ((Literal)item.FindControl("litName")).Text = data.Name;

                        var oldValue = data.OldValue.ToGuid();
                        var siteActionTemplate = SiteActionTemplateList.FirstOrDefault(a => a.ID == oldValue);
                        ((Literal)item.FindControl("litValue")).Text = siteActionTemplate.Title;
                        break;
                }
            }
        }



        /// <summary>
        /// Handles the OnUpdateCommand event of the rgMaterials control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgMaterials_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var id = Guid.Parse(item.GetDataKeyValue("ID").ToString());
            var material = MaterialList.FirstOrDefault(a => a.ID == id);
            var newValue = material.Value;

            switch ((MaterialType)material.Type)
            {
                case MaterialType.Url:
                    newValue = ((TextBox)item.FindControl("txtValue")).Text;
                    foreach (var siteActionTemplate in SiteActionTemplateList)
                    {
                        siteActionTemplate.MessageCaption = siteActionTemplate.MessageCaption.Replace(material.Value, newValue);
                        siteActionTemplate.MessageBody = siteActionTemplate.MessageBody.Replace(material.Value, newValue);
                    }
                    break;
                case MaterialType.File:
                    var dcbFile = ((DictionaryOnDemandComboBox)item.FindControl("dcbFile"));
                    if (dcbFile.SelectedId != Guid.Empty)
                        newValue = dcbFile.SelectedId.ToString();
                    break;
                case MaterialType.Form:
                    var dcbForm = ((DictionaryOnDemandComboBox)item.FindControl("dcbForm"));
                    if (dcbForm.SelectedId != Guid.Empty)
                        newValue = dcbForm.SelectedId.ToString();
                    break;
            }

            material.Value = newValue;
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
                int maxCode = DataManager.Links.SelectByCode(siteId, code);
                if (maxCode != 0) maxCode++;
                link.Code = code + (maxCode != 0 ? String.Format("[{0}]", maxCode >= 10 ? maxCode.ToString() : "0" + maxCode.ToString()) : "");

                DataManager.Links.Add(link);

                ((DictionaryOnDemandComboBox)editFormTableCell.FindControl("dcbFile")).BindData();
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



        protected void rgMaterials_OnEditCommand(object sender, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            var id = Guid.Parse(editedItem.GetDataKeyValue("ID").ToString());
            var material = MaterialList.FirstOrDefault(a => a.ID == id);
            if (material.Type == (int)MaterialType.ActionTemplate)
            {
                var rolesList = new List<WorkflowTemplateWizardRole>();
                var gridRoles = ((RadGrid)MultiPage.FindPageViewByID("GeneralInformation").Controls[0].FindControl("rgContactRoles"));
                foreach (GridDataItem item in gridRoles.Items)
                {
                    rolesList.Add(new WorkflowTemplateWizardRole
                    {
                        OldContactRoleID = item.GetDataKeyValue("ContactRoleID").ToString().ToGuid(),
                        OldEmail = item.GetDataKeyValue("RoleInTemplate").ToString(),
                        ContactRoleID = ((DictionaryOnDemandComboBox)item.FindControl("dcbContactRole")).SelectedId,
                        Email = ((TextBox)item.FindControl("txtFromEmail")).Text,
                        DisplayName = ((TextBox)item.FindControl("txtFromName")).Text
                    });
                }

                var siteActionTemplate = SiteActionTemplateList.FirstOrDefault(a => a.ID == id);

                if (siteActionTemplate.FromContactRoleID.HasValue)
                {
                    var role = rolesList.FirstOrDefault(a => a.OldContactRoleID == siteActionTemplate.FromContactRoleID);
                    if (role != null)
                    {
                        if (role.ContactRoleID != Guid.Empty)
                            siteActionTemplate.FromContactRoleID = role.ContactRoleID;
                        else
                        {
                            siteActionTemplate.FromContactRoleID = null;
                            siteActionTemplate.FromEmail = role.Email;
                            if (!string.IsNullOrEmpty(role.DisplayName))
                                siteActionTemplate.FromName = role.DisplayName;
                            else
                                siteActionTemplate.FromName = null;
                        }
                    }
                }
                else
                {
                    var role = rolesList.FirstOrDefault(a => a.OldEmail == siteActionTemplate.FromEmail);
                    if (role != null)
                    {
                        if (role.ContactRoleID != Guid.Empty)
                            siteActionTemplate.FromContactRoleID = role.ContactRoleID;
                        else
                        {
                            siteActionTemplate.FromEmail = role.Email;
                            if (!string.IsNullOrEmpty(role.DisplayName))
                                siteActionTemplate.FromName = role.DisplayName;
                            else
                                siteActionTemplate.FromName = null;
                        }
                    }
                }

                foreach (var siteActionTemplateRecipient in siteActionTemplate.tbl_SiteActionTemplateRecipient)
                {
                    if (siteActionTemplateRecipient.ContactRoleID.HasValue)
                    {
                        var role = rolesList.FirstOrDefault(a => a.OldContactRoleID == siteActionTemplateRecipient.ContactRoleID);
                        if (role != null)
                        {
                            if (role.ContactRoleID != Guid.Empty)
                                siteActionTemplateRecipient.ContactRoleID = role.ContactRoleID;
                            else
                            {
                                siteActionTemplateRecipient.ContactRoleID = null;
                                siteActionTemplateRecipient.Email = role.Email;
                                if (!string.IsNullOrEmpty(role.DisplayName))
                                    siteActionTemplateRecipient.DisplayName = role.DisplayName;
                            }
                        }
                    }
                    else
                    {
                        var role = rolesList.FirstOrDefault(a => a.OldEmail == siteActionTemplateRecipient.Email);
                        if (role != null)
                        {
                            if (role.ContactRoleID != Guid.Empty)
                                siteActionTemplateRecipient.ContactRoleID = role.ContactRoleID;
                            else
                            {
                                siteActionTemplateRecipient.ContactRoleID = null;
                                siteActionTemplateRecipient.Email = role.Email;
                                if (!string.IsNullOrEmpty(role.DisplayName))
                                    siteActionTemplateRecipient.DisplayName = role.DisplayName;
                            }
                        }
                    }
                }

                if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_ShowSiteActionTemplateRadWindow"))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_ShowSiteActionTemplateRadWindow", "ShowSiteActionTemplate('" + id + "');", true);
                e.Canceled = true;
                e.Item.Edit = false;
            }
        }
    }
}