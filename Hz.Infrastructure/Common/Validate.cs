using System.Text.RegularExpressions;

namespace Hz.Infrastructure.Common
{
    public static class Validate
    {
        /// <summary>
        /// 判断输入的字符串是否是一个合法的手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string input)
        {
            Regex regex = new Regex("^1[34578]\\d{9}$");
            return regex.IsMatch(input);
        }
    }
}