using SportissimoProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportissimoProject.Repositories.Interfaces
{
    public interface IAbonnementRepository
    {
        Task<Abonnement> GetByIdAsync(string id);
        Task<IEnumerable<Abonnement>> GetAllAsync();
        Task AddAsync(Abonnement abonnement);
        Task UpdateAsync(Abonnement abonnement);
        Task DeleteAsync(string id);
        Task<List<Abonnement>> GetByClientIdAsync(string clientId);
        Task<List<Abonnement>> GetByTypeAsync(TypeAbonnement typeGym);

        
    }
}
