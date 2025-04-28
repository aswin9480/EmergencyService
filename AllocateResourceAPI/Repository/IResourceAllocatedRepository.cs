using System.Collections.Generic;
using System.Threading.Tasks;
using AllocateResourceAPI.Models;

namespace AllocateResourceAPI.Repositories
{
    public interface IResourceAllocatedRepository
    {
        Task<IEnumerable<ResourceAllocated>> GetResourceAllocationsAsync();

        Task<ResourceAllocated> GetResourceAllocationAsync(int allocationId);

        Task AddResourceAllocationAsync(ResourceAllocated resourceAllocated);

        Task UpdateResourceAllocationAsync(ResourceAllocated resourceAllocated);

       
        Task DeleteResourceAllocationAsync(int allocationId);

        Task<bool> ResourceAllocationExistsAsync(int allocationId);
    }
}
