using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class UnitConverter : JavaScriptConverter
    {
        /// <summary>
        /// When overridden in a derived class, converts the provided dictionary into an object of the specified type.
        /// </summary>
        /// <param name="dictionary">An <see cref="T:System.Collections.Generic.IDictionary`2"/> instance of property data stored as name/value pairs.</param>
        /// <param name="type">The type of the resulting object.</param>
        /// <param name="serializer">The <see cref="T:System.Web.Script.Serialization.JavaScriptSerializer"/> instance.</param>
        /// <returns>
        /// The deserialized object.
        /// </returns>
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }

            if (ReferenceEquals(type, typeof(Unit)) && dictionary.Count > 0)
            {
                if ((bool)dictionary["IsEmpty"])
                    return Unit.Empty;

                var value = Convert.ToDouble(dictionary["Value"]);
                var unitType = (UnitType)Enum.Parse(typeof(UnitType), dictionary["Type"].ToString());
                var unit = new Unit(value, unitType);

                return unit;
            }

            return null;
        }



        /// <summary>
        /// When overridden in a derived class, builds a dictionary of name/value pairs.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="serializer">The object that is responsible for the serialization.</param>
        /// <returns>
        /// An object that contains key/value pairs that represent the object’s data.
        /// </returns>
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var unit = (Unit)obj;

            if (unit != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("Type", unit.Type);
                dict.Add("Value", unit.Value);
                dict.Add("IsEmpty", unit.IsEmpty);
                return dict;
            }

            return new Dictionary<string, object>();
        }



        /// <summary>
        /// When overridden in a derived class, gets a collection of the supported types.
        /// </summary>
        /// <returns>An object that implements <see cref="T:System.Collections.Generic.IEnumerable`1"/> that represents the types supported by the converter.</returns>
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(Unit) })); }
        }
    }
}
