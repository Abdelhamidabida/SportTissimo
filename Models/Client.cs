using System.ComponentModel.DataAnnotations;

namespace SportissimoProject.Models
{
    public class Client
    {
        [Required(ErrorMessage = "L'ID est requis.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "L'email doit être valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit comporter au moins 6 caractères.")]
        public string MotDePasse { get; set; }

        
        public int NbReservation { get; set; }


       

        
    }
}
