using System.Collections.Generic;
using System.Threading.Tasks;
using VictimAPI.Models;

namespace VictimAPI.Repositories
{
    public interface IIncidentRepository
    {
        Task<IEnumerable<IncidentTbl>> GetIncidentsAsync();
        Task<IncidentTbl> GetIncidentAsync(int id);
        Task AddIncidentAsync(IncidentTbl incident);
        Task UpdateIncidentAsync(IncidentTbl incident);
        Task DeleteIncidentAsync(int id);
        Task<bool> IncidentExistsAsync(int id);
    }
}
