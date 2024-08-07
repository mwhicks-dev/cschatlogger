using Microsoft.EntityFrameworkCore;
using CSChatLogger.Api;
using CSChatLogger.Entity;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<Context>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("CSChatLogger")));
builder.Services.Configure<ConnectionStringSettings>(options => options.ConnectionString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_DB_URI"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
