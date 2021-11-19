using ClientSideCaching;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = Environment.GetEnvironmentVariable("redisString");
if (string.IsNullOrEmpty(connectionString))
{
    connectionString = "localhost:6379";
}
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConnectionMultiplexer>(await ConnectionMultiplexer.ConnectAsync(connectionString));
builder.Services.AddSingleton<ClientSideCache>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
