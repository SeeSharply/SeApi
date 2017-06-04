using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SeApi.Common.Extensions;
using SeApi.Common.ResponseCode;

namespace SeApi.Core.Base
{
    public class ApiResult
    {
        public string RequestId { get; set; }
        public bool IsError { get; set; }
        public ApiResponse Response { get; set; }


        static Dictionary<int, string> contentDesc = new Dictionary<int, string>();
        static ApiResult()
        {
            var type = typeof(ResponseType);
            var keys = Enum.GetValues(type);
            foreach (var key in keys)
            {
                var des = Enum.GetName(type, key);
                var field = type.GetField(des);
                DescriptionAttribute descAttr = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                contentDesc.Add(Convert.ToInt32(key), descAttr == null ? "" : descAttr.Description);
            }
        }
        public static string CreateErrorResult(string requestId, ResponseType type, string message = "")
        {
            var desc = contentDesc[(int)type];
            var result = new ApiErrorResult()
            {
                ErrorCode = (int)type,
                IsError = true,
                ErrorMessage = desc + " " + message,
                RequestId = requestId,
            };
            return result.ToJson();
        }
    }

    public class ApiErrorResult : ApiResult
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}
