namespace SeApi.Common.Exceptions
{
    [System.Serializable]
    public class AuthException : System.Exception
    {
        public AuthException(string message) : base(message)
        {
        }

    }
}
