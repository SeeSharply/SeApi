using SeApi.Core.Model;

namespace SeApi.Core.Provider
{
    public abstract class ApiProvider
    {
        protected ApiMethod apimethod = null;

        public ApiProvider(ApiMethod apimethod)
        {
            this.apimethod = apimethod;
        }
        public abstract string Invoke(ApiRequestData requestData);
    }
}
