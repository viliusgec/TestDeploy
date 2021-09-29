# # syntax=docker/dockerfile:1
# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
# WORKDIR /app

# # Copy csproj and restore as distinct layers
# COPY NuGet.config NuGet.config 
# COPY TestDeploy/TestDeploy.csproj ./
# RUN dotnet restore

# # Copy everything else and build
# RUN dotnet clean TestDeploy.csproj -c Release
# RUN dotnet build TestDeploy.csproj -c Release
# RUN dotnet publish TestDeploy.csproj -c Release  -o /app

# # Build runtime image
# FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
# WORKDIR /app
# COPY --from=build-env /app/out .
# ENTRYPOINT ["dotnet", "TestDeploy.dll"]

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
Copy . .
COPY TestDeployment/TestDeployment.csproj ./
RUN dotnet restore TestDeployment.csproj

# Copy everything else and build
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TestDeployment.dll"]