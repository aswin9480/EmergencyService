namespace ResourceAvailableAPI.Models {
    public partial class Resource
    {

        public int ResourceId { get; set; }

        public string? ResourceType { get; set; }

        public string? ResourceName { get; set; }

        public int NumberOfAvailable { get; set; }

        public int TotalAvailable { get; set; }

        public string? ContactInfo { get; set; }
    }
}
