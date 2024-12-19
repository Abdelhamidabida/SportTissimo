using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SportissimoProject.Models;
using SportissimoProject.Repositories.Interfaces;
using SportissimoProject.Repositories;
using SportissimoProject.Services.Pricing;
using SportissimoProject.Commands;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services au conteneur
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration du contexte de base de données
var connectionString = builder.Configuration.GetConnectionString("dbcon");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

// Configuration des dépendances
builder.Services.AddScoped<PricingStrategyFactory>();
builder.Services.AddScoped<IAbonnementRepository, AbonnementRepository>();
builder.Services.AddScoped<ITerrainRepository, TerrainRepository>();
builder.Services.AddScoped<ICreateReservationCommand, CreateReservationCommandHandler>();
builder.Services.AddScoped<IUpdateReservationCommand, UpdateReservationCommandHandler>();
builder.Services.AddScoped<IDeleteReservationCommand, DeleteReservationCommandHandler>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

// Configuration d'Identity
builder.Services.AddIdentity<Client, IdentityRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

// Configuration de l'authentification JWT
var jwtKey = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"])),
            RoleClaimType = "roles" // Assurez-vous que le claim de rôle est correctement configuré
        };
    });



// Ajout des options JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });

// Configuration CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin()   // Ou spécifiez un domaine spécifique si nécessaire
               .AllowAnyMethod()
               .AllowAnyHeader());
});

// Configuration Swagger pour JWT
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Entrez le JWT avec le préfixe Bearer (ex : Bearer {token})",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseRouting();

app.UseAuthentication(); // Doit être avant UseAuthorization
app.UseAuthorization();

// Initialisation des rôles au démarrage
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await CreateRoles(roleManager);  // Appel à la méthode pour créer les rôles
}

app.MapControllers();

app.Run();

// Méthode pour créer les rôles si ils n'existent pas
async Task CreateRoles(RoleManager<IdentityRole> roleManager)
{
    string[] roleNames = { "admin", "user" };

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                Console.WriteLine($"Le rôle '{roleName}' a été créé avec succès.");
            }
            else
            {
                Console.WriteLine($"Erreur lors de la création du rôle '{roleName}'.");
            }
        }
        else
        {
            Console.WriteLine($"Le rôle '{roleName}' existe déjà.");
        }
    }
}
