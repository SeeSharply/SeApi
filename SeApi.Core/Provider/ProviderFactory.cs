using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SeApi.Common.Extensions;
using SeApi.Core.Base;
using SeApi.Core.Model;
using SeApi.Core.Attribute;

namespace SeApi.Core.Provider
{
    public class ApiMethod
    {
        public string Name { get; set; }
        public InvokeType InvokeType { get; set; }
        public IList<string> Urls { get; set; }
        public Type Type { get; set; }
        /// <summary>
        /// get post put delete
        /// </summary>
        public string HttpType { get; set; }
    }

    public class ProviderFactory
    {
        private static readonly ConcurrentDictionary<string, ApiMethod> apiMethods = new ConcurrentDictionary<string, ApiMethod>();

        static ProviderFactory()
        {
            //配置优先 
            SetByConfig();

            //dll 再进行
            SetByDll();
        }

        private static void SetByDll()
        {
            var assemblies = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "Apis")).GetFiles("*api.dll").Select(r => Assembly.LoadFrom(r.FullName));
            var postAttr = typeof(SePostAttribute);
            var getAttr = typeof(SeGetAttribute);
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    var interfaces = type.GetInterfaces();
                    if (interfaces.Any(@interface => @interface == typeof(IApi)))
                    {
                        var typeStr = type.ToString();
                        var array = typeStr.Split('.');
                        var methodname = array[0] + "." + array[array.Length - 1];
                        var attrs = type.GetCustomAttributes(true) ;
                        var httpType = "post";//默认post
                        //var postAttr = typeof(SePostAttribute);
                        //var postAttr = typeof(SePostAttribute);
                        
                        foreach (var item in attrs)
                        {
                            var p = item.GetType();
                            if (p==postAttr)
                            {
                                httpType = "post";
                            }
                            if (p == getAttr)
                            {
                                httpType = "get";
                            }
                            //其他类型请求
                            //
                        }
                       
                        string method = Constants.PREFIX + methodname.ToLower().Replace("api", "");
                        var apimethod = new ApiMethod()
                        {
                            Name = method,
                            InvokeType = InvokeType.dll,
                            Type = type,
                            HttpType = httpType
                        };
                        apiMethods.TryAdd(method, apimethod);
                    }
                }
            }
        }

        private static void SetByConfig()
        {
            try
            {
                string txt = "";
                using (StreamReader reader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "Apis", "apiconfig.json")))
                {
                    txt = reader.ReadToEnd();
                }

                IList<ApiMethod> list = txt.ToObject<List<ApiMethod>>();
                foreach (var item in list)
                {
                    item.InvokeType = item.Name.Split('.').Count() == 2 ? InvokeType.@class : InvokeType.method;
                    apiMethods.TryAdd(item.Name.ToLower(), item);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static ApiProvider Create(string method, bool isPost)
        {
            if (string.IsNullOrEmpty(method))
            {
                return null;
            }
            method = method.ToLower();
            if (apiMethods.ContainsKey(method))
            {
                var apimethod = apiMethods[method];
                string httpType=isPost?"post":"get";
                if (apimethod.HttpType!=httpType)
                {
                    throw new Exception(string.Format("method【{1}】请使用【{0}】谓词", apimethod.HttpType, method));
                }
                return GetProvider(apimethod, isPost);
            }
            foreach (var apimethod in apiMethods)
            {
                if (method.StartsWith(apimethod.Key))
                {
                    string httpType = isPost ? "post" : "get";
                    if (apimethod.Value.HttpType != httpType)
                    {
                        throw new Exception(string.Format("method【{1}】请使用【{0}】谓词", apimethod.Value.HttpType,method));
                    }
                    return GetProvider(apimethod.Value, isPost);
                }
            }

            return null;
        }

        private static ApiProvider GetProvider(ApiMethod apimethod, bool isPost)
        {
            if (apimethod.InvokeType == InvokeType.dll)
            {
                return new PluginProvider(apimethod) { };
            }
            if (apimethod.InvokeType == InvokeType.@class || apimethod.InvokeType == InvokeType.method)
            {
                return new WebProvider(apimethod, isPost) { };
            }
            return null;
        }

        public static IList<ApiMethod> GetApiMethods()
        {
            return apiMethods.Values.ToList();
        }
    }
}
