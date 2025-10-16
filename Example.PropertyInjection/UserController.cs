using Notification.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.PropertyInjection
{
    public class UserController
    {
        public INotificationService? NotificationService { get; set; }

        public void Register(string user)
        {
            Console.WriteLine($"User {user} registered!");
            NotificationService?.Send($"Welcome {user}, from Property Injection!");
        }
    }
}
