using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Excel;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;


namespace WebCounter.AdminPanel
{
    public partial class ImportEdit : LeadForceBasePage
    {
        private Guid _importId;
        private bool isPreviewed;
        private byte countPreviewRows = 15;
        private List<string> columns = new List<string>(); 
        private List<tbl_ImportColumn> _importColumns = new List<tbl_ImportColumn>(); 
        private List<tbl_ImportColumnRule> _importColumnRules = new List<tbl_ImportColumnRule>();


        public string CsvSeparator
        {
            get
            {
                return !string.IsNullOrEmpty(rcbCsvSeparator.SelectedValue)
                           ? rcbCsvSeparator.SelectedValue
                           : rcbCsvSeparator.Text;
            }
        }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Импорт - LeadForce";

            if (Page.RouteData.Values["id"] != null)
                _importId = Guid.Parse(Page.RouteData.Values["id"] as string);

            hlCancel.NavigateUrl = UrlsData.AP_Imports();

            if (!Page.IsPostBack)
            {
                ViewState["SheetName"] = string.Empty;

                BindData();
                BindImportColumns();

                _importColumnRules = DataManager.ImportColumnRules.SelectByImportID(_importId);
                BindImportColumnRules();
            }
            else
            {

                if (Request.Form["__EVENTTARGET"] == "ctl00$LabitecPage$ctl02$ContentHolder$lbtnSave") // !!!
                    isPreviewed = true;
            }

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(pnlWarning, pnlWarning);
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            EnumHelper.EnumToDropDownList<ImportType>(ref ddlType, false);

            rcbCsvSeparator.Items.Add(new RadComboBoxItem(",", ","));
            rcbCsvSeparator.Items.Add(new RadComboBoxItem(";", ";"));
            rcbCsvSeparator.Items.Add(new RadComboBoxItem("TAB", "t"));
            rcbCsvSeparator.SelectedIndex = 0;

            var import = DataManager.Import.SelectByID(SiteId, _importId);
            if (import != null)
            {
                txtName.Text = import.Name;
                ddlSheet.Items.Add(new ListItem(import.SheetName, import.SheetName));
                ddlImportTable.Items.FindByValue(import.ImportTable.ToString()).Selected = true;
                txtFirstRow.Text = import.FirstRow.ToString();
                txtFirstColumn.Text = import.FirstColumn.ToString();
                cbIsFirstRowAsColumnNames.Checked = import.IsFirstRowAsColumnNames;
                ddlType.Items.FindByValue(import.Type.ToString()).Selected = true;
                if ((ImportType)import.Type == ImportType.Csv)
                {
                    if (rcbCsvSeparator.FindItemIndexByValue(import.CsvSeparator, true) != -1)
                        rcbCsvSeparator.FindItemByValue(import.CsvSeparator).Selected = true;
                    else
                        rcbCsvSeparator.Text = import.CsvSeparator;
                }

                ViewState["SheetName"] = import.SheetName;

                ucImportTag.ImportId = _importId;

                pnlExcelSettings.Visible = ddlType.SelectedValue.ToEnum<ImportType>() == ImportType.Excel;
                pnlCsvSettings.Visible = ddlType.SelectedValue.ToEnum<ImportType>() == ImportType.Csv;
            }
        }



        /// <summary>
        /// Binds the import columns.
        /// </summary>
        protected void BindImportColumns()
        {
            _importColumns = DataManager.ImportColumn.SelectByImportID(_importId);
            rgImportColumns.DataSource = _importColumns;
            rgImportColumns.DataBind();
        }



        /// <summary>
        /// Binds the import column rules.
        /// </summary>
        protected void BindImportColumnRules()
        {
            rgImportColumnRules.DataSource = DataManager.ImportField.SelectAll().Where(a => (a.Order < 9 || a.Order > 12) && (a.Order < 24 || a.Order > 27));
            rgImportColumnRules.DataBind();

            upImportColumnRules.Update();
        }



