using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class GenerateSiteActionTemplateThumbnails : System.Web.UI.Page
    {
        private WebCounterEntities _dataContext = new WebCounterEntities();



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var siteActionTemplates = _dataContext.tbl_SiteActionTemplate.Where(a => a.SiteActionTemplateCategoryID == (int)SiteActionTemplateCategory.BaseTemplate || a.SiteActionTemplateCategoryID == (int)SiteActionTemplateCategory.System);
            foreach (var siteActionTemplate in siteActionTemplates)
            {
                var thumbnail = GetWebSiteThumb.ClassWSThumb.GetWebSiteThumbnail(string.Format("{0}/Handlers/SiteActionTemplateThumbnail.aspx?siteid={1}&id={2}",
                                                                                 BusinessLogicLayer.Configuration.Settings.LeadForceSiteUrl,
                                                                                 siteActionTemplate.SiteID, siteActionTemplate.ID), 800, 800, 130, 130);
                using (var stream = new System.IO.MemoryStream())
                {
                    var fileProvider = new FileSystemProvider();
                    thumbnail.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Position = 0;
                    fileProvider.Upload(siteActionTemplate.SiteID, "SiteActionTemplates", siteActionTemplate.ID.ToString() + ".png", stream, FileType.Thumbnail, false);
                }
            }
        }
    }
}