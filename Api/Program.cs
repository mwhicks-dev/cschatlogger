using Microsoft.EntityFrameworkCore;
using CSChatLogger.Api;
using CSChatLogger.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(options => 
    options.UseInMemoryDatabase("ChatLog"));

builder.Services.AddDbContext<Context>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("Context")));

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
