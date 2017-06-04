using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;
using SeApi.Common.Extensions;
using SeApi.Core.Base;
using SeApi.Common.ResponseCode;
using SeApi.Core.Model;

namespace SeApi.Core.Provider
{
    public class PluginProvider : ApiProvider
    {

        public PluginProvider(ApiMethod apimethod) : base(apimethod)
        {
        }

        public override string Invoke(ApiRequestData requestData)
        {
            var method = requestData.RequestString[Constants.METHOD];
            if (this.apimethod.Name.EqualsIgnoreCase(method))
            {
                var apiType = this.apimethod.Type;
                var invokeMethod = apiType.GetMethod("InvokeRequest");
                return invokeMethod.Invoke(Activator.CreateInstance(apiType), new[] { requestData }).ToString();
            }
            else
            {
                return ApiResult.CreateErrorResult(requestData.RequestId, ResponseType.Miss_Method, method);
            }
        }
    }
}
