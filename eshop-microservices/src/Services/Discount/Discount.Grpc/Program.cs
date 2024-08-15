using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<DiscountContext>(options => options.UseInMemoryDatabase(connectionString!));
builder.Services.AddScoped<CouponSeeder>();
var app = builder.Build();
var couponSeeder  = app.Services.CreateScope().ServiceProvider.GetRequiredService<CouponSeeder>();
couponSeeder.Seed();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();

app.Run();
