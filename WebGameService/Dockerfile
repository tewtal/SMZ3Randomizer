FROM mcr.microsoft.com/dotnet/core/sdk:5.0 AS build-env

# Copy code into working folder
WORKDIR /app
COPY . ./

# Build and publish .NET app
WORKDIR /app/WebGameService/
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:5.0
WORKDIR /app/
COPY --from=build-env /app/WebGameService/out .
ENTRYPOINT ["dotnet", "WebGameService.dll"]