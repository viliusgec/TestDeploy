WORKDIR /app

# Copy csproj and restore as distinct layers
Copy . .
COPY *.csproj ./
RUN dotnet restore TestDeployment.csproj

# Copy everything else and build
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY --from=ca-certificate.crt /etc/ssl/certs/ca-certificate.crt /etc/ssl/certs/ca-certificate.crt
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TestDeployment.dll"]