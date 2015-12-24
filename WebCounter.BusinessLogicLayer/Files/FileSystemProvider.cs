using System;
using System.IO;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.BusinessLogicLayer.Files
{
    public class FileSystemProvider
    {
        /// <summary>
        /// Uploads the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="module">The module.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="fileType">Type of the file.</param>
        /// <returns></returns>
        public string Upload(Guid siteId, string module, string filename, Stream fileStream, FileType fileType, bool overwrite = true)
        {
            if (overwrite)
                filename = GetFilename(siteId, module, filename, fileType);

            var directoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], siteId) + "\\" + module;

            directoryPath = Path.Combine(directoryPath, GetDirectoryByFileType(fileType));

            var filePath = Path.Combine(directoryPath, filename);
            
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {                                
                var buf = new byte[fileStream.Length];
                fileStream.Read(buf, 0, buf.Length);  
                fs.Write(buf, 0, buf.Length);
            }    

            return filename;
        }


        /// <summary>
        /// Uploads the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <returns></returns>
        public string Upload(Guid siteId, string filename, Stream fileStream)
        {
            var directoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], siteId);
            var filePath = string.Format("{0}/{1}", directoryPath, filename);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var fs = new FileStream(filePath, FileMode.CreateNew))
            {
                var buf = new byte[fileStream.Length];
                fileStream.Read(buf, 0, buf.Length);
                fs.Write(buf, 0, buf.Length);
            }

            return filename;
        }



        /// <summary>
        /// Gets the link.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="module">The module.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="fileType">Type of the file.</param>
        /// <returns></returns>
        public string GetLink(Guid siteId, string module, string filename, FileType fileType)
        {
            var virtualDirectoryPath =
                string.Concat(string.Format(WebConfigurationManager.AppSettings["filesVirtualPath"], siteId), "/",
                              module, "/", GetDirectoryByFileType(fileType), "/");

            return virtualDirectoryPath + filename;
        }


        public string GetRemoteLink(Guid siteId, string module, string filename, FileType fileType)
        {
            var link = string.Concat(string.Format(string.Format(WebConfigurationManager.AppSettings["filesUri"], siteId, module)), "/", GetDirectoryByFileType(fileType), "/");

            return link + filename;
        }



        /// <summary>
        /// Gets the physical path.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="module">The module.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="fileType">Type of the file.</param>
        /// <returns></returns>
        public string GetPhysicalPath(Guid siteId, string module, string filename, FileType fileType)
        {
            var directoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], siteId) + "\\" + module;

            directoryPath = Path.Combine(directoryPath, GetDirectoryByFileType(fileType));

            return Path.Combine(directoryPath, filename);
        }



        /// <summary>
        /// Deletes the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="module">The module.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="fileType">Type of the file.</param>
        public void Delete(Guid siteId, string module, string filename, FileType fileType)
        {
            var directoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], siteId) + "\\" + module;

            directoryPath = Path.Combine(directoryPath, GetDirectoryByFileType(fileType));

            var filePath = Path.Combine(directoryPath, filename);            

            if (File.Exists(filePath))
                File.Delete(filePath);            
        }



        /// <summary>
        /// Determines whether the specified site id is exist.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="module">The module.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="fileType">Type of the file.</param>
        /// <returns>
        ///   <c>true</c> if the specified site id is exist; otherwise, <c>false</c>.
        /// </returns>
        public bool IsExist(Guid siteId, string module, string filename, FileType fileType)
        {
            var directoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], siteId) + "\\" + module;

            directoryPath = Path.Combine(directoryPath, GetDirectoryByFileType(fileType));

            var filePath = Path.Combine(directoryPath, filename);

            return File.Exists(filePath);
        }



        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="directory">The directory.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="fileType">Type of the file.</param>
        /// <returns></returns>
        protected string GetFilename(Guid siteId, string directory, string filename, FileType fileType)
        {
            var newFilename = filename;
            var directoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], siteId) + "\\" + directory;
            directoryPath = Path.Combine(directoryPath, GetDirectoryByFileType(fileType));
            var filePath = Path.Combine(directoryPath, filename);

            var i = 0;
            while (File.Exists(filePath))
            {
                newFilename = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(filename), i.ToString("#000"), Path.GetExtension(filename));
                filePath = Path.Combine(directoryPath, newFilename);
                i++;
            }

            return newFilename;
        }



        /// <summary>
        /// Gets the view directory.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="module">The module.</param>
        /// <param name="fileType">Type of the file.</param>
        /// <returns></returns>
        public string GetViewDirectory(Guid siteId, string module, FileType fileType)
        {
            var directoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], siteId) + "\\" + module;

            var directory = GetDirectoryByFileType(fileType);

            directoryPath = Path.Combine(directoryPath, directory);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            

            return string.Concat(string.Format("~" + WebConfigurationManager.AppSettings["filesVirtualPath"], siteId), "/", module, "/", directory, "/");
        }



        /// <summary>
        /// Gets the floating button directory.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public string GetFloatingButtonDirectory(Guid siteId)
        {
            var directoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], siteId) + "\\" + "FloatingButtonIcons";

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            return string.Concat(string.Format("~" + WebConfigurationManager.AppSettings["filesVirtualPath"], siteId), "/FloatingButtonIcons", "/");
        }



        /// <summary>
        /// Gets the type of the directory by file.
        /// </summary>
        /// <param name="fileType">Type of the file.</param>
        /// <returns></returns>
        protected string GetDirectoryByFileType(FileType fileType)
        {
            var directory = string.Empty;

            switch (fileType)
            {
                case FileType.Flash:
                    directory = "Flashes";
                    break;
                case FileType.Document:
                    directory = "Documents";
                    break;
                case FileType.Image:
                    directory = "Images";
                    break;
                case FileType.Media:
                    directory = "Medias";
                    break;
                case FileType.Attachment:
                    directory = "Attachments";
                    break;
                case FileType.Thumbnail:
                    directory = "Thumbnails";
                    break;
            }

            return directory;
        }

        public static string GetFileSize(long Bytes)
        {
            if (Bytes >= 1073741824)
            {
                Decimal size = Decimal.Divide(Bytes, 1073741824);
                return String.Format("{0:##.##} ГБ", size);
            }
            else if (Bytes >= 1048576)
            {
                Decimal size = Decimal.Divide(Bytes, 1048576);
                return String.Format("{0:##.##} МБ", size);
            }
            else if (Bytes >= 1024)
            {
                Decimal size = Decimal.Divide(Bytes, 1024);
                return String.Format("{0:##.##} КБ", size);
            }
            else if (Bytes > 0 & Bytes < 1024)
            {
                Decimal size = Bytes;
                return String.Format("{0:##.##} Б", size);
            }
            else
            {
                return "0 Bytes";
            }

        }

        public FileMetadata Copy(Guid sourceSiteId, string sourceFileName, Guid destSiteId, string module)
        {
            var sourceDirectoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], sourceSiteId);
            var sourceFilePath = string.Format("{0}/{1}", sourceDirectoryPath, sourceFileName);

            var destDirectoryPath = string.Format(WebConfigurationManager.AppSettings["filesPath"], destSiteId);
            var newFileName = GetFilename(destSiteId, sourceFileName);
            var destFilePath = string.Format("{0}/{1}", destDirectoryPath, newFileName);

            if (!Directory.Exists(destDirectoryPath))
                Directory.CreateDirectory(destDirectoryPath);

            File.Copy(sourceFilePath, destFilePath);
            var fileInfo = new FileInfo(sourceFilePath);

            return new FileMetadata { Name = newFileName, Size = fileInfo.Length };
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

    public class FileMetadata
    {
        public string Name { get; set; }
        public long Size { get; set; }
    }
}
