using System;
using System.Reflection;
using System.Xml.Linq;
using Labitec.LeadForce.API.Core;
using Labitec.LeadForce.API.Core.Enumerations;
using Labitec.LeadForce.API.DataAccessLayer;
using Labitec.LeadForce.API.Interface;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using System.Xml.XPath;

namespace Labitec.LeadForce.API
{    
    public class Action : IAction
    {
        private const string Version = "1.0";
        private XDocument _result = new XDocument();
        private readonly APIAuthorization _authorization = new APIAuthorization();

        Action()
        {
            if (_result.Document != null)
                _result.Document.Add(new XElement("LeadForceResponse", new XAttribute("version", Version)));
        }



        /// <summary>
        /// Gets the actions.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public string GetActions(Guid siteId, string username, string password, string xml)
        {
            var user = _authorization.Authorize(siteId, username, password);
            if (user == null || !Access.Check(user, "API").Read)
                return StatusHelper.FormatMessage(StatusCodes.AccessDenied, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);

            XDocument inputXml;

            try
            {
                inputXml = XDocument.Parse(xml);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.GetActions error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InvalidParameters, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            try
            {
                var actions = Actions.GetActions(siteId, inputXml);
                _result = StatusHelper.FormatMessage(StatusCodes.Ok, MethodBase.GetCurrentMethod().Name, _result);
                _result.Document.Element("LeadForceResponse").Add(new XElement("Result", actions.Document.FirstNode));
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.GetActions error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InternalError, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            return _result.ToString(SaveOptions.DisableFormatting);
        }



        /// <summary>
        /// Creates the action.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public string CreateAction(Guid siteId, string username, string password, string xml)
        {
            var user = _authorization.Authorize(siteId, username, password);
            if (user == null || !Access.Check(user, "API").Write)
                return StatusHelper.FormatMessage(StatusCodes.AccessDenied, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);

            XDocument inputXml;

            try
            {
                inputXml = XDocument.Parse(xml);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.CreateAction error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InvalidParameters, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            try
            {
                Actions.CreateAction(siteId, inputXml.Document);
                _result = StatusHelper.FormatMessage(StatusCodes.Ok, MethodBase.GetCurrentMethod().Name, _result);

                //CounterServiceHelper.CheckEvent(siteId, Guid.Parse(inputXml.XPathSelectElement("/LeadForceRequest/Action/ContactID").Value));
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.CreateAction error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InternalError, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            return _result.ToString(SaveOptions.DisableFormatting);
        }
    }
}
