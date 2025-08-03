using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Application.Behaviors;
using OrderProcessing.Application.Commands;
using OrderProcessing.Domain.Interfaces;
using OrderProcessing.Infrastructure.Logging;
using OrderProcessing.Infrastructure.Persistence;
using OrderProcessing.Infrastructure.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 1) Configure Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    // Pass the in-flight LoggerConfiguration into your helper
    SerilogConfig.Configure(loggerConfiguration)
        .ReadFrom.Configuration(hostingContext.Configuration);
});

// 2) EF Core with Npgsql
builder.Services.AddDbContext<OrderContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3) MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly);
});

// 4) FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

// 5) Pipeline behavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// 6) Your repository
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// 7) Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 8) Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderProcessing API V1");
});
app.MapControllers();
app.Run();

public partial class Program { }