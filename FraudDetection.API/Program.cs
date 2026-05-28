using Microsoft.EntityFrameworkCore;
using FraudDetection.API.Data;
using FraudDetection.API.Middleware;

var builder =
    WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    options =>
        options.UseMySql(
            builder.Configuration
                .GetConnectionString(
                    "DefaultConnection"
                ),
            ServerVersion.AutoDetect(
                builder.Configuration
                    .GetConnectionString(
                        "DefaultConnection"
                    )
            )
        )
);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();

app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();