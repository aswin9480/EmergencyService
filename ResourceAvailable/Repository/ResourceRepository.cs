using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourceAvailableAPI.Data;
using ResourceAvailableAPI.Models;

namespace ResourceAvailableAPI.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ResourceContext _context;

        public ResourceRepository(ResourceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Resource>> GetResourcesAsync()
        {
            return await _context.Resources.ToListAsync();
        }

        public async Task<Resource> GetResourceAsync(int id)
        {
            return await _context.Resources.FindAsync(id);
        }

        public async Task AddResourceAsync(Resource resource)
        {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateResourceAsync(Resource resource)
        {
            _context.Entry(resource).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task DecrementResourceQuantityAsync(int resourceId, int quantity)
        {
            var resource = await _context.Resources.FindAsync(resourceId);
            if (resource == null)
            {
                throw new KeyNotFoundException("Resource not found.");
            }

            if (resource.NumberOfAvailable < quantity)
            {
                throw new InvalidOperationException("Insufficient resources available.");
            }

            resource.NumberOfAvailable -= quantity;
            _context.Entry(resource).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task DeleteResourceAsync(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource != null)
            {
                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ResourceExistsAsync(int id)
        {
            return await _context.Resources.AnyAsync(e => e.ResourceId == id);
        }
    }
}
