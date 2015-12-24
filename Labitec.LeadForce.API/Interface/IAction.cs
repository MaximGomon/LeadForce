using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Labitec.LeadForce.API.Interface
{    
    [ServiceContract]
    public interface IAction
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        string GetActions(Guid siteId, string username, string password, string xml);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        string CreateAction(Guid siteId, string username, string password, string xml);
    }
}