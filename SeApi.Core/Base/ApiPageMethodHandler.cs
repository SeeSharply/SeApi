using System;
using System.Collections.Generic;
using SeApi.Common.Extensions;
using SeApi.Core.Cache;

namespace SeApi.Core.Base
{
    public abstract class ApiPageMethodHandler<TResult, TParam> : ApiBaseMethodHandler<TResult, TParam>
        where TResult : PageResponse, new()
        where TParam : PageRequest
    {

    }
}