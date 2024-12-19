using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SportissimoProject.DTO;
using SportissimoProject.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<Client> _userManager;
    private readonly SignInManager<Client> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly Context _context;
    private readonly ILogger<AccountController> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountController(UserManager<Client> userManager,
                             SignInManager<Client> signInManager,
                             IConfiguration configuration,
                             Context context,
                             ILogger<AccountController> logger, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
        _logger = logger;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterNewUser(UserRegisterDto newUserDTO)
    {
        try
        {
            // Vérifier si l'utilisateur existe déjà
            var existingUser = await _userManager.FindByEmailAsync(newUserDTO.Email);
            if (existingUser != null)
            {
                _logger.LogWarning($"L'email {newUserDTO.Email} est déjà utilisé.");
                return BadRequest("L'email est déjà utilisé.");
            }

            // Créer un utilisateur Client
            var appUser = new Client
            {
                UserName = newUserDTO.Email,
                Email = newUserDTO.Email,
                Nom = newUserDTO.Nom,
                Prenom = newUserDTO.Prenom
            };

            // Créer l'utilisateur avec mot de passe
            var result = await _userManager.CreateAsync(appUser, newUserDTO.MotDePasse);
            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError($"Erreur lors de la création de l'utilisateur: {errorMessages}");
                return BadRequest(new { Erreur = "Erreur lors de la création de l'utilisateur: " + errorMessages });
            }

            // Attribuer le rôle en fonction de l'email (par exemple, "admin" pour admin@example.com, "user" sinon)
            var role = newUserDTO.Email == "admin@example.com" ? "admin" : "user";

            // Vérifier si le rôle existe avant de l'attribuer
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                _logger.LogError($"Le rôle {role} n'existe pas.");
                return BadRequest(new { Erreur = $"Le rôle {role} n'existe pas." });
            }

            // Ajouter le rôle à l'utilisateur
            var roleResult = await _userManager.AddToRoleAsync(appUser, role);
            if (!roleResult.Succeeded)
            {
                var roleErrorMessages = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                _logger.LogError($"Erreur lors de l'ajout du rôle à l'utilisateur: {roleErrorMessages}");
                return BadRequest(new { Erreur = "Erreur lors de l'ajout du rôle à l'utilisateur: " + roleErrorMessages });
            }

            _logger.LogInformation($"Utilisateur {newUserDTO.Email} créé avec succès et rôle '{role}' attribué.");
            return Ok("Utilisateur et client créés avec succès et rôle attribué.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Une erreur interne s'est produite: {ex.Message}");
            return StatusCode(500, new { Erreur = "Une erreur interne s'est produite. Veuillez réessayer plus tard." });
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model)
    {
        try
        {
            // Vérification de la validité du modèle
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Erreur = "Les données soumises sont invalides." });
            }

            // Vérifiez que l'email n'est pas vide
            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest(new { Erreur = "L'email est requis." });
            }

            // Recherche de l'utilisateur par email
            var user = await _userManager.FindByEmailAsync(model.Email);

            // Vérifier si l'utilisateur existe
            if (user == null)
            {
                _logger.LogWarning($"Tentative de connexion avec un email inexistant : {model.Email}");
                return Unauthorized(new { Erreur = "Email ou mot de passe incorrect." });
            }

            // Vérification du mot de passe
            var result = await _signInManager.PasswordSignInAsync(user, model.MotDePasse, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                _logger.LogWarning($"Mot de passe incorrect pour l'utilisateur : {model.Email}");
                return Unauthorized(new { Erreur = "Email ou mot de passe incorrect." });
            }

            // Création du JWT Token
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            // Retourner le token JWT généré
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Une erreur interne est survenue : {ex.Message}");
            return StatusCode(500, new { Erreur = "Une erreur interne s'est produite. Veuillez réessayer plus tard." });
        }
    }




    [HttpGet("clients")]
    public async Task<IActionResult> GetClients()
    {
        try
        {
            // Récupérer la liste des clients depuis la base de données
            var clients = await _context.Clients
                .Select(c => new
                {
                    c.Id,
                    c.Email,
                    c.Nom,
                    c.Prenom
                })
                .ToListAsync();

            // Vérifier si la liste est vide
            if (clients == null || !clients.Any())
            {
                return NotFound(new { Message = "Aucun client trouvé." });
            }

            // Pour chaque client, récupérer les rôles de manière asynchrone
            var clientsWithRoles = new List<object>();
            foreach (var client in clients)
            {
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(client.Id));
                clientsWithRoles.Add(new
                {
                    client.Id,
                    client.Email,
                    client.Nom,
                    client.Prenom,
                    Roles = roles
                });
            }

            return Ok(clientsWithRoles);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Une erreur s'est produite lors de la récupération des clients : {ex.Message}");
            return StatusCode(500, new { Erreur = "Une erreur interne s'est produite. Veuillez réessayer plus tard." });
        }
    }

}