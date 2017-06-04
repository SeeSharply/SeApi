using System;
using System.ComponentModel;

namespace SeApi.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDesc(this Enum @enum)
        {
            var field = @enum.GetType().GetField(@enum.ToString());
            DescriptionAttribute descAttr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return descAttr == null ? "" : descAttr.Description;
        }
    }
}
