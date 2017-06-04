using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeApi.Common.Exceptions;
using SeApi.Common.ResponseCode;
using SeApi.Core.Attribute;
using SeApi.Core.Cache;

namespace SeApi.Core.Checker
{
    public class AnonymousChecker
    {
        private static readonly Type attrType = typeof(AllowAnonymousAttribute);

        public static void Check<T>(T t)
        {
            var type = t.GetType();
            if (AttributeCache.Has(type, attrType))
            {
                return;
            }
            else
            {
                throw new ApiException(ResponseType.Required, Constants.TOKEN);
            }
        }
    }
}
