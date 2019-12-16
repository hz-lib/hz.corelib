using System;
using System.Threading;
using System.Threading.Tasks;
namespace Hz.Infrastructure.MQ {
    public interface IMQ {
        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="type">转发类型，fanout,direct,topic</param>
        /// <param name="routingKey">路由</param>
        /// <typeparam name="T"></typeparam>
        void Publish<T> (T msg, string exchangeName, string type = "direct", string routingKey = "");
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="cancelToken">退出消费线程token</param>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="action">接收到消息后的动作</param>
        /// <param name="type">转发类型，fanout,direct,topic</param>
        /// <param name="routingKey">队列绑定到交换机的路由值，一个队列可以绑定多个值（topic：xx.#  xx.*）,#：0或多字符，*：一个字符</param>
        /// <typeparam name="T"></typeparam>
        void Receive<T> (CancellationToken cancelToken, string exchangeName, string queueName, Action<T> action, string type = "direct", string routingKey = "");
    }
}