using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Labitec.LeadForce.API.Core.Enumerations;
using WebCounter.BusinessLogicLayer.Common;

namespace Labitec.LeadForce.API.Core
{
    public static class StatusHelper
    {
        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="method">The method.</param>
        /// <param name="inputXml">The input XML.</param>
        /// <returns></returns>
        public static XDocument FormatMessage(StatusCodes code, string method, XDocument inputXml)
        {
            inputXml.Document.Element("LeadForceResponse").Add(new XAttribute("datetime", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss.fff")));
            inputXml.Document.Element("LeadForceResponse").Add(new XAttribute("method", method));
            inputXml.Document.Element("LeadForceResponse").Add(new XElement("status", EnumHelper.GetEnumDescription(code)));

            return inputXml;
        }
    }
}