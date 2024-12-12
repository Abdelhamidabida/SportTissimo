public class UpdateReservationCommand
{
    public string ReservationId { get; set; }
    public string ClientId { get; set; }
    public string TerrainId { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
}
