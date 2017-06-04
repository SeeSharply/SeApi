using System;
using SeApi.Common.ResponseCode;

namespace SeApi.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : BasePropertyAttribute
    {
        public RequiredAttribute()
        {

        }

        public override ResponseType Type
        {
            get
            {
                return ResponseType.Required;
            }
        }

        public override bool IsError(object val)
        {
            return val.Equals(null) || string.IsNullOrEmpty(val.ToString());
        }
    }
}
