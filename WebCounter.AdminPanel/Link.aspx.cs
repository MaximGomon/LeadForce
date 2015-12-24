using System;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class Link : LeadForceBasePage
    {
        private Guid _linkId;
        public Access access;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Ссылки - LeadForce";

            access = Access.Check();
            if (!access.Write)
                btnUpdate.Visible = false;

            if (Page.RouteData.Values["id"] != null)
                _linkId = Guid.Parse(Page.RouteData.Values["id"] as string);

            hlCancel.NavigateUrl = UrlsData.AP_SiteActivityRules((int)RuleType.Link);

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var link = DataManager.Links.SelectById(SiteId, _linkId);

            if (link != null)
            {
                txtName.Text = link.Name;
                txtURL.Text = link.URL;
                txtCode.Text = link.Code;
            }
        }



        /// <summary>
        /// Handles the OnClick event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var link = new tbl_Links();

            var checkCode = DataManager.Links.Select(SiteId, txtCode.Text);            

            if (_linkId != Guid.Empty)
            {
                link = DataManager.Links.SelectById(_linkId);
                if (checkCode != null && checkCode.ID == _linkId)
                    checkCode = null;
            }

            ucMessage.Text = "";
            if (checkCode == null)
            {
                link.SiteID = SiteId;
                link.Name = txtName.Text;
                link.RuleTypeID = (int) RuleType.Link;
                if (_linkId == Guid.Empty)
                    link.Code = txtCode.Text;
                link.URL = txtURL.Text;

                if (_linkId != Guid.Empty)                
                    DataManager.Links.Update(link);                
                else
                {
                    link.OwnerID = CurrentUser.Instance.ContactID;
                    link.ID = Guid.NewGuid();
                    DataManager.Links.Add(link);                    
                }
            }
            else            
                ucMessage.Text = "Правило с таким кодом уже существует.";            

            Response.Redirect(UrlsData.AP_SiteActivityRules((int)RuleType.Link));
        }
    }
}