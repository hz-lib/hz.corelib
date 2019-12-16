using System;
using Hz.Infrastructure.Ocr;
using Xunit;

namespace Hz.Test
{
    public class OcrUnitTest
    {
        private readonly IOcr _ocrClient = null;

        public OcrUnitTest()
        {
            _ocrClient = new BaiduOcr(options => {
                options.ApiKey = "";
                options.ScretKey = "";
            });
        }

        [Fact]
        public void GeneralBasicTest()
        {
            var res = _ocrClient.GeneralBasic("/home/hzgod/work/hz/Hz.Lib/Hz.Test/res/testOcr.png");
            Assert.NotNull(res);
        }

        [Fact]
        public void GeneralBasicUrlTest()
        {
            var res = _ocrClient.GeneralBasicUrl("http://poyn27v3k.bkt.clouddn.com/yht/testOcr.png");

            Assert.NotNull(res);
        }
    }
}