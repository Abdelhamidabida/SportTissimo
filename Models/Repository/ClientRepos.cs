using Microsoft.EntityFrameworkCore;
using SportissimoProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportissimoProject.Repository
{
    public class ClientRepo : IClientRepo
    {
        private readonly Context _context;

        public ClientRepo(Context context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAll()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> Get(string id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client> GetByName(string name)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Nom == name);
        }

        public async Task<Client> GetByEmail(string email)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Client> Add(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> Update(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> Delete(string id)
        {
            var client = await Get(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
            return client;
        }
    }
}
