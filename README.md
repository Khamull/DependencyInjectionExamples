# 🧠 Dependency Injection Examples in .NET 9

This repository contains a collection of **Dependency Injection (DI)** examples implemented in **C# (.NET 8)** — each example demonstrates a different technique and design principle within the ASP.NET Core ecosystem.

The goal is to illustrate how DI works at different levels: constructors, properties, methods, middleware and factories — following **SOLID** principles and common design patterns.

---

## 📂 Project Index

| Project | Description | Example Focus |
|---|---|---|
| `Example.ConstructorInjection` | Demonstrates classic **constructor-based DI** | Recommended approach for most scenarios |
| `Example.PropertyInjection` | Shows **property-based DI**, useful for optional dependencies | Post-construction injection |
| `Example.MethodInjection` | Uses **method parameters** to inject dependencies only when needed | Stateless, utility-style injection |
| `Example.MiddlewareInjection` | Example of DI applied in **custom middleware** | Pipeline-based injection using `_next(context)` |
| `Example.DICoreWithoutMiddleware` | Demonstrates **core DI container usage without HTTP pipeline** | Manual resolution using `app.Services` |
| `Example.FatoryInjection` | Combines DI with **Factory Pattern** and **Keyed Services** for dynamic implementation selection | Runtime flexibility using interfaces |

---

## 1. Constructor Injection

**Type:** Constructor

```csharp
public class NotificationController
{
    private readonly INotificationService _service;

    public NotificationController(INotificationService service)
    {
        _service = service;
    }

    public void Notify(string message) => _service.Send(message);
}
```

### Key Points
- The most common and recommended DI style.
- Dependency provided via constructor — immutable after creation.
- Promotes **Single Responsibility** and **Dependency Inversion (D in SOLID)**.

---

## 2. Property Injection

**Type:** Property

```csharp
public class NotificationManager
{
    public INotificationService? NotificationService { get; set; }

    public void Notify(string message) => NotificationService?.Send(message);
}
```

### Key Points
- Dependency assigned **after object creation**.
- Useful for **optional dependencies**.
- Requires care with null-safety (possibility of uninitialized property).

---

## 3. Method Injection

**Type:** Method

```csharp
public class NotificationUtility
{
    public void Notify(string message, INotificationService service)
    {
        service.Send(message);
    }
}
```

### Key Points
- Dependency passed **per method call**.
- Ideal for stateless or utility-style services.
- Does not store the dependency internally.

---

## 4. Middleware Injection

**Type:** Middleware

```csharp
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"Request: {context.Request.Path}");
        await _next(context);
    }
}
```

### Key Points
- Uses constructor injection inside middleware.
- Follows the **Chain of Responsibility** pattern.
- Ideal for cross-cutting concerns (logging, auth, caching).

---

## 5. Core DI Without Middleware

**Type:** Manual resolution via DI container

```csharp
var app = builder.Build();

using var scope = app.Services.CreateScope();
var processor = scope.ServiceProvider.GetRequiredService<UserProcessor>();

processor.Run();
```

### Key Points
- Demonstrates how to resolve dependencies outside the HTTP pipeline.
- Useful for background services, scripts, or console-style runners.
- Uses the built-in `IServiceProvider` from ASP.NET Core.

---

## 6. DI in Controller / Route

**Type:** Minimal API / Controller (constructor DI)

```csharp
app.MapPost("/notify", (UserProcessor processor) =>
{
    processor.Process("Hello from route!");
    return Results.Ok("Notification sent!");
});
```

### Key Points
- `UserProcessor` is injected automatically by the container.
- Fully integrated with the ASP.NET Core request lifecycle.
- Clean, declarative style that fits Minimal APIs.

---

## 7. Factory DI with Keyed Services

**Type:** Factory + DI + Keyed Services

```csharp
public interface INotificationFactory
{
    INotificationService Create(string type);
}

public class NotificationFactory : INotificationFactory
{
    private readonly IServiceProvider _provider;

    public NotificationFactory(IServiceProvider provider) => _provider = provider;

    public INotificationService Create(string type) => type switch
    {
        "sms" => _provider.GetRequiredKeyedService<INotificationService>("sms"),
        "email" => _provider.GetRequiredKeyedService<INotificationService>("email"),
        _ => throw new NotImplementedException()
    };
}
```

### Registering Keyed Services

```csharp
builder.Services.AddKeyedTransient<INotificationService, EmailNotificationService>("email");
builder.Services.AddKeyedTransient<INotificationService, SMSNotificationService>("sms");
builder.Services.AddSingleton<INotificationFactory, NotificationFactory>();
```

### Example Usage

```csharp
app.MapPost("/notify/{type}", (string type, INotificationFactory factory) =>
{
    var service = factory.Create(type);
    service.Send("Factory-based notification!");
    return Results.Ok($"Sent via {type}");
});
```

### Key Points
- Combines **Factory Pattern** with **Keyed Services**.
- Enables dynamic runtime resolution based on identifiers like `"email"` or `"sms"`.
- Maintains **Open/Closed** and **Dependency Inversion** principles.
- Demonstrates **Factory**, **Strategy**, and **Dependency Injection** patterns working together.

---

## 🧩 SOLID Principles Across All Examples

| Principle | Description | Applied Where |
|---|---|---|
| **S – Single Responsibility** | Each service has a single responsibility | All projects |
| **O – Open/Closed** | New types (e.g. new notification channels) can be added without modifying existing code | FactoryDI, ConstructorInjection |
| **L – Liskov Substitution** | Implementations can substitute each other safely | All DI examples |
| **I – Interface Segregation** | Clients depend on small, specific interfaces | Notification services |
| **D – Dependency Inversion** | High-level modules depend on abstractions, not concretes | All projects |

---

## 🧱 Design Patterns Demonstrated

| Pattern | Description | Example |
|---|---|---|
| **Dependency Injection** | Decouples creation from usage | All |
| **Strategy** | Swap behavior via injected interface | ConstructorInjection, FactoryDI |
| **Factory** | Centralized creation logic | FactoryDI |
| **Chain of Responsibility** | Pipeline of middleware | MiddlewareInjection |
| **Service Locator (controlled)** | Direct container resolution when appropriate | CoreWithoutMiddleware |

---

## ▶️ How to Run

Each example is a standalone project.

```bash
# Example: run the Factory DI demo
cd Example.FactoryDI
dotnet run
```

Then test with:

```
POST http://localhost:5217/notify/email
POST http://localhost:5217/notify/sms
```

(Adjust the port according to your launch settings.)

---

## 👤 Author

**Charles Path**  
Software Engineer | .NET Ecosystem | Architecture & Legacy Support Enthusiast  

> Passionate about clean design, code readability, and coffee ☕
