using System;
using System.ComponentModel;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.UserControls.Payment
{
    public partial class PaymentStatus : System.Web.UI.UserControl
    {
        public event PaymentStatusChangedEventHandler PaymentStatusChanged;
        public delegate void PaymentStatusChangedEventHandler(object sender, PaymentStatusChangedEventArgs e);
        public class PaymentStatusChangedEventArgs : EventArgs
        {
            public Guid? ResponsibleId { get; set; }            
        }


        protected DataManager DataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid PaymentId
        {
            get
            {
                if (ViewState["PaymentId"] == null)
                    ViewState["PaymentId"] = Guid.Empty;
                return (Guid)ViewState["PaymentId"];
            }
            set
            {
                ViewState["PaymentId"] = value;
            }
        }

        

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid PaymentStatusId
        {
            get
            {
                if (ViewState["PaymentStatusId"] == null)
                    ViewState["PaymentStatusId"] = Guid.Empty;
                return (Guid)ViewState["PaymentStatusId"];
            }
            set
            {
                ViewState["PaymentStatusId"] = value;
            }
        }

        

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {                     
            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            var paymentStatus = DataManager.PaymentStatus.SelectById(CurrentUser.Instance.SiteID, PaymentStatusId);

            if (paymentStatus != null)            
                lrlPaymentStatus.Text = paymentStatus.Title;                            
            else
            {
                var paymentDefaultStatus = DataManager.PaymentStatus.SelectDefault(CurrentUser.Instance.SiteID);
                if (paymentDefaultStatus != null)
                {
                    lrlPaymentStatus.Text = paymentDefaultStatus.Title;
                    PaymentStatusId = paymentDefaultStatus.ID;
                }
            }

            var paymentTransitions =
                DataManager.PaymentTransition.SelectAll(CurrentUser.Instance.SiteID).Where(
                    rt =>
                    rt.IsActive && rt.InitialPaymentStatusID == PaymentStatusId);
            rlvPaymentTransitions.DataSource = paymentTransitions;
            rlvPaymentTransitions.DataBind();
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnRequirementTransition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnPaymentTransition_OnClick(object sender, EventArgs e)
        {
            var radButton = (RadButton) sender;
            PaymentStatusId = Guid.Parse(radButton.CommandArgument);
            BindData();

            if (PaymentStatusChanged != null)
            {                
                var paymentStatus = DataManager.PaymentStatus.SelectById(CurrentUser.Instance.SiteID, PaymentStatusId);
                //var serviceLevel = DataManager.ServiceLevel.SelectById(CurrentUser.Instance.SiteID, ServiceLevelId);

                var eventArgs = new PaymentStatusChangedEventArgs();

                //if (paymentStatus != null && serviceLevel != null)
                //{
                //    var responsibleId = DataManager.RequirementStatus.SelectResponsibleId(paymentStatus, serviceLevel, CompanyId);
                //    eventArgs.ResponsibleId = responsibleId;
                //}

                PaymentStatusChanged(this, eventArgs);
            }
        }
    }
}