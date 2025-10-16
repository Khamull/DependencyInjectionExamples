using Notification.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.ConstructorInjection
{
    public class UserController
    {
        private readonly INotificationService _notificationService;

        public UserController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void Register(string user)
        {
            Console.WriteLine($"User {user} registered!");
            _notificationService.Send($"Welcome {user}, from Constructor Injection!");
        }
    }
}
