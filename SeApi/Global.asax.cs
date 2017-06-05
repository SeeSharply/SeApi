using log4net.Config;
using SeApi.Core.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SeApi
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

            RouteTable.Routes.Add(new Route("api", new BaseRouteHandler()));
            //RouteTable.Routes.Add(new Route("api/token", new TokenRouteHandler()));
            //RouteTable.Routes.Add(new Route("api/login", new LoginRouteHandler()));
            //注册restfull路由地址
            RestRoute.Regist(RouteTable.Routes);
            XmlConfigurator.Configure(); 
        }

       
    }
}