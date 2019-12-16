using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hz.Infrastructure.Common {
    public class Build {
        #region 随机码
        /// <summary>
        /// 生成4为随机数嘛
        /// </summary>
        /// <returns></returns>
        public static string SMSCode4 () {
            Random r = new Random ();
            string code = r.Next (1000, 9999).ToString ();

            return code;
        }

        /// <summary>
        /// 随机数种子
        /// </summary>
        /// <value></value>
        private static char[] baseChars = {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'a',
            'b',
            'c',
            'd',
            'e',
            'f',
            'g',
            'h',
            'i',
            'j',
            'k',
            'l',
            'm',
            'n',
            'o',
            'p',
            'q',
            'r',
            's',
            't',
            'u',
            'v',
            'w',
            'x',
            'y',
            'z',
            'A',
            'B',
            'C',
            'D',
            'E',
            'F',
            'G',
            'H',
            'I',
            'J',
            'K',
            'L',
            'M',
            'N',
            'O',
            'P',
            'Q',
            'R',
            'S',
            'T',
            'U',
            'V',
            'W',
            'X',
            'Y',
            'Z'
        };

        /// <summary>
        /// 生成指定位数的随机字符（字符+数字）
        /// </summary>
        /// <param name="length">随机数位数</param>
        /// <returns></returns>
        public static string RandomString (int length) {
            StringBuilder sb = new StringBuilder ();
            Random rd = new Random ();
            for (int i = 0; i < length; i++) {
                sb.Append (baseChars[rd.Next (62)]);
            }

            return sb.ToString ();
        }
        #endregion
        public static string GetIP()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
              .Select(p => p.GetIPProperties())
              .SelectMany(p => p.UnicastAddresses)
              .Where(p => p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !System.Net.IPAddress.IsLoopback(p.Address))
              .FirstOrDefault()?.Address.ToString();
        }
    }
}
