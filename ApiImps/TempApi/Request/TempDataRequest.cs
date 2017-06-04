using SeApi.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempApi.Request
{
    public class TempDataRequest : ApiRequest
    {
        public string Name { get; set; }

        public TempData data { get; set; }
    }

    public class TempData:BaseObject
    {
        public int id { get; set; }

        public string title { get; set; }
    }
}
