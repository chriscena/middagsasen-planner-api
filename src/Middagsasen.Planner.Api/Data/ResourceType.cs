﻿namespace Middagsasen.Planner.Api.Data
{
    public class ResourceType
    {
        public int ResourceTypeId { get; set; }
        public string Name { get; set; } = null!;
        public int DefaultStaff { get; set; } = 1;
        public bool Inactive { get; set; }
    }
}