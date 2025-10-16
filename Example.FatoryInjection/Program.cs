using Example.FatoryInjection.Factorys;
using Example.FatoryInjection.Interfaces;
using Notification.Abstractions;
using Notification.EmailService;
using Notification.SMSService;

namespace Example.FatoryInjection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddAuthorization();
            // Serviços de notificação
            //builder.Services.AddTransient<EmailNotificationService>();
            //builder.Services.AddTransient<SMSNotificationService>();

            builder.Services.AddKeyedTransient<INotificationService, EmailNotificationService>("email");
            builder.Services.AddKeyedTransient<INotificationService, SMSNotificationService>("sms");

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            
            // Factory
            builder.Services.AddSingleton<INotificationFactory, NotificationFactory>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost("/notify", (string name, string type, INotificationFactory factory) =>
            {
                var service = factory.Create(type);
                service.Send($"{name} notified via {type}!");
                return Results.Ok($"Notification sent to {name} via {type}");
            });

            app.Run();
        }
    }
}
