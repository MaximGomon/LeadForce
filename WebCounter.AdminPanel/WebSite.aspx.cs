using System;
using System.Data;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.WebSite;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class WebSite : LeadForceBasePage
    {
        protected Guid _webSiteId;
        protected Access Access;
        protected RadAjaxManager RadAjaxManager = null;



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {        

            Access = Access.Check();
            if (!Access.Write)
                lbtnSave.Visible = false;

            Title = "Мини сайты - LeadForce";

            if (Page.RouteData.Values["id"] != null)
                _webSiteId = Guid.Parse(Page.RouteData.Values["id"] as string);

            if (_webSiteId == Guid.Empty)
            {
                rtsTabs.Tabs[1].Visible = false;
            }

            hlCancel.NavigateUrl = UrlsData.AP_WebSites();
            dcbSiteDomain.SiteID = SiteId;            

            gridWebSitePage.Where.Add(new GridWhere() { Column = "WebSiteID", Value = _webSiteId.ToString()});
            RadAjaxManager = RadAjaxManager.GetCurrent(Page);
            RadAjaxManager.AjaxSettings.AddAjaxSetting(RadAjaxManager, gridWebSitePage);
            RadAjaxManager.AjaxRequest += RadAjaxManager_AjaxRequest;
            
            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Handles the AjaxRequest event of the RadAjaxManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void RadAjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "RebindWebPages")
            {
                gridWebSitePage.Rebind();
            }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var webSite = DataManager.WebSite.SelectById(SiteId, _webSiteId);

            dcbSiteDomain.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { DbType = DbType.Int32, Name = "StatusID", Value = ((int)SiteDomainStatus.LeadForceDomain).ToString()});                        

            if (webSite != null)
            {
                ucExternalResources.DestinationId = webSite.ID;
                txtTitle.Text = webSite.Title;
                txtDescription.Text = webSite.Description;
                dcbSiteDomain.SelectedIdNullable = webSite.SiteDomainID;

                if (!webSite.SiteDomainID.HasValue)
                {
                    plTemporaryAddress.Visible = true;
                    lrlTemporaryAddress.Text = DataManager.WebSite.GetWebSiteUrl(CurrentUser.Instance.SiteID, _webSiteId);
                }                

                if (!string.IsNullOrEmpty(webSite.FavIcon))
                {
                    var fsp = new FileSystemProvider();
                    imgFavIcon.ImageUrl = fsp.GetLink(SiteId, "WebSites", webSite.FavIcon, FileType.Image);
                }
                else                
                    imgFavIcon.Visible = false;
              
            }
            else
            {
                imgFavIcon.Visible = false;                             
            }            
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            Save();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSaveAndContinue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSaveAndContinue_OnClick(object sender, EventArgs e)
        {
            Save(true);
        }



        /// <summary>
        /// Saves the specified save and continue.
        /// </summary>
        /// <param name="saveAndContinue">if set to <c>true</c> [save and continue].</param>
        protected void Save(bool saveAndContinue = false)
        {
            if (!Access.Write)
                return;

            var webSite = DataManager.WebSite.SelectById(SiteId, _webSiteId) ?? new tbl_WebSite();
            webSite.Title = txtTitle.Text;
            webSite.Description = txtDescription.Text;
            webSite.SiteDomainID = dcbSiteDomain.SelectedIdNullable;

            if (rauFavIcon.UploadedFiles.Count > 0)
            {
                var fsp = new FileSystemProvider();
                if (!string.IsNullOrEmpty(webSite.FavIcon))
                    fsp.Delete(SiteId, "WebSites", webSite.FavIcon, FileType.Image);

                webSite.FavIcon = fsp.Upload(SiteId, "WebSites", rauFavIcon.UploadedFiles[0].FileName, rauFavIcon.UploadedFiles[0].InputStream, FileType.Image);
            }

            if (webSite.ID == Guid.Empty)
            {
                webSite.SiteID = SiteId;
                DataManager.WebSite.Add(webSite);
            }
            else
                DataManager.WebSite.Update(webSite);

            DataManager.ExternalResource.Update(ucExternalResources.ExternalResourceList, webSite.ID);

            if (!saveAndContinue)
                Response.Redirect(_webSiteId != Guid.Empty ? UrlsData.AP_WebSites() : UrlsData.AP_WebSiteEdit(webSite.ID));
            else
                Response.Redirect(UrlsData.AP_WebSiteEdit(webSite.ID));
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridWebSitePage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridWebSitePage_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {                
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((Literal)item.FindControl("lrlTitle")).Text = data["tbl_WebSitePage_Title"].ToString();

                ((HyperLink) item.FindControl("hlEdit")).NavigateUrl = "javascript:;";
                ((HyperLink)item.FindControl("hlEdit")).Attributes.Add("onclick", string.Format("EditPage('{0}','{1}');", data["tbl_WebSitePage_ID"], data["tbl_WebSitePage_Title"]));                

                ((LinkButton)e.Item.FindControl("lbDelete")).CommandArgument = data["ID"].ToString();


                var lbtnStatus = (LinkButton) e.Item.FindControl("lbtnStatus");
                lbtnStatus.CommandArgument = data["ID"].ToString();
                if (int.Parse(data["tbl_WebSitePage_WebSiteElementStatusID"].ToString()) == (int)WebSiteElementStatus.Active)
                {
                    lbtnStatus.CssClass = "page-active";
                    lbtnStatus.Text = "Деактивировать";
                }
                else
                {
                    lbtnStatus.CssClass = "page-not-active";
                    lbtnStatus.Text = "Активировать";
                }
            }
        }



        /// <summary>
        /// Handles the OnCommand event of the lbDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            DataManager.WebSitePage.Delete(CurrentUser.Instance.SiteID, (Guid.Parse(e.CommandArgument.ToString())));
            gridWebSitePage.Rebind();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnStatus_OnClick(object sender, EventArgs e)
        {
            var lbtnStatus = (LinkButton) sender;
            var webSitePage = DataManager.WebSitePage.SelectById(CurrentUser.Instance.SiteID,(Guid.Parse(lbtnStatus.CommandArgument)));
            if (webSitePage.WebSiteElementStatusID == (int)WebSiteElementStatus.Active)
                webSitePage.WebSiteElementStatusID = (int) WebSiteElementStatus.NoActive;
            else
                webSitePage.WebSiteElementStatusID = (int)WebSiteElementStatus.Active;

            DataManager.WebSitePage.Update(webSitePage);

            gridWebSitePage.Rebind();
        }
    }
}