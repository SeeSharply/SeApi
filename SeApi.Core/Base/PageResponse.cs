namespace SeApi.Core.Base
{
    public class PageResponse : ApiResponse
    {
        //public bool HasNext { get; set; }
        public string Msg { get; set; }
        public int TotalResults { get; set; }
    }
}
