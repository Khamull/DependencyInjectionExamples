using Notification.Abstractions;

namespace Example.MiddlewareInjection.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly INotificationService _notificationService;

        public LoggingMiddleware(RequestDelegate next, INotificationService notificationService)
        {
            _next = next;
            _notificationService = notificationService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _notificationService.Send($"[Middleware] Request to {context.Request.Path}");
            await _next(context);
        }
    }
}
