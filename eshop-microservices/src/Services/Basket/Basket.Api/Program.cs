using Basket.Api.Data;
using BuildingBlocks.Behaviors.ValidationBehaviors;
using BuildingBlocks.Exceptions.Handler;
using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);
//Add dependency

var currentAssembly = typeof(Program).Assembly;
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(currentAssembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>) );
});

builder.Services.AddValidatorsFromAssembly(currentAssembly);

builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
    config.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddScoped<IBasketRepository>(provider => 
{
    var basketRepository = provider.GetRequiredService<BasketRepository>();
    return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();
//Add pipeline

app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
