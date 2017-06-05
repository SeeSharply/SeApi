using SeApi.Core.Attribute;
using SeApi.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempApi.Request;
using TempApi.Response;

namespace TempApi.Handler
{
    [SeGet]
    public class TempData : ApiMethodHandler<TempResponse, TempDataRequest>
    {
        public override TempResponse Invoke(TempDataRequest request)
        {
            var res = new TempResponse();
            var blog = new Blog();
            blog.title = request.Name;
            blog.id = 1;
            res.blog = blog;
            res.data = "这里是 data";
            return res;
        }
    }
}


