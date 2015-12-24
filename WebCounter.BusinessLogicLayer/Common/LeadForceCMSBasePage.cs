using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.WebSite;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class LeadForceCMSBasePage : Page
    {
        public DataManager DataManager = new DataManager();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (WebSiteId == Guid.Empty)
                Response.Redirect(ResolveUrl("~/SiteNotFound.aspx"));

            if (Page.Master != null)
            {
                var head = Page.Master.FindControl("head");
                var lrlCounteCode = new Literal() { Text = ScriptTemplates.Counter(SiteId) };
                head.Controls.Add(lrlCounteCode);
                var analytics = Page.Master.FindControl("analytics");
                var lrlScriptCode = new Literal() { Text = ScriptTemplates.Script(true) };
                analytics.Controls.Add(lrlScriptCode);
            }            
        }

        public Guid WebSiteId
        {
            get
            {
                var webSiteId = Guid.Empty;
                tbl_WebSite webSite = null;

                var id = Page.RouteData.Values["webSiteId"] as string;
                if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out webSiteId))
                {
                    webSite = DataManager.WebSite.SelectById(webSiteId);                    
                    if (webSite != null)
                        return webSite.ID;
                }

                var domain = Request.Url.Host;

                if (domain.ToLower().StartsWith("xn--"))
                {
                    IdnMapping idn = new IdnMapping();
                    webSite = DataManager.WebSite.SelectByDomain(idn.GetUnicode(domain)) ??
                              DataManager.WebSite.SelectByAlias(idn.GetUnicode(domain));
                }
                else
                {
                    webSite = DataManager.WebSite.SelectByDomain(domain) ?? DataManager.WebSite.SelectByAlias(domain);
                }



                if (webSite != null)
                    return webSite.ID;                

                return Guid.Empty;
            }
        }


        public void SetMetaData(tbl_WebSitePage page)
        {
            Title = page.MetaTitle;
            if (!string.IsNullOrEmpty(page.MetaKeywords))
            {
                Page.MetaKeywords = page.MetaKeywords;                
            }

            if (!string.IsNullOrEmpty(page.MetaDescription))
            {
                Page.MetaDescription = page.MetaDescription;                
            }

            var webSite = page.tbl_WebSite;

            if (!string.IsNullOrEmpty(webSite.FavIcon) && Page.Master != null)
            {
                var fsp = new FileSystemProvider();                
                var lrlFavIcon = (Literal)Page.Master.FindControl("lrlFavIcon");
                lrlFavIcon.Text = string.Format("<link rel=\"icon\" href=\"{0}\" type=\"image/ico\" />", fsp.GetLink(SiteId, "WebSites", webSite.FavIcon, FileType.Image));                
            }
        }


        public void ProceedResources(tbl_WebSitePage page)
        {
            if (!page.tbl_ExternalResource.Any())
                return;

            foreach (var resource in page.tbl_ExternalResource)
            {
                switch ((ExternalResourceType)resource.ExternalResourceTypeID)
                {
                    case ExternalResourceType.JavaScript:
                        var result = string.Empty;
                        if (!string.IsNullOrEmpty(resource.File))
                        {
                            var fsp = new FileSystemProvider();
                            var filePath = fsp.GetLink(page.tbl_WebSite.SiteID, "ExternalResource", resource.File, FileType.Attachment);
                            result += string.Format(@"<script type=""text/javascript"" src=""{0}""></script>", filePath);
                        }
                        if (!string.IsNullOrEmpty(resource.Url))
                            result += string.Format(@"<script type=""text/javascript"" src=""{0}""></script>", resource.Url);
                        if (!string.IsNullOrEmpty(resource.Text))
                        {
                            if (!resource.Text.Contains("<script"))
                                result += string.Format(@"<script type=""text/javascript"">{0}</script>", resource.Text);
                            else
                                result += resource.Text;
                        }

                        PutToPlace((ResourcePlace)resource.ResourcePlaceID, result);
                        break;
                    case ExternalResourceType.CSS:
                        var css = string.Empty;
                        if (!string.IsNullOrEmpty(resource.File))
                        {
                            var fsp = new FileSystemProvider();
                            var filePath = fsp.GetLink(page.tbl_WebSite.SiteID, "ExternalResource", resource.File, FileType.Attachment);
                            css += string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}"" />", filePath);
                        }
                        if (!string.IsNullOrEmpty(resource.Url))
                            css += string.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}"" />", resource.Url);
                        if (!string.IsNullOrEmpty(resource.Text))
                        {
                            if (!resource.Text.Contains("<style"))
                                css += string.Format(@"<style type=""text/css"">{0}</style>", resource.Text);
                            else
                                css += resource.Text;
                        }

                        PutToPlace((ResourcePlace)resource.ResourcePlaceID, css);

                        break;
                }
            }
        }



        /// <summary>
        /// Puts to place.
        /// </summary>
        /// <param name="place">The place.</param>
        /// <param name="resource">The resource.</param>
        protected void PutToPlace(ResourcePlace place, string resource)
        {
            switch(place)
            {
                case ResourcePlace.Header:
                    ((Literal)Page.Master.FindControl("lrlResourcesHead")).Text += resource;
                    break;
                case ResourcePlace.Body:
                    ((Literal)Page.Master.FindControl("lrlResourcesBody")).Text += resource;
                    break;
            }
        }



        /// <summary>
        /// Gets the site id.
        /// </summary>
        public Guid SiteId
        {
            get
            {
                if (WebSiteId != Guid.Empty)
                {
                    var site = DataManager.WebSite.SelectById(WebSiteId);
                    if (site != null)
                        return site.SiteID;
                }

                return Guid.Empty;
            }
        }
    }
}
