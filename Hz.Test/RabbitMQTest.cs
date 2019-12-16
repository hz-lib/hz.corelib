using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hz.Infrastructure.MQ;
using Xunit;

namespace Hz.Test {
    public class RabbitMQTest {
        private readonly IMQ _mq;
        public RabbitMQTest () {
            _mq = new Hz.Infrastructure.MQ.RabbitMQ (new MQConfig {
                HostName = "192.168.0.50",
                    Port = 5672,
                    UserName = "pos",
                    Password = "cc324100",
                    VirtualHost = "pos_vhost"
            });
        }

        [Fact]
        public void PublishTest () {

            for (int i = 0; i < 10; i++) {
                Thread.Sleep (300);
                _mq.Publish (new TestModel {
                    testStr = $"testStr{i}",
                        testInt = i
                }, "exchangeFanout", Hz.Infrastructure.MQ.RabbitMQ.Fanout);
            }
            Assert.True (true);
        }

        [Fact]
        public void ReceiveTest () {
            List<TestModel> models = new List<TestModel> ();
            CancellationTokenSource source = new CancellationTokenSource ();
            Task.Run (() => {
                _mq.Receive<TestModel> (source.Token, "exchangeFanout", $"queueFanout{2}", model => {
                    models.Add (model);
                    Console.WriteLine (Newtonsoft.Json.JsonConvert.SerializeObject (model));
                }, Hz.Infrastructure.MQ.RabbitMQ.Fanout);
            });

            source.Cancel ();
            Assert.True (true);
        }
    }
    class TestModel {
        public string testStr { get; set; }
        public int testInt { get; set; }
        public DateTime testTime { get; set; } = DateTime.Now;
    }
}