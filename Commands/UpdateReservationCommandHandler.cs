using SportissimoProject.Commands;
using SportissimoProject.Repositories.Interfaces;

public class UpdateReservationCommandHandler : IUpdateReservationCommand
{
    private readonly IReservationRepository _reservationRepository;

    public UpdateReservationCommandHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Reservation> ExecuteAsync(UpdateReservationCommand command)
    {
        // Recherche de la réservation à mettre à jour
        var reservation = await _reservationRepository.GetByIdAsync(command.ReservationId);
        if (reservation == null)
        {
            return null; // La réservation n'existe pas
        }

        // Mise à jour de la réservation
        reservation.ClientId = command.ClientId;
        reservation.TerrainId = command.TerrainId;
        reservation.DateDebut = command.DateDebut;
        reservation.DateFin = command.DateFin;

        // Sauvegarde de la réservation mise à jour
        await _reservationRepository.UpdateAsync(reservation);

        return reservation;
    }
}
