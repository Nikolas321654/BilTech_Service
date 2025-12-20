using BShop.Domain.Model;
using BShop.GraphQL.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddTypeExtension<ProductQuery>()
    .AddTypeExtension<ShopQuery>()
    .AddTypeExtension<ShopSalesQuery>()
    .AddTypeExtension<WarehouseTransferQuery>()
    .AddMutationType()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGraphQL();
app.Run();