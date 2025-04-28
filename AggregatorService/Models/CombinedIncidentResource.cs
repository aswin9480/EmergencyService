namespace AggregatorService.Models
{
    public class CombinedIncidentResource
    {
        public int incidentId { get; set; }
        public string Name { get; set; }
        public string IncidentType { get; set; }
        public string Location { get; set; }
        public DateTime? IncidentDateTime { get; set; }
        public string Severity { get; set; }

        // Resource API fields

        public int allocationId { get; set; }
        public string ResourceName { get; set; }
        public int quantityAllocated { get; set; }
    }

}
