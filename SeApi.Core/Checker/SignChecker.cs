using System;
using System.Collections.Generic;
using SeApi.Common.Exceptions;
using SeApi.Common.ResponseCode;

namespace SeApi.Core.Checker
{
    public class SignChecker
    {
        public static void Check(IDictionary<string, string> dict, string signKey)
        {
            var requestSign = "";
            if (dict.ContainsKey("sign"))
            {
                requestSign = dict["sign"];
                dict.Remove("sign");
            }
            else
            {
                throw new ApiException(ResponseType.Miss_Sign, "sign");
            }

            var sign = Common.Utils.SignTopRequest(dict, signKey);
            if (!requestSign.Equals(sign, StringComparison.OrdinalIgnoreCase))
            {
                throw new ApiException(ResponseType.Sign_Error, "sign");
            }
        }
    }
}
