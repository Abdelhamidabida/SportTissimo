using SportissimoProject.Models;

namespace SportissimoProject.Repositories.Interfaces
{
    public interface ITerrainRepository
    {
        Task<IEnumerable<Terrain>> GetAllAsync(); // Récupérer tous les terrains
        Task<Terrain> GetByIdAsync(string id);   // Récupérer un terrain par ID (string)
        Task AddAsync(Terrain terrain);           // Ajouter un nouveau terrain
        Task UpdateAsync(Terrain terrain);        // Mettre à jour un terrain
        Task DeleteAsync(string id);              // Supprimer un terrain
    }
}
