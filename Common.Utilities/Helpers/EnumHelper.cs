using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Common.Utilities.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// Get Given Enum As List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<KeyValuePair<string, int>> GetEnumList<T>()
        {
            var list = new List<KeyValuePair<string, int>>();
            foreach (var e in System.Enum.GetValues(typeof(T)))
            {
                var value=(e as Enum).GetAttributeOfType();

                if (string.IsNullOrEmpty(value))
                {
                    value = e.ToString();
                }


                list.Add(new KeyValuePair<string, int>(value, (int)e));
            }
            return list;
        }

        /// <summary>
        /// Get <code>Description</code> attribute value from enum
        /// </summary>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public static string GetAttributeOfType(this Enum enumVal)
        {
            if (enumVal == null)
                return String.Empty;

            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? ((DescriptionAttribute)attributes[0]).Description : String.Empty;
        }
    }
}
