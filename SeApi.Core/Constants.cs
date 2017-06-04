namespace SeApi.Core
{
    public sealed class Constants
    {
        public const string REQUESTID = "requestid";

        public const string INVOKETYPE = "invoketype";

        //public const string PREFIX = "beefun.";

        public const string CHARSET_UTF8 = "utf-8";

        public const string CONTENT_ENCODING_GZIP = "gzip";

        public const string PARAMETER_NAME_SIGN = "sign";

        public const string API = "api";

        public const string APP_KEY = "appkey";

        public const string TIMESTAMP = "timestamp";

        public const string USERID = "userid";


        public const string TOKEN = "token";

        public const string AUTHTOKEN = "auth.token";

        public const string LOGIN = "auth.login";

        public const string AUTHTOKENVERIFY = "auth.tokenverify";

        public const string GETSIGNKEY = "auth.getsignkey";

        public const string METHOD = "method";

        public static string PREFIX
        {
            get
            {
                var configPrefix = System.Configuration.ConfigurationManager.AppSettings["prefix"];
                if (string.IsNullOrEmpty(configPrefix))
                {
                    return "se.";
                }
                else
                {
                    return configPrefix + ".";
                }
            }
        }
    }
}
