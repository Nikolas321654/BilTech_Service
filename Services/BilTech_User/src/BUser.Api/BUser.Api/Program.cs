using BUser.Api;
using BUser.Application;
using BUser.Application.Services;
using BUser.Api.Extensions;
using BUser.Api.GraphQl.Mutations;
using BUser.Api.GraphQl.Query;
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
builder.Services.AddScoped<IUserFabric, UserFabric>();
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services
    .AddGraphQLServer()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddAuthorization()
    .AddQueryType(d => d.Name("Query"))
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<UserMutation>()
    .AddTypeExtension<UserQuery>()
    .AddType(new HotChocolate.Types.EnumType<BUser.Domain.Roles>(d => d.Name("DomainRoles")))
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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DbUserContext>();
        context.Database.Migrate();
        Console.WriteLine("----> Db is migrated successfully !");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"----> Error, migrated error: {ex.Message}");
    }
}

app.UseAuthentication();
app.UseAuthorization();
app.MapGraphQL();
// app.UseHttpsRedirection();
app.Run();