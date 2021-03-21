using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeaponMaker
{
    public static class Copier
    {
        /// <summary>
        /// Will copy all values from targetObject into originalObject.
        /// Useful when you want to assing an object to another and fire the 
        /// RaisePropertyChanged.
        /// </summary>
        public static void CopyProperties<T>(T originalObject, T targetObject, string[] ignoredProperties = null)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (ignoredProperties.Contains(property.Name))
                {
                    continue;
                }
                var value = property.GetValue(targetObject) ?? default;
                property.SetValue(originalObject, value);
            }
        }
    }
}
