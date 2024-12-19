using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SportissimoProject.Models
{
    public class Context : IdentityDbContext<Client> // ApplicationUser représente la classe d'utilisateur de Identity
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        // Définition des tables pour chaque modèle
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Abonnement> Abonnements { get; set; }
        public DbSet<Terrain> Terrains { get; set; }
       

        // Vous pouvez ajouter d'autres DbSet pour d'autres entités du projet


      
    }
}