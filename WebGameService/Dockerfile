FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

# Copy code into working folder
WORKDIR /app
COPY . ./

# Build and publish .NET app
WORKDIR /app/
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app/
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WebGameServer.dll"]