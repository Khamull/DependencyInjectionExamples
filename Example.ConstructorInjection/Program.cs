using Notification.EmailService;
using Notification.Abstractions;

namespace Example.ConstructorInjection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            INotificationService service = new EmailNotificationService();
            var controller = new UserController(service);

            controller.Register("Charles");
        }
    }
}
