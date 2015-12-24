using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Labitec.UI.Dictionary;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class SiteColumn : LeadForceBasePage
    {        
        private Guid _siteColumnId;
        public string strCommandArgument = string.Empty;     

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Реквизиты посетителя - LeadForce";

            string sSiteColumnId = Page.RouteData.Values["ID"] as string;
            
            hlCancel.NavigateUrl = UrlsData.AP_SiteColumns();
            if (!string.IsNullOrEmpty(sSiteColumnId))
            {
                Guid.TryParse(sSiteColumnId, out _siteColumnId);
            }

            if (!Page.IsPostBack)
            {
                BindaCategories();

                ddTypeID.DataSource = DataManager.ColumnTypes.SelectAll();
                ddTypeID.DataTextField = "Title";
                ddTypeID.DataValueField = "ID";
                ddTypeID.DataBind();

                if (_siteColumnId != Guid.Empty)
                {
                    var siteColumn = DataManager.SiteColumns.SelectById(SiteId, _siteColumnId);
                    if (siteColumn != null)
                    {
                        txtName.Text = siteColumn.Name;
                        txtCode.Text = siteColumn.Code;
                        ddlCategoryID.Items.FindByValue(siteColumn.CategoryID.ToString()).Selected = true;
                        ddTypeID.Items.FindByValue(siteColumn.TypeID.ToString()).Selected = true;
                        if ((ColumnType)siteColumn.TypeID == ColumnType.Enum)
                            gvSiteColumnValues.Visible = true;
                    }

                    ViewState["SiteColumnValues"] = DataManager.SiteColumnValues.SelectAll(_siteColumnId);
                }

                BindData();
            }

            var radAjaxManager = RadAjaxManager.GetCurrent(this.Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbcDictionaryCategory, ddlCategoryID, null);
            var filters = new List<FilterColumn> { new FilterColumn() { Name = "SiteID", DbType = DbType.Guid, Value = SiteId.ToString() } };
            lbcDictionaryCategory.Filters = filters;
            lbcDictionaryCategory.DictionaryChanged += lbcDictionaryCategory_DictionaryChanged;
        }



        /// <summary>
        /// Bindas the categories.
        /// </summary>
        private void BindaCategories()
        {
            ddlCategoryID.DataSource = DataManager.ColumnCategories.SelectAll(SiteId);
            ddlCategoryID.DataTextField = "Title";
            ddlCategoryID.DataValueField = "ID";
            ddlCategoryID.DataBind();
        }



        /// <summary>
        /// LBCs the dictionary category dictionary changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        void lbcDictionaryCategory_DictionaryChanged(object sender)
        {
            BindaCategories();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (ViewState["SiteColumnValues"] != null && ((List<tbl_SiteColumnValues>)ViewState["SiteColumnValues"]).Count > 0)
            {
                gvSiteColumnValues.DataSource = (List<tbl_SiteColumnValues>) ViewState["SiteColumnValues"];
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
        /// Handles the RowEditing event of the gvSiteColumnValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
        protected void gvSiteColumnValues_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSiteColumnValues.EditIndex = e.NewEditIndex;
            BindData();
        }



        /// <summary>
        /// Handles the RowCancelingEdit event of the gvSiteColumnValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
        protected void gvSiteColumnValues_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSiteColumnValues.EditIndex = -1;
            BindData();
        }



        /// <summary>
        /// Handles the RowUpdating event of the gvSiteColumnValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewUpdateEventArgs"/> instance containing the event data.</param>
        protected void gvSiteColumnValues_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var txtValue = (TextBox)gvSiteColumnValues.Rows[e.RowIndex].Cells[0].FindControl("txtValue");

            var siteColumnValues = new List<tbl_SiteColumnValues>();
            if (ViewState["SiteColumnValues"] != null)
                siteColumnValues = (List<tbl_SiteColumnValues>)ViewState["SiteColumnValues"];

            siteColumnValues[e.RowIndex].Value = txtValue.Text;

            ViewState["SiteColumnValues"] = siteColumnValues;

            gvSiteColumnValues.EditIndex = -1;
            BindData();
        }



        /// <summary>
        /// Handles the Click event of the BtnAddValue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnAddValue_Click(object sender, EventArgs e)
        {
            var siteColumnValues = new List<tbl_SiteColumnValues>();
            if (ViewState["SiteColumnValues"] != null)
                siteColumnValues = (List<tbl_SiteColumnValues>)ViewState["SiteColumnValues"];

            var txtValue = (TextBox)gvSiteColumnValues.FooterRow.Cells[0].FindControl("txtValue");

            var siteColumnValue = new tbl_SiteColumnValues();
            siteColumnValue.Value = txtValue.Text;

            siteColumnValues.Add(siteColumnValue);
            ViewState["SiteColumnValues"] = siteColumnValues;

            gvSiteColumnValues.EditIndex = -1;
            BindData();
        }



        /// <summary>
        /// Handles the RowDeleting event of the gvSiteColumnValues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void gvSiteColumnValues_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var siteColumnValues = new List<tbl_SiteColumnValues>();
            if (ViewState["SiteColumnValues"] != null)
                siteColumnValues = (List<tbl_SiteColumnValues>)ViewState["SiteColumnValues"];

            siteColumnValues.RemoveAt(e.RowIndex);
            ViewState["SiteColumnValues"] = siteColumnValues;

            BindData();
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
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var siteColumns = new tbl_SiteColumns();

            var checkCode = DataManager.SiteColumns.SelectByCode(SiteId, txtCode.Text);

            if (_siteColumnId != Guid.Empty)
            {
                siteColumns = DataManager.SiteColumns.SelectById(SiteId, _siteColumnId);
                if (checkCode != null && checkCode.ID == _siteColumnId)
                    checkCode = null;
            }

            ErrorMessage.Text = "";
            if (checkCode == null)
            {
                siteColumns.SiteID = SiteId;
                siteColumns.CategoryID = Guid.Parse(ddlCategoryID.SelectedValue);
                siteColumns.TypeID = int.Parse(ddTypeID.SelectedValue);
                siteColumns.Name = txtName.Text;
                siteColumns.Code = txtCode.Text;
                if (_siteColumnId != Guid.Empty)
                {
                    DataManager.SiteColumns.Update(siteColumns);
                }
                else
                {
                    siteColumns = DataManager.SiteColumns.Add(siteColumns);
                    _siteColumnId = siteColumns.ID;
                }

                if ((ColumnType)siteColumns.TypeID != ColumnType.Enum)
                {
                    var siteColumnValues = DataManager.SiteColumnValues.SelectAll(_siteColumnId);
                    foreach (var item in siteColumnValues)
                    {
                        try
                        {
                            DataManager.SiteColumnValues.Delete(item);
                        }
                        catch { }
                    }
                }
                else
                {
                    var siteColumnValues = new List<tbl_SiteColumnValues>();
                    if (ViewState["SiteColumnValues"] != null)
                    {
                        var siteColumnValuesOld = DataManager.SiteColumnValues.SelectAll(_siteColumnId);
                        siteColumnValues = (List<tbl_SiteColumnValues>)ViewState["SiteColumnValues"];
                        foreach (var siteColumnValue in siteColumnValues)
                        {
                            siteColumnValue.SiteColumnID = _siteColumnId;
                            var removeItemsiteColumnValue = siteColumnValuesOld.SingleOrDefault(a => a.ID == siteColumnValue.ID);
                            if (removeItemsiteColumnValue != null)
                            {
                                DataManager.SiteColumnValues.Update(siteColumnValue);
                                siteColumnValuesOld.Remove(removeItemsiteColumnValue);
                            }
                            else
                                DataManager.SiteColumnValues.Add(siteColumnValue);
                        }

                        if (siteColumnValuesOld != null && siteColumnValuesOld.Count > 0)
                        {
                            foreach (var item in siteColumnValuesOld)
                            {
                                try
                                {
                                    DataManager.SiteColumnValues.Delete(item);
                                }
                                catch { }
                            }
                        }
                    }
                }

                Response.Redirect(UrlsData.AP_SiteColumns());
            }
            else
                ErrorMessage.Text = "Реквизит с таким кодом уже существует.<br /><br />";
        }
    }
}