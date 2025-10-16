using Notification.Abstractions;

namespace Example.DICoreWithoutMiddleware
{
    public class UserProcessor
    {
        private readonly INotificationService _notificationService;

        public UserProcessor(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void Process(string username)
        {
            Console.WriteLine($"Processing user {username}...");
            _notificationService.Send($"<<->> {username} processed successfully!");
        }
    }
}
