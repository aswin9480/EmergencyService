using System.Collections.Generic;
using System.Threading.Tasks;
using ResourceAvailableAPI.Models;

namespace ResourceAvailableAPI.Repositories
{
    public interface IResourceRepository
    {
        Task<IEnumerable<Resource>> GetResourcesAsync();
        Task<Resource> GetResourceAsync(int id);
        Task AddResourceAsync(Resource resource);
        Task UpdateResourceAsync(Resource resource);
        Task DecrementResourceQuantityAsync(int resourceId, int quantity);
        Task DeleteResourceAsync(int id);
        Task<bool> ResourceExistsAsync(int id);
    }
}
