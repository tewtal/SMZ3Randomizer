FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
RUN apt-get update -yq && apt-get install cmake build-essential python3.9 -yq

# Install node 18
RUN curl -sSL "https://deb.nodesource.com/setup_18.x" -o nodesource_setup.sh \
 && bash nodesource_setup.sh \
 && apt-get install nodejs -yq

# Build asar
WORKDIR /asar
RUN curl -sSL "https://github.com/RPGHacker/asar/archive/v1.81.tar.gz" -o v1.81.tar.gz \
 && tar xfz v1.81.tar.gz \
 && cd asar-1.81 \
 && cmake src \
 && make

# Copy code into working folder
WORKDIR /app
COPY . ./

# Create IPS patch from combo ASM code project
WORKDIR /app/alttp_sm_combo_randomizer_rom/
RUN cp /asar/asar-1.81/asar/asar-standalone resources/asar \
 && chmod +x ./build.sh \
 && ./build.sh \
 && cd build \
 && gzip zsm.ips \
 && cp zsm.ips.gz /app/WebRandomizer/ClientApp/src/resources/

# Create IPS patch from sm ASM code project
WORKDIR /app/sm_randomizer_rom/
RUN cp /asar/asar-1.81/asar/asar-standalone resources/asar \
 && chmod +x ./build.sh \
 && ./build.sh \
 && cd build \ 
 && gzip sm.ips \
 && cp sm.ips.gz /app/WebRandomizer/ClientApp/src/resources/

# Build and publish .NET app
WORKDIR /app
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
COPY docker-entrypoint.sh /usr/local/bin/
RUN chmod +x /usr/local/bin/docker-entrypoint.sh
ENTRYPOINT ["/usr/local/bin/docker-entrypoint.sh"]
