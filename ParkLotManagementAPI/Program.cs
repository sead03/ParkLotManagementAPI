using Microsoft.EntityFrameworkCore;
using ParkLotManagementAPI.EfCore;
using ParkLotManagementAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EF_DataContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Ef_Postgres_Db")));

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddScoped<DbHelper>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
