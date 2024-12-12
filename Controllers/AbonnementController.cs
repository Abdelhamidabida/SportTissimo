using Microsoft.AspNetCore.Mvc;
using SportissimoProject.Models;
using SportissimoProject.Repositories.Interfaces;
using SportissimoProject.Services.Pricing;
using System.Threading.Tasks;

namespace SportissimoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbonnementController : ControllerBase
    {
        private readonly IAbonnementRepository _abonnementRepository;
        private readonly PricingStrategyFactory _pricingStrategyFactory;

        // Injection des dépendances
        public AbonnementController(IAbonnementRepository abonnementRepository, PricingStrategyFactory pricingStrategyFactory)
        {
            _abonnementRepository = abonnementRepository;
            _pricingStrategyFactory = pricingStrategyFactory;
        }

        // Action pour créer un abonnement
        [HttpPost]
        public async Task<IActionResult> CreateAbonnement([FromBody] Abonnement abonnement)
        {
            if (abonnement == null)
            {
                return BadRequest("Les données de l'abonnement sont manquantes.");
            }

            // Calcul du prix en fonction du type de gym et de la fréquence de paiement
            var strategy = _pricingStrategyFactory.GetStrategy(abonnement.TypeGym);
            abonnement.Prix = strategy.CalculatePrice(abonnement.FrequencePaiement);

            // Ajout de l'abonnement dans la base de données
            await _abonnementRepository.AddAsync(abonnement);

            // Retourne un code de succès 201 avec l'URL de la ressource nouvellement créée
            return CreatedAtAction(nameof(GetAbonnement), new { id = abonnement.Id }, abonnement);
        }

        // Action pour récupérer un abonnement par son ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAbonnement(string id)
        {
            var abonnement = await _abonnementRepository.GetByIdAsync(id);
            if (abonnement == null)
            {
                return NotFound();
            }

            return Ok(abonnement);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abonnement>>> GetAllAbonnements()
        {
            try
            {
                // Récupérer tous les abonnements depuis la base de données
                var abonnements = await _abonnementRepository.GetAllAsync();

                // Si la liste est vide ou nulle, renvoyer un statut 404 NotFound
                if (abonnements == null )
                {
                    return NotFound("Aucun abonnement trouvé.");
                }

                // Retourner la liste des abonnements avec un code 200 OK
                return Ok(abonnements);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, retourner un statut 500 avec le message d'erreur
                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
            }
        }
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<Abonnement>>> GetAbonnementsByClientId(string clientId)
        {
            try
            {
                // Récupérer tous les abonnements d'un client spécifique
                var abonnements = await _abonnementRepository.GetByClientIdAsync(clientId);

                // Si aucun abonnement n'est trouvé pour ce client, retourner un statut 404 NotFound
                if (abonnements == null )
                {
                    return NotFound($"Aucun abonnement trouvé pour le client avec ID {clientId}.");
                }

                // Retourner la liste des abonnements avec un code 200 OK
                return Ok(abonnements);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, retourner un statut 500 avec le message d'erreur
                return StatusCode(500, $"Erreur du serveur : {ex.Message}");
            }
        }
        [HttpGet("type/{typeGym}")]
        public async Task<ActionResult<IEnumerable<Abonnement>>> GetAbonnementsByType(TypeAbonnement typeGym)
        {
            var abonnements = await _abonnementRepository.GetByTypeAsync(typeGym);

            if (abonnements == null )
            {
                return NotFound($"Aucun abonnement trouvé pour le type de gym {typeGym}");
            }

            return Ok(abonnements);
        }
    }
}
