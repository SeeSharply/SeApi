using System;
using SeApi.Common.ResponseCode;

namespace SeApi.Core.Attribute
{
    public class TimeInfoAttribute : BasePropertyAttribute
    {
        public TimeInfoAttribute()
        {

        }

        public override ResponseType Type
        {
            get
            {
                return ResponseType.Time_Error;
            }
        }

        public override bool IsError(object val)
        {
            if (val == null) return true;
            DateTime thisDateTime = new DateTime();
            if (DateTime.TryParse(val.ToString(), out thisDateTime))
                return false;
            else
                return true;
        }
    }
}
