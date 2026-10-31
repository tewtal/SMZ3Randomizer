#!/usr/bin/bash

export DOTNET_ROLL_FORWARD=Major
cd WebRandomizer
ASPNETCORE_ENVIRONMENT=Development dotnet run

