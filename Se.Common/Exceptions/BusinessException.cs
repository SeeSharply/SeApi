namespace SeApi.Common.Exceptions
{
    [System.Serializable]
    public class BusinessException : System.Exception
    {
        public BusinessException(string message) : base(message)
        {
        }

    }
}
