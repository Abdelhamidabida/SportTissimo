using SportissimoProject.Models;
using System.ComponentModel.DataAnnotations;

public class Coach
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string Nom { get; set; }

    // Liste des clients qui ont réservé ce coach
    public List<Client> Clients { get; set; }
    [Required]
    [Range(0.0, double.MaxValue, ErrorMessage = "Le prix doit être un nombre positif.")]
    public double Prix { get; set; }
}