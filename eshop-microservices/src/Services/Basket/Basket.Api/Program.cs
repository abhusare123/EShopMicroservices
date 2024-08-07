var builder = WebApplication.CreateBuilder(args);
//Add dependency

var app = builder.Build();
//Add pipeline

app.MapGet("/", () => "Hello World!");

app.Run();
