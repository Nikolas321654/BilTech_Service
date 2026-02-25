using HotChocolate;

namespace BUser.Api.Models;

[GraphQLName("UserRoles")]
public enum Roles
{
    ShopEmployee,
    WarehouseEmployee,
    CalculationEmployee
}