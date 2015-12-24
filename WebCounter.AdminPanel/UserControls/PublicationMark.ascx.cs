using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class PublicationMark : System.Web.UI.UserControl
    {
        private DataManager dataManager = new DataManager();

        [Serializable]
        private class PublicationMarkStructure
        {
            public Guid ID { get; set; }
            public Guid PublicationID { get; set; }
            public Guid? PublicationCommentID { get; set; }
            public string PublicationComment { get; set; }
            public DateTime CreatedAt { get; set; }
            public Guid UserID { get; set; }
            public string UserName { get; set; }
            public int TypeID { get; set; }
            public string Type { get; set; }
            public int Rank { get; set; }

        }

        private List<PublicationMarkStructure> _PublicationMarkStructure = new List<PublicationMarkStructure>();

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
                BindPublicationTerms();
                rgPublicationMarks.Culture = new CultureInfo("ru-RU");
            }

        }



        private void BindPublicationTerms()
        {

            foreach (var v in dataManager.PublicationMark.SelectByPublicationId(PublicationID).ToList())
            {
                _PublicationMarkStructure.Add(new PublicationMarkStructure()
                                                  {
                                                      ID = v.ID
                                                      , PublicationID = PublicationID
                                                      , PublicationCommentID = v.PublicationCommentID
                                                      , PublicationComment = v.tbl_PublicationComment != null ? v.tbl_PublicationComment.CreatedAt.ToShortDateString():""
                                                      , CreatedAt = v.CreatedAt
                                                      , TypeID = v.TypeID
                                                      , Type = EnumHelper.GetEnumDescription((PublicationMarkType)v.TypeID)
                                                      , Rank =  v.Rank
                                                      , UserID = v.UserID
                                                      , UserName = v.tbl_Contact != null ? v.tbl_Contact.UserFullName:""
                                                  });
            }
            ViewState["PublicationMark"] = _PublicationMarkStructure;
        }




        /// <summary>
        /// Handles the NeedDataSource event of the rgPublicationMarks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationMarks_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgPublicationMarks.DataSource = ViewState["PublicationMark"];
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgPublicationMarks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationMarks_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var id = Guid.Parse(item.GetDataKeyValue("ID").ToString());
            var publicationMark = ((List<PublicationMarkStructure>)ViewState["PublicationMark"]).Where(s => s.ID == id).FirstOrDefault();
            
            publicationMark.CreatedAt = ((RadDatePicker)item.FindControl("rdpDate")).SelectedDate != null ? (DateTime)((RadDatePicker)item.FindControl("rdpDate")).SelectedDate : DateTime.Now;


            publicationMark.PublicationCommentID = Guid.Parse(((DropDownList)item.FindControl("ddlComment")).SelectedValue);
            if (publicationMark.PublicationCommentID!=null){
             var comment = dataManager.PublicationComment.SelectById((Guid)publicationMark.PublicationCommentID);
                publicationMark.PublicationComment = comment.CreatedAt.ToShortDateString();
            }else
            {
                publicationMark.PublicationComment = "";
            }
            publicationMark.TypeID = int.Parse(((DropDownList)item.FindControl("ddlType")).SelectedValue);
            publicationMark.Type = EnumHelper.GetEnumDescription((PublicationMarkType) publicationMark.TypeID);

            publicationMark.UserID = (Guid)((ContactComboBox)item.FindControl("ucUser")).SelectedValue;
            var user = dataManager.Contact.SelectById(SiteID, publicationMark.UserID);
            publicationMark.UserName = user != null ? (user.UserFullName ?? "") : "";

            publicationMark.Rank = int.Parse(((RadNumericTextBox)item.FindControl("rtbRank")).Value.ToString());
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgPublicationMarks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationMarks_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var publicationMark = new PublicationMarkStructure();
            publicationMark.ID = Guid.NewGuid();
            publicationMark.CreatedAt = ((RadDatePicker)item.FindControl("rdpDate")).SelectedDate != null ? (DateTime)((RadDatePicker)item.FindControl("rdpDate")).SelectedDate : DateTime.Now;


            publicationMark.PublicationCommentID = Guid.Parse(((DropDownList)item.FindControl("ddlComment")).SelectedValue == "" ? Guid.Empty.ToString() : ((DropDownList)item.FindControl("ddlComment")).SelectedValue);
            if (publicationMark.PublicationCommentID != null)
            {
                var comment = dataManager.PublicationComment.SelectById((Guid)publicationMark.PublicationCommentID);
                publicationMark.PublicationComment = comment.CreatedAt.ToShortDateString();
            }
            else
            {
                publicationMark.PublicationComment = "";
            }
            publicationMark.TypeID = int.Parse(((DropDownList)item.FindControl("ddlType")).SelectedValue);
            publicationMark.Type = EnumHelper.GetEnumDescription((PublicationMarkType)publicationMark.TypeID);

            publicationMark.UserID = (Guid)((ContactComboBox)item.FindControl("ucUser")).SelectedValue;
            var user = dataManager.Contact.SelectById(SiteID, publicationMark.UserID);
            publicationMark.UserName = user != null ? (user.UserFullName ?? "") : "";

            publicationMark.Rank = int.Parse(((RadNumericTextBox)item.FindControl("rtbRank")).Value.ToString());
            ((List<PublicationMarkStructure>)ViewState["PublicationMark"]).Add(publicationMark);
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgPublicationMarks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPublicationMarks_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<PublicationMarkStructure>)ViewState["PublicationMark"]).Remove(
                ((List<PublicationMarkStructure>)ViewState["PublicationMark"]).Where(s => s.ID == id).FirstOrDefault());
        }




        public void Save(Guid PublicationID)
        {
            var publicationMark = new List<tbl_PublicationMark>();

            foreach (var v in (List<PublicationMarkStructure>)ViewState["PublicationMark"])
            {
                publicationMark.Add(new tbl_PublicationMark() { ID = v.ID, PublicationID = PublicationID, Rank = v.Rank, UserID = v.UserID, PublicationCommentID = v.PublicationCommentID, TypeID = v.TypeID, CreatedAt = v.CreatedAt});
            }

            dataManager.PublicationMark.DeleteAll(PublicationID);
            dataManager.PublicationMark.Add(publicationMark);

        }

        protected void rgPublicationMarks_DataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            //{
            //    Control target = e.Item.FindControl("targetControl");
            //    if (!Object.Equals(target, null))
            //    {
            //        if (!Object.Equals(this.RadToolTipManager1, null))
            //        {
            //            //Add the button (target) id to the tooltip manager
            //            this.RadToolTipManager1.TargetControls.Add(target.ClientID,
            //                                                       (e.Item as GridDataItem).GetDataKeyValue("PublicationCommentID").ToString(), true);

            //        }
            //    }
            //}
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var item = e.Item as GridEditableItem;

                ContactComboBox ucUser = (ContactComboBox)item.FindControl("ucUser");
                ucUser.FilterByFullName = true;                
                ucUser.ValidationGroup = "valPublicationMarkGroup";                

                var ddlType = (DropDownList)item.FindControl("ddlType");
                ddlType.Items.Clear();
                foreach (var publicationMarkType in EnumHelper.EnumToList<PublicationMarkType>())
                    ddlType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(publicationMarkType), ((int)publicationMarkType).ToString()));

                var ddlComment = (DropDownList)item.FindControl("ddlComment");
                ddlComment.DataSource = dataManager.PublicationComment.SelectByPublicationId(PublicationID);
                ddlComment.DataValueField = "ID";
                ddlComment.DataTextField = "CreatedAt";
                ddlComment.Items.Add(new ListItem() { Text = "Выберите значение",Value = Guid.Empty.ToString(),Selected = true});
                ddlComment.DataBind();

                ((RadDatePicker)item.FindControl("rdpDate")).SelectedDate = DateTime.Now;

                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var publicationMarks = (PublicationMarkStructure)item.DataItem;

                    ((RadDatePicker)item.FindControl("rdpDate")).SelectedDate = publicationMarks.CreatedAt;
                    ((RadNumericTextBox) item.FindControl("rtbRank")).Value = publicationMarks.Rank;
                    
                    ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(publicationMarks.TypeID.ToString()));

                    ddlComment.SelectedIndex = ddlComment.Items.IndexOf(ddlComment.Items.FindByValue(publicationMarks.PublicationCommentID.ToString()));

                    ucUser.SelectedValue = publicationMarks.UserID;

                }
            }
        }



        protected void OnAjaxUpdate(object sender, ToolTipUpdateEventArgs e)
        {
            this.UpdateToolTip(e.Value, e.UpdatePanel);
        }



        private void UpdateToolTip(string elementID, UpdatePanel panel)
        {
            Control ctrl = Page.LoadControl("PublicationCommentDetail.ascx");
            panel.ContentTemplateContainer.Controls.Add(ctrl);
            PublicationCommentDetail details = (PublicationCommentDetail)ctrl;
            details.PublicationCommentID = elementID;
        }



        protected void rgPublicationMarks_ItemCommand(object source, GridCommandEventArgs e)
        {
            //if (e.CommandName == "Sort" || e.CommandName == "Page")
            //{
            //    RadToolTipManager1.TargetControls.Clear();
            //}
        }
    }
}