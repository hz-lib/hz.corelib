using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Hz.Infrastructure.Cache
{
    public class RedisCache: ICache
    {
        private readonly IConnectionMultiplexer _connection;
        private readonly IDatabase _cache;
        public RedisCache(string connectionString, int dbNumber)
        {
            _connection = ConnectionMultiplexer.Connect(connectionString);
            _cache = _connection.GetDatabase(dbNumber);
        }

        private string _serializeObject(object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
         public Task<bool> SetAsync(string key,object value)
         {
             return _cache.StringSetAsync(key,_serializeObject(value));
         }
         /// <summary>
         /// 新增缓存
         /// </summary>
         /// <param name="key"></param>
         /// <param name="value"></param>
         /// <param name="timeSpan">缓存持续时间（过期时间）</param>
         /// <returns></returns>
         public Task<bool> SetAsync(string key, object value, TimeSpan timeSpan)
         {
             return _cache.StringSetAsync(key,_serializeObject(value),timeSpan);
         }
         /// <summary>
         /// 缓存是否存在
         /// </summary>
         /// <param name="key"></param>
         /// <returns></returns>
         public Task<bool> ExistsAsync(string key)
         {
             return _cache.KeyExistsAsync(key);
         }
         /// <summary>
         /// 获取缓存内容
         /// </summary>
         /// <param name="key"></param>
         /// <typeparam name="T"></typeparam>
         /// <returns></returns>
         public async Task<T> GetAsync<T>(string key)
         {
             var strValue = await _cache.StringGetAsync(key);
             return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(strValue);
         }
         /// <summary>
         /// 删除缓存
         /// </summary>
         /// <param name="key"></param>
         /// <returns></returns>
         public Task<bool> RemoveAsync(string key)
         {
             return _cache.KeyDeleteAsync(key);
         }
    }
}