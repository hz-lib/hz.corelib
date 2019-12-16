using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Dapper;
using System.Threading.Tasks;

namespace Hz.Infrastructure.DB
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _dbConnection = null;
        private IDbTransaction _transaction = null;
        private bool _isCommit = false;
        private TCDbConfig _config = new TCDbConfig() {
            DbType = DbType.MySql
        };
        public UnitOfWork(Action<TCDbConfig> dbConfig)
        {
            dbConfig(_config);
            InitDbConnection();
        }

        private void InitDbConnection()
        {
            if (_dbConnection == null)
            {
                switch (_config.DbType)
                {
                    case DbType.MySql:
                        _dbConnection = new MySqlConnection(_config.ConnectString);
                        break;
                    case DbType.MsSql:
                        _dbConnection = new SqlConnection(_config.ConnectString);
                        break;
                    default:
                        throw new Exception("数据库类型未定义");
                }
            }
        }

        #region 数据库执行逻辑
        public dynamic QueryFirst(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QueryFirst(sql,param) : _dbConnection.QueryFirst(sql,param,_transaction);
        }
        public T QueryFirst<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QueryFirst<T>(sql,param) : _dbConnection.QueryFirst<T>(sql,param,_transaction);
        }
        public Task<dynamic> QueryFirstAsync(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QueryFirstAsync(sql,param) : _dbConnection.QueryFirstAsync(sql,param,_transaction);
        }
        public Task<T> QueryFirstAsync<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QueryFirstAsync<T>(sql,param) : _dbConnection.QueryFirstAsync<T>(sql,param,_transaction);
        }

        public dynamic QueryFirstOrDefault(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QueryFirstOrDefault(sql,param) : _dbConnection.QueryFirstOrDefault(sql,param,_transaction);
        }
        public T QueryFirstOrDefault<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QueryFirstOrDefault<T>(sql,param) : _dbConnection.QueryFirstOrDefault<T>(sql,param,_transaction);
        }
        public Task<dynamic> QueryFirstOrDefaultAsync(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QueryFirstOrDefaultAsync(sql,param) : _dbConnection.QueryFirstOrDefaultAsync(sql,param,_transaction);
        }
        public Task<T> QueryFirstOrDefaultAsync<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QueryFirstOrDefaultAsync<T>(sql,param) : _dbConnection.QueryFirstOrDefaultAsync<T>(sql,param,_transaction);
        }

        public dynamic QuerySingle(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QuerySingle(sql,param) : _dbConnection.QuerySingle(sql,param,_transaction);
        }
        public T QuerySingle<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QuerySingle<T>(sql,param) : _dbConnection.QuerySingle<T>(sql,param,_transaction);
        }
        public Task<dynamic> QuerySingleAsync(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QuerySingleAsync(sql,param) : _dbConnection.QuerySingleAsync(sql,param,_transaction);
        }
        public Task<T> QuerySingleAsync<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QuerySingleAsync<T>(sql,param) : _dbConnection.QuerySingleAsync<T>(sql,param,_transaction);
        }

        public dynamic QuerySingleOrDefault(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QuerySingleOrDefault(sql,param) : _dbConnection.QuerySingleOrDefault(sql,param,_transaction);
        }
        public T QuerySingleOrDefault<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QuerySingleOrDefault<T>(sql,param) : _dbConnection.QuerySingleOrDefault<T>(sql,param,_transaction);
        }
        public Task<dynamic> QuerySingleOrDefaultAsync(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QuerySingleOrDefaultAsync(sql,param) : _dbConnection.QuerySingleOrDefaultAsync(sql,param,_transaction);
        }
        public Task<T> QuerySingleOrDefaultAsync<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QuerySingleOrDefaultAsync<T>(sql,param) : _dbConnection.QuerySingleOrDefaultAsync<T>(sql,param,_transaction);
        }

        public IEnumerable<dynamic> Query(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.Query(sql,param) : _dbConnection.Query(sql,param,_transaction);
        }
        public IEnumerable<T> Query<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.Query<T>(sql,param) : _dbConnection.Query<T>(sql,param,_transaction);
        }
        public Task<IEnumerable<dynamic>> QueryAsync(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QueryAsync(sql,param) : _dbConnection.QueryAsync(sql,param,_transaction);
        }
        public Task<IEnumerable<T>> QueryAsync<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.QueryAsync<T>(sql,param) : _dbConnection.QueryAsync<T>(sql,param,_transaction);
        }

        public int Execute(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.Execute(sql,param) : _dbConnection.Execute(sql,param,_transaction);
        }
        public Task<int> ExecuteAsync(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.ExecuteAsync(sql,param) : _dbConnection.ExecuteAsync(sql,param,_transaction);
        }

        public object ExecuteScalar(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.ExecuteScalar(sql,param) : _dbConnection.ExecuteScalar(sql,param,_transaction);
        }
        public T ExecuteScalar<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.ExecuteScalar<T>(sql,param) : _dbConnection.ExecuteScalar<T>(sql,param,_transaction);
        }
        public Task<object> ExecuteScalarAsync(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.ExecuteScalarAsync(sql,param) : _dbConnection.ExecuteScalarAsync(sql,param,_transaction);
        }
        public Task<T> ExecuteScalarAsync<T>(string sql,object param = null)
        {
            return _transaction == null ? _dbConnection.ExecuteScalarAsync<T>(sql,param) : _dbConnection.ExecuteScalarAsync<T>(sql,param,_transaction);
        }
        #endregion

        #region 事务处理
        public void ExecuteInTrans(Action transAction)
        {
            try{
                BeginTrans();
                transAction();
                Commit();
            }
            catch (Exception ex)
            {
                RollBack();
                throw ex;
            }
            finally
            {
                Dispose();
            }
        }
        private void BeginTrans()
        {
            // InitDbConnection();
            if(_dbConnection.State==ConnectionState.Closed) _dbConnection.Open();
            _transaction = _dbConnection.BeginTransaction();
            _isCommit = false;
        }
        private void Commit()
        {
            if(_transaction!=null)
            {
                if(!_isCommit)
                    _transaction.Commit();
                _isCommit = true;
                _transaction=null;
            }
        }
        private void RollBack()
        {
            if (_transaction != null)
            {
                if (!_isCommit)
                    _transaction.Rollback();
                _isCommit = true;
                _transaction = null;
            }
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                if (_transaction != null)
                {
                    if (!_isCommit)
                        _transaction.Commit();
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_dbConnection != null)
                {
                    if (_dbConnection.State != ConnectionState.Closed)
                        _dbConnection.Close();
                    _dbConnection.Dispose();
                    _dbConnection = null;
                }
                _isCommit = true;
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~UnitOfWork() {
          // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
          Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}