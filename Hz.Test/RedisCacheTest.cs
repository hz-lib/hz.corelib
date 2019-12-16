using System;
using System.Threading.Tasks;
using Xunit;
using Hz.Infrastructure.Cache;

namespace Hz.Test
{
    public class RedisCacheTest
    {
        private readonly ICache _cache;
        public RedisCacheTest()
        {
            _cache = new RedisCache("192.168.0.24:6400,abortConnect=true,allowAdmin=true,connectRetry=3,password=cc324100", 3);
        }

        [Fact]
        public async void SetTest()
        {
            var res1 =await _cache.SetAsync("key001", "value0011");
            var res2 = await _cache.SetAsync("key002","value002",TimeSpan.FromMinutes(10));
            Assert.True(res1 && res2);
        }

        [Fact]
        public async void GetTest()
        {
            var res1 = await _cache.GetAsync<string>("key002");
            Assert.True("value002".Equals(res1));
        }

        [Fact]
        public async void ExistsTest()
        {
            var res1 = await _cache.ExistsAsync("key001");
            var res2 = await _cache.ExistsAsync("key003");

            Assert.True(res1 && !res2);
        }

        [Fact]
        public async void RemoveTest()
        {
            var res1 = await _cache.RemoveAsync("key002");
            Assert.True(res1);
        }
    }
}