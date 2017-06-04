using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SeApi.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull<T>(this T @object)
        {
            return Equals(@object, null);
        }

        public static string ToJson<T>(this T @object)
        {
            return JsonConvert.SerializeObject(@object, new JsonSerializerSettings
            {
                ContractResolver = new LowercaseContractResolver()
            });
        }

        //public static string ToJson<T>(this T @object)
        //{
        //    return JsonConvert.SerializeObject(@object, new JsonSerializerSettings
        //    {
        //        ContractResolver = new LowercaseContractResolver()
        //    });
        //}

        public class LowercaseContractResolver : DefaultContractResolver
        {
            protected override string ResolvePropertyName(string propertyName)
            {
                return propertyName.ToLower();
            }
        }
    }
}
