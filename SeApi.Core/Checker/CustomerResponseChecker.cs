using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeApi.Core.Attribute;
using SeApi.Core.Cache;

namespace SeApi.Core.Checker
{
    public class CustomerResponseChecker
    {
        private static readonly Type attrType = typeof(CustomerResponseAttribute);

        public static bool IsCustomerResponse<T>(T t)
        {
            var type = t.GetType();
            return AttributeCache.Has(type, attrType);
        }
    }
}
