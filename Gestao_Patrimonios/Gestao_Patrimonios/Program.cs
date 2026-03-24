using DotNetEnv;
using Gestao_Patrimonios.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CARREGA O .env
Env.Load();

// PEGA A CONNECTION STRING DO .env
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

// CONECTA AO BANCO COM A CONNECTION STRING DO .env
builder.Services.AddDbContext<Gestao_PatrimoniosContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
