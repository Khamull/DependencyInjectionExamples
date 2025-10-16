using Notification.EmailService;

namespace Example.MethodInjection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var controller = new UserController();
            controller.Register("Charles", new EmailNotificationService());
        }
    }
}
