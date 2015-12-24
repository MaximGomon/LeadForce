using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.Controls
{
    public class BaseWizardStep : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();
        
        public bool IsEditMode
        {
            get
            {
                if (ViewState["IsEditMode"] == null)
                    ViewState["IsEditMode"] = false;

                return (bool)ViewState["IsEditMode"];
            }
            set { 
                ViewState["IsEditMode"] = value;                 
            }
        }

        public Guid? EditObjectId
        {
            get
            {                
                return (Guid?)ViewState["EditObjectId"];
            }
            set { ViewState["EditObjectId"] = value; }
        }


        /// <summary>
        /// Gets the multi page.
        /// </summary>
        public RadMultiPage MultiPage
        {
            get
            {
                return (RadMultiPage)this.Parent.FindControl("rmpWizard");
            }
        }



        /// <summary>
        /// Gets the tab strip.
        /// </summary>
        public RadTabStrip TabStrip
        {
            get
            {
                return (RadTabStrip)this.Parent.FindControl("rtsWizard");
            }
        }




        /// <summary>
        /// Gets the page views count.
        /// </summary>
        public int PageViewsCount
        {
            get
            {
                var multiPage = (RadMultiPage)this.Parent.FindControl("rmpWizard");

                if (multiPage != null)
                    return multiPage.PageViews.Count;
                
                return 0;
            }
        }


        /// <summary>
        /// Binds the data.
        /// </summary>
        public virtual void BindData()
        {            
            FindControl("lbtnNext").Visible = !IsEditMode;
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnNext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void lbtnNext_OnClick(object sender, EventArgs e)
        {
            GoToNextTab();
        }



        /// <summary>
        /// Goes to next tab.
        /// </summary>
        private void GoToNextTab()
        {
            var tabStrip = (RadTabStrip)this.Parent.FindControl("rtsWizard");            
            var currentTabIndex = tabStrip.SelectedIndex;

            if (currentTabIndex < tabStrip.Tabs.Count - 1)
            {
                var nextTab = tabStrip.Tabs[currentTabIndex + 1];
                if (!string.IsNullOrEmpty(nextTab.Value))
                {
                    nextTab.Enabled = true;
                    nextTab.Selected = true;
                    if (!IsEditMode)
                        tabStrip.ValidationGroup = nextTab.Value;
                    GoToNextPageView(nextTab.Value);
                }                
            }
        }



        /// <summary>
        /// Goes to next page view.
        /// </summary>
        private void GoToNextPageView(string viewName)
        {
            var multiPage = (RadMultiPage)this.Parent.FindControl("rmpWizard");

            var nextPageView = multiPage.FindPageViewByID(viewName);
            if (nextPageView == null)
            {
                nextPageView = new RadPageView { ID = viewName };
                multiPage.PageViews.Add(nextPageView);
            }
            nextPageView.Selected = true;
        }



        /// <summary>
        /// Gets the post back control.
        /// </summary>
        /// <returns></returns>
        protected Control GetPostBackControl()
        {
            Control control = null;
            var ctrlname = Page.Request.Params.Get("__EVENTTARGET");
            if (!string.IsNullOrEmpty(ctrlname))            
                control = Page.FindControl(ctrlname);            
            else            
                foreach (var c in (from string ctl in Page.Request.Form select Page.FindControl(ctl)).OfType<LinkButton>())
                {
                    control = c;
                    break;
                }
            
            return control;
        }
    }
}