using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;

namespace WebCounter.AdminPanel.Handlers
{
    /// <summary>
    /// Summary description for CommentsFileUpload
    /// </summary>
    public class CommentsFileUpload : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            try
            {
                if (context.Request.Files.Count > 0)
                {
                    var uploadedFile = context.Request.Files[0];
                    var commentType = string.Empty;
                    if (context.Request["commentType"] != null)
                        commentType = EnumHelper.GetEnumDescription(context.Request["commentType"].ToEnum<CommentTables>());
                    else
                        commentType = "PublicationComments";

                    var fsp = new FileSystemProvider();
                    var fileName = fsp.Upload(CurrentUser.Instance.SiteID, commentType,
                                              uploadedFile.FileName, uploadedFile.InputStream, FileType.Attachment);

                    context.Response.Write(string.Format("{{ error: '', msg: '{0}'}}", fileName));
                }
            }
            catch (Exception ex)
            {
                Log.Error("Ошибка загрузки файла", ex);
                context.Response.Write("{ error: 'Ошибка загрузки файла', msg: ''}");
            }
        }



        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}