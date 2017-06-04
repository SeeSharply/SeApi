using System;
using System.Collections.Generic;
using SeApi.Common.Exceptions;
using SeApi.Common.ResponseCode;

namespace SeApi.Core.Checker
{
    public class SystemParamsChecker
    {
        public static void Check(IDictionary<string, string> requestString)
        {
            var keys = requestString.Keys;
            //这里检测系统参数，比如appkey，sign，timestamp等等
            if (!keys.Contains(Constants.APP_KEY))
            {
                throw new ApiException(ResponseType.Required, Constants.APP_KEY);
            }
            if (!keys.Contains(Constants.PARAMETER_NAME_SIGN))
            {
                throw new ApiException(ResponseType.Required, Constants.PARAMETER_NAME_SIGN);
            }
            if (!keys.Contains(Constants.TIMESTAMP))
            {
                throw new ApiException(ResponseType.Required, Constants.TIMESTAMP);
            }


            var timestamp = requestString[Constants.TIMESTAMP];


            var result = DateTime.Now;
            if (DateTime.TryParse(timestamp, out result))
            {
                var min = DateTime.Now.AddMinutes(-5);
                var max = DateTime.Now.AddMinutes(5);
                if (result < min || result > max)
                {
#if DEBUG

#else
                    throw new ApiException(ResponseType.Request_Expires, "");
#endif
                }
            }
            else
            {
                throw new ApiException(ResponseType.Timestamp_Error, "");
            }
        }
    }
}
