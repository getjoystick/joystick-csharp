using Joystick.Client;
using System.Configuration;
using Joystick.Client.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.

builder.Services.AddJoystickClient();
builder.Services.AddOptions<JoystickClientConfig>()
    .Bind(builder.Configuration.GetSection("JoystickConfig"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
