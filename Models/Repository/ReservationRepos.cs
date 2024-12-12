using Microsoft.EntityFrameworkCore;
using SportissimoProject.Models;
using SportissimoProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportissimoProject.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly Context _context;

        public ReservationRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                                 .Include(r => r.Client)
                                 .Include(r => r.Terrain)
                                 .ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(string id)
        {
            return await _context.Reservations
                                 .Include(r => r.Client)
                                 .Include(r => r.Terrain)
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var reservation = await GetByIdAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByClientAsync(string clientId)
        {
            return await _context.Reservations
                                 .Include(r => r.Terrain)
                                 .Where(r => r.ClientId == clientId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByTerrainAsync(string terrainId)
        {
            return await _context.Reservations
                                 .Include(r => r.Client)
                                 .Where(r => r.TerrainId == terrainId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Reservations
                                 .Include(r => r.Client)
                                 .Include(r => r.Terrain)
                                 .Where(r => r.DateDebut < endDate && r.DateFin > startDate)
                                 .ToListAsync();
        }

        public async Task<bool> IsTerrainAvailableAsync(string terrainId, DateTime startDate, DateTime endDate)
        {
            var overlappingReservation = await _context.Reservations
                                                       .Where(r => r.TerrainId == terrainId
                                                                   && r.DateDebut < endDate
                                                                   && r.DateFin > startDate)
                                                       .FirstOrDefaultAsync();
            return overlappingReservation == null;
        }
    }
}
