using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events;
using _2_DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;

namespace _1_BusinessLayer.Concrete.Tools.Workers
{
    public class QueueSender : QueueConnection
    {
        public async Task NotificationQueueSendAsync(List<NotificationEvent> notificationEvents)
        {

            foreach (var notificationEvent in notificationEvents)
            {
                var body = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(notificationEvent));
                var basicProperties = new BasicProperties(); // Explicitly create basic properties
                basicProperties.Expiration = (10 * 60 * 1000).ToString(); // Message expiration time in milliseconds (10 minutes)
                basicProperties.DeliveryMode = (DeliveryModes)2; // Make message persistent
                await _senderChannel.BasicPublishAsync(_defaultExchangeName, _notificationRoutingKey, false, basicProperties, body);
            }
        }

        public async Task MailQueueSendAsync(List<MailEvent> mailEvents)
        {

            foreach (var mailEvent in mailEvents)
            {
                var body = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(mailEvent));
                var basicProperties = new BasicProperties(); // Explicitly create basic properties
                basicProperties.Expiration = (10 * 60 * 1000).ToString(); // Message expiration time in milliseconds (10 minutes)
                basicProperties.DeliveryMode = (DeliveryModes)2; // Make message persistent
                await _senderChannel.BasicPublishAsync(_defaultExchangeName, _mailRoutingKey, false, basicProperties, body);
            }
        }

    }
}