        /// <summary>
        /// Saves this instance.
        /// </summary>
        protected void Save()
        {
            var import = DataManager.Import.SelectByID(SiteId, _importId) ?? new tbl_Import();

            // Save Import
            import.SiteID = SiteId;
            import.Name = txtName.Text;
            import.ImportTable = byte.Parse(ddlImportTable.SelectedValue);
            import.SheetName = ddlSheet.SelectedValue;
            import.FirstRow = int.Parse(txtFirstRow.Text);
            import.FirstColumn = int.Parse(txtFirstColumn.Text);
            import.IsFirstRowAsColumnNames = cbIsFirstRowAsColumnNames.Checked;
            import.Type = (int)ddlType.SelectedValue.ToEnum<ImportType>();
            import.CsvSeparator = ddlType.SelectedValue.ToEnum<ImportType>() == ImportType.Csv ? CsvSeparator : null;

            if (import.ID == Guid.Empty)
            {
                import.ID = Guid.NewGuid();
                DataManager.Import.Add(import);
            }
            else
                DataManager.Import.Update(import);

            _importId = import.ID;

            // Save Import Columns
            DataManager.ImportColumn.DeleteByImportID(import.ID);
            byte order = 1;
            foreach (GridDataItem item in rgImportColumns.Items)
            {
                var importColumnID = Guid.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());
                var importColumn = new tbl_ImportColumn
                {
                    ID = importColumnID,
                    ImportID = import.ID,
                    Name = ((RadTextBox)item.FindControl("txtName")).Text,
                    Source = ((HtmlGenericControl)item.FindControl("spanSource")).InnerText,
                    SystemName = string.Format("Column{0}", order + txtFirstColumn.Value - 1),
                    PrimaryKey = ((CheckBox)item.FindControl("cbPrimaryKey")).Checked,
                    SecondaryKey = ((CheckBox)item.FindControl("cbSecondaryKey")).Checked,
                    Order = order
                };
                DataManager.ImportColumn.Add(importColumn);
                order++;
            }

