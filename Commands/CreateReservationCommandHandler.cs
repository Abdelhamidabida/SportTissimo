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

        
        terrain.Disponibilite = false;

        // Mettre à jour le terrain dans la base de données
        await _terrainRepository.UpdateAsync(terrain);

        return reservation;
    }
}
