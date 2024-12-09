namespace SportissimoProject.Models
{
    public class Terrain
    {
        public string Id { get; set; } // Identifiant unique du terrain

        public string Nom { get; set; } // Nom du terrain (par exemple "Terrain 1", "Terrain 2")

        public string Type { get; set; } // Type de terrain (ex : football, tennis, etc.)

        public double Prix { get; set; } // Prix de la réservation par heure

        public Boolean Disponibilite { get; set; } // Disponibilité du terrain (ex : "Libre", "Occupé", etc.)

        // Relation avec les réservations
        public List<Reservation> Reservations { get; set; } // Liste des réservations associées à ce terrain
    }
}
