using Microsoft.EntityFrameworkCore;
using SportissimoProject.Models;
using SportissimoProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportissimoProject.Repositories
{
    public class AbonnementRepository : IAbonnementRepository
    {
        private readonly Context _context;

        public AbonnementRepository(Context context)
        {
            _context = context;
        }

        public async Task<Abonnement> GetByIdAsync(string id)
        {
            // Récupérer un abonnement par son ID
            return await _context.Abonnements.FindAsync(id);
        }

        public async Task<IEnumerable<Abonnement>> GetAllAsync()
        {
            // Récupérer tous les abonnements
            return await Task.FromResult(_context.Abonnements.ToList());
        }

        public async Task AddAsync(Abonnement abonnement)
        {
            // Ajouter un nouvel abonnement
            await _context.Abonnements.AddAsync(abonnement);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Abonnement abonnement)
        {
            // Mettre à jour un abonnement existant
            var existingAbonnement = await _context.Abonnements.FindAsync(abonnement.Id);
            if (existingAbonnement != null)
            {
                existingAbonnement.ClientId = abonnement.ClientId;
                existingAbonnement.TypeGym = abonnement.TypeGym;
                existingAbonnement.FrequencePaiement = abonnement.FrequencePaiement;
                existingAbonnement.DateDebut = abonnement.DateDebut;
                existingAbonnement.DateFin = abonnement.DateFin;
                existingAbonnement.Prix = abonnement.Prix;

                _context.Abonnements.Update(existingAbonnement);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Abonnement non trouvé.");
            }
        }

        public async Task DeleteAsync(string id)
        {
            // Supprimer un abonnement
            var abonnement = await _context.Abonnements.FindAsync(id);
            if (abonnement != null)
            {
                _context.Abonnements.Remove(abonnement);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Abonnement non trouvé.");
            }
        }
        public async Task<List<Abonnement>> GetByClientIdAsync(string clientId)
        {
            return await _context.Abonnements
                                 .Where(a => a.ClientId == clientId)
                                 .ToListAsync();
        }
        public async Task<List<Abonnement>> GetByTypeAsync(TypeAbonnement typeGym)
        {
            return await _context.Abonnements
                                 .Where(a => a.TypeGym == typeGym)
                                 .ToListAsync();
        }

    }
    

}
