# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files first for better layer caching
COPY ["PokerManager.Api/PokerManager.Api.csproj", "PokerManager.Api/"]
COPY ["Core/PokerManager.Domain/PokerManager.Domain.csproj", "Core/PokerManager.Domain/"]
COPY ["Core/PokerManager.Application/PokerManager.Application.csproj", "Core/PokerManager.Application/"]
COPY ["Infrastructure/PokerManager.Persistence/PokerManager.Persistence.csproj", "Infrastructure/PokerManager.Persistence/"]

# Restore dependencies
RUN dotnet restore "PokerManager.Api/PokerManager.Api.csproj"

# Copy everything else
COPY . .

# Build
WORKDIR "/src/PokerManager.Api"
RUN dotnet build "PokerManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PokerManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokerManager.Api.dll"]