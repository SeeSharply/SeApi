using System;
using System.Collections.Generic;
using SeApi.Common.Extensions;
using SeApi.Core.Cache;

namespace SeApi.Core.Base
{
    public abstract class ApiPageMethodHandler<TResult, TParam> : ApiBaseMethodHandler<TResult, TParam> where TResult : PageResponse, new() where TParam : PageRequest
    {
        public abstract string DoPrepare(TParam request);

        protected int TotalResults = 0;

        protected string Sql = "";

        public virtual int GetItemCount( TParam request)
        {
            Func<int> func = () =>
            {
                int sum = DoSum(request);
                if (sum < 0)
                {
                    var querySql = DoPrepare(request);
                    string countSql = "select count(*) from (" + querySql + ") __a";
                    return 0;
                    //ÕâÀïÖ´ÐÐsql
                }
                else
                {
                    return sum;
                }
            };

            var data = PageResultCache.GetData(request.ProfileId, request.EtypeId, Sql, func);
            return data.Result;
        }

        public IList<K> GetItemList<K>(TParam request) where K : class, new()
        {
            var items = GetItemList(dbHelper, request);
            if (!items.IsNull() && items.Count != 0)
            {
                return items.ToEntities<K>();
            }
            else
            {
                return null;
            }
        }

        public IHashObjectList GetItemList(TParam request)
        {
            var querySql = DoPrepare(dbHelper, request);
            Sql = querySql;
            querySql += string.Format(" limit {0},{1}", (request.PageNo - 1) * request.PageSize, request.PageSize);
            IHashObjectList list = null;
            list = dbHelper.Select(querySql);
            TotalResults = GetItemCount(dbHelper, request);
            return list;
        }

        public virtual int DoSum(TParam request)
        {
            return -1;
        }

        public override TResult OnAfter(TResult result)
        {
            result.TotalResults = TotalResults;
            return result;
        }
    }
}