using System;
using Hz.Infrastructure.Common;
using Xunit;

namespace Hz.Test {
    public class PinyinTest {
        [Fact]
        public void TestHanziToPinyin () {
            var result = Pinyin.GetInitials ("测试一下As01");
            Assert.True ("CSYXAs01".Equals (result));
        }
    }
}