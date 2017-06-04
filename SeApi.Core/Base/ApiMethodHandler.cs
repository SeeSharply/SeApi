namespace SeApi.Core.Base
{
    public abstract class ApiMethodHandler<TResult, TParam> :
        ApiBaseMethodHandler<TResult, TParam> where TResult : ApiResponse, new() where TParam : ApiRequest
    {

    }
}
