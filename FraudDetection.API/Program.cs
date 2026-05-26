using Microsoft.EntityFrameworkCore;

using FraudDetection.API.Data;

var builder =
    WebApplication.CreateBuilder(args);

/*Services*/

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

/*DB*/

builder.Services.AddDbContext<AppDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration
                .GetConnectionString(
                    "DefaultConnection"
                )
        )
);

/*APP*/

var app =
    builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();