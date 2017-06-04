using System;
using SeApi.Core.Attribute;
using SeApi.Core.Cache;

namespace SeApi.Core.Checker
{
    public class CallBackChecker
    {
        private static readonly Type attrType = typeof(CallBackAttribute);

        public static bool IsCallBack<T>(T t)
        {
            var type = t.GetType();
            return AttributeCache.Has(type, attrType);
        }

        public static bool IsCallBack(Type type)
        {
            return AttributeCache.Has(type, attrType);
        }
    }
}
