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
                    //callback�ӿ�,�����в���У��,���д����ԵĽӿڷ������ü���������������ƽ̨�ص�

                    //����ص���Ҫ��������������д������
                }
                else
                {
                    //ϵͳ�������
                   // SystemParamsChecker.Check(requestString);

                    //�������������������������ĳЩ���ԵĽӿڼȲ��ü��sign��Ҳ���ü��token
                    //����AnonymousChecker�Ľӿڿ���������ʲ���Ҫtoken
                    //AnonymousChecker.Check(this);

                    //������ ǩ��
                    //ǩ����������Զ��壬��cheker�����޸ľ����ˣ��������Ҫ����ֱ�Ӳ���

                    //Checker.SignChecker.Check(requestString, signkey);


                    //�������token

                    //������token��֤֮��һ�㶼���Եõ��û�Ψһ��ʶ��������userid�����Ǵ��뵽���Ǳ�д��api�������棻
                    //��������дapi����Ա��ȫ���ù�����֤��һ��Ķ������ڱ�дapi��ʱ��ֱ���õ�userid�þ�����
                    //�����Ҫ�������ôֱ����������ͺ���

                    //requestString.Add(Constants.USERID, "");

                }
                    var request = ConverRequestObject(requestString);
                    //����������������ĺϷ��ԣ����磬����Ϊʵ����������ṩ��required��stringlength�����ԣ����ʧ�ܾͻ��׳��쳣
                    if (!CallBackChecker.IsCallBack(this))
                    {
                        RequestChecker.Check(request);
                    }
                    SeLog.Log.Fatal("123");

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
                return ApiResult.CreateErrorResult(requestData.RequestId, apiex.Type, apiex.Message);
            }
            catch (BusinessException businessex)
            {
                return ApiResult.CreateErrorResult(requestData.RequestId, ResponseType.Business_Error, businessex.Message);
            }
            catch (AuthException authex)
            {
                return ApiResult.CreateErrorResult(requestData.RequestId, ResponseType.Auth_Error, authex.Message);
            }
            catch (Exception ex)
            {
                return ApiResult.CreateErrorResult(requestData.RequestId, ResponseType.Error, ex.Message);
            }
            //ע����api�쳣�����¼��־����ʱû���ṩ
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
                    //ֵ����
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