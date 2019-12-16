using System.Threading.Tasks;

namespace Hz.Infrastructure.Token
{
    public interface IToken
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        Task<string> GetTokenAsync();
    }
}