using SeApi.Common.Extensions;
using SeApi.Common.ResponseCode;

namespace SeApi.Core.Attribute
{
    public class StringLengthAttribute : BasePropertyAttribute
    {
        public override ResponseType Type
        {
            get { return ResponseType.StringLength; }
        }

        public int MaxLength { get; set; }
        public int MinLength { get; set; }


        public StringLengthAttribute(int maxLength)
        {
            this.MaxLength = maxLength;
        }

        public StringLengthAttribute() : this(9999)
        {

        }

        public override bool IsError(object val)
        {
            if (val.IsNull() && MinLength > 0)
            {
                return true;
            }
            var str = val.ToString();
            if (str.Length < MinLength)
            {
                return true;
            }
            if (str.Length > MaxLength)
            {
                return true;
            }
            return false;
        }
    }
}
