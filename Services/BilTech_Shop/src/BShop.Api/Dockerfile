FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/BilTech_Shop/src/BShop.Api/BShop.Api.csproj", "Services/BilTech_Shop/src/BShop.Api/"]
COPY ["Services/BilTech_Shop/src/BShop.Application/BShop.Application.csproj", "Services/BilTech_Shop/src/BShop.Application/"]
COPY ["Services/BilTech_Shop/src/BShop.Domain/BShop.Domain.csproj", "Services/BilTech_Shop/src/BShop.Domain/"]
COPY ["Services/BilTech_Shop/src/BShop.Infrastructure/BShop.Infrastructure.csproj", "Services/BilTech_Shop/src/BShop.Infrastructure/"]
RUN dotnet restore "Services/BilTech_Shop/src/BShop.Api/BShop.Api.csproj"
COPY . .
WORKDIR "/src/Services/BilTech_Shop/src/BShop.Api"
RUN dotnet build "./BShop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BShop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BShop.Api.dll"]
