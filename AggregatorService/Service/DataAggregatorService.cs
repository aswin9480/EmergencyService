using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AggregatorService.Models;
using VictimAPI.Models;
using ResourceAvailableAPI.Models;
using System.Linq;
using AllocateResourceAPI.Models;
using System;

namespace AggregatorService.Services
{
    public class DataAggregatorService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DataAggregatorService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // Method to fetch and combine data from the Victim API and Resource API
        public async Task<List<CombinedIncidentResource>> GetCombinedData()
        {
            try
            {
                // Fetch data from Victim API
                var victimResponse = await _httpClient.GetStringAsync(_configuration["VictimAPI:BaseUrl"] + "/Incidents");
                if (string.IsNullOrEmpty(victimResponse))
                {
                    Console.WriteLine("Failed to fetch incidents data from Victim API.");
                    return new List<CombinedIncidentResource>();
                }

                var incidents = JsonConvert.DeserializeObject<List<IncidentTbl>>(victimResponse);
                if (incidents == null || !incidents.Any())
                {
                    Console.WriteLine("No incidents found in Victim API.");
                    return new List<CombinedIncidentResource>();
                }

                // Fetch data from Resource API
                var resourceResponse = await _httpClient.GetStringAsync(_configuration["ResourceAPI:BaseUrl"] + "/ResourceAllocated");
                if (string.IsNullOrEmpty(resourceResponse))
                {
                    Console.WriteLine("Failed to fetch resources data from Resource API.");
                    return new List<CombinedIncidentResource>();
                }

                var resources = JsonConvert.DeserializeObject<List<ResourceAllocated>>(resourceResponse);
                if (resources == null || !resources.Any())
                {
                    Console.WriteLine("No resources found in Resource API.");
                    return new List<CombinedIncidentResource>();
                }

                // Combine data where IncidentTbl.Id equals Resource.IncidentId
                var combinedData = (from incident in incidents
                                    join resource in resources
                                    on incident.Id equals resource.IncidentId
                                    select new CombinedIncidentResource
                                    {
                                        incidentId = incident.Id,
                                        Name = incident.Name,
                                        IncidentType = incident.IncidentType,
                                        Location = incident.Location,
                                        IncidentDateTime = incident.IncidentDateTime,
                                        Severity = incident.Severity,
                                        allocationId = resource.AllocationId,
                                        ResourceName = resource.ResourceName,
                                        quantityAllocated = resource.QuantityAllocated
                                    }).ToList();

                return combinedData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCombinedData: {ex.Message}");
                return new List<CombinedIncidentResource>();
            }
        }
        
    }
}
