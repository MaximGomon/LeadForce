using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Labitec.LeadForce.API.Interface
{    
    [ServiceContract]
    public interface IRequirement
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        string GetRequirements(Guid siteId, string username, string password, string xml);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        string UpdateRequirement(Guid siteId, string username, string password, string xml);
    }
}