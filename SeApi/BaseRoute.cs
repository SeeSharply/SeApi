/* created by livemore 2017-06-04
 * 实现IHttpHandler以便实现自己的http请求实现
 * 通过自定义的http处理实现很多东西
 */
using SeApi.Common;
using SeApi.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace SeApi
{
    public class BaseRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new BaseHttpHandler();
        }
    }
    public class BaseHttpHandler:IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }
        public void ProcessRequest(HttpContext context)
        {
            ProcessCore(context, null);
        }

        public void ProcessCore(HttpContext context, IDictionary<string, string> otherParams)
        {
            var request = context.Request;
            var response = context.Response;
            var method = request.HttpMethod.ToLower();
            var result = string.Empty;

            ApiRequestData requestData = new ApiRequestData
            {
                InvokeType = InvokeType.api,
                ClientIp = Utils.GetClientIP(request),
                ServerIp = Utils.GetServerIP(request),
                Url = request.Url.ToString(),
                RequestId = Guid.NewGuid().ToString(),
            };

            Stopwatch stopwatch = Stopwatch.StartNew();
            switch (method)
            {
                case "get":
                    result = "这里是get";
                    requestData.GetRequestString =
                        requestData.RequestString = Utils.CollectionToDict(request.QueryString);
                    if (otherParams != null)
                    {
                        foreach (var key in otherParams.Keys)
                        {
                            if (!requestData.RequestString.ContainsKey(key))
                            {
                                requestData.RequestString.Add(key, otherParams[key]);
                            }
                        }
                    }
                    result = Core.Provider.Executor.Invoke(requestData);
                    //*/
                    break;
                case "post":
                    result = "这里是post";
                    requestData.GetRequestString = Utils.CollectionToDict(request.QueryString);
                    if (request.Form.Count != 0)
                    {
                        requestData.RequestString = Utils.CollectionToDict(request.Form);
                        if (otherParams != null)
                        {
                            foreach (var key in otherParams.Keys)
                            {
                                if (!requestData.RequestString.ContainsKey(key))
                                {
                                    requestData.RequestString.Add(key, otherParams[key]);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (StreamReader reader = new StreamReader(request.InputStream, Encoding.UTF8))
                        {
                            var content = reader.ReadToEnd();
                            if (!string.IsNullOrEmpty(content))
                            {
                                requestData.RequestString = Utils.BuildParamters(content);
                                if (otherParams != null)
                                {
                                    foreach (var key in otherParams.Keys)
                                    {
                                        if (!requestData.RequestString.ContainsKey(key))
                                        {
                                            requestData.RequestString.Add(key, otherParams[key]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    result = Core.Provider.Executor.Invoke(requestData, true);
                   //*/
                    break;
                default:
                    result = "请使用get或者post谓词";
                    break;
            }

            stopwatch.Stop();
            var processTime = stopwatch.Elapsed;
            response.ContentType = "application/json";
            response.Write(result);
            response.End();
        }
    }
}