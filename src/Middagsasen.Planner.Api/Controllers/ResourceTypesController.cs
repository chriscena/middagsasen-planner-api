﻿using Microsoft.AspNetCore.Mvc;
using Middagsasen.Planner.Api.Authentication;
using Middagsasen.Planner.Api.Services.Events;

namespace Middagsasen.Planner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ResourceTypesController : ControllerBase
    {
        public ResourceTypesController(IResourceTypesService resourceTypesService)
        {
            ResourceTypesService = resourceTypesService;
        }

        public IResourceTypesService ResourceTypesService { get; }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResourceTypeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await ResourceTypesService.GetResourceTypes());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventTemplateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var resourceType = await ResourceTypesService.GetResourceTypeById(id);
            return resourceType != null ? Ok(resourceType) : NotFound();
        }

        [HttpPost]
        [Authorize(Role = Roles.Administrator)]
        [ProducesResponseType(typeof(EventTemplateResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(ResourceTypeRequest request)
        {
            var resourceType = await ResourceTypesService.CreateResourceType(request);
            return Created($"/api/resourcetypes/{resourceType.Id}", resourceType);
        }

        [HttpPut("{id}")]
        [Authorize(Role = Roles.Administrator)]
        [ProducesResponseType(typeof(EventTemplateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, [FromBody]ResourceTypeRequest request)
        {
            var resourceType = await ResourceTypesService.UpdateResourceType(id, request);
            return (resourceType == null) ? NotFound() : Ok(resourceType);
        }

        [HttpDelete("{id}")]
        [Authorize(Role = Roles.Administrator)]
        [ProducesResponseType(typeof(EventTemplateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var resourceType = await ResourceTypesService.DeleteResourceType(id);
            return (resourceType == null) ? NotFound() : Ok(resourceType);
        }

        [HttpPost("{id}/training")]
        [ProducesResponseType(typeof(TrainingResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTraining(int id, [FromBody] TrainingRequest request)
        {
            var training = await ResourceTypesService.CreateTraining(id, request);
            return Created($"{training.ResourceTypeId}/training/{training.Id}", training);
        }
    }
}
