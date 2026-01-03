using BShop;
using BShop.Application.Services;
using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;
using BShop.GraphQL;
using BShop.GraphQL.Mutations;
using BShop.GraphQL.Queries;
using BShop.Infrastructure;
using BShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IWarehouseTransferRepository, WarehouseTransferRepository>();
builder.Services.AddScoped<IShopStorageRepository, ShopStorageRepository>();
builder.Services.AddScoped<IShopChecksRepository, ShopChecksRepository>();

builder.Services.AddDbContext<ShopDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(ShopDbContext)));
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IShopStorageService, ShopStorageService>();
builder.Services.AddScoped<IWarehouseTransferService, WarehouseTransferService>();
builder.Services.AddScoped<IShopSaleService, ShopSaleService>();

builder.Services.AddScoped<BShop.Domain.Interfaces.IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IErrorFilter, ErrorFilter>();

builder.Services.AddGraphQLServer()
    .AddErrorFilter<ErrorFilter>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment())
    .AddQueryType(d => d.Name("Query"))
    .AddTypeExtension<ProductQuery>()
    .AddTypeExtension<ShopQuery>()
    .AddTypeExtension<WarehouseTransferQuery>()
    .AddTypeExtension<WorkerQuery>()
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<ProductMutation>()
    .AddTypeExtension<ShopMutation>()
    .AddTypeExtension<WorkerMutation>()
    .AddTypeExtension<WarehouseTransferMutation>()
    .AddTypeExtension<ShopStorageMutation>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("**** Shop API is running! ****");
Console.ForegroundColor = ConsoleColor.White;

app.MapGraphQL();
app.Run();