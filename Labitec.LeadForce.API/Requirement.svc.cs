using System;
using System.Configuration;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using Labitec.LeadForce.API.Core;
using Labitec.LeadForce.API.Core.Enumerations;
using Labitec.LeadForce.API.DataAccessLayer;
using Labitec.LeadForce.API.Interface;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.DocumentManagement;

namespace Labitec.LeadForce.API
{
    public class Requirement : IRequirement
    {
        private const string Version = "1.0";
        private XDocument _result = new XDocument();
        private readonly APIAuthorization _authorization = new APIAuthorization();

        Requirement()
        {
            if (_result.Document != null)
                _result.Document.Add(new XElement("LeadForceResponse", new XAttribute("version", Version)));
        }



        /// <summary>
        /// Gets the requirements.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public string GetRequirements(Guid siteId, string username, string password, string xml)
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
                Log.Error(string.Format("LeadForce.API.GetRequirements error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InvalidParameters, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            try
            {
                var requirements = Requirements.GetRequirements(siteId, inputXml);
                _result = StatusHelper.FormatMessage(StatusCodes.Ok, MethodBase.GetCurrentMethod().Name, _result);
                _result.Document.Element("LeadForceResponse").Add(new XElement("Result", requirements.Document.FirstNode));
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.GetRequirements error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InternalError, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            return _result.ToString(SaveOptions.DisableFormatting).Replace("=\"/files/", "=\"" + Settings.LeadForceSiteUrl + "/files/");
        }



        /// <summary>
        /// Updates the requirement.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public string UpdateRequirement(Guid siteId, string username, string password, string xml)
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
                Log.Error(string.Format("LeadForce.API.UpdateRequirement error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InvalidParameters, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            try
            {
                var requirementId = Requirements.UpdateRequirement(siteId, user.ID, inputXml.Document);
                if (requirementId.HasValue)
                {                    
                    var dataManager = new DataManager();
                    var requirement = dataManager.Requirement.SelectById(siteId, requirementId.Value);
                    var requestSourceType = dataManager.RequirementType.SelectById(siteId, requirement.RequirementTypeID);
                    var documentNumerator = DocumentNumerator.GetNumber((Guid)requestSourceType.NumeratorID, requirement.CreatedAt, requestSourceType.tbl_Numerator.Mask, "tbl_Requirement");
                    requirement.Number = documentNumerator.Number;
                    requirement.SerialNumber = documentNumerator.SerialNumber;
                    dataManager.Requirement.Update(requirement);
                }
                _result = StatusHelper.FormatMessage(StatusCodes.Ok, MethodBase.GetCurrentMethod().Name, _result);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("LeadForce.API.UpdateRequirement error, SiteId: {0}, UserName: {1}, Xml: {2}", siteId, username, xml), ex);
                return StatusHelper.FormatMessage(StatusCodes.InternalError, MethodBase.GetCurrentMethod().Name, _result).ToString(SaveOptions.DisableFormatting);
            }

            return _result.ToString(SaveOptions.DisableFormatting);
        }
    }
}
