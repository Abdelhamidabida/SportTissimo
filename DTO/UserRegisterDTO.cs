using System.ComponentModel.DataAnnotations;

public class UserRegisterDto
{
    [Required(ErrorMessage = "L'email est obligatoire.")]
    [EmailAddress(ErrorMessage = "Le format de l'email est invalide.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
    public string Nom { get; set; }

    [Required(ErrorMessage = "Le prénom est obligatoire.")]
    [StringLength(100, ErrorMessage = "Le prénom ne peut pas dépasser 100 caractères.")]
    public string Prenom { get; set; }

    [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
    public string MotDePasse { get; set; }
}
