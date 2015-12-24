using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace WebCounter.BusinessLogicLayer.Files
{
    public class FSProvider : IFileProvider
    {
        /// <summary>
        /// Uploads the specified file upload.
        /// </summary>
        /// <param name="fileUpload">The file upload.</param>
        public void Upload(Guid siteId, string filename, FileUpload fileUpload)
        {
            var directoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], siteId);
            var filePath = string.Format("{0}/{1}", directoryPath, filename);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            if (File.Exists(filePath))
                File.Delete(filePath);

            fileUpload.SaveAs(filePath);
        }



        /// <summary>
        /// Gets the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public byte[] Get(Guid siteId, string filename)
        {
            var webClient = new WebClient();
            byte[] result = webClient.DownloadData(string.Format(WebConfigurationManager.AppSettings["filesUri"], siteId, filename));

            return result;
        }



        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public string GetFilename(Guid siteId, string filename)
        {
            var newFilename = filename;
            var directoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], siteId);
            var filePath = string.Format("{0}/{1}", directoryPath, filename);

            var i = 0;
            while (File.Exists(filePath))
            {
                newFilename = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(filename), i.ToString("#000"), Path.GetExtension(filename));
                filePath = string.Format("{0}/{1}", directoryPath, newFilename);
                i++;
            }

            return newFilename;
        }
    }
}
