using Microsoft.AspNetCore.Mvc;
using VivesActivities.Services;

namespace VivesActivities.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly LocationService _locationService;

        public LocationsController(LocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public IActionResult Find()
        {
            var locations = _locationService.Find();
            return Ok(locations);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var location = _locationService.Get(id);
            return Ok(location);
        }
    }
}
