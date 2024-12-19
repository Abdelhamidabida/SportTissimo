using Microsoft.AspNetCore.Identity;
using SportissimoProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Client : IdentityUser
{
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
    public string? Nom { get; set; }

    [StringLength(100, ErrorMessage = "Le prénom ne peut pas dépasser 100 caractères.")]
    public string? Prenom { get; set; }

    public int NbReservation { get; set; }

    // Navigation vers les abonnements, etc.
    [JsonIgnore]
    public List<Abonnement> Abonnements { get; set; }
}
