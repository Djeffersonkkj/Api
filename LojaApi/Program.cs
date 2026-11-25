using LojaApi.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<LojaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LojaDb")));

// builder.Services.AddDbContext<LojaDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("LojaDb")));
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();;
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await CargaInicial.ExecutarAsync(app);
await app.RunAsync();
