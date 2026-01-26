using System.Text;
using BShop;
using BShop.Application.Services;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.GraphQL;
using BShop.GraphQL.Mutations;
using BShop.GraphQL.Queries;
using BShop.Infrastructure;
using BShop.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var jwtSecretKey = configuration["JWT_SECRET_KEY"];
if (string.IsNullOrEmpty(jwtSecretKey))
    throw new Exception("JWT_SECRET_KEY is not configured in environment variables!");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAuthorization();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IWarehouseTransferRepository, WarehouseTransferRepository>();
builder.Services.AddScoped<IShopStorageRepository, ShopStorageRepository>();
builder.Services.AddScoped<IShopChecksRepository, ShopChecksRepository>();

builder.Services.AddPooledDbContextFactory<ShopDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(ShopDbContext)));
});

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IDbContextFactory<ShopDbContext>>().CreateDbContext());

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IShopStorageService, ShopStorageService>();
builder.Services.AddScoped<IWarehouseTransferService, WarehouseTransferService>();
builder.Services.AddScoped<IShopSaleService, ShopSaleService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();

builder.Services.AddScoped<BShop.Domain.Interfaces.IUnitOfWork, UnitOfWork>();


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddErrorFilter<ErrorFilter>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment())
    .AddQueryType(d => d.Name("Query"))
    .AddTypeExtension<ProductQuery>()
    .AddTypeExtension<ShopQuery>()
    .AddTypeExtension<WarehouseTransferQuery>()
    .AddTypeExtension<WorkerQuery>()
    .AddTypeExtension<ProductTypeQuery>()
    .AddTypeExtension<ShopStorageQuery>()
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<ProductMutation>()
    .AddTypeExtension<ShopMutation>()
    .AddTypeExtension<WorkerMutation>()
    .AddTypeExtension<ProductTypeMutation>()
    .AddTypeExtension<WarehouseTransferMutation>()
    .AddTypeExtension<ShopStorageMutation>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var contextFactory = services.GetRequiredService<IDbContextFactory<ShopDbContext>>();
        using var context = contextFactory.CreateDbContext();
        var pendingMigrations = context.Database.GetPendingMigrations().ToList();

        if (pendingMigrations.Any())
        {
            Console.WriteLine(
                $"----> Found {pendingMigrations.Count} pending migrations: {string.Join(", ", pendingMigrations)}");
            Console.WriteLine("----> Applying migrations...");
            context.Database.Migrate();
            Console.WriteLine("----> Database updated successfully!");
        }
        else
        {
            Console.WriteLine("----> No pending migrations found. Database is up to date.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"----> Migration error: {ex.Message}");
        if (ex.InnerException != null)
        {
            Console.WriteLine($"----> Inner exception: {ex.InnerException.Message}");
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("**** Shop API is running! ****");
Console.ForegroundColor = ConsoleColor.White;

app.MapGraphQL();
app.Run();