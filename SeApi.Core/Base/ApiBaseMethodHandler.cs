using System;
using System.Collections.Generic;
using System.Linq;
using SeApi.Common.Exceptions;
using SeApi.Common.Extensions;
using SeApi.Common.ResponseCode;
using SeApi.Core.Cache;
using SeApi.Core.Checker;
using SeApi.Core.Provider;
using Newtonsoft.Json;
using SeApi.Core.Model;
using SeApi.Common;
namespace SeApi.Core.Base
{
    public abstract class ApiBaseMethodHandler<TResult, TParam> : IApiHandler<TResult, TParam> where TResult : ApiResponse, new() where TParam : ApiRequest
    {
        public abstract TResult Invoke(TParam request);

        public string InvokeRequest(ApiRequestData requestData)
        {
            try
            {
                var requestString = requestData.RequestString;

                if (CallBackChecker.IsCallBack(this))
                {
                    //callback接口,不进行参数校验,带有此特性的接口方法不用检测参数，用于其他平台回调

                    //如果回调需要做其他操作，请写在这里
                }
                else
                {
                    //系统参数检查
                   // SystemParamsChecker.Check(requestString);

                    //在这里可以做其他操作，比如某些特性的接口既不用检测sign，也不用检测token
                    //带有AnonymousChecker的接口可以任意访问不需要token
                    //AnonymousChecker.Check(this);

                    //这里检测 签名
                    //签名规则可以自定义，在cheker里面修改就行了，如果不需要，就直接不做

                    //Checker.SignChecker.Check(requestString, signkey);


                    //这里检验token

                    //做完了token验证之后，一般都可以得到用户唯一标识，这里用userid；我们传入到我们编写的api方法里面；
                    //这样，编写api的人员完全不用关心验证这一块的东西，在编写api的时候直接拿到userid用就行了
                    //如果需要多个，那么直接在这儿做就好了

                    //requestString.Add(Constants.USERID, "");

                }
                    var request = ConverRequestObject(requestString);
                    //这里用作检验参数的合法性，例如，我们为实体类的属性提供了required，stringlength等特性；检测失败就会抛出异常
                    if (!CallBackChecker.IsCallBack(this))
                    {
                        RequestChecker.Check(request);
                    }
                    log4net.Ext.IExtLog log = log4net.Ext.ExtLogManager.GetLogger("filelog");
                    var response = Invoke(request);

                    if (!response.IsNull())
                    {
                        response = OnAfter(response);
                    }

                    if (CustomerResponseChecker.IsCustomerResponse(this))
                    {
                        return response.ToJson();
                    }
                    else
                    {
                        ApiResult result = new ApiResult()
                        {
                            Response = response,
                            RequestId = requestData.RequestId,
                        };
                        return result.ToJson();
                    }
            }
            catch (ApiException apiex)
            {
                //可以在异常的时候多记录一些东西
                SeLog.Log.Error("apiex Error:",apiex);
                return ApiResult.CreateErrorResult(requestData.RequestId, apiex.Type, apiex.Message);
            }
            catch (BusinessException businessex)
            {
                //可以在异常的时候多记录一些东西
                SeLog.Log.Error("businessex Error:", businessex);
                return ApiResult.CreateErrorResult(requestData.RequestId, ResponseType.Business_Error, businessex.Message);
            }
            catch (Exception ex)
            {
                //可以在异常的时候多记录一些东西
                SeLog.Log.Error("ex Error:", ex);
                return ApiResult.CreateErrorResult(requestData.RequestId, ResponseType.Error, ex.Message);
            }
        }

        private TParam ConverRequestObject(IDictionary<string, string> requestString)
        {
            var targetType = typeof(TParam);

            var instance = Activator.CreateInstance(targetType);
            var properties = PropertyCache.GetData(targetType);

            foreach (var propertyInfo in properties)
            {
                var key = requestString.Keys.FirstOrDefault(k => string.Equals(k, propertyInfo.Name, StringComparison.OrdinalIgnoreCase));
                if (key != null)
                {
                    //值类型
                    if (propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType == typeof(string))
                    {
                        IConvertible convertible;
                        if (propertyInfo.PropertyType.IsEnum)
                        {
                            var value = Enum.Parse(propertyInfo.PropertyType, requestString[key]);
                            convertible = value as IConvertible;
                        }
                        else
                        {
                            var value = requestString[key];
                            convertible = value as IConvertible;
                        }
                        if (convertible != null && typeof(IConvertible).IsAssignableFrom(propertyInfo.PropertyType))
                        {
                            var targetValue = convertible.ToType(propertyInfo.PropertyType, null);
                            propertyInfo.SetValue(instance, targetValue, null);
                            continue;
                        }
                    }
                    else
                    {
                        var targetValue = JsonConvert.DeserializeObject(requestString[key], propertyInfo.PropertyType);
                        propertyInfo.SetValue(instance, targetValue, null);
                        continue;
                    }
                }
            }

            return (TParam)instance;
        }

        public virtual TResult OnAfter(TResult result)
        {
            return result;
        }
    }

}