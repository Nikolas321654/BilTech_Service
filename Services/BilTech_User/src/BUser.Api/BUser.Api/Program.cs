using BUser.Api.Extensions;
using BUser.Domain.Interfaces;
using BUser.Domain.Model;
using BUser.Infrastructure;
using BUser.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));

builder.Services.AddDbContext<DbUserContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(DbUserContext)));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
// app.UseHttpsRedirection();
app.Run();