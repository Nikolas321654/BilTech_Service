using BOrder.Domain.Interfaces;
using BOrder.Domain.Models;
using BOrder.Infrastructure;
using BOrder.Infrastructure.Repositories;
using Messaging.Kafka;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddConsumer<OrderEntity, OrderCreatedMessageHandler>(
    builder.Configuration.GetSection("Kafka:KafkaSettings"));


builder.Services.AddScoped<IOrderRepository, OrderRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();