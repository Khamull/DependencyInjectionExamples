using Example.MiddlewareInjection.Middleware;
using Notification.Abstractions;
using Notification.EmailService;

namespace Example.MiddlewareInjection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddAuthorization();
            // 🧩 Registro de dependência no container
            builder.Services.AddTransient<INotificationService, EmailNotificationService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // 🧱 Middleware com injeção de dependência automática
            app.UseMiddleware<LoggingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Endpoint para testar
            app.MapGet("/", (INotificationService service) =>
            {
                service.Send("Hello from Minimal API!");
                return "Check your console!";
            });

            app.Run();
        }
    }
}
