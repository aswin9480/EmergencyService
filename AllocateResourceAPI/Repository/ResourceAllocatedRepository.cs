using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AllocateResourceAPI.Data;
using AllocateResourceAPI.Models;

namespace AllocateResourceAPI.Repositories
{
    public class ResourceAllocatedRepository : IResourceAllocatedRepository
    {
        private readonly ResourceAllocationContext _context;

        public ResourceAllocatedRepository(ResourceAllocationContext context)
        {
            _context = context;
        }

        // Get all resource allocations
        public async Task<IEnumerable<ResourceAllocated>> GetResourceAllocationsAsync()
        {
            return await _context.ResourceAllocatedTbl.ToListAsync();
        }

        // Get a specific resource allocation by AllocationId
        public async Task<ResourceAllocated> GetResourceAllocationAsync(int allocationId)
        {
            return await _context.ResourceAllocatedTbl.FindAsync(allocationId);
        }

        // Add a new resource allocation
        public async Task AddResourceAllocationAsync(ResourceAllocated resourceAllocated)
        {
            _context.ResourceAllocatedTbl.Add(resourceAllocated);
            await _context.SaveChangesAsync();
        }

        // Update an existing resource allocation
        public async Task UpdateResourceAllocationAsync(ResourceAllocated resourceAllocated)
        {
            _context.Entry(resourceAllocated).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete a resource allocation by AllocationId
        public async Task DeleteResourceAllocationAsync(int allocationId)
        {
            var resourceAllocated = await _context.ResourceAllocatedTbl.FindAsync(allocationId);
            if (resourceAllocated != null)
            {
                _context.ResourceAllocatedTbl.Remove(resourceAllocated);
                await _context.SaveChangesAsync();
            }
        }

        // Check if a resource allocation exists by AllocationId
        public async Task<bool> ResourceAllocationExistsAsync(int allocationId)
        {
            return await _context.ResourceAllocatedTbl.AnyAsync(e => e.AllocationId == allocationId);
        }
    }
}
