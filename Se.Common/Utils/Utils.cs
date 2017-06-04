using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;


namespace SeApi.Common
{
    public class Utils
    {
        public static IDictionary<string, string> CollectionToDict(NameValueCollection queryString)
        {
            IDictionary<string, string> paramters = new Dictionary<string, string>();
            foreach (var key in queryString.AllKeys)
            {
                var lowerKey = key.ToLower();

                if (!paramters.Keys.Contains(lowerKey))
                {
                    paramters.Add(lowerKey, queryString[key]);
                }
            }
            return paramters;
        }

        public static IDictionary<string, string> BuildParamters(string queryString)
        {
            IDictionary<string, string> paramters = new Dictionary<string, string>();
            string[] paramValues = queryString.Split('&');
            string result = null;
            foreach (string param in paramValues)
            {
                string[] values = param.Split('=');
                if (!paramters.ContainsKey(values[0]))
                {
                    paramters.Add(values[0], values[1]);
                }
            }
            return paramters;
        }


        public static string GetClientIP(HttpRequest request)
        {
            string text = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(text))
            {
                text = request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                if (text.IndexOf(".") == -1)
                {
                    text = "";
                }
                else
                {
                    if (text.IndexOf(",") != -1)
                    {
                        text = text.Replace(" ", "").Replace("'", "");
                        string[] array = text.Split(",".ToCharArray());
                        text = array[array.Length - 1].Replace(";", "");
                    }
                }
            }
            if (string.IsNullOrEmpty(text))
            {
                text = request.UserHostAddress;
            }
            string pattern = "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$";
            string result;
            if (!string.IsNullOrEmpty(text) && Regex.IsMatch(text, pattern))
            {
                result = text;
            }
            else
            {
                result = "127.0.0.1";
            }
            return result;
        }

        public static string GetIP(HttpRequest request)
        {
            throw new NotImplementedException();
        }

        public static string GetServerIP(HttpRequest request)
        {
            try
            {
                if (request == null)
                {
                    return string.Empty;
                }
                return request.ServerVariables.Get("LOCAL_ADDR");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string SignTopRequest(IDictionary<string, string> parameters, string signKey)
        {
            return SignTopRequest(parameters, null, signKey);
        }

        public static string SignTopRequest(IDictionary<string, string> parameters, string body, string signKey)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder();

            foreach (KeyValuePair<string, string> kv in sortedParams)
            {
                if (!string.IsNullOrEmpty(kv.Key) && !string.IsNullOrEmpty(kv.Value))
                {
                    query.Append(kv.Key).Append(kv.Value);
                }
            }

            // 第三步：把请求主体拼接在参数后面
            if (!string.IsNullOrEmpty(body))
            {
                query.Append(body);
            }

            // 第四步：使用MD5
            byte[] bytes;

            query.Append(signKey);
            MD5 md5 = MD5.Create();
            bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));


            // 第五步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("X2"));
            }

            return result.ToString();
        }

        public static string SignSHA256(string content)
        {
            return SecurityHelper.SHA256Encrypt(content);
        }

        public static string SignSHA256(IDictionary<string, string> parameters)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder();

            foreach (KeyValuePair<string, string> kv in sortedParams)
            {
                if (!string.IsNullOrEmpty(kv.Key) && !string.IsNullOrEmpty(kv.Value))
                {
                    query.Append(kv.Key).Append(kv.Value);
                }
            }

            return SecurityHelper.SHA256Encrypt(query.ToString());
        }
    }
}
