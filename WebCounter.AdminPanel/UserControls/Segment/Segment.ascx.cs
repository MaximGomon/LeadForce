using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Segment
{
    public partial class Segment : System.Web.UI.UserControl
    {
        public Access access;
        public Guid _segmentID;
        public Guid SiteId = new Guid();
        private DataManager _dataManager = new DataManager();


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public int ObjectTypeId
        {
            get
            {
                object o = ViewState["ObjectTypeId"];
                return (o == null ? 0 : (int)o);
            }
            set
            {
                ViewState["ObjectTypeId"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            access = Access.Check();
            if (!access.Write)
            {
                lbtnSave.Visible = false;
            }

            SiteId = ((LeadForceBasePage)Page).SiteId;
            string segmentId = Page.RouteData.Values["id"] as string;

            if (!Guid.TryParse(segmentId, out _segmentID))
            {
            //    Response.Redirect(UrlsData.AP_ContactSegments());
            }
            
            
            hlCancel.NavigateUrl = UrlsData.AP_ContactSegments();
            gridSegments.SiteID = ((LeadForceBasePage)Page).SiteId;
                


            if (!Page.IsPostBack)
            {
                BindData();
            }

        }

        

        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {

            var segment = _dataManager.SiteTags.SelectById(_segmentID);

            if (segment != null)
            {
                txtName.Text = segment.Name;
                txtDescription.Text = segment.Description;

                var SelectedItems = new List<Guid>();
                if (segment.tbl_SiteTagObjects != null)
                {
                    foreach (var id in segment.tbl_SiteTagObjects)
                    {
                        SelectedItems.Add(id.ObjectID);
                    }
                }
                if (SelectedItems.Count > 0) gridSegments.SelectedItems = SelectedItems;
            }

        }


        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var segment = _dataManager.SiteTags.SelectById(_segmentID) ?? new tbl_SiteTags();
            segment.Name = txtName.Text;
            segment.Description = txtDescription.Text;
            //segment.ObjectTypeID = ObjectTypeId;
            segment.ObjectTypeID = 1;
            segment.SiteID = SiteId;
            segment.UserID = CurrentUser.Instance.ID;
            if (segment.ID == Guid.Empty)
            {
                segment.ID = Guid.NewGuid();
                _dataManager.SiteTags.Add(segment);
            }
            else
            {
                _dataManager.SiteTags.Update(segment);
            }
            var id = new List<Guid>();
            id.Add(segment.ID);
            var objects = _dataManager.SiteTagObjects.SelectIdsByTagID(id);
            foreach (var guid in objects)
            {
                var obj = _dataManager.SiteTagObjects.Select(segment.ID, guid);
                _dataManager.SiteTagObjects.Delete(obj);
            }
            var SelectedItems = gridSegments.SelectedItems;
            if (SelectedItems.Count != 0)
            {
                foreach (var selectedItem in SelectedItems)
                {
                    var obj = new tbl_SiteTagObjects()
                                  {
                                      ID = Guid.NewGuid(),
                                      ObjectID = selectedItem,
                                      SiteTagID = segment.ID,
                                  };
                    _dataManager.SiteTagObjects.Add(obj);
                }
            }

            Response.Redirect(UrlsData.AP_ContactSegments());
        }  
    }
}