using System.ComponentModel;

namespace SeApi.Common.ResponseCode
{
    public enum ResponseType
    {
        /// <summary>
        /// 未找到对应用户
        /// </summary>
        [Description("未找到对应用户")]
        User_Invalid = 1000,
        /// <summary>
        /// 回调地址错误
        /// </summary>
        [Description("回调地址错误")]
        RedirectUrl_Error = 1001,
        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error = 999,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Suceessed = 0,
        /// <summary>
        /// 缺少方法
        /// </summary>
        [Description("缺少方法:")]
        Miss_Method = 1,
        /// <summary>
        /// 参数是必须的
        /// </summary>
        [Description("参数是必须的:")]
        Required = 2,
        /// <summary>
        /// 路由错误
        /// </summary>
        [Description("路由错误:")]
        RouterError = 5,
        /// <summary>
        /// 字符串长度错误
        /// </summary>
        [Description("字符串长度错误:")]
        StringLength = 6,
        /// <summary>
        /// 缺少sign
        /// </summary>
        [Description("缺少sign")]
        Miss_Sign = 7,
        /// <summary>
        /// 签名错误
        /// </summary>
        [Description("签名错误")]
        Sign_Error = 10,
        /// <summary>
        /// ip不在允许访问名单内
        /// </summary>
        [Description("ip不在允许访问名单内")]
        Ip_Error = 11,
        /// <summary>
        /// 业务错误
        /// </summary>
        [Description("业务错误:")]
        Business_Error = 12,
        /// <summary>
        /// 错误的token
        /// </summary>
        [Description("错误的token")]
        Token_Error = 13,
        /// <summary>
        /// 时间格式错误
        /// </summary>
        [Description("时间格式错误")]
        Time_Error = 14,
        /// <summary>
        /// 授权错误
        /// </summary>
        [Description("授权错误")]
        Auth_Error = 15,
        /// <summary>
        /// 请求已过期
        /// </summary>
        [Description("请求已过期")]
        Request_Expires = 16,
        /// <summary>
        /// 时间戳错误
        /// </summary>
        [Description("时间戳错误")]
        Timestamp_Error = 17,
        /// <summary>
        /// 字段格式错误
        /// </summary>
        [Description("字段格式错误")]
        DataType_Error = 18,
    }
}
