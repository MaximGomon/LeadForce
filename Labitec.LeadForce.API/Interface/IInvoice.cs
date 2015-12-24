using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Labitec.LeadForce.API.Interface
{    
    [ServiceContract]
    public interface IInvoice
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        string GetInvoices(Guid siteId, string username, string password, string xml);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        string UpdateInvoice(Guid siteId, string username, string password, string xml);
    }
}