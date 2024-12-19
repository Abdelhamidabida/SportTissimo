using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportissimoProject.Models;
using SportissimoProject.Repositories.Interfaces;
using SportissimoProject.Services.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportissimoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AbonnementController : ControllerBase
    {
        private readonly IAbonnementRepository _abonnementRepository;
        private readonly Context _context;

        // Injection des dépendances
        public AbonnementController(Context context, IAbonnementRepository abonnementRepository)
        {
            _context = context;
            _abonnementRepository = abonnementRepository;
        }

        // Méthode pour calculer la date de fin en fonction de la fréquence de paiement
        private DateTime CalculerDateFin(DateTime dateDebut, FrequencePaiement frequencePaiement)
        {
            return frequencePaiement switch
            {
                FrequencePaiement.Mensuel => dateDebut.AddMonths(1),
                FrequencePaiement.Trimestriel => dateDebut.AddMonths(3),
                FrequencePaiement.Annuel => dateDebut.AddYears(1),
                _ => dateDebut // Par défaut, retourne la date de début si la fréquence est invalide
            };
        }

        // Vérifie si un abonnement existe déjà pour un client et un type de gym
        private async Task<bool> ValidateAbonnementUniqueness(string clientId, TypeAbonnement typeGym)
        {
            return await _context.Abonnements
                .AnyAsync(a => a.ClientId == clientId && a.TypeGym == typeGym);
        }

        // POST api/Abonnement
        [HttpPost]
        public async Task<ActionResult<Abonnement>> CreateAbonnement([FromBody] Abonnement? abonnement)
        {
            // Vérifier que l'objet Abonnement est fourni
            if (abonnement == null)
            {
                return BadRequest("L'abonnement est requis.");
            }

            // Vérifier si un abonnement actif existe déjà pour ce client et ce type de gym
            if (await ValidateAbonnementUniqueness(abonnement.ClientId, abonnement.TypeGym))
            {
                return BadRequest("Un abonnement pour ce type de gym existe déjà.");
            }

            // Calculer la date de fin en fonction de la fréquence de paiement
            abonnement.DateFin = CalculerDateFin(abonnement.DateDebut, abonnement.FrequencePaiement);

            // Obtenir la bonne stratégie de tarification à partir de la factory
            var pricingStrategyFactory = new PricingStrategyFactory();
            var pricingStrategy = pricingStrategyFactory.GetStrategy(abonnement.TypeGym);

            // Appliquer la stratégie de tarification pour calculer le prix
            abonnement.Prix = pricingStrategy.CalculatePrice(abonnement.FrequencePaiement);

            // Créer un nouvel ID pour l'abonnement
            abonnement.Id = Guid.NewGuid().ToString();

            // Ajouter l'abonnement à la base de données via le repository
            await _abonnementRepository.AddAsync(abonnement);

            // Charger les informations du client en même temps que l'abonnement
            var abonnementAvecClient = await _context.Abonnements
                .Include(a => a.Client)  // Inclure le client dans la récupération de l'abonnement
                .FirstOrDefaultAsync(a => a.Id == abonnement.Id);

            // Retourner l'abonnement avec les informations du client
            return CreatedAtAction(nameof(GetAbonnementById), new { id = abonnementAvecClient.Id }, abonnementAvecClient);
        }

        // GET api/Abonnement/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Abonnement>> GetAbonnementById(string id)
        {
            var abonnement = await _abonnementRepository.GetByIdAsync(id);

            if (abonnement == null)
            {
                return NotFound();
            }

            return abonnement;
        }

        // GET api/Abonnement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abonnement>>> GetAllAbonnements()
        {
            try
            {
                var abonnements = await _abonnementRepository.GetAllAsync();
                if (abonnements == null)
                {
                    return NotFound("Aucun abonnement trouvé.");
                }

                return Ok(abonnements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
            }
        }

        // GET api/Abonnement/client/{clientId}
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<Abonnement>>> GetAbonnementsByClientId(string clientId)
        {
            var abonnements = await _abonnementRepository.GetByClientIdAsync(clientId);

            if (abonnements == null || !abonnements.Any())
            {
                return NotFound($"Aucun abonnement trouvé pour le client avec ID {clientId}.");
            }

            return Ok(abonnements);
        }

        // GET api/Abonnement/type/{typeGym}
        [HttpGet("type/{typeGym}")]
        public async Task<ActionResult<IEnumerable<Abonnement>>> GetAbonnementsByType(string typeGym)
        {
            try
            {
                var type = (TypeAbonnement)Enum.Parse(typeof(TypeAbonnement), typeGym, true);
                var abonnements = await _abonnementRepository.GetByTypeAsync(type);

                if (abonnements == null || !abonnements.Any())
                {
                    return NotFound($"Aucun abonnement trouvé pour le type de gym {typeGym}");
                }

                return Ok(abonnements);
            }
            catch (ArgumentException)
            {
                return BadRequest("Le type de gym spécifié est invalide.");
            }
        }
    }
}
