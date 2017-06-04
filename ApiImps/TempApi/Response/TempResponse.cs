using SeApi.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempApi.Response
{
    public class TempResponse : ApiResponse
    {
        public string data { get; set; }
        public string Name { get; set; }

        public Blog blog { get; set; }
    }
    public class Blog:BaseObject
    {
        public int id { get; set; }
        public string title { get; set; }

        public string content { get; set; }
    }
}
