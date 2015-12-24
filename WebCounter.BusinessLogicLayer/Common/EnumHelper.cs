using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class EnumHelper
    {
        /// <summary>
        /// Gets the enum description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute),false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }



        /// <summary>
        /// Enums to list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> EnumToList<T>()
        {
            var enumType = typeof(T);
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            var enumValArray = Enum.GetValues(enumType);
            var enumValList = new List<T>(enumValArray.Length);            
            enumValList.AddRange(from int val in enumValArray select (T) Enum.Parse(enumType, val.ToString()));
            return enumValList;
        }



        /// <summary>
        /// Enums to drop down list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dropDownList">The drop down list.</param>
        /// <param name="showEmpty">if set to <c>true</c> [show empty].</param>
        public static void EnumToDropDownList<T>(ref DropDownList dropDownList, bool showEmpty = true)
        {
            var enumType = typeof(T);

            if (showEmpty)
                dropDownList.Items.Add(new ListItem("Выберите значение", ""));

            foreach (var item in EnumToList<T>())
                dropDownList.Items.Add(new ListItem(GetEnumDescription((Enum)Enum.Parse(enumType, item.ToString())), ((int)Enum.Parse(enumType, item.ToString())).ToString()));
        }



        /// <summary>
        /// Enums to drop down list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dropDownList">The drop down list.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="showEmpty">if set to <c>true</c> [show empty].</param>
        public static void EnumToDropDownList<T>(ref DropDownList dropDownList, string selectedValue, bool showEmpty = true)
        {
            var enumType = typeof(T);

            if (showEmpty)
                dropDownList.Items.Add(new ListItem("Выберите значение", ""));

            foreach (var item in EnumToList<T>())
                dropDownList.Items.Add(new ListItem(GetEnumDescription((Enum)Enum.Parse(enumType, item.ToString())), ((int)Enum.Parse(enumType, item.ToString())).ToString()));

            dropDownList.Items.FindByValue(selectedValue).Selected = true;
        }


        /*public static void EnumToDropDownList(Type enumeration, ref DropDownList dropDownList, bool showEmpty = true)
        {
            string[] names = Enum.GetNames(enumeration);
            var values = Enum.GetValues(enumeration);

            for (int i = 0; i < names.Length; i++)
            {
                dropDownList.Items.Add(new ListItem(EnumHelper.GetEnumDescription((Enum)Enum.Parse(enumeration, names[i])), (values.GetValue(i)).ToString()));
                //ht.Add(Convert.ToInt32(values.GetValue(i)).ToString(), names[i]);
            }

        }*/
    }
}
