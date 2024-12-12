using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportissimoProject.Models;
using SportissimoProject.Repositories.Interfaces;
using SportissimoProject.Repositories;
using SportissimoProject.Repository;
using SportissimoProject.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using SportissimoProject.Services.Pricing;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var cnx = builder.Configuration.GetConnectionString("dbcon");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(cnx));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ITerrainRepository, TerrainRepository>();

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IAbonnementRepository, AbonnementRepository>();

builder.Services.AddScoped<ICreateReservationCommand, CreateReservationCommandHandler>();
builder.Services.AddScoped<IUpdateReservationCommand, UpdateReservationCommandHandler>();
builder.Services.AddScoped<IDeleteReservationCommand, DeleteReservationCommandHandler>();




builder.Services.AddScoped<PricingStrategyFactory>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });






builder.Services.AddScoped<IClientRepo, ClientRepo>();
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
