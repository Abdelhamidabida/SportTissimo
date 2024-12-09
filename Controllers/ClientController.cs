using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportissimoProject.Models;
using SportissimoProject.Repository;

namespace SportissimoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepo repository;

        public ClientController(IClientRepo repository)
        {
            this.repository = repository;
        }

        // Récupérer tous les clients
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await repository.GetAll());
        }

        // Ajouter un client
        [HttpPost]
        public async Task<IActionResult> Ajouter(Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Vérifier si le client existe déjà (par exemple, vérifier l'email)
                if (await repository.GetByEmail(client.Email) != null)
                {
                    ModelState.AddModelError(string.Empty, "Client existe déjà !!");
                    return BadRequest(ModelState);
                }

                await repository.Add(client);
                return CreatedAtAction(nameof(GetByID), new { id = client.Id }, client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Récupérer un client par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var client = await repository.Get(id);
            if (client == null)
                return NotFound("Client inexistant");
            return Ok(client);
        }

        // Mettre à jour un client
        [HttpPut]
        public async Task<IActionResult> Edit(Client client)
        {
            var existingClient = await repository.Get(client.Id);
            if (existingClient == null)
                return NotFound("Client inexistant");

            await repository.Update(client);
            return Ok(client);
        }

        // Supprimer un client
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var client = await repository.Delete(id);
            if (client == null)
                return NotFound("Client inexistant");
            return Ok(client);
        }
    }
}
