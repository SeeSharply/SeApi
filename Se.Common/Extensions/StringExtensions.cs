using System;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace SeApi.Common.Extensions
{
    public static class StringExtensions
    {
        public static string MD5crypto(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return ByteArrayToHex(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str)));
        }

        public static string ByteArrayToHex(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i = (int)(i + 1))
            {
                builder.Append(((byte)bytes[i]).ToString("x2"));
            }
            return builder.ToString();
        }

        public static T ToObject<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static object ToObject(this string str, Type type)
        {
            return JsonConvert.DeserializeObject(str, type);
        }

        public static bool EqualsIgnoreCase(this string str, string str2)
        {
            return str.Equals(str2, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
