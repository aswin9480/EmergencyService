using System;
using System.Collections.Generic;

namespace VictimAPI.Models;
public partial class IncidentTbl
{
    public int  Id { get; set; }

    public string? Name { get; set; }

    public string? IncidentType { get; set; }

    public string? Location { get; set; }

    public DateTime? IncidentDateTime { get; set; }

    public string? Severity{ get; set; }

    public string? Description { get; set; }
}
