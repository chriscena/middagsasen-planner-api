using Microsoft.EntityFrameworkCore;
using Middagsasen.Planner.Api.Data;

namespace Middagsasen.Planner.Api.Services.Events
{
    public class EventsService : IResourceTypesService
    {
        public EventsService(PlannerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public PlannerDbContext DbContext { get; }

        public async Task<IEnumerable<ResourceTypeResponse>> GetResourceTypes()
        {
            var resourceTypes = await DbContext.ResourceTypes.AsNoTracking().Where(r => !r.Inactive).ToListAsync();
            return resourceTypes.Select(Map).ToList();
        }

        public async Task<ResourceTypeResponse?> GetResourceTypeById(int id)
        {
            var resourceType = await DbContext.ResourceTypes.AsNoTracking().SingleOrDefaultAsync(r => r.ResourceTypeId == id);
            return (resourceType == null) ? null : Map(resourceType);
        }

        public async Task<ResourceTypeResponse> CreateResourceType(ResourceTypeRequest request)
        {
            var resourceType = new ResourceType
            {
                Name = request.Name,
                DefaultStaff = request.DefaultStaff,
            };

            DbContext.ResourceTypes.Add(resourceType);
            await DbContext.SaveChangesAsync();

            return Map(resourceType);
        }

        public async Task<ResourceTypeResponse?> UpdateResourceType(int id, ResourceTypeRequest request)
        {
            var resourceType = await DbContext.ResourceTypes.SingleOrDefaultAsync(r => r.ResourceTypeId == id);
            if (resourceType == null) { return null; }

            resourceType.Name = request.Name;
            resourceType.DefaultStaff = request.DefaultStaff;

            await DbContext.SaveChangesAsync();

            return Map(resourceType);
        }

        public async Task<ResourceTypeResponse> DeleteResourceType(int id)
        {
            var resourceType = await DbContext.ResourceTypes.SingleOrDefaultAsync(r => r.ResourceTypeId == id);
            if (resourceType == null) { return null; }

            resourceType.Inactive = true;

            await DbContext.SaveChangesAsync();
            return Map(resourceType);
        }



        private ResourceTypeResponse Map(ResourceType resourceType) => new ResourceTypeResponse
        {
            Id = resourceType.ResourceTypeId,
            Name = resourceType.Name,
            DefaultStaff = resourceType.DefaultStaff,
        };


    }
}
