using Microsoft.AspNetCore.Mvc;
using Middagsasen.Planner.Api.Authentication;

namespace Middagsasen.Planner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class EventsController : ControllerBase
    {
    }
}
