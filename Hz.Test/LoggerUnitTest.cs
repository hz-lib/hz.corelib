using Hz.Infrastructure.Logger;
using Xunit;

namespace Hz.Test
{
    public class LoggerUnitTest
    {
        private readonly ILogger _logger;
        public LoggerUnitTest()
        {
            _logger = new Nlogger("testProject");
        }

        [Fact]
        public void TestInfo()
        {
            _logger.Info("info类型日志记录");
            Assert.True(true);
        }

        [Fact]
        public void TestError()
        {
            _logger.Error("error日志测试");
            Assert.True(true);
        }
    }
}