using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events;
using _1_BusinessLayer.Concrete.Tools.Senders;
using _2_DataAccessLayer.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace _1_BusinessLayer.Concrete.Tools.BackgroundServices.MessageBackgroundService
{
    public class NotificationConsumer : AsyncDefaultBasicConsumer
    {
        private readonly Func<MailEvent?, NotificationEvent?, Task> _handleMessage;

        public NotificationConsumer(IChannel channel, Func<MailEvent?, NotificationEvent?, Task> handleMessage)
            : base(channel)
        {
            _handleMessage = handleMessage;
        }

        public override async Task HandleBasicDeliverAsync(
            string consumerTag,
            ulong deliveryTag,
            bool redelivered,
            string exchange,
            string routingKey,
            IReadOnlyBasicProperties properties,
            ReadOnlyMemory<byte> body,
            CancellationToken cancellationToken = default)
        {
            var notification = Encoding.UTF8.GetString(body.Span);
            var notificationEvent = System.Text.Json.JsonSerializer.Deserialize<NotificationEvent>(notification);
            if (notificationEvent != null)
            {
                await _handleMessage(null, notificationEvent);
            }
            await Channel.BasicAckAsync(deliveryTag, false, cancellationToken);
        }
    }
    public class MailConsumer : AsyncDefaultBasicConsumer
    {
        private readonly Func<MailEvent?, NotificationEvent?, Task> _handleMail;

        public MailConsumer(IChannel channel, Func<MailEvent?, NotificationEvent?, Task> handleMail)
            : base(channel)
        {
            _handleMail = handleMail;
        }

        public override async Task HandleBasicDeliverAsync(
            string consumerTag,
            ulong deliveryTag,
            bool redelivered,
            string exchange,
            string routingKey,
            IReadOnlyBasicProperties properties,
            ReadOnlyMemory<byte> body,
            CancellationToken cancellationToken = default)
        {
            var mail = Encoding.UTF8.GetString(body.Span);
            var mailEvent = System.Text.Json.JsonSerializer.Deserialize<MailEvent>(mail);
            if (mailEvent != null)
            {
                await _handleMail(mailEvent, null);
            }
            await Channel.BasicAckAsync(deliveryTag, false, cancellationToken);
        }
    }

    public class QueueReceiver : QueueConnection
    {
        GeneralSender _generalSender;
        public QueueReceiver(GeneralSender generalSender)
        {
            _generalSender = generalSender;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await base.ExecuteAsync(stoppingToken);
            await StartReceivingMailAsync(_generalSender.GeneralSocialSend);
            await StartReceivingNotificationAsync(_generalSender.GeneralSocialSend);
            await Task.Delay(Timeout.Infinite, stoppingToken);

        }

        public async Task StartReceivingMailAsync(Func<MailEvent?, NotificationEvent?, Task> handleMail)
        {
            mailConsumer = new MailConsumer(_receiverChannel, handleMail);
            await _receiverChannel.BasicQosAsync(0, 1, false);
            await _receiverChannel.BasicConsumeAsync(_notificationQueueName, false, notificationConsumer);
        }

        public async Task StartReceivingNotificationAsync(Func<MailEvent?, NotificationEvent?, Task> handleNotification)
        {
            notificationConsumer = new NotificationConsumer(_receiverChannel, handleNotification);
            await _receiverChannel.BasicQosAsync(0, 1, false);
            await _receiverChannel.BasicConsumeAsync(_notificationQueueName, false, notificationConsumer);

        }
    }
}
