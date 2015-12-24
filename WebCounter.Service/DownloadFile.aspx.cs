using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Files;

namespace WebCounter.Service
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        private DataManager dataManager = new DataManager();



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid _siteActivityRuleId;

            if (Guid.TryParse(Request.QueryString["ID"], out _siteActivityRuleId))
            {
                var link = dataManager.Links.SelectById(_siteActivityRuleId);

                IFileProvider fileProvider = new FSProvider();
                var file = fileProvider.Get(link.SiteID, link.URL);
                if (file != null && file.Length > 0)
                {
                    var fileName = link.URL;
                    if (Request.Browser.Browser.ToLower() == "ie")
                    {
                        fileName = Server.UrlEncode(fileName);
                        if (fileName != null)
                            fileName = fileName.Replace(@"+", @"%20");
                    }

                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.BinaryWrite(file);
                    Response.End();
                }
                else
                {
                    Response.Write("This file does not exist.");
                }
            }
        }
    }
}