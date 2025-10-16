using Notification.Abstractions;

namespace Example.MethodInjection
{
    public class UserController
    {
        public void Register(string user, INotificationService notificationService)
        {
            Console.WriteLine($"User {user} registered!");
            notificationService.Send($"Welcome {user}, from Method Injection!");
        }
    }
}
