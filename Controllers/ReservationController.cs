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
