using System;
using System.Threading.Tasks;

namespace Hz.Infrastructure.Cache
{
    public interface ICache
    {
        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
         Task<bool> SetAsync(string key,object value);
         /// <summary>
         /// 新增缓存
         /// </summary>
         /// <param name="key"></param>
         /// <param name="value"></param>
         /// <param name="timeSpan">缓存持续时间（过期时间）</param>
         /// <returns></returns>
         Task<bool> SetAsync(string key, object value, TimeSpan timeSpan);
         /// <summary>
         /// 缓存是否存在
         /// </summary>
         /// <param name="key"></param>
         /// <returns></returns>
         Task<bool> ExistsAsync(string key);
         /// <summary>
         /// 获取缓存内容
         /// </summary>
         /// <param name="key"></param>
         /// <typeparam name="T"></typeparam>
         /// <returns></returns>
         Task<T> GetAsync<T>(string key);
         /// <summary>
         /// 删除缓存
         /// </summary>
         /// <param name="key"></param>
         /// <returns></returns>
         Task<bool> RemoveAsync(string key);
    }
}