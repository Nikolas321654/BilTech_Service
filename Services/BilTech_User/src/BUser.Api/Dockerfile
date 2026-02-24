FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Services/BilTech_User/src/BUser.Api/BUser.Api/BUser.Api.csproj", "Services/BilTech_User/src/BUser.Api/BUser.Api/"]
COPY ["Services/BilTech_User/src/BUser.Api/BUser.Application/BUser.Application.csproj", "Services/BilTech_User/src/BUser.Api/BUser.Application/"]
COPY ["Services/BilTech_User/src/BUser.Api/BUser.Domain/BUser.Domain.csproj", "Services/BilTech_User/src/BUser.Api/BUser.Domain/"]
COPY ["Services/BilTech_User/src/BUser.Api/BUser.Infrastructure/BUser.Infrastructure.csproj", "Services/BilTech_User/src/BUser.Api/BUser.Infrastructure/"]

RUN dotnet restore "Services/BilTech_User/src/BUser.Api/BUser.Api/BUser.Api.csproj"

COPY . .
WORKDIR "/src/Services/BilTech_User/src/BUser.Api/BUser.Api"
RUN dotnet build "BUser.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "BUser.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BUser.Api.dll"]