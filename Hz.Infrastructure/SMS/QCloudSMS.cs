using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using qcloudsms_csharp;
using qcloudsms_csharp.httpclient;
using qcloudsms_csharp.json;

namespace Hz.Infrastructure.SMS {
    public class QCloudSMS : ISMS {
        private readonly int _appId = 0;
        private readonly string _appKey = "";
        public QCloudSMS (string appId, string appKey) {
            _appId = int.Parse (appId);
            _appKey = appKey;
        }
        public bool SendMessage (string phone, object templateId,string sign, params string[] args) {
            // 验证手机号码
            if (!Common.Validate.IsMobilePhone (phone))
                return false;
            try {
                SmsSingleSender sender = new SmsSingleSender (_appId, _appKey);

                var result = sender.sendWithParam ("86", phone, Convert.ToInt32(templateId), args, sign, "", "");

                return true;

            } catch (Exception e) {
                throw e;
            }
        }

        public bool SendMultiMessage (string[] phones, object templateId,string sign, params string[] args) {
            var listPhones = phones.Distinct ().ToList ();
            foreach (var item in listPhones) {
                if (!Common.Validate.IsMobilePhone (item)) {
                    listPhones.Remove (item);
                }
            }
            if (listPhones.Count < 1) return false;
            try {

                SmsMultiSender sender = new SmsMultiSender (_appId, _appKey);

                var result = sender.sendWithParam ("86", listPhones.ToArray (), (int) templateId, args, sign, "", "");

                return true;

            } catch (Exception e) {
                throw e;
            }
        }
    }
}