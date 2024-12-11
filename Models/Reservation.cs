using SportissimoProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Reservation
{
    public string Id { get; set; } // Identifiant unique de la réservation

    [ForeignKey("ClientId")] // Assurer la relation avec Client
    public string ClientId { get; set; }
    public Client Client { get; set; }

    [ForeignKey("TerrainId")] // Assurer la relation avec Terrain
    public string TerrainId { get; set; }
    public Terrain Terrain { get; set; }

    public DateTime DateReservation { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
}
