using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace WebCounter.BusinessLogicLayer.Files
{
    public interface IFileProvider
    {
        void Upload(Guid siteId, string filename, FileUpload fileUpload);
        byte[] Get(Guid siteId, string filename);
        string GetFilename(Guid siteId, string filename);
    }
}