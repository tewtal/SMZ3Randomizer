FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
RUN apt-get update -yq && apt-get install nodejs npm cmake build-essential python3.7 -yq

# Build asar
WORKDIR /asar
RUN curl -sSL "https://github.com/RPGHacker/asar/archive/v1.71.tar.gz" -o v1.71.tar.gz \
 && tar xfz v1.71.tar.gz \
 && cd asar-1.71 \
 && cmake src \
 && make

# Copy code into working folder
WORKDIR /app
COPY . ./

# Create IPS patch from ASM code project
WORKDIR /app/alttp_sm_combo_randomizer_rom/
RUN cp /asar/asar-1.71/asar/asar-standalone resources/asar \
 && find . -name build.py -exec python3.7 {} \; \
 && cd resources \
 && python3.7 create_dummies.py 00.sfc ff.sfc \
 && ./asar --no-title-check ../src/main.asm 00.sfc \
 && ./asar --no-title-check ../src/main.asm ff.sfc \
 && python3.7 create_ips.py 00.sfc ff.sfc ../build/zsm.ips \
 && cd ../build/ \ 
 && gzip zsm.ips \
 && cp zsm.ips.gz /app/WebRandomizer/ClientApp/src/resources/

# Build and publish .NET app
WORKDIR /app
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
COPY docker-entrypoint.sh /usr/local/bin/
RUN chmod +x /usr/local/bin/docker-entrypoint.sh
ENTRYPOINT ["/usr/local/bin/docker-entrypoint.sh"]