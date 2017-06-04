using SeApi.Core.Base;
using SeApi.Core.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeApi.Core.Cache
{
    public class PropertyCache
    {
        protected static readonly ConcurrentDictionary<Type, PropertyInfo[]> cached = new ConcurrentDictionary<Type, PropertyInfo[]>();

        public static PropertyInfo[] GetData(Type type)
        {
            Func<Type, PropertyInfo[]> func = (x) =>
            {
                return x.GetProperties();
            };

            return PropertyCache.cached.GetOrAdd(type, func);
        }
    }

    public class AttributeCache
    {
        protected static readonly ConcurrentDictionary<MemberInfo, object[]> cached = new ConcurrentDictionary<MemberInfo, object[]>();

        public static object[] GetData(MemberInfo memberInfo)
        {
            Func<MemberInfo, object[]> func = (x) =>
            {
                return x.GetCustomAttributes(true);
            };

            return AttributeCache.cached.GetOrAdd(memberInfo, func);
        }

        public static bool Has(MemberInfo memberInfo, Type attrType)
        {
            var objs = GetData(memberInfo);
            foreach (var obj in objs)
            {
                if (obj.GetType() == attrType)
                {
                    return true;
                }
            }
            return false;
        }

        public static T GetAttribute<T>(MemberInfo memberInfo, Type attrType) where T : System.Attribute
        {
            var objs = GetData(memberInfo);
            foreach (var obj in objs)
            {
                if (obj.GetType() == attrType)
                {
                    return obj as T;
                }
            }
            return null;
        }
    }

    public class PageResultCache
    {
        protected static readonly ConcurrentDictionary<string, TotalPageItem> cached = new ConcurrentDictionary<string, TotalPageItem>();


        public static TotalPageItem GetData(ulong profileId, ulong etypeId, string sql, Func<int> newFunc)
        {
            Func<string, TotalPageItem> func = (x) =>
            {
                int result = newFunc();
                return new TotalPageItem() { Result = result };
            };

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(profileId);
            stringBuilder.Append(etypeId);
            stringBuilder.Append(sql);
            var key = SeApi.Common.SecurityHelper.MD5Encrypt(stringBuilder.ToString());
            var data = PageResultCache.cached.GetOrAdd(key, func);
            if (DateTime.Now > data.ExpireTime)
            {
                var newData = func(key);
                cached.TryUpdate(key, newData, data);
                return newData;
            }
            else
            {
                return data;
            }
        }
    }

 
    public class TotalPageItem
    {
        public int Result { get; set; }

        private DateTime expireTime = DateTime.Now.AddMinutes(1);

        public DateTime ExpireTime
        {
            get
            {
                return expireTime;
            }

            set
            {
                expireTime = value;
            }
        }
    }
}
