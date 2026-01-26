FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BilTech_Shop/src/BShop/BShop.Api/BShop.Api.csproj", "BilTech_Shop/src/BShop/BShop.Api/"]
COPY ["BilTech_Shop/src/BShop/BShop.Application/BShop.Application.csproj", "BilTech_Shop/src/BShop/BShop.Application/"]
COPY ["BilTech_Shop/src/BShop/BShop.Domain/BShop.Domain.csproj", "BilTech_Shop/src/BShop/BShop.Domain/"]
COPY ["BilTech_Shop/src/BShop/BShop.Infrastructure/BShop.Infrastructure.csproj", "BilTech_Shop/src/BShop/BShop.Infrastructure/"]
RUN dotnet restore "BilTech_Shop/src/BShop/BShop.Api/BShop.Api.csproj"
COPY . .
WORKDIR "/src/BilTech_Shop/src/BShop/BShop.Api"
RUN dotnet build "./BShop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BShop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BShop.Api.dll"]
