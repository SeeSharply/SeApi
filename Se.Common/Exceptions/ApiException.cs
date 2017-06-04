using SeApi.Common.ResponseCode;

namespace SeApi.Common.Exceptions
{
    [System.Serializable]
    public class ApiException : System.Exception
    {
        public ResponseType Type { get; set; }
        public string Message { get; set; }

        public ApiException(ResponseType responseType, string msg)
        {
            this.Type = responseType;
            this.Message = msg;
        }
    }
}
