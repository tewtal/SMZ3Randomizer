#!/bin/bash

# Run database migrations if any
cd /app
mkdir DB
dotnet tool install --global dotnet-ef
dotnet-ef database update
dotnet WebRandomizer.dll