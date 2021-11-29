using System;
using System.Linq;
using System.Runtime.Serialization;

namespace PizzaApp
{
    public static class EnumHelper
    {
        public static string GetEnumMemberAttrValue(Type enumType, object enumVal)
        {
            var memInfo = enumType.GetMember(enumVal.ToString());
            var attr = memInfo[0].GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            if(attr != null)
            {
                return attr.Value;
            }

            return null;
        }

        /// <summary>
        /// Get Random Enumeration
        /// </summary>
        /// <param name="random"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T NextEnum<T>(this Random random)
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}