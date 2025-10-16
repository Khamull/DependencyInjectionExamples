
using Microsoft.AspNetCore.Mvc;
using Notification.Abstractions;
using Notification.EmailService;

namespace Example.DICoreWithoutMiddleware
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            // Registramos nossa dependência no container
            builder.Services.AddTransient<INotificationService, EmailNotificationService>();
            builder.Services.AddTransient<UserProcessor>();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // 👇 Aqui estamos resolvendo a dependência diretamente do container
            var _processor = app.Services.GetRequiredService<UserProcessor>();

            app.MapPost("/notify", ([FromBody] string name, UserProcessor processor) =>
            {
                processor.Process(name);
                return Results.Ok($"Notification sent to {name}");
            });

            app.Run();
        }
    }
}
