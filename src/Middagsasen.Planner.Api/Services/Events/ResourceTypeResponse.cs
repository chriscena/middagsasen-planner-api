﻿namespace Middagsasen.Planner.Api.Services.Events
{
    public class ResourceTypeResponse
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; } = null!;
        public int DefaultStaff { get; internal set; }
    }
}