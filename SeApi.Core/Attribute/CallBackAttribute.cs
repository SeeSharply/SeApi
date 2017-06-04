using System;

namespace SeApi.Core.Attribute
{
    /// <summary>
    /// 供外部回调特性,不进行参数校验,方法内部自己校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CallBackAttribute : System.Attribute
    {
    }
}
