using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Hz.Infrastructure.Token {
    public class BaiduToken : IToken {
        private TokenOptions _options = new TokenOptions ();
        private readonly string _memoryKey = "baidu_token";
        private readonly IMemoryCache _cache = null;

        public BaiduToken (Action<TokenOptions> opAction) {
            opAction (_options);
            _cache = new MemoryCache (Options.Create (new MemoryCacheOptions ()));
        }

        public async Task<string> GetTokenAsync () {
            var token = await _cache.GetOrCreateAsync (_memoryKey, async entry => {
                var tokenResult = await _RequestToken ();
                entry.SetSlidingExpiration (TimeSpan.FromSeconds (tokenResult.expires_in));
                return tokenResult.access_token;
            });

            return token;
        }

        private async Task<TokenResult> _RequestToken () {
            string authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient ();
            List<KeyValuePair<string, string>> paraList = new List<KeyValuePair<string, string>> () {
                new KeyValuePair<string, string> ("grant_type", "client_credentials"),
                new KeyValuePair<string, string> ("client_id", _options.AppKey),
                new KeyValuePair<string, string> ("client_secret", _options.SecretKey)
            };

            var response = await client.PostAsync (authHost, new FormUrlEncodedContent (paraList));

            var result = await response.Content.ReadAsStringAsync ();

            var tokenResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResult> (result);

            tokenResult.expires_in -= 2 * 24 * 60 * 60;
            return tokenResult;
        }
    }
}