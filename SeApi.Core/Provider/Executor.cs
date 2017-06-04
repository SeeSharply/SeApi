using System;
using SeApi.Core.Base;
using SeApi.Common.ResponseCode;
using SeApi.Core.Model;

namespace SeApi.Core.Provider
{
    public class Executor
    {
        public static string Invoke(ApiRequestData requestData)
        {
            return Invoke(requestData, false);
        }

        public static string Invoke(ApiRequestData requestData, bool isPost)
        {
            try
            {
                string method = "";
                if (requestData.RequestString.TryGetValue(Constants.METHOD, out method))
                {
                    var provider = ProviderFactory.Create(method, isPost);
                    if (provider == null)
                    {
                        return ApiResult.CreateErrorResult(requestData.RequestId, Common.ResponseCode.ResponseType.Miss_Method, method);
                    }
                    return provider.Invoke(requestData);
                }
                else
                {
                    return ApiResult.CreateErrorResult(requestData.RequestId, Common.ResponseCode.ResponseType.Required, Constants.METHOD);
                }
            }

            catch (Exception ex)
            {
                return ApiResult.CreateErrorResult(requestData.RequestId, ResponseType.Error, ex.Message);
            }
        }
    }
}