            // Save Import Column Rules
            DataManager.ImportColumnRules.DeleteByImportID(import.ID);
            foreach (GridDataItem item in rgImportColumnRules.Items)
            {
                var importFieldID = Guid.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());
                if (!string.IsNullOrEmpty(((DropDownList)item.FindControl("ddlImportColumns")).SelectedValue))
                {
                    var importColumnRule = new tbl_ImportColumnRule
                    {
                        ImportID = import.ID,
                        ImportFieldID = importFieldID,
                        ImportColumnID = Guid.Parse(((DropDownList)item.FindControl("ddlImportColumns")).SelectedValue),
                        IsRequired = ((CheckBox)item.FindControl("cbIsRequired")).Checked,
                        SQLCode = ((RadTextBox)item.FindControl("txtSQLCode")).Text
                    };
                    /*if (((DropDownList)item.FindControl("ddlImportFieldDictionary")).Visible)
                        importColumnRule.ImportFieldDictionaryID = Guid.Parse(((DropDownList)item.FindControl("ddlImportFieldDictionary")).SelectedValue);
                    else
                        importColumnRule.ImportFieldDictionaryID = null;*/

                    DataManager.ImportColumnRules.Add(importColumnRule);
                }
            }

            // Save Import Tags
            ucImportTag.ImportId = _importId;
            ucImportTag.Save();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSaveAndImport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSaveAndImport_OnClick(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 1200;

            Save();

            if (uploadedFile.UploadedFiles.Count > 0)
            {
                if (ddlType.SelectedValue.ToEnum<ImportType>() == ImportType.Excel) // Excel
                {
                    var sheet = ViewState["SheetName"].ToString();
                    IExcelDataReader excelReader = null;
                    if (uploadedFile.UploadedFiles[0].GetExtension() == ".xls")
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(uploadedFile.UploadedFiles[0].InputStream);
                    }
                    if (uploadedFile.UploadedFiles[0].GetExtension() == ".xlsx")
                    {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(uploadedFile.UploadedFiles[0].InputStream);
                    }
                    if (excelReader != null)
                    {
                        var result = excelReader.AsDataSet();

                        ddlSheet.Items.Clear();
                        foreach (DataTable table in result.Tables)
                            ddlSheet.Items.Add(new ListItem(table.TableName, table.TableName));

                        if (!string.IsNullOrEmpty(sheet) && ddlSheet.Items.FindByValue(sheet) != null)
                            ddlSheet.Items.FindByValue(sheet).Selected = true;
                        else
                        {
                            sheet = ddlSheet.Items[0].Value;
                            ViewState["SheetName"] = sheet;
                        }

                        if (result.Tables[sheet].Rows.Count > 0)
                        {
                            var tmpImportTableName = string.Format("TmpImport_{0}", _importId.ToString().Replace("-", ""));
                            var sqlTableCreator = new SqlTableCreator();
                            sqlTableCreator.Connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]);
                            sqlTableCreator.Connection.Open();
                            sqlTableCreator.DestinationTableName = tmpImportTableName;
                            sqlTableCreator.CreateFromDataTable(result.Tables[sheet]);
                            sqlTableCreator.Connection.Close();

                            if (txtFirstRow.Value > 1) // удаление лишних строк
                            {
                                for (int i = 1; i < txtFirstColumn.Value; i++)
                                    result.Tables[sheet].Rows.RemoveAt(i - 1);
                            }

                            if (cbIsFirstRowAsColumnNames.Checked)
                                result.Tables[sheet].Rows.RemoveAt(0);
                            BulkInsertDataTable(ConfigurationManager.AppSettings["ADONETConnectionString"], tmpImportTableName, result.Tables[sheet]);

                            DataManager.Import.DoImport(_importId);
                        }
                    }
                }
                else // CSV
                {
                    var sr = new StreamReader(uploadedFile.UploadedFiles[0].InputStream, Encoding.GetEncoding("windows-1251"));
                    var csvDataTable = new DataTable();
                    string strTextFromFile = "";
                    string[] strArrSplitText = null;
                    while ((strTextFromFile = sr.ReadLine()) != null)
                    {
                        var options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase));
                        var r = new Regex(string.Format(@"\{0}(?=(?:[^\""]*\""[^\""]*\"")*(?![^\""]*\""))", CsvSeparator), options);

                        strArrSplitText = r.Split(strTextFromFile);
                        for (int i = 0; i < strArrSplitText.Length; i++)
                            strArrSplitText[i] = strArrSplitText[i].Trim('\"');

                        if (csvDataTable.Columns.Count == 0)
                        {
                            for (int i = 0; i < strArrSplitText.Length; i++)
                                csvDataTable.Columns.Add("Column" + (i + 1).ToString(), typeof(string));
                        }

                        var row = csvDataTable.NewRow();
                        for (int i = 0; i < strArrSplitText.Length; i++)
                            row[i] = strArrSplitText[i];

                        csvDataTable.Rows.Add(row);
                    }

                    var tmpImportTableName = string.Format("TmpImport_{0}", _importId.ToString().Replace("-", ""));
                    var sqlTableCreator = new SqlTableCreator();
                    sqlTableCreator.Connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]);
                    sqlTableCreator.Connection.Open();
                    sqlTableCreator.DestinationTableName = tmpImportTableName;
                    sqlTableCreator.CreateFromDataTable(csvDataTable);
                    sqlTableCreator.Connection.Close();

                    if (txtFirstRow.Value > 1) // удаление лишних строк
                    {
                        for (int i = 1; i < txtFirstColumn.Value; i++)
                            csvDataTable.Rows.RemoveAt(i - 1);
                    }

                    if (cbIsFirstRowAsColumnNames.Checked)
                        csvDataTable.Rows.RemoveAt(0);
                    BulkInsertDataTable(ConfigurationManager.AppSettings["ADONETConnectionString"], tmpImportTableName, csvDataTable);

                    DataManager.Import.DoImport(_importId);
                }
            }

            Response.Redirect(UrlsData.AP_Imports());
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            Save();
            Response.Redirect(UrlsData.AP_Imports());
        }



        /// <summary>
        /// Handles the OnFileUploaded event of the uploadedFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.FileUploadedEventArgs"/> instance containing the event data.</param>
        protected void uploadedFile_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            if (!isPreviewed)
            {
                Preview(!(rgImportColumns.Items.Count > 0));
            }
        }



        /// <summary>
        /// Previews the specified rebuild columns.
        /// </summary>
        /// <param name="rebuildColumns">if set to <c>true</c> [rebuild columns].</param>
        protected void Preview(bool rebuildColumns = true)
        {
            pnlWarning.Visible = false;
            rgPreview.Visible = true;

            pnlExcelSettings.Visible = ddlType.SelectedValue.ToEnum<ImportType>() == ImportType.Excel;
            pnlCsvSettings.Visible = ddlType.SelectedValue.ToEnum<ImportType>() == ImportType.Csv;

            var sheet = ViewState["SheetName"].ToString();

            if (ddlType.SelectedValue.ToEnum<ImportType>() == ImportType.Excel) // Excel
            {
                IExcelDataReader excelReader = null;

                if (uploadedFile.UploadedFiles[0].GetExtension() == ".xls")
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(uploadedFile.UploadedFiles[0].InputStream);
                }

                if (uploadedFile.UploadedFiles[0].GetExtension() == ".xlsx")
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(uploadedFile.UploadedFiles[0].InputStream);
                }

                if (excelReader != null && excelReader.IsValid)
                {
                    var result = excelReader.AsDataSet();

                    ddlSheet.Items.Clear();
                    foreach (DataTable table in result.Tables)
                        ddlSheet.Items.Add(new ListItem(table.TableName, table.TableName));

                    if (!string.IsNullOrEmpty(sheet) && ddlSheet.Items.FindByValue(sheet) != null)
                        ddlSheet.Items.FindByValue(sheet).Selected = true;
                    else
                    {
                        sheet = ddlSheet.Items[0].Value;
                        ViewState["SheetName"] = sheet;
                    }

                    var previewTable = result.Tables[sheet].Rows.Cast<DataRow>().Take(countPreviewRows).CopyToDataTable();

                    var firstRow = int.Parse(txtFirstRow.Text) - 1;
                    var firstColumn = int.Parse(txtFirstColumn.Text) - 1;
                    for (var i = firstColumn; i < previewTable.Columns.Count; i++)
                        columns.Add(previewTable.Rows[firstRow][i].ToString());


                    // Change column names to numeric
                    for (var i = 0; i < previewTable.Columns.Count; i++)
                        previewTable.Columns[i].ColumnName = (i + 1).ToString();

                    // Add numeric
                    var dc = previewTable.Columns.Add(" ");
                    dc.SetOrdinal(0);

                    for (var i = 0; i < previewTable.Rows.Count; i++)
                        previewTable.Rows[i][0] = i + 1;


                    rgPreview.DataSource = previewTable.DefaultView;
                    rgPreview.DataBind();

                    excelReader.Close();

                    upPreview.Update();
                    upImportArea.Update();
                    isPreviewed = true;

                    if (rebuildColumns)
                    {
                        for (var i = 0; i < columns.Count; i++)
                            _importColumns.Add(new tbl_ImportColumn
                            {
                                ID = Guid.NewGuid(),
                                Name = columns[i],
                                Source = string.Format("{0}:{1}", firstRow + 1, firstColumn + i + 1),
                                SystemName = string.Format("Column{0}", i + 1)
                            });

                        rgImportColumns.DataSource = _importColumns;
                        rgImportColumns.DataBind();

                        BindImportColumnRules();
                    }
                }
                else
                    pnlWarning.Visible = true;
            }
            else // CSV
            {
                //if (uploadedFile.UploadedFiles.Count > 0 && uploadedFile.UploadedFiles[0].GetExtension() == ".csv")
                if (uploadedFile.UploadedFiles.Count > 0)
                {
                    var sr = new StreamReader(uploadedFile.UploadedFiles[0].InputStream, Encoding.GetEncoding("windows-1251"));
                    var csvDataTable = new DataTable();
                    string strTextFromFile = "";
                    string[] strArrSplitText = null;
                    while ((strTextFromFile = sr.ReadLine()) != null)
                    {
                        var options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase));
                        var r = new Regex(string.Format(@"\{0}(?=(?:[^\""]*\""[^\""]*\"")*(?![^\""]*\""))", CsvSeparator), options);

                        strArrSplitText = r.Split(strTextFromFile);
                        for (int i = 0; i < strArrSplitText.Length; i++)
                            strArrSplitText[i] = strArrSplitText[i].Trim('\"');

                        if (csvDataTable.Columns.Count == 0)
                        {
                            for (int i = 0; i < strArrSplitText.Length; i++)
                                csvDataTable.Columns.Add((i + 1).ToString(), typeof(string));
                        }

                        var row = csvDataTable.NewRow();
                        for (int i = 0; i < strArrSplitText.Length; i++)
                            row[i] = strArrSplitText[i];

                        csvDataTable.Rows.Add(row);
                    }

                    for (var i = 0; i < csvDataTable.Columns.Count; i++)
                        columns.Add(csvDataTable.Rows[0][i].ToString());

                    // Add numeric
                    var dc = csvDataTable.Columns.Add(" ");
                    dc.SetOrdinal(0);

                    for (var i = 0; i < csvDataTable.Rows.Count; i++)
                        csvDataTable.Rows[i][0] = i + 1;

                    rgPreview.DataSource = csvDataTable.DefaultView;
                    rgPreview.DataBind();

                    upPreview.Update();
                    upImportArea.Update();
                    isPreviewed = true;

                    if (rebuildColumns)
                    {
                        for (var i = 0; i < columns.Count; i++)
                            _importColumns.Add(new tbl_ImportColumn
                            {
                                ID = Guid.NewGuid(),
                                Name = columns[i],
                                Source = string.Format("{0}:{1}", 1, i + 1),
                                SystemName = string.Format("Column{0}", i + 1)
                            });

                        rgImportColumns.DataSource = _importColumns;
                        rgImportColumns.DataBind();

                        BindImportColumnRules();
                    }
                }
            }
        }



        /// <summary>
        /// Handles the OnRowDataBound event of the gvPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gvPreview_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.Visible = false;
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlSheet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlSheet_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["SheetName"] = ((DropDownList)sender).SelectedValue;
            Preview();
        }



        /// <summary>
        /// Handles the OnTextChanged event of the txtFirst control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void txtFirst_OnTextChanged(object sender, EventArgs e)
        {
            Preview();
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgImportColumns control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgImportColumns_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (tbl_ImportColumn)e.Item.DataItem;

                ((RadTextBox)item.FindControl("txtName")).Text = data.Name;
                ((HtmlGenericControl)item.FindControl("spanSource")).InnerText = data.Source;
                ((CheckBox)item.FindControl("cbPrimaryKey")).Checked = data.PrimaryKey;
                ((CheckBox)item.FindControl("cbSecondaryKey")).Checked = data.SecondaryKey;
            }
        }



        public static void BulkInsertDataTable(string connectionString, string tableName, DataTable table)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var bulkCopy = new SqlBulkCopy
                    (
                    connection,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                    );

                bulkCopy.DestinationTableName = tableName;
                connection.Open();

                bulkCopy.WriteToServer(table);
                connection.Close();
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgImportColumnRules control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgImportColumnRules_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var importFieldID = Guid.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"].ToString());

                if (importFieldID == Guid.Parse("030bd3e1-7092-4f0e-a856-14f34f284266"))
                    ((HtmlGenericControl)item.FindControl("spanTableName")).InnerText = "Компании";
                if (importFieldID == Guid.Parse("4553fa29-7aa0-40d3-8940-ea7ca5915604"))
                    ((HtmlGenericControl)item.FindControl("spanTableName")).InnerText = "Контакты";

                ((DropDownList)item.FindControl("ddlImportColumns")).Items.Clear();
                ((DropDownList)item.FindControl("ddlImportColumns")).Items.Add(new ListItem("", ""));

                var importField = DataManager.ImportField.SelectByID(importFieldID);
                if (importField.IsDictionary)
                {
                    ((DropDownList)item.FindControl("ddlImportFieldDictionary")).DataSource = DataManager.ImportFieldDictionary.SelectByImportFieldID(importFieldID);
                    ((DropDownList)item.FindControl("ddlImportFieldDictionary")).DataTextField = "Title";
                    ((DropDownList)item.FindControl("ddlImportFieldDictionary")).DataValueField = "ID";
                    ((DropDownList)item.FindControl("ddlImportFieldDictionary")).DataBind();
                    ((DropDownList)item.FindControl("ddlImportFieldDictionary")).Visible = true;
                }

                foreach (var importColumn in _importColumns)
                {
                    ((DropDownList)item.FindControl("ddlImportColumns")).Items.Add(new ListItem(importColumn.Name, importColumn.ID.ToString()));
                    var importColumnRule = _importColumnRules.Where(a => a.ImportFieldID == importFieldID && a.ImportColumnID == importColumn.ID).SingleOrDefault();
                    if (importColumnRule != null)
                    {
                        ((DropDownList)item.FindControl("ddlImportColumns")).Items.FindByValue(importColumn.ID.ToString()).Selected = true;
                        ((RadTextBox)item.FindControl("txtSQLCode")).Text = importColumnRule.SQLCode;
                        ((CheckBox)item.FindControl("cbIsRequired")).Checked = importColumnRule.IsRequired;

                        if (importField.IsDictionary && importColumnRule.ImportFieldDictionaryID != null)
                        {
                            ((DropDownList)item.FindControl("ddlImportFieldDictionary")).Items.FindByValue(importColumnRule.ImportFieldDictionaryID.ToString()).Selected = true;
                        }
                    }
                }
            } 
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            pnlExcelSettings.Visible = ddlType.SelectedValue.ToEnum<ImportType>() == ImportType.Excel;
            pnlCsvSettings.Visible = ddlType.SelectedValue.ToEnum<ImportType>() == ImportType.Csv;

            upImportArea.Update();
        }



        protected void txtCsvSeparator_OnTextChanged(object sender, EventArgs e)
        {
            Preview();
        }

        protected void rcbCsvSeparator_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Preview();
        }
    }
}