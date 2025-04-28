using System;
using System.Collections.Generic;

namespace AllocateResourceAPI.Models;
public partial class ResourceAllocated
{
    public int IncidentId { get; set; }

    public int AllocationId { get; set; }

    public string IncidentType { get; set; }

    public string Severity { get; set; }

    public string Location { get; set; }
    public int ResourceId { get; set; }

    public string ResourceName { get; set; }

    public int QuantityAllocated { get; set; }

   
}
