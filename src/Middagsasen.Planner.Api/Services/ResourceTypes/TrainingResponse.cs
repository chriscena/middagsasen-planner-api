﻿namespace Middagsasen.Planner.Api.Services.ResourceTypes
{
    public class TrainingResponse
    {
        public int Id { get; set; }
        public int ResourceTypeId { get; set; }
        public string? ResourceTypeName { get; set; }
        public string? Confirmed { get; set; }
        public int? ConfirmedById { get; set; }
        public string? ConfirmedByName { get; set; }
        public bool? TrainingComplete { get; set; }
    }
}