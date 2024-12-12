using SportissimoProject.Commands.SportissimoProject.Commands;
using SportissimoProject.Commands;
using SportissimoProject.Repositories.Interfaces;

public class DeleteReservationCommandHandler : IDeleteReservationCommand
{
    private readonly IReservationRepository _reservationRepository;

    public DeleteReservationCommandHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<bool> ExecuteAsync(DeleteReservationCommand command)
    {
        var reservation = await _reservationRepository.GetByIdAsync(command.ReservationId);
        if (reservation == null)
        {
            return false; // La réservation n'existe pas
        }

        await _reservationRepository.DeleteAsync(command.ReservationId);
        return true; // Suppression réussie
    }
}
