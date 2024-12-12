using System;
using System.ComponentModel.DataAnnotations;

namespace SportissimoProject.Models
{
    // Enum pour définir la fréquence de paiement
    public enum FrequencePaiement
    {
        Mensuel,       // Paiement mensuel
        Trimestriel,   // Paiement trimestriel
        Annuel         // Paiement annuel
    }

    // Enum pour définir les types d'abonnement pour le gym
    public enum TypeAbonnement
    {
        SalleDeSport, // Salle de sport
        CrossFit,     // CrossFit
        Yoga,         // Yoga
        Boxe,         // Boxe
        Dance,        // Danse
        Fitness,      // Fitness
    }

    public class Abonnement
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Id généré automatiquement

        public string ClientId { get; set; } // Identifiant du client (clé étrangère), non requis

        public Client? Client { get; set; } // Relation avec Client, non requis

        [Required]
        public TypeAbonnement TypeGym { get; set; } // Type du gym (par exemple Salle de sport, CrossFit, etc.)

        [Required]
        public FrequencePaiement FrequencePaiement { get; set; } // Fréquence de paiement (Mensuel, Trimestriel, Annuel)

        [Required]
        public DateTime DateDebut { get; set; }

        [Required]
        public DateTime DateFin { get; set; }

        public double Prix { get; set; } // Prix de l'abonnement
    }
}
