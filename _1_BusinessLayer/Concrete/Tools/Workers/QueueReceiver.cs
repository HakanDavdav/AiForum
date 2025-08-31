using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace _1_BusinessLayer.Concrete.Tools.Workers
{
    public  class QueueReceiver : QueueConnection
    {
        public QueueReceiver()
        {
            _receiverChannel.ExchangeDeclareAsync(_defaultExchangeName, ExchangeType.Direct, durable: true, autoDelete: false).Wait();
            _receiverChannel.QueueDeclareAsync(_notificationQueueName, durable: true, exclusive: false, autoDelete: false).Wait();
            _receiverChannel.QueueDeclareAsync(_mailQueueName, durable: true, exclusive: false, autoDelete: false).Wait();
            _receiverChannel.ExchangeBindAsync(_notificationQueueName, _defaultExchangeName, _notificationRoutingKey).Wait();
            _receiverChannel.ExchangeBindAsync(_mailQueueName, _defaultExchangeName, _mailRoutingKey).Wait();
            notificationConsumer = new AsyncEventingBasicConsumer(_receiverChannel);
            mailConsumer = new AsyncEventingBasicConsumer(_receiverChannel);
            _receiverChannel.BasicConsumeAsync(_mailQueueName, false, mailConsumer).Wait();
            _receiverChannel.BasicConsumeAsync(_notificationQueueName, false, notificationConsumer).Wait();

        }


    }
}
