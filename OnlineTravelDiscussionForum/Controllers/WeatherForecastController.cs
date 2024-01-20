using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineTravelDiscussionForum.OtherObjects;
using Microsoft.Identity.Web.Resource;

namespace OnlineTravelDiscussionForum.Controllers
{
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
       {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            return Ok(Summaries);
        }

        [HttpGet]
        [Route("GetUserRole")]
        [Authorize(Roles = StaticRoles.USER)]
        public IActionResult GetUserRole()
        {
            return Ok(Summaries);
        }

        [HttpGet]
        [Route("GetAdminRole")]
        [Authorize(Roles = StaticRoles.ADMIN)]
        public IActionResult GetAdminRole()
        {
            return Ok(Summaries);
        }

        [HttpGet]
        [Route("GetModeratorRole")]
        [Authorize(Roles = StaticRoles.MODERATOR)]
        public IActionResult GetOwnerRole()
        {
            return Ok(Summaries);
        }


    }
}

