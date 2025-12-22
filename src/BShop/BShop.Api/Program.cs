    using BShop;
    using BShop.GraphQL.Queries;
    using BShop.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    builder.Services.AddGraphQLServer()
        .AddQueryType(d => d.Name("Query"))
        .AddTypeExtension<ProductQuery>()
        .AddTypeExtension<ShopQuery>()
        .AddTypeExtension<ShopSalesQuery>()
        .AddTypeExtension<WarehouseTransferQuery>()
        .AddTypeExtension<ShopStorageQuery>()
        // .AddMutationType(d => d.Name("Mutation"))
        .AddProjections()
        .AddFiltering()
        .AddSorting();

    builder.Services.AddDbContext<ShopDbContext>(options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(ShopDbContext)));
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapGraphQL();
    app.Run();