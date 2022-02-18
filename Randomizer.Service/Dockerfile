FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Copy code into working folder
WORKDIR /app
COPY . ./

# Build and publish .NET app
WORKDIR /app/Randomizer.Service/
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/
COPY --from=build-env /app/Randomizer.Service/out .
ENTRYPOINT ["dotnet", "Randomizer.Service.dll"]