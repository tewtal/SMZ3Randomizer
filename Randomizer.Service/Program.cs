using Randomizer.Service.Services;
using Randomizer.Shared.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Add database service
builder.Services.AddDbContext<RandomizerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");

    })
);

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseGrpcWeb();
app.UseCors();

app.MapGrpcService<SessionService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<MetadataService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<EventService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGet("/", () => "This is a backend service host for the Super Metroid and A Link to the Past Randomizer.\nGo here to setup a new game: https://samus.link/");

app.UseDeveloperExceptionPage();

app.Run();
