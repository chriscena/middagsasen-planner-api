using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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

        public async Task<ResourceTypeResponse?> DeleteResourceType(int id)
        {
            var resourceType = await DbContext.ResourceTypes.SingleOrDefaultAsync(r => r.ResourceTypeId == id);
            if (resourceType == null) { return null; }

            resourceType.Inactive = true;

            await DbContext.SaveChangesAsync();
            return Map(resourceType);
        }

        public async Task<IEnumerable<EventResponse?>> GetEvents()
        {
            var events = await DbContext.Events
                .Include(e => e.Resources)
                    .ThenInclude(r => r.Shifts)
                        .ThenInclude(s => s.User)
                .AsNoTracking()
                .ToListAsync();

            return events.Select(Map).ToList();
        }

        private ResourceTypeResponse Map(ResourceType resourceType) => new ResourceTypeResponse
        {
            Id = resourceType.ResourceTypeId,
            Name = resourceType.Name,
            DefaultStaff = resourceType.DefaultStaff,
        };

        private EventResponse Map(Event evnt) => new EventResponse 
        {
            Id = evnt.EventId,
            Name = evnt.Name,
            StartTime = evnt.StartTime.ToSimpleIsoString(),
            EndTime = evnt.EndTime.ToSimpleIsoString(),
            Resources = evnt.Resources.Select(Map).ToList()
        };

        private ResourceResponse Map(EventResource resource) => new ResourceResponse
        {
            Id = resource.EventResourceId,
            ResourceType = Map(resource.ResourceType),
            StartTime = resource.StartTime.ToSimpleIsoString(),
            EndTime = resource.EndTime.ToSimpleIsoString(),
            MinimumStaff = resource.MinimumStaff,
            Shifts = resource.Shifts.Select(Map).ToList()
        };

        private ShiftResponse Map(EventResourceUser shift) => new ShiftResponse
        {
            Id = shift.EventResourceUserId,
            User = Map(shift.User),
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Comment = shift.Comment,
        };

        private ShiftUserResponse Map(User user) => new ShiftUserResponse
        {
            Id = user.UserId,
            PhoneNumber = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = $"{user.FirstName ?? ""} {user.LastName ?? ""}".Trim(),
        };
    }
}
