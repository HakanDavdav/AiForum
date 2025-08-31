using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace _1_BusinessLayer.Concrete.Tools.Workers
{
    public class QueueConnection
    {
        public readonly IChannel _receiverChannel;
        public readonly IChannel _senderChannel;
        public readonly string _defaultExchangeName = "notification_exchange";
        public readonly string _mailRoutingKey = "mail_routingKey";
        public readonly string _notificationRoutingKey = "notification_routingKey";
        public readonly string _mailQueueName = "mail_queue";
        public readonly string _notificationQueueName = "notification_queue";
        public AsyncDefaultBasicConsumer mailConsumer;
        public AsyncDefaultBasicConsumer notificationConsumer;

        public QueueConnection()
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqps://davdav:davdav@localhost:5672"),
                ClientProvidedName = "Rabbit Queue App"
            };
            var connection = factory.CreateConnectionAsync.wa();
            _senderChannel = connection.CreateModel();
            _receiverChannel = connection.CreateModel();
        }
    }
}
