using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportissimoProject.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Le prenom est requis.")]
        [StringLength(100, ErrorMessage = "Le prenom ne peut pas dépasser 100 caractères.")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "L'email doit être valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit comporter au moins 6 caractères.")]
        public string MotDePasse { get; set; }

        
        public int NbReservation { get; set; }


       

        
    }
}
