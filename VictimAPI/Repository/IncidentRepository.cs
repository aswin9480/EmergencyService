using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VictimAPI.Data;
using VictimAPI.Models;

namespace VictimAPI.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly VictimContext _context;

        public IncidentRepository(VictimContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IncidentTbl>> GetIncidentsAsync()
        {
            return await _context.IncidentTbls.ToListAsync();
        }

        public async Task<IncidentTbl> GetIncidentAsync(int id)
        {
            return await _context.IncidentTbls.FindAsync(id);
        }

        public async Task AddIncidentAsync(IncidentTbl incident)
        {
            _context.IncidentTbls.Add(incident);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIncidentAsync(IncidentTbl incident)
        {
            _context.Entry(incident).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIncidentAsync(int id)
        {
            var incident = await _context.IncidentTbls.FindAsync(id);
            if (incident != null)
            {
                _context.IncidentTbls.Remove(incident);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IncidentExistsAsync(int id)
        {
            return await _context.IncidentTbls.AnyAsync(e => e.Id == id);
        }
    }
}
