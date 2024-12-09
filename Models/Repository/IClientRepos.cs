using SportissimoProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportissimoProject.Repository
{
    public interface IClientRepo
    {
        Task<List<Client>> GetAll(); // Récupérer tous les clients
        Task<Client> Get(string id); // Récupérer un client par ID
        Task<Client> GetByName(string name); // Récupérer un client par nom
        Task<Client> GetByEmail(string email); // Récupérer un client par email
        Task<Client> Add(Client client); // Ajouter un client
        Task<Client> Update(Client client); // Mettre à jour un client
        Task<Client> Delete(string id); // Supprimer un client
    }
}
