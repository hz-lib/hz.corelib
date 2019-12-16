using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Baidu.Aip.Ocr;
using Newtonsoft.Json.Linq;

namespace Hz.Infrastructure.Ocr {
    public class BaiduOcr : IOcr {
        private readonly OcrOptions _options = new OcrOptions ();
        private readonly Baidu.Aip.Ocr.Ocr _client = null;

        private readonly Dictionary<string, object> _baiduOptions = null;
        public BaiduOcr (Action<OcrOptions> action) {
            action (_options);
            _client = new Baidu.Aip.Ocr.Ocr (_options.ApiKey, _options.ScretKey);
            _client.Timeout = 1000 * 60;

            _baiduOptions = new Dictionary<string, object> () { { "language_type", "CHN_ENG" }, { "detect_language", "true" }
            };
        }

        public string GeneralBasic (string path) {
            var image = File.ReadAllBytes (path);
            // 带参数通用文字识别，图片为本地图片
            var result = _client.GeneralBasic (image, _baiduOptions);

            var strs = _dealResult (result);

            return strs.ToString ();
        }

        public string GeneralBasic (byte[] image) {
            var result = _client.GeneralBasic (image, _baiduOptions);

            var strs = _dealResult (result);

            return strs.ToString ();
        }

        private string _dealResult (JObject obj) {
            var words = obj.GetValue ("words_result");
            var sb = new StringBuilder ();
            foreach (var item in words) {
                sb.Append ($"{item.Value<string>("words")}\n");
            }

            return sb.ToString ();
        }

        public string GeneralBasicUrl (string url) {
            var result = _client.GeneralBasicUrl (url);
            var strs = _dealResult (result);

            return strs.ToString ();
        }
    }
}