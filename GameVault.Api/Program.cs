var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "GameVault.Api lista. Los datos salen de /odata.");

app.Run();
