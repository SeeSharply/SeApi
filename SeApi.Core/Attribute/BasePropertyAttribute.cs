using System;
using SeApi.Common.ResponseCode;

namespace SeApi.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BasePropertyAttribute : System.Attribute
    {
        public abstract ResponseType Type { get; }

        public abstract bool IsError(object val);

    }
}
