using Example.FatoryInjection.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Notification.Abstractions;
using Notification.EmailService;
using Notification.SMSService;
using Microsoft.Extensions.DependencyInjection;

namespace Example.FatoryInjection.Factorys
{
    public class NotificationFactory : INotificationFactory
    {
        private readonly IServiceProvider _provider;

        public NotificationFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public INotificationService Create(string type)
        {
            return type.ToLower() switch
            {
                
                "email" => _provider.GetRequiredKeyedService<INotificationService>("email"),
                "sms" => _provider.GetRequiredKeyedService<INotificationService>("sms"),
                _ => throw new ArgumentException("Unknown type")
            };
        }
    }
}
