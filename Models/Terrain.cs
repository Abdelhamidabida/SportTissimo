using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SportissimoProject.Models
{
    public class Terrain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Nom { get; set; } // Nom du terrain (par exemple "Terrain 1", "Terrain 2")

        public string Type { get; set; } // Type de terrain (ex : football, tennis, etc.)

        public double Prix { get; set; } // Prix de la réservation par heure

        public Boolean Disponibilite { get; set; } // Disponibilité du terrain (ex : "Libre", "Occupé", etc.)

         
    }
}
