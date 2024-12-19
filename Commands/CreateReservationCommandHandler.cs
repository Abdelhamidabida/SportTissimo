using SportissimoProject.Commands;
using SportissimoProject.Repositories.Interfaces;

public class CreateReservationCommandHandler : ICreateReservationCommand
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ITerrainRepository _terrainRepository;

    public CreateReservationCommandHandler(IReservationRepository reservationRepository, ITerrainRepository terrainRepository)
    {
        _reservationRepository = reservationRepository;
        _terrainRepository = terrainRepository;
    }

    public async Task<Reservation> ExecuteAsync(CreateReservationCommand command)
    {
        // Vérifier si le terrain existe
        var terrain = await _terrainRepository.GetByIdAsync(command.TerrainId);
        if (terrain == null)
        {
            throw new InvalidOperationException("Terrain non trouvé.");
        }

        // Vérifier si la date est dans le passé
        if (command.DateDebut < DateTime.Now)
        {
            throw new InvalidOperationException("Vous ne pouvez pas réserver une date passée.");
        }

        // Ajouter 1 heure à DateDebut
        command.DateDebut = command.DateDebut.AddHours(1);
        command.DateFin = command.DateFin.AddHours(1);
        // Vérifier les conflits de réservation
        var reservations = await _reservationRepository.GetReservationsByTerrainAndDateAsync(command.TerrainId, command.DateDebut.Date);

        var hasConflict = reservations.Any(r =>
            (command.DateDebut < r.DateFin && command.DateFin > r.DateDebut)
        );

        if (hasConflict)
        {
            throw new InvalidOperationException("Ce créneau est déjà réservé pour le terrain sélectionné.");
        }

        // Créer la réservation
        var reservation = new Reservation
        {
            Id = Guid.NewGuid().ToString(),
            ClientId = command.ClientId,
            TerrainId = command.TerrainId,
            DateDebut = command.DateDebut,
            DateFin = command.DateFin,
            DateReservation = DateTime.Now
        };

        // Ajouter la réservation à la base de données
        await _reservationRepository.AddAsync(reservation);

        // Journalisation (optionnelle)
        Console.WriteLine($"Réservation créée pour le terrain {command.TerrainId} à {command.DateDebut}.");

        return reservation;
    }



}
