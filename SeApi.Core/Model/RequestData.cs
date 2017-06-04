using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeApi.Core.Model
{
    public class ApiRequestData
    {
        private string id = Guid.NewGuid().ToString();// Cuid.NewCuid();

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public IDictionary<string, string> RequestString { get; set; }

        public IDictionary<string, string> GetRequestString { get; set; }

        private DateTime requestTime = DateTime.Now;

        public DateTime RequestTime
        {
            get
            {
                return requestTime;
            }
            set
            {
                requestTime = value;
            }
        }

        public string ClientIp { get; set; }

        public string ServerIp { get; set; }

        public string Url { get; set; }

        public long Elapsed { get; set; }

        public InvokeType InvokeType { get; set; }

        public string RequestId { get; set; }
    }

    public enum InvokeType
    {
        @api = 0,
        @method = 1,
        @class = 2,
        @dll = 3,
    }
}
