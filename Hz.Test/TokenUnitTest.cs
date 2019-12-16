using System;
using System.Threading.Tasks;
using Hz.Infrastructure.Token;
using Xunit;

namespace Hz.Test
{
    public class TokenUnitTest
    {
        private readonly IToken _token;

        public TokenUnitTest()
        {
            _token = new BaiduToken(options => {
                options.AppKey = "";
                options.SecretKey = "";
            });
        }
        [Fact]
        public async void GetTokenTest()
        {
            var tokenResult = await  _token.GetTokenAsync();

            var test2 = "";

            await Task.Run(async () => {
                test2 = await _token.GetTokenAsync();
            });

            Assert.True(tokenResult.Equals(test2));
        }
    }
}