using Notification.EmailService;
using Notification.Abstractions;

namespace Example.PropertyInjection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var controller = new UserController
            {
                NotificationService = new EmailNotificationService()
            };

            controller.Register("Charles");
        }
    }
}
