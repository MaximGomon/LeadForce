using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.Ajax.Samples;

namespace WebCounter.Service
{
    [ServiceContract]
    public interface ICounterService
    {
        /*[OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "LG_CounterService", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void LG_CounterService(string SiteID, string ContactID, string URL, string RefferURL, string Resolution);*/


        [OperationContract]
        [WebGet(UriTemplate = "LG_CounterService?SiteID={SiteID}&ContactID={ContactID}&URL={URL}&RefferURL={RefferURL}&Resolution={Resolution}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [JSONPBehavior(callback = "jscallback")]
        void LG_CounterService(string SiteID, string ContactID, string URL, string RefferURL, string Resolution);



        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "LG_HelperService", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string LG_HelperService(string SiteID, string ContactID, string URL);



        [OperationContract]
        [JSONPBehavior(callback = "jscallback")]
        [WebGet(UriTemplate = "LG_LinkService?SiteID={SiteID}&ContactID={ContactID}&ActivityCode={ActivityCode}&register={register}", ResponseFormat = WebMessageFormat.Json)]
        JsonResponse LG_LinkService(string SiteID, string ContactID, string ActivityCode, string register);



        [OperationContract]
        [WebGet(UriTemplate = "LG_FormService?SiteID={SiteID}&ContactID={ContactID}&ActivityCode={ActivityCode}&Mode={Mode}&FromVisit={FromVisit}&Through={Through}&Period={Period}&Parameter={Parameter}&ContactCategory={ContactCategory}&PopupAppear={PopupAppear}&register={register}&ContainerID={ContainerID}", ResponseFormat = WebMessageFormat.Json)]
        [JSONPBehavior(callback = "jscallback")]
        JsonResponse LG_FormService(string SiteID, string ContactID, string ActivityCode, string Mode, string FromVisit, string Through, string Period, string Parameter, string ContactCategory, string PopupAppear, string register, string ContainerID);



        [OperationContract]
        [WebGet(UriTemplate = "AP_FormService?SiteID={SiteID}&ContactID={ContactID}&ActivityCode={ActivityCode}&Mode={Mode}&ContainerID={ContainerID}", ResponseFormat = WebMessageFormat.Json)]
        [JSONPBehavior(callback = "jscallback")]
        JsonResponse AP_FormService(string SiteID, string ContactID, string ActivityCode, string Mode, string ContainerID);



        [OperationContract]
        [WebGet(UriTemplate = "LG_FormServiceResult?SiteID={SiteID}&ContactID={ContactID}&ActivityCode={ActivityCode}&Parameter={Parameter}&register={register}&FormData={FormData}", ResponseFormat = WebMessageFormat.Json)]
        [JSONPBehavior(callback = "jscallback")]
        JsonResponse LG_FormServiceResult(string SiteID, string ContactID, string ActivityCode, string Parameter, string register, string FormData);



        [OperationContract]
        [WebGet(UriTemplate = "LG_FormServiceCancel?SiteID={SiteID}&ContactID={ContactID}&ActivityCode={ActivityCode}&Parameter={Parameter}")]
        void LG_FormServiceCancel(string SiteID, string ContactID, string ActivityCode, string Parameter);


        [OperationContract]
        [WebGet(UriTemplate = "LG_FormServiceOpen?SiteID={SiteID}&ContactID={ContactID}&ActivityCode={ActivityCode}&Parameter={Parameter}")]
        void LG_FormServiceOpen(string SiteID, string ContactID, string ActivityCode, string Parameter);


        [OperationContract]
        [WebGet(UriTemplate = "LG_LinkProcessing?ContactID={ContactID}&ActionLinkID={ActionLinkID}&Resolution={Resolution}", ResponseFormat = WebMessageFormat.Json)]
        [JSONPBehavior(callback = "jscallback")]
        JsonResponse LG_LinkProcessing(string ContactID, string ActionLinkID, string Resolution);
    }

    [DataContract]
    public class NameValuePair
    {
        [DataMember]
        public string N { get; set; }
        [DataMember]
        public string V { get; set; }
    }

    [CollectionDataContract(Namespace = "")]
    public class NameValuePairs : List<NameValuePair> { }

    public class JsonResponse
    {
        public string RuleType;
        public string Value;
        public string ContainerID;
        public string Mode;
        public string ActivityCode;
        public string Parameter;
        public string PopupAppear;
    }
}