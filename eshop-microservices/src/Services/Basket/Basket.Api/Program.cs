using Basket.Api.Data;
using BuildingBlocks.Behaviors.ValidationBehaviors;

var builder = WebApplication.CreateBuilder(args);
//Add dependency

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>) );
});
builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database"));
    config.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var app = builder.Build();
//Add pipeline

app.MapCarter();

app.Run();
