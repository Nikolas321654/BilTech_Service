FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BShop.Api/BShop.Api.csproj", "BShop.Api/"]
COPY ["BShop.Application/BShop.Application.csproj", "BShop.Application/"]
COPY ["BShop.Domain/BShop.Domain.csproj", "BShop.Domain/"]
COPY ["BShop.Infrastructure/BShop.Infrastructure.csproj", "BShop.Infrastructure/"]
RUN dotnet restore "BShop.Api/BShop.Api.csproj"
COPY . .
WORKDIR "/src/BShop.Api"
RUN dotnet build "./BShop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BShop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BShop.Api.dll"]
