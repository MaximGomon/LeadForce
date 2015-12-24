using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class WidgetBase : System.Web.UI.UserControl
    {
        public DataManager DataManager = new DataManager();
        public tbl_AccessProfile AccessProfile { get; set; }
        public Guid WidgetId { get; set; }
        public bool IsLeftColumn { get; set; }


        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id)
                return root;

            return (from Control c in root.Controls select FindControlRecursive(c, id)).FirstOrDefault(rc => rc != null);
        }
    }
}
