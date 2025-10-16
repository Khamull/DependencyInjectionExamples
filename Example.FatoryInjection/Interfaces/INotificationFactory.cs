using Notification.Abstractions;

namespace Example.FatoryInjection.Interfaces
{
    public interface INotificationFactory
    {
        INotificationService Create(string type);
    }
}
