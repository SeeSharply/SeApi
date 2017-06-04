using System;
using System.Collections.Generic;
using System.Reflection;
using SeApi.Common.Exceptions;
using SeApi.Core.Attribute;
using SeApi.Core.Cache;

namespace SeApi.Core.Checker
{
    /// <summary>
    /// request参数检查,request参数属性进行判断
    /// </summary>
    public class RequestChecker
    {
        static IList<Type> propertyAttributes = new List<Type>();

        static RequestChecker()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(BasePropertyAttribute));
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.BaseType == typeof(BasePropertyAttribute))
                {
                    propertyAttributes.Add(type);
                }
            }

        }

        public static void Check<T>(T t)
        {
            var type = t.GetType();
            var properties = PropertyCache.GetData(type);
            foreach (var property in properties)
            {
                object obj = property.GetValue(t, null);
                foreach (var attributeType in propertyAttributes)
                {
                    CheckResult(attributeType, property, obj);
                }
            }
        }

        private static void CheckResult(Type attributeType, PropertyInfo property, object obj)
        {
            if (AttributeCache.Has(property, attributeType))
            {
                var @attribute = AttributeCache.GetAttribute<BasePropertyAttribute>(property, attributeType);
                if (@attribute.IsError(obj))
                {
                    throw new ApiException(@attribute.Type, property.Name);
                }
            }
        }
    }
}
