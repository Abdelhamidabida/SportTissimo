namespace SportissimoProject.Models
{
    public class Reservation
    {
        public string Id { get; set; } // Identifiant unique de la réservation

        // Clé étrangère pour le client
        public string ClientId { get; set; }
        // Référence au client qui a fait la réservation
        public Client Client { get; set; }

        // Clé étrangère pour le terrain réservé
        public string TerrainId { get; set; }
        // Référence au terrain réservé
        public Terrain Terrain { get; set; }

        // Date de la réservation
        public DateTime DateReservation { get; set; }
        // Date de début de la réservation
        public DateTime DateDebut { get; set; }
        // Date de fin de la réservation
        public DateTime DateFin { get; set; }
    }
}
