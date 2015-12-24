using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Labitec.LeadForce.API.Interface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProduct" in both code and config file together.
    [ServiceContract]
    public interface IProduct
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        string GetProducts(Guid siteId, string username, string password, string xml);

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        string UpdateProduct(Guid siteId, string username, string password, string xml);
    }
}
