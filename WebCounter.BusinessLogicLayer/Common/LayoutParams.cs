using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class LayoutParams
    {
        public string Name { get; set; }
        public string Value { get; set; }



        /// <summary>
        /// Serializes the specified params list.
        /// </summary>
        /// <param name="paramsList">The params list.</param>
        /// <returns></returns>
        public static string Serialize(List<LayoutParams> paramsList)
        {
            var xml = new XElement("root",
                                          new XElement("params",
                                                       from param in paramsList
                                                       select (new XElement("param",
                                                                            new XAttribute("name", param.Name),
                                                                            new XAttribute("value", param.Value)))));

            return xml.ToString();
        }



        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static List<LayoutParams> Deserialize(string xml)
        {
            var xDocument = XDocument.Parse(xml);

            return (from param in xDocument.Descendants("param")
                   select new LayoutParams
                              {
                                  Name = param.Attribute("name").Value,
                                  Value = param.Attribute("value").Value                                  
                              }).ToList();
        }        




        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static string GetQuery(string xml)
        {
            return string.Join("&",
                               Deserialize(xml).Select(
                                   layoutParam => string.Format("{0}={1}", layoutParam.Name, layoutParam.Value)).ToList());
        }
    }


    public static class LayoutParamsExtension
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="layoutParams">The layout params.</param>
        /// <param name="systemName">Name of the system.</param>
        /// <returns></returns>
        public static string GetValue(this List<LayoutParams> layoutParams, string systemName)
        {
            var layoutParam = layoutParams.SingleOrDefault(o => o.Name == systemName);

            if (layoutParam != null)
                return layoutParam.Value;

            return string.Empty;
        }



        /// <summary>
        /// Gets the bool value.
        /// </summary>
        /// <param name="layoutParams">The layout params.</param>
        /// <param name="systemName">Name of the system.</param>
        /// <returns></returns>
        public static bool GetBoolValue(this List<LayoutParams> layoutParams, string systemName)
        {
            var layoutParam = layoutParams.SingleOrDefault(o => o.Name == systemName);

            if (layoutParam != null)
                return !string.IsNullOrEmpty(layoutParam.Value) && bool.Parse(layoutParam.Value);

            return false;
        }



        public static int? GetIntValue(this List<LayoutParams> layoutParams, string systemName)
        {
            var layoutParam = layoutParams.SingleOrDefault(o => o.Name == systemName);

            if (layoutParam != null)
                return !string.IsNullOrEmpty(layoutParam.Value) ? (int?)int.Parse(layoutParam.Value) : null;

            return null;
        }
    }
}
