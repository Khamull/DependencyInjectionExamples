using Notification.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.SMSService
{
    public class SMSNotificationService : INotificationService
    {
        public void Send(string message) => Console.WriteLine($"[::] SMS: {message}");
    }
}
