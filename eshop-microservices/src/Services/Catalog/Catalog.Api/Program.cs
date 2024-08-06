var builder = WebApplication.CreateBuilder(args);
//Add Services
builder.Services.AddCarter();
builder.Services.AddMediatR(config => { 
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
builder.Services.AddMarten(builder.Configuration.GetConnectionString("Database")!)
    .UseLightweightSessions();

var app = builder.Build();

//Configure http request pipeline

app.MapCarter();

app.Run();
