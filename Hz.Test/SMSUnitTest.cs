using System;
using Microsoft.Extensions.Configuration;
using Xunit;

using Hz.Infrastructure.SMS;
using Hz.Infrastructure.Common;

namespace Hz.Test
{
    public class SMSUnitTest
    {
        private readonly ISMS sms = null;

        public SMSUnitTest()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("config.json");
            sms = new QCloudSMS("","");
        }

        [Fact]
        public void TestQCloudSMSSendMsg()
        {
            var phone = "13757034962";
            var code = Build.SMSCode4();
            var result = sms.SendMessage(phone,"295172","林小兵中医诊所", code);
            // 295174
            Assert.True(result);
        }
    }
}