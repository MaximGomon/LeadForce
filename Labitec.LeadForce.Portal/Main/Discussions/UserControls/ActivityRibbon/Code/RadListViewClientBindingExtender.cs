using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;

namespace Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon.Code
{
    /// <summary>
    /// A simple RadListView extender which to showcase a 
    /// possible basic client-binding implementation
    /// </summary>
    [TargetControlType(typeof(RadListView))]
    public class RadListViewClientBindingExtender : ExtenderControl
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (DataBindingSettings.EnableAutoLoad)
            {
                SetupAutoLoad();
            }
        }



        /// <summary>
        /// Setups the auto load.
        /// </summary>
        private void SetupAutoLoad()
        {
            if (!string.IsNullOrEmpty(TargetControlID))
            {
                var targetControl = Page.FindControl(TargetControlID) as RadListView;
                if (targetControl != null)
                {
                    if (targetControl.DataSource == null && string.IsNullOrEmpty(targetControl.DataSourceID))
                    {
                        targetControl.ItemTemplate = new DummyTemplate();
                        targetControl.DataSource = Enumerable.Range(1, 1);
                    }
                }
            }
        }


        class DummyTemplate : ITemplate
        {
            #region ITemplate Members

            public void InstantiateIn(Control container)
            {
            }

            #endregion
        }

        string _clientItemTemplate;
        [Browsable(false), DefaultValue(""),
         PersistenceMode(PersistenceMode.InnerProperty),
         TemplateContainer(typeof(RadListView))]
        public string ClientItemTemplate
        {
            get
            {
                if (_clientItemTemplate == null)
                {
                    _clientItemTemplate = string.Empty;
                }
                return _clientItemTemplate;
            }
            set { _clientItemTemplate = value; }
        }

        private ClientDataBindingSettings _dataBinding;
        [NotifyParentProperty(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Category("Client"), PersistenceMode(PersistenceMode.InnerProperty)]
        public ClientDataBindingSettings DataBindingSettings
        {
            get
            {
                if (_dataBinding == null)
                {
                    _dataBinding = new ClientDataBindingSettings();
                }
                return _dataBinding;
            }
        }

        private string HtmlEncode(string text)
        {
            return HttpContext.Current.Server.HtmlEncode(text);
        }

        protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors(Control targetControl)
        {
            var descriptor = new ScriptBehaviorDescriptor("Telerik.Web.UI.RadListViewClientBindingExtender", targetControl.ClientID);

            descriptor.AddProperty("_clientItemTemplate", HtmlEncode(ClientItemTemplate.Trim()));
            DataBindingSettings.DescribeClientProperties(descriptor);

            return new ScriptDescriptor[] { descriptor };

        }

        protected override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var reference = new ScriptReference { Path = ResolveUrl("~/Scripts/RadListViewClientBindingExtender.js") };

            return new ScriptReference[] { reference };

        }
    }

    public class ClientDataBindingSettings
    {
        /// <summary>
        /// Gets or sets if RadListView should be auto databind client-side
        /// on the first load
        /// </summary>
        [DefaultValue(false)]
        public bool EnableAutoLoad
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the clientid of the html element 
        /// which will be used as container for the data items
        /// </summary>
        [DefaultValue("")]
        public string ClientItemContainerID
        {
            get;
            set;
        }

        [DefaultValue("")]
        public string LocationUrl
        {
            get;
            set;
        }

        [DefaultValue("")]
        public string MethodName
        {
            get;
            set;
        }

        [DefaultValue("")]
        public string CallBackFunction
        {
            get;
            set;
        }

        internal void DescribeClientProperties(ScriptBehaviorDescriptor descriptor)
        {
            if (!string.IsNullOrEmpty(ClientItemContainerID))
            {
                descriptor.AddProperty("_itemContainerId", ClientItemContainerID);
            }
            if (!string.IsNullOrEmpty(LocationUrl))
            {
                descriptor.AddProperty("_locationUrl", LocationUrl);
            }
            if (!string.IsNullOrEmpty(MethodName))
            {
                descriptor.AddProperty("_methodName", MethodName);
            }
            if (!string.IsNullOrEmpty(CallBackFunction))
            {
                descriptor.AddProperty("_callBackFunction", CallBackFunction);
            }
            if (EnableAutoLoad)
            {
                descriptor.AddProperty("_enableAutoLoad", EnableAutoLoad);
            }
        }
    }
}