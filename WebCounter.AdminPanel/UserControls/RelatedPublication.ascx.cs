using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class RelatedPublication : System.Web.UI.UserControl
    {
        private DataManager dataManager = new DataManager();

        [Serializable]
        private class RelatedPublicationStructure
        {
            public Guid ID { get; set; }
            public string Title { get; set; }
            public Guid BasePublicationID { get; set; }
            public Guid? RecordID { get; set; }
            public Guid ModuleID { get; set; }
            public string Module { get; set; }

    }

        private List<RelatedPublicationStructure> _relatedPublicationStructure = new List<RelatedPublicationStructure>();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid PublicationID
        {
            get
            {
                object o = ViewState["PublicationID"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["PublicationID"] = value;
            }
        }



        /// <summary>
        /// Gets or sets the siteID.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        public Guid SiteID
        {
            get
            {
                if (ViewState["SiteID"] == null)
                    return Guid.Empty;
                return (Guid)ViewState["SiteID"];
            }
            set { ViewState["SiteID"] = value; }
        }




        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindPublication();
                rgRelatedPublication.Culture = new CultureInfo("ru-RU");
            }

        }



        private void BindPublication()
        {

            foreach (var v in dataManager.RelatedPublication.SelectByPublicationId(PublicationID).ToList())
            {
                _relatedPublicationStructure.Add(new RelatedPublicationStructure() { ID = v.ID, BasePublicationID = PublicationID, RecordID = v.RecordID, Title = v.RecordID!=null ? dataManager.RelatedPublication.GetRecordTitle(v.tbl_Module.TableName, v.RecordID):"", Module = (v.tbl_Module != null ? v.tbl_Module.Title : ""), ModuleID = v.ModuleID });
            }
            ViewState["RelatedPublication"] = _relatedPublicationStructure;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgRelatedPublication control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgRelatedPublication_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;
                var item = e.Item as GridEditableItem;


                var rcbModules = (RadComboBox)item.FindControl("rcbModules");
                var modules = dataManager.Module.SelectAll().Where(a => a.TableName != null);
                rcbModules.DataSource = modules;
                rcbModules.DataTextField = "Title";
                rcbModules.DataValueField = "ID";
                rcbModules.DataBind();

                var rcbRecords = (RadComboBox)item.FindControl("rcbRecords");
                var edsRecords = (EntityDataSource)item.FindControl("edsRecords");
                rcbRecords.Items.Clear();
                var module = modules.FirstOrDefault();
                if (module != null)
                {
                    PopulateRecordsList(rcbRecords, module.ID.ToString());
                }

                if (!(gridEditFormItem.DataItem is GridInsertionObject))
                {
                    var relatedPublication = (RelatedPublicationStructure)gridEditFormItem.DataItem;

                    if (relatedPublication != null)
                    {
                        rcbModules.SelectedIndex = rcbModules.Items.IndexOf(rcbModules.Items.FindItemByValue((relatedPublication.ModuleID).ToString()));

                        rcbRecords.Items.Clear();
                        module = modules.Where(a => a.ID == relatedPublication.ModuleID).FirstOrDefault();
                        if (module != null)
                        {
                            PopulateRecordsList(rcbRecords, module.ID.ToString());
                            rcbRecords.SelectedIndex = rcbRecords.Items.IndexOf(rcbRecords.Items.FindItemByValue((relatedPublication.RecordID ?? Guid.Empty).ToString()));
                        }
                    }
                }
            }
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgRelatedPublication control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgRelatedPublication_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgRelatedPublication.DataSource = ViewState["RelatedPublication"];
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgRelatedPublication control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgRelatedPublication_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var id = Guid.Parse(item.GetDataKeyValue("ID").ToString());
            var productComplectation = ((List<RelatedPublicationStructure>)ViewState["RelatedPublication"]).Where(s => s.ID == id).FirstOrDefault();
            productComplectation.RecordID = Guid.Parse(((RadComboBox)item.FindControl("rcbRecords")).SelectedValue);
            productComplectation.ModuleID = Guid.Parse(((RadComboBox)item.FindControl("rcbModules")).SelectedValue);
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgRelatedPublication control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgRelatedPublication_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //var item = e.Item as GridEditableItem;
            var relatedPublication = new RelatedPublicationStructure();
            relatedPublication.ID = Guid.NewGuid();
            relatedPublication.BasePublicationID = PublicationID;
            relatedPublication.RecordID = Guid.Parse(((RadComboBox)e.Item.FindControl("rcbRecords")).SelectedValue);
            relatedPublication.ModuleID = Guid.Parse(((RadComboBox)e.Item.FindControl("rcbModules")).SelectedValue);
            var module = dataManager.Module.SelectAll().Where(a => a.ID == relatedPublication.ModuleID).FirstOrDefault();
            relatedPublication.Title = (relatedPublication.RecordID != null && relatedPublication.RecordID != Guid.Empty)
                                           ? dataManager.RelatedPublication.GetRecordTitle(module.TableName,
                                                                                           relatedPublication.RecordID)
                                           : "";
            relatedPublication.Module = module.Title;

            ((List<RelatedPublicationStructure>)ViewState["RelatedPublication"]).Add(relatedPublication);
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgRelatedPublication control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgRelatedPublication_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<RelatedPublicationStructure>)ViewState["RelatedPublication"]).Remove(
                ((List<RelatedPublicationStructure>)ViewState["RelatedPublication"]).Where(s => s.ID == id).FirstOrDefault());
        }




        public void Save(Guid BasePublicationID)
        {
            List<tbl_RelatedPublication> relatedPublication = new List<tbl_RelatedPublication>();

            foreach (var v in (List<RelatedPublicationStructure>)ViewState["RelatedPublication"])
            {
                relatedPublication.Add(new tbl_RelatedPublication() { ID = v.ID, PublicationID = BasePublicationID, RecordID = v.RecordID != Guid.Empty ? v.RecordID:null, ModuleID = v.ModuleID});
            }

            dataManager.RelatedPublication.DeleteAll(PublicationID);
            dataManager.RelatedPublication.Add(relatedPublication);

        }



        protected void PopulateRecordsList(object sender, string moduleId)
        {
            Guid gModuleId;
            if (Guid.TryParse(moduleId, out gModuleId))
            {

            RadComboBox rcbRecords = (RadComboBox)sender;
            rcbRecords.Items.Clear();
            var edsRecords = (EntityDataSource)rcbRecords.Parent.FindControl("edsRecords");
            var module = dataManager.Module.SelectById(gModuleId);
            if (module != null)
            {
                edsRecords.EntitySetName = module.TableName;
                edsRecords.WhereParameters.Clear();
                if (module.Name != "Dictionaries")
                {
                    edsRecords.Where = "it.[SiteID] = GUID '" + SiteID.ToString() + "'";
                }
                rcbRecords.DataSourceID = "edsRecords";
                switch (module.Name)
                {
                    case "Orders":
                        rcbRecords.DataTextField = "Number";
                        break;
                    case "Companies":
                        rcbRecords.DataTextField = "Name";
                        break;
                    case "Contacts":
                        rcbRecords.DataTextField = "UserFullName";
                        edsRecords.Where = "it.[SiteID] = GUID '" + SiteID.ToString() +
                                           "' AND it.[UserFullName] IS NOT NULL";
                        break;
                    case "SourceMonitorings":
                        rcbRecords.DataTextField = "Name";
                        break;
                    case "MassMails":
                        rcbRecords.DataTextField = "Name";
                        break;
                    default:
                        rcbRecords.DataTextField = "Title";
                        break;
                }
                rcbRecords.DataValueField = "ID";
                rcbRecords.DataBind();
                rcbRecords.Items.Insert(0, new RadComboBoxItem("Выберите значение", Guid.Empty.ToString()){Selected = true});
                //rcbRecords.Text = "Выберите значение";
            }
            }
        }

        protected void rcbModules_IndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox rcbRecords = (RadComboBox)((RadComboBox)sender).Parent.FindControl("rcbRecords");
            PopulateRecordsList(rcbRecords, e.Value);
        }
    }
}