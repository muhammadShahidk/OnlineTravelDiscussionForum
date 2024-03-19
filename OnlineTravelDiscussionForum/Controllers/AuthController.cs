using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.OtherObjects;

namespace OnlineTravelDiscussionForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        // Route For Seeding my roles to DB
        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            
            var seerRoles = await _authService.SeedRolesAsync();

            return Ok(seerRoles);
        }


        // Route -> Register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {

            var registerResult = await _authService.RegisterAsync(registerDto);

            if (registerResult.IsSucceed)
                return Ok(registerResult);

            return BadRequest(registerResult);
        }


        // Route -> Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            //check if use is approved by admin
            try
            {
                var latestAprovalRequest = await _authService.CheckIfUserIsApproved(loginDto);
                if (!latestAprovalRequest)
                {
                    return Unauthorized("Your account is not approved yet");
                }

            }
            catch (Exception ex)
            {

                return Unauthorized(ex.Message);
            }


            var loginResult = await _authService.LoginAsync(loginDto);

            if (loginResult.IsSucceed)
                return Ok(loginResult);

            return Unauthorized(loginResult);
        }



        // Route -> make user -> admin
        [HttpPost]
        [Route("make-admin")]

        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeAdminAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        // Route -> make user -> owner
        [HttpPost]
        [Route("make-moderator")]
        [Authorize(Roles = $"{StaticRoles.ADMIN}")]

        public async Task<IActionResult> MakeOwner([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeOwnerAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }


        //MAKE USER -> USER IF HE IS ADMIN OR MODERATOR
        [HttpPost]
        [Route("make-user")]
        [Authorize(Roles = $"{StaticRoles.ADMIN}")]
        public async Task<IActionResult> MakeUser([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeUserAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }




        [HttpPost]
        [Route("get-user-rols")]
        //[Authorize(Roles = $"{StaticRoles.ADMIN}")]

        public async Task<IActionResult> GetUserRols()
        {
            var operationResult = await _authService.GetUserRolsAsync();

            if (operationResult != null)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }
    }
}

