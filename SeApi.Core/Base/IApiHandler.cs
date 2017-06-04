namespace SeApi.Core.Base
{
    public interface IApiHandler<TResult, TParam> : IApi
    {
        TResult Invoke(TParam request);
    }
}
