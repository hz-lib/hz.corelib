using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Hz.Infrastructure.DB
{
    public interface IUnitOfWork:IDisposable
    {
        void ExecuteInTrans(Action transAction);
        dynamic QueryFirst(string sql,object param = null);
        T QueryFirst<T>(string sql,object param = null);
        Task<dynamic> QueryFirstAsync(string sql,object param = null);
        Task<T> QueryFirstAsync<T>(string sql,object param = null);

        dynamic QueryFirstOrDefault(string sql,object param = null);
        T QueryFirstOrDefault<T>(string sql,object param = null);
        Task<dynamic> QueryFirstOrDefaultAsync(string sql,object param = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql,object param = null);

        dynamic QuerySingle(string sql,object param = null);
        T QuerySingle<T>(string sql,object param = null);
        Task<dynamic> QuerySingleAsync(string sql,object param = null);
        Task<T> QuerySingleAsync<T>(string sql,object param = null);

        dynamic QuerySingleOrDefault(string sql,object param = null);
        T QuerySingleOrDefault<T>(string sql,object param = null);
        Task<dynamic> QuerySingleOrDefaultAsync(string sql,object param = null);
        Task<T> QuerySingleOrDefaultAsync<T>(string sql,object param = null);

        IEnumerable<dynamic> Query(string sql,object param = null);
        IEnumerable<T> Query<T>(string sql,object param = null);
        Task<IEnumerable<dynamic>> QueryAsync(string sql,object param = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql,object param = null);

        int Execute(string sql,object param = null);
        Task<int> ExecuteAsync(string sql,object param = null);

        object ExecuteScalar(string sql,object param = null);
        T ExecuteScalar<T>(string sql,object param = null);
        Task<object> ExecuteScalarAsync(string sql,object param = null);
        Task<T> ExecuteScalarAsync<T>(string sql,object param = null);
    }
}