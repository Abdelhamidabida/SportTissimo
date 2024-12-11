using Microsoft.EntityFrameworkCore; // Ajout de cette directive
using SportissimoProject.Models;
using SportissimoProject.Repositories.Interfaces;

public class TerrainRepository : ITerrainRepository
{
    private readonly Context _context;

    public TerrainRepository(Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Terrain>> GetAllAsync()
    {
        return await _context.Terrains.ToListAsync();  // ToListAsync() nécessite Microsoft.EntityFrameworkCore
    }

    public async Task<Terrain> GetByIdAsync(string id)
    {
        try
        {
            return await _context.Terrains.FirstOrDefaultAsync(t => t.Id == id);
        }
        catch (Exception ex)
        {
            // Log l'exception ou gère-la selon tes besoins
            throw new Exception("Erreur lors de la récupération du terrain", ex);
        }
    }


    public async Task UpdateAsync(Terrain terrain)
    {
        _context.Terrains.Update(terrain);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Terrain terrain)
    {
        await _context.Terrains.AddAsync(terrain);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var terrain = await _context.Terrains.FindAsync(id);
        if (terrain != null)
        {
            _context.Terrains.Remove(terrain);
            await _context.SaveChangesAsync();
        }
    }
}
