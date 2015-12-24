using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class PublicationComment : System.Web.UI.UserControl
    {
        private DataManager dataManager = new DataManager();

        [Serializable]
        private class PublicationCommentStructure
        {
            public Guid ID { get; set; }
            public DateTime Date { get; set; }
            public Guid UserID { get; set; }
            public Guid PublicationID { get; set; }
            public string UserName { get; set; }
            public string Comment { get; set; }
            public string FileName { get; set; }
            public bool isOfficialAnswer { get; set; }
            public string isOfficialAnswerStr { get; set; }
        }

        private List<PublicationCommentStructure> _PublicationCommentStructure = new List<PublicationCommentStructure>();

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
                rgPublicationComment.Culture = new CultureInfo("ru-RU");
            }

        }



        private void BindPublication()
        {

            foreach (var v in dataManager.PublicationComment.SelectByPublicationId(PublicationID).ToList())
            {
                _PublicationCommentStructure.Add(new PublicationCommentStructure() { ID = v.ID, PublicationID = PublicationID, Comment = v.Comment, Date = v.CreatedAt, FileName = v.FileName, isOfficialAnswer = v.isOfficialAnswer, UserID = v.UserID, UserName = v.tbl_Contact!=null ? v.tbl_Contact.UserFullName:"", isOfficialAnswerStr = (v.isOfficialAnswer ? "Да" : "Нет") });
            }
            ViewState["PublicationComment"] = _PublicationCommentStructure;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgPublicationComment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationComment_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;
                var item = e.Item as GridEditableItem;

                ContactComboBox ucUser = (ContactComboBox) item.FindControl("ucUser");
                ucUser.FilterByFullName = true;                
                ucUser.ValidationGroup = "valRelatedPublicationCommentGroup";                

                ((RadDatePicker) item.FindControl("rdpDate")).SelectedDate = DateTime.Now;

                if (!(gridEditFormItem.DataItem is GridInsertionObject))
                {
                    var publicationComment = (PublicationCommentStructure)gridEditFormItem.DataItem;

                    if (publicationComment != null)
                    {
                        ucUser.SelectedValue = publicationComment.UserID;
                        ((TextBox) item.FindControl("txtComment")).Text = publicationComment.Comment;
                        ((RadDatePicker) item.FindControl("rdpDate")).SelectedDate = publicationComment.Date;
                        ((CheckBox) item.FindControl("cbOfficial")).Checked = publicationComment.isOfficialAnswer;
                    }
                }
            }
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgPublicationComment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationComment_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgPublicationComment.DataSource = ViewState["PublicationComment"];
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgPublicationComment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationComment_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var id = Guid.Parse(item.GetDataKeyValue("ID").ToString());
            var publicationComment = ((List<PublicationCommentStructure>)ViewState["PublicationComment"]).Where(s => s.ID == id).FirstOrDefault();

            publicationComment.UserID = (Guid)((ContactComboBox)item.FindControl("ucUser")).SelectedValue;
            var user = dataManager.Contact.SelectById(SiteID, publicationComment.UserID);
            publicationComment.UserName = user != null ? (user.UserFullName ?? "") : "";
            publicationComment.Comment = ((TextBox)item.FindControl("txtComment")).Text;
            publicationComment.Date = ((RadDatePicker)item.FindControl("rdpDate")).SelectedDate != null ? (DateTime)((RadDatePicker)item.FindControl("rdpDate")).SelectedDate : DateTime.Now;
            publicationComment.isOfficialAnswer = ((CheckBox)item.FindControl("cbOfficial")).Checked;
            publicationComment.isOfficialAnswerStr = publicationComment.isOfficialAnswer ? "Да" : "Нет";

        }



        /// <summary>
        /// Handles the InsertCommand event of the rgPublicationComment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationComment_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var publicationComment = new PublicationCommentStructure();
            publicationComment.ID = Guid.NewGuid();

            publicationComment.UserID = (Guid)((ContactComboBox)item.FindControl("ucUser")).SelectedValue;
            var user = dataManager.Contact.SelectById(SiteID,publicationComment.UserID);
            publicationComment.UserName = user != null ? (user.UserFullName ?? "")  : "";
            publicationComment.Comment = ((TextBox)item.FindControl("txtComment")).Text;
            publicationComment.Date = ((RadDatePicker)item.FindControl("rdpDate")).SelectedDate != null ? (DateTime)((RadDatePicker)item.FindControl("rdpDate")).SelectedDate : DateTime.Now;
            publicationComment.isOfficialAnswer = ((CheckBox)item.FindControl("cbOfficial")).Checked;
            publicationComment.isOfficialAnswerStr = publicationComment.isOfficialAnswer ? "Да" : "Нет";
            ((List<PublicationCommentStructure>)ViewState["PublicationComment"]).Add(publicationComment);
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgPublicationComment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationComment_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<PublicationCommentStructure>)ViewState["PublicationComment"]).Remove(
                ((List<PublicationCommentStructure>)ViewState["PublicationComment"]).Where(s => s.ID == id).FirstOrDefault());
        }




        public void Save(Guid PublicationID)
        {

            var publicationCommentValues = new List<PublicationCommentStructure>();
            if (ViewState["PublicationComment"] != null)
            {
                var publicationCommentValuesOld = dataManager.PublicationComment.SelectByPublicationId(PublicationID).ToList();
                publicationCommentValues = (List<PublicationCommentStructure>)ViewState["PublicationComment"];
                foreach (var publicationCommentValue in publicationCommentValues)
                {
                    publicationCommentValue.PublicationID = PublicationID;
                    var updatesitePublicationComment = new tbl_PublicationComment();
                    updatesitePublicationComment.ID = publicationCommentValue.ID;
                    updatesitePublicationComment.PublicationID = PublicationID;
                    updatesitePublicationComment.UserID = publicationCommentValue.UserID;
                    updatesitePublicationComment.isOfficialAnswer = publicationCommentValue.isOfficialAnswer;
                    updatesitePublicationComment.Comment = publicationCommentValue.Comment;
                    updatesitePublicationComment.CreatedAt = publicationCommentValue.Date;
                    updatesitePublicationComment.FileName = publicationCommentValue.FileName ?? "";

                    var removePublicationCommentValue = publicationCommentValuesOld.SingleOrDefault(a => a.ID == publicationCommentValue.ID);
                    if (removePublicationCommentValue != null)
                    {
                        dataManager.PublicationComment.Update(updatesitePublicationComment);
                        publicationCommentValuesOld.Remove(removePublicationCommentValue);
                    }
                    else
                        dataManager.PublicationComment.Add(updatesitePublicationComment);
                }

                if (publicationCommentValuesOld != null && publicationCommentValuesOld.Count() > 0)
                {
                    foreach (var item in publicationCommentValuesOld)
                    {
                        try
                        {
                            dataManager.PublicationComment.Delete(item);
                        }
                        catch { }
                    }
                }
            }
        }

        protected void AsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            
        }
    }
}