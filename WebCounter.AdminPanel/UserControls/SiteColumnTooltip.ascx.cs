using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SiteColumnTooltip : System.Web.UI.UserControl
    {
        private DataManager dataManager = new DataManager();
        public Guid siteID = new Guid();


        public event EventHandler Saved;


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteColumnID
        {
            get
            {
                object o = ViewState["SiteColumnID"];
                if (o == null || (Guid)o == Guid.Empty)
                {
                    ViewState["SiteColumnID"] = Guid.NewGuid();
                    return (Guid)ViewState["SiteColumnID"];
                }

                return (Guid)o;
                //return (o == null ? new Guid() : (Guid)o);
            }
            set
            {
                ViewState["SiteColumnID"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<tbl_SiteColumns> SiteColumns
        {
            get
            {
                object o = ViewState["SiteColumns"];
                return (o == null ? null : (List<tbl_SiteColumns>)o);
            }
            set
            {
                ViewState["SiteColumns"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<tbl_SiteColumnValues> SiteColumnValues
        {
            get
            {
                object o = ViewState["SiteColumnValues"];
                return (o == null ? null : (List<tbl_SiteColumnValues>)o);
            }
            set
            {
                ViewState["SiteColumnValues"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<tbl_SiteColumnValues> SiteColumnValuesEdit
        {
            get
            {
                object o = ViewState["SiteColumnValuesEdit"];
                return (o == null ? null : (List<tbl_SiteColumnValues>)o);
            }
            set
            {
                ViewState["SiteColumnValuesEdit"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public System.Collections.Generic.Dictionary<string, string> AdditionalFields
        {
            get
            {
                object o = ViewState["AdditionalFields"];
                return (o == null ? new Dictionary<string, string>() : (Dictionary<string, string>)o);
            }
            set
            {
                ViewState["AdditionalFields"] = value;
            }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            siteID = ((LeadForceBasePage)Page).SiteId;
        }


        public void BindData()
        {
            gvSiteColumnValues.Visible = false;

            var dynamicSiteColumns = SiteColumns ?? new List<tbl_SiteColumns>();
            var siteColumn = dynamicSiteColumns.FirstOrDefault(a => a.ID == SiteColumnID);

            ddlCategoryID.DataSource = dataManager.ColumnCategories.SelectAll(siteID);
            ddlCategoryID.DataTextField = "Title";
            ddlCategoryID.DataValueField = "ID";
            ddlCategoryID.DataBind();

            var columnTypes = new List<tbl_ColumnTypes>();
            if (AdditionalFields != null && AdditionalFields.Count > 0 && AdditionalFields["FormFieldType"] != null)
            {
                switch ((FormFieldType)int.Parse(AdditionalFields["FormFieldType"]))
                {
                    case FormFieldType.Input:
                        columnTypes = dataManager.ColumnTypes.SelectAll().Where(a => a.tbl_SiteColumns.Where(x => x.TypeID == (int)ColumnType.String || x.TypeID == (int)ColumnType.Number || x.TypeID == (int)ColumnType.Date).Count() > 0).ToList();
                        break;
                    case FormFieldType.Textarea:
                        columnTypes = dataManager.ColumnTypes.SelectAll().Where(a => a.tbl_SiteColumns.Where(x => x.TypeID == (int)ColumnType.Text).Count() > 0).ToList();
                        break;
                    case FormFieldType.Select:
                        columnTypes = dataManager.ColumnTypes.SelectAll().Where(a => a.tbl_SiteColumns.Where(x => x.TypeID == (int)ColumnType.Enum).Count() > 0).ToList();
                        break;
                }
            }
            else
                columnTypes = dataManager.ColumnTypes.SelectAll();

            ddTypeID.DataSource = columnTypes;
            ddTypeID.DataTextField = "Title";
            ddTypeID.DataValueField = "ID";
            ddTypeID.DataBind();

            if (siteColumn != null)
            {
                txtName_SiteColumnValue.Text = siteColumn.Name;
                txtCode_SiteColumnValue.Text = siteColumn.Code;
                ddlCategoryID.Items.FindByValue(siteColumn.CategoryID.ToString()).Selected = true;
                ddTypeID.Items.FindByValue(siteColumn.TypeID.ToString()).Selected = true;
                if ((ColumnType)siteColumn.TypeID == ColumnType.Enum)
                    gvSiteColumnValues.Visible = true;
            }
            else
            {
                txtName_SiteColumnValue.Text = string.Empty;
                txtCode_SiteColumnValue.Text = string.Empty;
                ddlCategoryID.Items[0].Selected = true;
                ddTypeID.Items[0].Selected = true;
                gvSiteColumnValues.Visible = false;
                if ((ColumnType)int.Parse(ddTypeID.SelectedValue) == ColumnType.Enum)
                    gvSiteColumnValues.Visible = true;
            }

            var dynamicSiteColumnValues = SiteColumnValues ?? new List<tbl_SiteColumnValues>();
            SiteColumnValuesEdit = dynamicSiteColumnValues.Where(a => a.SiteColumnID == SiteColumnID).ToList();

            BindSiteColumnTooltip();
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddTypeID control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvSiteColumnValues.Visible = false;
            if ((ColumnType)int.Parse(((DropDownList)sender).SelectedValue) == ColumnType.Enum)
            {
                gvSiteColumnValues.Visible = true;
            }
        }



        /// <summary>
        /// Binds the site column tooltip.
        /// </summary>
        private void BindSiteColumnTooltip()
        {
            //var dynamicSiteColumnValues = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValues"] ?? new List<tbl_SiteColumnValues>();
            var dynamicSiteColumnValuesEdit = SiteColumnValuesEdit ?? new List<tbl_SiteColumnValues>();

            var columnValues = dynamicSiteColumnValuesEdit.Where(a => a.SiteColumnID == SiteColumnID).ToList();
            if (columnValues.Count > 0)
            {
                gvSiteColumnValues.DataSource = columnValues;
                gvSiteColumnValues.DataBind();
            }
            else
            {
                var fakeRow = new List<tbl_SiteColumnValues>();
                fakeRow.Add(new tbl_SiteColumnValues { Value = string.Empty });
                gvSiteColumnValues.DataSource = fakeRow;
                gvSiteColumnValues.DataBind();
                gvSiteColumnValues.Rows[0].Visible = false;
            }
        }


        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var siteColumnId = SiteColumnID;
            var dynamicSiteColumns = SiteColumns ?? new List<tbl_SiteColumns>();

            var checkCode = dataManager.SiteColumns.SelectByCode(siteID, txtCode_SiteColumnValue.Text);
            if (checkCode != null && checkCode.ID == siteColumnId)
                checkCode = null;

            if (checkCode == null)
            {
                checkCode = dynamicSiteColumns.FirstOrDefault(a => a.SiteID == siteID && a.Code == txtCode_SiteColumnValue.Text);
                if (checkCode != null && checkCode.ID == siteColumnId)
                    checkCode = null;
            }

            TooltipErrorMessage.Text = "";
            if (checkCode == null)
            {
                var siteColumn = dynamicSiteColumns.FirstOrDefault(a => a.ID == SiteColumnID) ?? new tbl_SiteColumns();

                if (siteColumn.ID == Guid.Empty)
                    siteColumn.ID = SiteColumnID; //Guid.NewGuid();
                siteColumn.SiteID = siteID;
                siteColumn.CategoryID = Guid.Parse(ddlCategoryID.SelectedValue);
                siteColumn.TypeID = int.Parse(ddTypeID.SelectedValue);
                siteColumn.Name = txtName_SiteColumnValue.Text;
                siteColumn.Code = txtCode_SiteColumnValue.Text;

                if (AdditionalFields != null && AdditionalFields.Count > 0 && AdditionalFields["SiteActivityRuleID"] != null)
                {
                    siteColumn.SiteActivityRuleID = Guid.Parse(AdditionalFields["SiteActivityRuleID"]);
                }

                int index = dynamicSiteColumns.FindIndex(a => a.ID == siteColumn.ID);
                if (index != -1)
                    dynamicSiteColumns[index] = siteColumn;
                else
                    dynamicSiteColumns.Add(siteColumn);

                var dynamicSiteColumnValues = SiteColumnValues ?? new List<tbl_SiteColumnValues>();
                var dynamicSiteColumnValuesEdit = SiteColumnValuesEdit ?? new List<tbl_SiteColumnValues>();

                var valueIDs = dynamicSiteColumnValues.Where(a => a.SiteColumnID == SiteColumnID).Select(a => a.ID).ToList();
                foreach (var valueID in valueIDs)
                {
                    var removeItem = dynamicSiteColumnValues.FirstOrDefault(a => a.ID == valueID);
                    dynamicSiteColumnValues.Remove(removeItem);
                }

                dynamicSiteColumnValues.AddRange(dynamicSiteColumnValuesEdit);

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "hideTooltip", "HideTooltip()", true);

                ////LoadProperties();
                ////UpdatePanel1.Update();

                SiteColumnID = siteColumn.ID;
                SiteColumns = dynamicSiteColumns;
                SiteColumnValues = dynamicSiteColumnValues;
                SiteColumnValuesEdit = dynamicSiteColumnValuesEdit;

                if (Saved != null)
                    Saved(this, EventArgs.Empty);                        

            }
            else
                TooltipErrorMessage.Text = "Реквизит с таким кодом уже существует.<br /><br />";
        }




        /// <summary>
        /// Handles the RowEditing event of the gvSiteColumnValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
        protected void gvSiteColumnValues_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSiteColumnValues.EditIndex = e.NewEditIndex;
            BindSiteColumnTooltip();
        }



        /// <summary>
        /// Handles the RowCancelingEdit event of the gvSiteColumnValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
        protected void gvSiteColumnValues_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSiteColumnValues.EditIndex = -1;
            BindSiteColumnTooltip();
        }



        /// <summary>
        /// Handles the RowUpdating event of the gvSiteColumnValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewUpdateEventArgs"/> instance containing the event data.</param>
        protected void gvSiteColumnValues_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var txtValue = (TextBox)gvSiteColumnValues.Rows[e.RowIndex].Cells[0].FindControl("txtValue");

            //var dynamicSiteColumnValues = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValues"] ?? new List<tbl_SiteColumnValues>();
            var dynamicSiteColumnValuesEdit = SiteColumnValuesEdit ?? new List<tbl_SiteColumnValues>();

            dynamicSiteColumnValuesEdit.FirstOrDefault(a => a.ID == Guid.Parse(gvSiteColumnValues.DataKeys[e.RowIndex].Value.ToString())).Value = txtValue.Text;

            SiteColumnValuesEdit = dynamicSiteColumnValuesEdit;

            gvSiteColumnValues.EditIndex = -1;
            BindSiteColumnTooltip();
        }



        /// <summary>
        /// Handles the Click event of the BtnAddValue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnAddValue_Click(object sender, EventArgs e)
        {
            //var dynamicSiteColumnValues = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValues"] ?? new List<tbl_SiteColumnValues>();
            var dynamicSiteColumnValuesEdit = SiteColumnValuesEdit ?? new List<tbl_SiteColumnValues>();

            var txtValue = (TextBox)gvSiteColumnValues.FooterRow.Cells[0].FindControl("txtValue");

            var siteColumnValue = new tbl_SiteColumnValues();
            siteColumnValue.ID = Guid.NewGuid();
            siteColumnValue.SiteColumnID = SiteColumnID;
            siteColumnValue.Value = txtValue.Text;

            dynamicSiteColumnValuesEdit.Add(siteColumnValue);

            SiteColumnValuesEdit = dynamicSiteColumnValuesEdit;

            gvSiteColumnValues.EditIndex = -1;
            BindSiteColumnTooltip();
        }



        /// <summary>
        /// Handles the RowDeleting event of the gvSiteColumnValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void gvSiteColumnValues_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //var dynamicSiteColumnValues = (List<tbl_SiteColumnValues>)ViewState["DynamicSiteColumnValues"] ?? new List<tbl_SiteColumnValues>();
            var dynamicSiteColumnValuesEdit = SiteColumnValuesEdit ?? new List<tbl_SiteColumnValues>();

            var removeItem = dynamicSiteColumnValuesEdit.FirstOrDefault(a => a.ID == Guid.Parse(gvSiteColumnValues.DataKeys[e.RowIndex].Value.ToString()));
            dynamicSiteColumnValuesEdit.Remove(removeItem);

            SiteColumnValuesEdit = dynamicSiteColumnValuesEdit;

            BindSiteColumnTooltip();
        }
    }
}