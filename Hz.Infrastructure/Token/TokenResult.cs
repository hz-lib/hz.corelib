namespace Hz.Infrastructure.Token
{
    public class TokenResult
    {
        public string access_token { get; set; }
        /// <summary>
        /// Token有效期（秒为单位）
        /// </summary>
        /// <value></value>
        public long expires_in { get; set; }
    }
}