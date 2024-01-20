﻿namespace Middagsasen.Planner.Api.Services.ResourceTypes
{
    public interface IResourceTypesService
    {
        Task<ResourceTypeResponse?> CreateResourceType(ResourceTypeRequest request);
        Task<ResourceTypeResponse?> DeleteResourceType(int id);
        Task<ResourceTypeResponse?> GetResourceTypeById(int id);
        Task<IEnumerable<ResourceTypeResponse>> GetResourceTypes();
        Task<ResourceTypeResponse?> UpdateResourceType(int id, ResourceTypeRequest request);
        Task<TrainingResponse> CreateTraining(int id, TrainingRequest request);
    }
}