using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Hz.Infrastructure.MQ {
    public class RabbitMQ : IMQ {
        public const string Fanout = "fanout";
        public const string Direct = "direct";
        public const string Topic = "topic";

        public static ConnectionFactory testFac = new ConnectionFactory ();
        private readonly IConnectionFactory _connectionFactory;

        private List<IConnection> _connections;
        private List<IModel> _models;

        public RabbitMQ (MQConfig config) {
            _connectionFactory = new ConnectionFactory {
                HostName = config.HostName,
                Port = config.Port,
                UserName = config.UserName,
                Password = config.Password,
                VirtualHost = config.VirtualHost
            };

            _connections = new List<IConnection> ();
            _models = new List<IModel> ();
        }

        public void Publish<T> (T msg, string exchangeName, string type, string routingKey = "") {
            using (IConnection con = _connectionFactory.CreateConnection ()) {
                using (IModel channel = con.CreateModel ()) {
                    channel.ExchangeDeclare (exchangeName, type : type, durable : true);
                    var strMsg = Newtonsoft.Json.JsonConvert.SerializeObject (msg);
                    var body = Encoding.UTF8.GetBytes (strMsg);

                    channel.BasicPublish (exchangeName, routingKey, null, body);
                }

            }

        }

        public void Receive<T> (CancellationToken cancelToken, string exchangeName, string queueName, Action<T> action, string type, string routingKey = "") {
            using (IConnection con = _connectionFactory.CreateConnection ()) {
                using (IModel channel = con.CreateModel ()) {
                    channel.ExchangeDeclare (exchangeName, type : type, durable : true);
                    channel.QueueDeclare (queueName, durable : true, exclusive : false, autoDelete : false, arguments : null);
                    channel.QueueBind (queueName, exchangeName, routingKey : routingKey);

                    channel.BasicQos (0, 1, false);

                    var consumer = new EventingBasicConsumer (channel);

                    consumer.Received += (model, ea) => {
                        var resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<T> (Encoding.UTF8.GetString (ea.Body));
                        try {
                            action (resModel);
                            // 确认消息已经被消费
                            channel.BasicAck (ea.DeliveryTag, false);
                        } catch (Exception) {
                            // 重新返回队列
                            channel.BasicNack (ea.DeliveryTag, false, true);
                        }
                    };
                    // 启动消费者，设置为手动确认消费
                    channel.BasicConsume (queueName, autoAck : false, consumer : consumer);
                    while (true) {
                        if (cancelToken.IsCancellationRequested) break;
                    }
                }
            }
        }

        public void Receive<T> (string exchangeName, string type, string queueName, Action<T> action, string routingKey = "") {
            IConnection _con = _connectionFactory.CreateConnection ();
            _connections.Add (_con);
            IModel _channel = _con.CreateModel ();
            _models.Add (_channel);
            _channel.ExchangeDeclare (exchangeName, type : type, durable : true);
            _channel.QueueDeclare (queueName, durable : true, exclusive : false, autoDelete : false, arguments : null);
            _channel.QueueBind (queueName, exchangeName, routingKey : routingKey);

            _channel.BasicQos (0, 1, false);

            var consumer = new EventingBasicConsumer (_channel);

            consumer.Received += (model, ea) => {
                var resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<T> (Encoding.UTF8.GetString (ea.Body));
                try {
                    action (resModel);
                    // 确认消息已经被消费
                    _channel.BasicAck (ea.DeliveryTag, false);
                } catch (Exception) {
                    // 重新返回队列
                    _channel.BasicNack (ea.DeliveryTag, false, true);
                }
            };
            // 启动消费者，设置为手动确认消费
            _channel.BasicConsume (queueName, autoAck : false, consumer : consumer);
        }

        public void Dispose () {
            foreach (var model in _models) {
                model.Dispose ();
            }

            foreach (var con in _connections) {
                con.Dispose ();
            }
        }
    }
}