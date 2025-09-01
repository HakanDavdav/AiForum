using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using _1_BusinessLayer.Concrete.Events;
using _1_BusinessLayer.Concrete.Tools.Managers.UserToolManagers;
using _2_DataAccessLayer.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace _1_BusinessLayer.Concrete.Tools.Workers
{
    public class NotificationConsumer : AsyncDefaultBasicConsumer
    {
        private readonly Func<NotificationEvent, Task> _handleMessage;

        public NotificationConsumer(IChannel channel, Func<NotificationEvent, Task> handleMessage)
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
                await _handleMessage(notificationEvent);
            }
            await Channel.BasicAckAsync(deliveryTag, false, cancellationToken);
        }
    }
    public class MailConsumer : AsyncDefaultBasicConsumer
    {
        private readonly Func<MailEvent, Task> _handleMail;

        public MailConsumer(IChannel channel, Func<MailEvent, Task> handleMail)
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
                await _handleMail(mailEvent);
            }
            await Channel.BasicAckAsync(deliveryTag, false, cancellationToken);
        }
    }

    public class QueueReceiver : QueueConnection
    {
        ActivityBaseManager _activityBaseManager;
        AbstractUserRepository _userRepository;
        AbstractBotRepository _botRepository;
        AbstractEntryRepository _entryRepository;
        AbstractPostRepository _postRepository;
        public QueueReceiver(ActivityBaseManager activityBaseManager,AbstractUserRepository userRepository, 
            AbstractBotRepository botRepository, AbstractEntryRepository entryRepository, AbstractPostRepository postRepository)
        {
            _activityBaseManager = activityBaseManager;
            _userRepository = userRepository;
            _botRepository = botRepository;
            _entryRepository = entryRepository;
            _postRepository = postRepository;

        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await base.ExecuteAsync(stoppingToken);
            await StartReceivingNotificationAsync(handleNotifications);

            await Task.Delay(Timeout.Infinite, stoppingToken);

        }

        public async Task StartReceivingMailAsync(Func<MailEvent, Task> handleMail)
        {
            mailConsumer = new MailConsumer(_receiverChannel, handleMail);
        }

        public async Task StartReceivingNotificationAsync(Func<NotificationEvent, Task> handleNotification)
        {
            notificationConsumer = new NotificationConsumer(_receiverChannel, handleNotification);
            await _receiverChannel.BasicQosAsync(0, 1, false);
            await _receiverChannel.BasicConsumeAsync(_notificationQueueName, false, notificationConsumer);

        }

        public async Task handleNotifications(NotificationEvent notificationEvent)
        {
            string additionalInfo = string.Empty;
            var fromUser = await _userRepository.GetByIdAsync(notificationEvent.SenderUserId);
            var fromBot = await _botRepository.GetByIdAsync(notificationEvent.SenderBotId);
            var toUser = await _userRepository.GetByIdAsync(notificationEvent.ReceiverUserId);
            await _activityBaseManager.CreateNotificationAsync(fromUser, fromBot, toUser, notificationEvent.Type, notificationEvent.AdditionalInfo, notificationEvent.AdditionalId);

        }

        public async Task handleMails(MailEvent mailEvent)
        {
            // Implement mail handling logic here
        }

    }
}
