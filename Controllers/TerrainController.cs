using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportissimoProject.DTO;
using SportissimoProject.Models;
using SportissimoProject.Repositories.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class TerrainController : ControllerBase
{
    private readonly ITerrainRepository _terrainRepository;

    public TerrainController(ITerrainRepository terrainRepository)
    {
        _terrainRepository = terrainRepository;
    }
 
    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var terrains = await _terrainRepository.GetAllAsync();
        return Ok(terrains);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)  // id en string
    {
        var terrain = await _terrainRepository.GetByIdAsync(id);
        if (terrain == null)
            return NotFound("Terrain non trouvé.");
        return Ok(terrain);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Terrain terrain)
    {
        await _terrainRepository.AddAsync(terrain);
        return CreatedAtAction(nameof(GetById), new { id = terrain.Id }, terrain);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTerrain(string id, [FromBody] TerrainUpdateDTO terrainUpdateDto)
    {
        var terrain = await _terrainRepository.GetByIdAsync(id);
        if (terrain == null)
        {
            return NotFound(); // Si le terrain n'existe pas
        }

        // Mise à jour de la disponibilité si la valeur est présente dans le DTO
        if (terrainUpdateDto.Disponibilite.HasValue)
        {
            terrain.Disponibilite = terrainUpdateDto.Disponibilite.Value;
        }

        // Mise à jour du prix si la valeur est présente dans le DTO
        if (terrainUpdateDto.Prix.HasValue)
        {
            terrain.Prix = terrainUpdateDto.Prix.Value;
        }

        // Sauvegarder les modifications dans la base de données
        await _terrainRepository.UpdateAsync(terrain);

        return NoContent(); // Réponse sans contenu pour indiquer que tout s'est bien passé
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)  // id en string
    {
        var existingTerrain = await _terrainRepository.GetByIdAsync(id);
        if (existingTerrain == null)
            return NotFound("Terrain non trouvé.");

        await _terrainRepository.DeleteAsync(id);
        return NoContent();
    }
}
