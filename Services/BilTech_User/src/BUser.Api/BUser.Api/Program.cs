using BShop.Application.Services;
using BUser.Api;
using BUser.Api.Extensions;
using BUser.Api.GraphQl.Mutations;
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
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.AddAutoMapper(typeof(MappingProfile)); 

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType(d => d.Name("Query"))
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<UserMutation>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();


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
app.MapGraphQL();
// app.UseHttpsRedirection();
app.Run();