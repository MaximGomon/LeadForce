using System;
using System.Configuration;
using System.Reflection;
using System.Xml.Linq;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using Labitec.LeadForce.API.Core;
using Labitec.LeadForce.API.Core.Enumerations;
using Labitec.LeadForce.API.DataAccessLayer;
using Labitec.LeadForce.API.Interface;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace Labitec.LeadForce.API
{    
    public class Contact : IContact
    {        
        private const string Version = "1.0";
        private XDocument _result = new XDocument();
        private readonly APIAuthorization _authorization = new APIAuthorization();

        Contact()
        {
            if (_result.Document != null)
                _result.Document.Add(new XElement("LeadForceResponse", new XAttribute("version", Version)));
        }


        /// <summary>
        /// Gets the contacts.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public string GetContacts(Guid siteId, string username, string password, string xml)
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
                Log.Error(string.Format("LeadForce.API.GetContacts error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InvalidParameters, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);                
            }

            try
            {
                var contacts = Contacts.GetContacts(siteId, inputXml);
                _result = StatusHelper.FormatMessage(StatusCodes.Ok, MethodBase.GetCurrentMethod().Name, _result);
                _result.Document.Element("LeadForceResponse").Add(new XElement("Result", contacts.Document.FirstNode));
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.GetContacts error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InternalError, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            return _result.ToString(SaveOptions.DisableFormatting);
        }



        /// <summary>
        /// Updates the contact.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public string UpdateContact(Guid siteId, string username, string password, string xml)
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
                Log.Error(string.Format("LeadForce.API.UpdateContact error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InvalidParameters, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            try
            {
                Contacts.UpdateContact(siteId, user.ID, inputXml.Document);
                _result = StatusHelper.FormatMessage(StatusCodes.Ok, MethodBase.GetCurrentMethod().Name, _result);                
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.UpdateContact error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InternalError, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            return _result.ToString(SaveOptions.DisableFormatting);
        }



        /// <summary>
        /// Checks the name.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="fullName">The full name.</param>
        /// <returns></returns>
        public string CheckName(Guid siteId, string username, string password, string fullName)
        {
            var user = _authorization.Authorize(siteId, username, password);
            if (user == null || !Access.Check(user, "API").Read)
                return StatusHelper.FormatMessage(StatusCodes.AccessDenied, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);

            try
            {
                var nc = new NameChecker(ConfigurationManager.AppSettings["ADONETConnectionString"]);

                nc.CheckName(fullName, NameCheckerFormat.IOF, Correction.Correct);

                _result = StatusHelper.FormatMessage(StatusCodes.Ok, MethodBase.GetCurrentMethod().Name, _result);

                _result.Document.Element("LeadForceResponse").Add(new XElement("Result",
                                                                               new XElement("CheckName",
                                                                                            new XElement("Surname", nc.Surname),
                                                                                            new XElement("Name", nc.Name),
                                                                                            new XElement("Patronymic", nc.Patronymic),
                                                                                            new XElement("IsNameCorrect", nc.IsNameCorrect.ToString()),
                                                                                            new XElement("Gender", nc.Gender != null ? EnumHelper.GetEnumDescription(nc.Gender) : string.Empty,
                                                                                                new XAttribute("system", nc.Gender != null ? ((int)nc.Gender).ToString() : string.Empty)))));
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.CheckName error, SiteId: {0}, UserName: {1}, FullName: {2}", siteId,username, fullName), ex);
                return StatusHelper.FormatMessage(StatusCodes.InternalError, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            return _result.ToString(SaveOptions.DisableFormatting);
        }
    }
}
