using BankingApplication.Transaction.Domain.EventHandlers;
using BankingApplication.Transaction.Domain.Events;
using BankingApplicaton.Domain;
using IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TransactionData.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transfer Microservice", Version = "v1" });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
Dependencies.RegisterServices(builder.Services);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddDbContext<TransactionDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("TransactionDbConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transfer Microservice V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Configure Event Bus
ConfigureEventBus(app);

app.Run();

// Add ConfigureEventBus method
void ConfigureEventBus(WebApplication app)
{
    var eventBus = app.Services.GetRequiredService<IEventBus>();
    eventBus.Subscribe<TransferCreatedEvent, TransferingEventHandler>();
}