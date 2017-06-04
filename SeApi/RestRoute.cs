/* created by livemore 2017-06-04
 * 注册路由
 * 
 * 例如方法为method=se.temp.tempdata
 * 注册为api/se/temp/tempdata
 */ 
using SeApi.Core.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SeApi
{
    public class RestRoute
    {
        public static void Regist(RouteCollection routes)
        {
            var apimethods = ProviderFactory.GetApiMethods();

            foreach (var apiMethod in apimethods)
            {
                //if (Core.Checker.CallBackChecker.IsCallBack(apiMethod.Type))
                //{
                string method = apiMethod.Name.Replace(".", @"/");
                routes.Add(new Route("api/" + method, new RestRouteRouteHandler()));
                //}
            }
        }
    }

    public class RestRouteRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new RestRouteHttpHandler();
        }
    }

    public class RestRouteHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            var rest = new BaseHttpHandler();
            IDictionary<string, string> methodParams = new Dictionary<string, string>();
            var path = context.Request.Url.AbsolutePath;
            path = path.Substring(5, path.Length - 5);
            string method = path.Replace(@"/", ".");
            methodParams.Add("method", method);
            rest.ProcessCore(context, methodParams);
        }
    }
}