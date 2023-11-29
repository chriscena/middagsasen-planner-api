﻿namespace Middagsasen.Planner.Api.Services.Events
{
    public class ResourceResponse
    {
        public int Id { get; set; }
        public ResourceTypeResponse ResourceType { get; set; }
        public string StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;
        public int MinimumStaff { get; set; }
        public IEnumerable<ShiftResponse> Shifts { get; set; } = null!;
    }
}