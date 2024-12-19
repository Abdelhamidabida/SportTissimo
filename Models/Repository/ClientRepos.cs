//using Microsoft.EntityFrameworkCore;
//using SportissimoProject.Models;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SportissimoProject.Repository
//{
//    public class ClientRepo : IClientRepo
//    {
//        private readonly Context _context;
//        private readonly IConfiguration _configuration;

//        public ClientRepo(Context context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }
//        public string GetConnectionString()
//        {
//            return _configuration.GetConnectionString("DefaultConnection");
//        }
//        public async Task AddClientAsync(Client client)
//        {
//            _context.Clients.Add(client);
//            await _context.SaveChangesAsync();
//        }
//        public async Task<List<Client>> GetAll()
//        {
//            try
//            {
//                // Vérifiez si des valeurs NULL existent avant de retourner les clients
//                var clients = await _context.Clients
//                    .Where(c => c.ApplicationUserId != null && c.Nom != null && c.Prenom != null && c.Email != null)
//                    .ToListAsync();

//                return clients;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("An error occurred while retrieving clients: " + ex.Message);
//            }
//        }









//        public async Task<Client> Get(string id)
//        {
//            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
//        }

//        public async Task<Client> GetByName(string name)
//        {
//            return await _context.Clients.FirstOrDefaultAsync(c => c.Nom == name);
//        }

//        public async Task<Client> GetByEmail(string email)
//        {
//            return await _context.Clients.FirstOrDefaultAsync(c => c.Email == email);
//        }

//        public async Task<Client> Add(Client client)
//        {
//            _context.Clients.Add(client);
//            await _context.SaveChangesAsync();
//            return client;
//        }

//        public async Task<Client> Update(Client client)
//        {
//            _context.Clients.Update(client);
//            await _context.SaveChangesAsync();
//            return client;
//        }

//        public async Task<Client> Delete(string id)
//        {
//            var client = await Get(id);
//            if (client != null)
//            {
//                _context.Clients.Remove(client);
//                await _context.SaveChangesAsync();
//            }
//            return client;
//        }

       
//    }
//}
