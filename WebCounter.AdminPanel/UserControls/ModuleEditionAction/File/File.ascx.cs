using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.ModuleEditionAction.File
{
    public partial class File : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid FileId
        {
            get
            {
                object o = ViewState["FileId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["FileId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbUpdateFile_OnClick(object sender, EventArgs e)
        {

            var a = FileId;
            var fsp = new FileSystemProvider();
            if (rauFile.UploadedFiles.Count > 0)
            {
                string fileName = null;
                if (rauFile.UploadedFiles.Count > 0)
                {                    
                    IFileProvider fileProvider = new FSProvider();
                    fileName = fileProvider.GetFilename(CurrentUser.Instance.SiteID, rauFile.UploadedFiles[0].FileName);
                    fsp.Upload(CurrentUser.Instance.SiteID, fileName, rauFile.UploadedFiles[0].InputStream);

                }
                var link = _dataManager.Links.SelectById(FileId) ?? new tbl_Links();
                link.SiteID = ((LeadForceBasePage)Page).SiteId;
                link.Name = fileName;
                link.RuleTypeID = (int)RuleType.File;
                link.URL = fileName;
                link.FileSize = rauFile.UploadedFiles[0].InputStream.Length;
                string code = String.Format("file_[{0}]_[{1}]", DateTime.Now.ToString("ddMMyyyy"), DateTime.Now.ToString("mmss"));
                int maxCode = _dataManager.Links.SelectByCode(((LeadForceBasePage)Page).SiteId, code);
                if (maxCode != 0) maxCode++;
                link.Code = code + (maxCode != 0 ? String.Format("[{0}]", maxCode >= 10 ? maxCode.ToString() : "0" + maxCode.ToString()) : "");
                if (link.ID == Guid.Empty)
                {
                    _dataManager.Links.Add(link);
                } else
                {
                    _dataManager.Links.Update(link);                    
                }
                ((Labitec.UI.BaseWorkspace.Grid)FindControl("gridLinks", Page.Controls)).Rebind();

            }
        }

        public override Control FindControl(string id)
        {
            var ctrl = base.FindControl(id);

            if (ctrl == null)
                ctrl = FindControl(id, Controls);

            return ctrl;
        }


        public static Control FindControl(string id, ControlCollection col)
        {
            foreach (Control c in col)
            {
                Control child = FindControlRecursive(c, id);
                if (child != null)
                    return child;
            }
            return null;
        }


        private static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control rc = FindControlRecursive(c, id);
                if (rc != null)
                    return rc;
            }
            return null;
        }
    }
}