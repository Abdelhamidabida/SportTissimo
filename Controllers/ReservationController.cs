using Microsoft.AspNetCore.Mvc;
using SportissimoProject.Commands;
using SportissimoProject.Commands.SportissimoProject.Commands;
using SportissimoProject.Models;
using SportissimoProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportissimoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ICreateReservationCommand _createReservationCommand;
        private readonly IUpdateReservationCommand _updateReservationCommand;
        private readonly IDeleteReservationCommand _deleteReservationCommand;
        private readonly IReservationRepository _reservationRepository;

        // Injection des dépendances via le constructeur
        public ReservationController(
            ICreateReservationCommand createReservationCommand,
            IUpdateReservationCommand updateReservationCommand,
            IDeleteReservationCommand deleteReservationCommand,
            IReservationRepository reservationRepository)
        {
            _createReservationCommand = createReservationCommand;
            _updateReservationCommand = updateReservationCommand;
            _deleteReservationCommand = deleteReservationCommand;
            _reservationRepository = reservationRepository;
        
        }

        [HttpGet("CheckConflict")]
        public async Task<IActionResult> CheckConflict([FromQuery] string terrainId, [FromQuery] DateTime dateDebut, [FromQuery] DateTime dateFin)
        {
            bool conflict = await _reservationRepository.HasConflictAsync(terrainId, dateDebut, dateFin);
            return Ok(new { conflict });
        }





        [HttpGet("AvailableTimes")]
        public async Task<IActionResult> GetAvailableTimes([FromQuery] string terrainId, [FromQuery] DateTime date)
        {
            if (string.IsNullOrWhiteSpace(terrainId))
            {
                return BadRequest("L'identifiant du terrain est invalide.");
            }

            try
            {
                // Récupérer les réservations existantes pour le terrain et la date donnée
                var reservations = await _reservationRepository.GetReservationsByTerrainAndDateAsync(terrainId, date.Date);

                var operatingHours = new List<TimeSpan>
{
    new TimeSpan(10, 0, 0),
    new TimeSpan(12, 0, 0),
    new TimeSpan(14, 0, 0),
    new TimeSpan(16, 0, 0),
    new TimeSpan(18, 0, 0),
    new TimeSpan(20, 0, 0),
    new TimeSpan(22, 0, 0)
};


                // Extraire les heures réservées (en les convertissant au format TimeSpan)
                var reservedTimes = reservations.Select(r => r.DateDebut.TimeOfDay).ToHashSet();

                // Filtrer les heures disponibles
                var availableTimes = operatingHours.Where(t => !reservedTimes.Contains(t)).ToList();

                // Retourner les heures disponibles
                return Ok(availableTimes.Select(t => t.ToString(@"hh\:mm")));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur : {ex.Message}");
            }
        }



















        [HttpGet("GetAvailableHours")]
        public async Task<IActionResult> GetAvailableHours([FromQuery] string terrainId, [FromQuery] DateTime date)
        {
            // Récupérer toutes les réservations pour ce terrain et cette date
            var reservations = await _reservationRepository.GetReservationsByTerrainAndDateAsync(terrainId, date.Date);

            // Générer toutes les plages horaires possibles (par exemple, de 8h à 22h, intervalle de 1h30)
            var allSlots = new List<(TimeSpan start, TimeSpan end)>();
            for (var time = new TimeSpan(8, 0, 0); time < new TimeSpan(22, 0, 0); time = time.Add(TimeSpan.FromMinutes(90)))
            {
                allSlots.Add((time, time.Add(TimeSpan.FromMinutes(90))));
            }

            // Filtrer les plages horaires déjà réservées
            var availableSlots = allSlots.Where(slot =>
                !reservations.Any(res =>
                    res.DateDebut.TimeOfDay < slot.end && res.DateFin.TimeOfDay > slot.start))
                .Select(slot => $"{slot.start:hh\\:mm} - {slot.end:hh\\:mm}")
                .ToList();

            return Ok(availableSlots);
        }

        // POST: api/reservation
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
        {
            if (command == null)
            {
                return BadRequest("Les données de réservation sont manquantes.");
            }

            // Utilisation de la commande pour créer la réservation
            var reservation = await _createReservationCommand.ExecuteAsync(command);

            if (reservation == null)
            {
                return BadRequest("La réservation n'a pas pu être créée.");
            }

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        // PUT: api/reservation/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(string id, [FromBody] UpdateReservationCommand command)
        {
            if (command == null)
            {
                return BadRequest("Les données de réservation sont manquantes.");
            }

            // Mise à jour de l'ID de la réservation dans la commande
            command.ReservationId = id;

            // Exécution de la commande via le handler
            var updatedReservation = await _updateReservationCommand.ExecuteAsync(command);

            if (updatedReservation == null)
            {
                return BadRequest("La réservation n'a pas pu être mise à jour.");
            }

            return Ok(updatedReservation);
        }


        // DELETE: api/reservation/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(string id)
        {
            var command = new DeleteReservationCommand { ReservationId = id };

            // Exécute la commande de suppression via le handler
            var result = await _deleteReservationCommand.ExecuteAsync(command);

            if (!result)
            {
                return BadRequest("La réservation n'a pas pu être supprimée.");
            }

            return NoContent(); // Suppression réussie
        }


        // GET: api/reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return Ok(reservations);
        }

        // GET: api/reservation/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(string id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);

            if (reservation == null)
            {
                return NotFound("Réservation non trouvée.");
            }

            return Ok(reservation);
        }
    }
}
