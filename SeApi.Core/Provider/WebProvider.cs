using System;
using System.Collections.Generic;
using System.Net;
using SeApi.Common;
using SeApi.Core.Base;
using SeApi.Common.ResponseCode;
using SeApi.Core.Model;

namespace SeApi.Core.Provider
{
    public class WebProvider : ApiProvider
    {
        public WebProvider(ApiMethod apimethod, bool isPost) : base(apimethod)
        {
            this.IsPost = isPost;
        }

        public bool IsPost { get; set; }

        public override string Invoke(ApiRequestData requestData)
        {
            WebUtils web = new WebUtils();
            var url = apimethod.Urls[0];
            url += ConvertToWebUrl(requestData.RequestString[Constants.METHOD]);
            IDictionary<string, string> dictionary = requestData.RequestString;
            dictionary.Add(Constants.INVOKETYPE, ((int)this.apimethod.InvokeType).ToString());
            dictionary.Add(Constants.REQUESTID, requestData.RequestId.ToString());

            try
            {
                if (IsPost)
                {
                    url = WebUtils.BuildRequestUrl(url, requestData.GetRequestString);
                    return web.DoPost(url, dictionary);
                }
                else
                {
                    return web.DoGet(url, dictionary);
                }
            }
            catch (WebException we)
            {
                HttpWebResponse errorResponse = we.Response as HttpWebResponse;

                if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    return ApiResult.CreateErrorResult(requestData.RequestId, ResponseType.Miss_Method, requestData.RequestString[Constants.METHOD]);
                }
                else
                {
                    return ApiResult.CreateErrorResult(requestData.RequestId, ResponseType.RouterError,((int)errorResponse.StatusCode) + " " + requestData.RequestString[Constants.METHOD]);
                }

            }
            catch (Exception ex)
            {
                return ApiResult.CreateErrorResult(requestData.RequestId, ResponseType.Error, ex.Message);
            }

        }

        public static string ConvertToWebUrl(string method)
        {
            return "api/" + method.ToLower().Replace(".", @"/");
        }
    }
}
