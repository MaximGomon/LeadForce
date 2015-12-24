using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.WorkflowTemplateWizard
{
    public partial class Qualification : WorkflowTemplateWizardStep
    {
        private int _contactObjectTypeId;

        protected void Page_Load(object sender, EventArgs e)
        {
            _contactObjectTypeId = DataManager.ObjectTypes.SelectByName("tbl_Contact").ID;
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public override void BindData()
        {
            base.BindData();
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgWorkflowTemplateElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElement_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (tbl_WorkflowTemplateElement)e.Item.DataItem;

                ((Literal)e.Item.FindControl("workflowElementTemplateName")).Text = data.Name;
                ((Literal)e.Item.FindControl("workflowElementTemplateDescription")).Text = data.Description;

                var dcbTag = ((DictionaryOnDemandComboBox)e.Item.FindControl("dcbTag"));
                dcbTag.SiteID = CurrentUser.Instance.SiteID;
                var filter = new DictionaryOnDemandComboBox.DictionaryFilterColumn
                {
                    Name = "ObjectTypeID",
                    DbType = DbType.Int32,
                    Value = _contactObjectTypeId.ToString()
                };
                dcbTag.Filters.Add(filter);
                dcbTag.DataBind();

                if (IsEditMode)
                {
                    var tag = DataManager.WorkflowTemplateElementTag.SelectAll(data.ID).ToList()[0];
                    ((RadioButtonList)e.Item.FindControl("rblOperation")).Items.FindByValue(tag.Operation.ToString()).Selected = true;
                    var siteTag = DataManager.SiteTags.SelectById(tag.SiteTagID);
                    dcbTag.SelectedId = tag.SiteTagID;
                    dcbTag.SelectedText = siteTag.Name;
                }
            }
        }



        /// <summary>
        /// Handles the OnNeedDataSource event of the rgWorkflowTemplateElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgWorkflowTemplateElement_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Guid workflowTemplateId = Guid.Empty;

            if (!IsEditMode)
                workflowTemplateId = CurrentWorkflowTemplate;
            else if (EditObjectId.HasValue)
                workflowTemplateId = EditObjectId.Value;

            var workflowTemplateElements = DataManager.WorkflowTemplateElement.SelectAll(workflowTemplateId).Where(a => a.ElementType == (int)WorkflowTemplateElementType.Tag).ToList();
            if (workflowTemplateElements.Count > 0)
                rgWorkflowTemplateElement.DataSource = workflowTemplateElements;
        }
    }
}