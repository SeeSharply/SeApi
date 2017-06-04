using SeApi.Core.Attribute;
using Newtonsoft.Json;

namespace SeApi.Core.Base
{
    public class PageRequest : ApiRequest
    {
        private int pageNo = 1;


        [Required]
        public int PageNo
        {
            get
            {
                return pageNo;
            }

            set
            {
                pageNo = value;
            }
        }

        private int pageSize = 1;

        [Required]
        public int PageSize
        {
            get
            {
                return pageSize;
            }

            set
            {
                pageSize = value;
            }
        }
    }
}
