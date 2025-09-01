using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace _1_BusinessLayer.Concrete.Tools.Workers
{
    public class QueueConnection : BackgroundService
    {
        public IConnection connection;
        public IChannel _receiverChannel;
        public IChannel _senderChannel;
        public readonly string _defaultExchangeName = "notification_exchange";
        public readonly string _mailRoutingKey = "mail_routingKey";
        public readonly string _notificationRoutingKey = "notification_routingKey";
        public readonly string _mailQueueName = "mail_queue";
        public readonly string _notificationQueueName = "notification_queue";
        public AsyncDefaultBasicConsumer mailConsumer;
        public AsyncDefaultBasicConsumer notificationConsumer;

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqps://davdav:davdav@localhost:5672"),
                ClientProvidedName = "Rabbit Queue App"
            };
            connection = await factory.CreateConnectionAsync();
            _senderChannel = await connection.CreateChannelAsync();
            _receiverChannel = await connection.CreateChannelAsync();


            await _senderChannel.ExchangeDeclareAsync(_defaultExchangeName, ExchangeType.Direct, durable: true, autoDelete: false);
            await _senderChannel.QueueDeclareAsync(_mailQueueName, durable: true, exclusive: false, autoDelete: false);
            await _senderChannel.QueueDeclareAsync(_notificationQueueName, durable: true, exclusive: false, autoDelete: false);
            await _senderChannel.QueueBindAsync(_mailQueueName, _defaultExchangeName, _mailRoutingKey);
            await _senderChannel.QueueBindAsync(_notificationQueueName, _defaultExchangeName, _notificationRoutingKey);

            await _receiverChannel.ExchangeDeclareAsync(_defaultExchangeName, ExchangeType.Direct, durable: true, autoDelete: false);
            await _receiverChannel.QueueDeclareAsync(_notificationQueueName, durable: true, exclusive: false, autoDelete: false);
            await _receiverChannel.QueueDeclareAsync(_mailQueueName, durable: true, exclusive: false, autoDelete: false);
            await _receiverChannel.QueueBindAsync(_notificationQueueName, _defaultExchangeName, _notificationRoutingKey);
            await _receiverChannel.QueueBindAsync(_mailQueueName, _defaultExchangeName, _mailRoutingKey);


        }
    }
}
