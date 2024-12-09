namespace SportissimoProject.Models
{
    public class Admin
    {
        public string Id { get; set; } // Identifiant unique de l'administrateur
        public string Nom { get; set; } // Nom de l'administrateur
        public string Email { get; set; } // Email de l'administrateur
        public string MotDePasse { get; set; } // Mot de passe de l'administrateur

        // Relation avec les réservations
        public List<Reservation> Reservations { get; set; } // Liste des réservations que l'administrateur peut consulter
        public List<Client> Clients { get; set; }

        public List<Abonnement> Abonnements { get; set; }
    }
}
