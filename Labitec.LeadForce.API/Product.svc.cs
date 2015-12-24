using System;
using System.Reflection;
using System.Xml.Linq;
using Labitec.LeadForce.API.Core;
using Labitec.LeadForce.API.Core.Enumerations;
using Labitec.LeadForce.API.DataAccessLayer;
using Labitec.LeadForce.API.Interface;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace Labitec.LeadForce.API
{    
    public class Product : IProduct
    {
        private const string Version = "1.0";
        private XDocument _result = new XDocument();
        private readonly APIAuthorization _authorization = new APIAuthorization();

        Product()
        {
            if (_result.Document != null)
                _result.Document.Add(new XElement("LeadForceResponse", new XAttribute("version", Version)));
        }


        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public string GetProducts(Guid siteId, string username, string password, string xml)
        {
            var user = _authorization.Authorize(siteId, username, password);
            if (user == null || !Access.Check(user, "API").Read || (user.AccessLevelID != (int)AccessLevel.Administrator && user.AccessLevelID != (int)AccessLevel.SystemAdministrator))
                return StatusHelper.FormatMessage(StatusCodes.AccessDenied, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);

            XDocument inputXml;

            try
            {
                inputXml = XDocument.Parse(xml);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.GetProducts error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InvalidParameters, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            try
            {
                var products = Products.GetProducts(siteId, inputXml);
                _result = StatusHelper.FormatMessage(StatusCodes.Ok, MethodBase.GetCurrentMethod().Name, _result);
                _result.Document.Element("LeadForceResponse").Add(new XElement("Result", products.Document.FirstNode));
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.GetProducts error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InternalError, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            return _result.ToString(SaveOptions.DisableFormatting);
        }



        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public string UpdateProduct(Guid siteId, string username, string password, string xml)
        {
            var user = _authorization.Authorize(siteId, username, password);
            if (user == null || !Access.Check(user, "API").Read || (user.AccessLevelID != (int)AccessLevel.Administrator && user.AccessLevelID != (int)AccessLevel.SystemAdministrator))
                return StatusHelper.FormatMessage(StatusCodes.AccessDenied, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
                            
            XDocument inputXml;

            try
            {
                inputXml = XDocument.Parse(xml);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.UpdateUser error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InvalidParameters, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            try
            {
                Products.UpdateProduct(siteId, user.ContactID, inputXml.Document);                
                _result = StatusHelper.FormatMessage(StatusCodes.Ok, MethodBase.GetCurrentMethod().Name, _result);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.UpdateUser error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InternalError, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            return _result.ToString(SaveOptions.DisableFormatting);
        }
    }
}
