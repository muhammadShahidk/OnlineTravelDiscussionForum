using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;
using OnlineTravelDiscussionForum.OtherObjects;

namespace OnlineTravelDiscussionForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IProfileService _ProfileService;

        public BanUserController(UserManager<ApplicationUser> userManager, IMapper mapper, IUserService userService, IProfileService profileService, ForumDbContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userService = userService;
            _ProfileService = profileService;
        }

        //get all bened users
        [HttpGet]
        //[Authorize(Roles = $"{StaticRoles.MODERATOR}")]
        public async Task<IActionResult> GetBannedUsers()
        {
            var userBanned = await _userService.GetAllBannedUsers();
            if (userBanned == null)
            {
                return NotFound("No banned users found");
            }
            return Ok(userBanned);
        }


        //get all users status is there any banned user
        [HttpGet("all/status")]
        //[Authorize(Roles = $"{StaticRoles.MODERATOR}")]
        public async Task<IActionResult> GetAllUsersStatus()
        {
            List<BandUsersStatusResponceDto> userBanned = await _userService.GetAllUsersStatus();
            if (userBanned == null)
            {
                return NotFound("No banned users found");
            }
            return Ok(userBanned);
        }


        [HttpGet("history/{userId}")]
        //[Authorize(Roles = $"{StaticRoles.MODERATOR}")]
        public async Task<IActionResult> GetBannedUserHistory( string userId)
        {
            var userBanned = await _userService.GetBanndUserHistery(userId);
            if (userBanned == null)
            {
                return NotFound("No banned users found");
            }
            return Ok(userBanned);
        }

        [HttpGet("history")]
        //[Authorize(Roles = $"{StaticRoles.MODERATOR}")]
        public async Task<IActionResult> GetBannedUserUniqueHistory()
        {
            var userBanned = await _userService.GetAllBannedUniqueHistery();
            if (userBanned == null)
            {
                return NotFound("No banned users found");
            }
            return Ok(userBanned);
        }

        //ban user
        [HttpPost()]
        //[Authorize(Roles = $"{StaticRoles.MODERATOR}")]
        public async Task<IActionResult> BanUser(bandUserRequestDto banUser)
        {
            try
            {
                bandUserResponceDto result = await _userService.BanUser(banUser);
                if (result != null)
                {
                    return Ok(new
                    {
                        message = "User banned successfully",
                        user = result
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message);

                throw;
            }
            return BadRequest("Something went wrong");
        }

        //change band status
        [HttpPut()]
        //[Authorize(Roles = $"{StaticRoles.MODERATOR}")]
        public async Task<IActionResult> ChangeBandStatus(ChangeBandStatusDto banUser)
        {
            try
            {
                bandUserResponceDto result = await _userService.ChangeBandStatus(banUser);
                if (result != null)
                {
                    return Ok(new
                    {
                        message = $"User banned satatus updated to {result.Status} successfully",
                        user = result
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

                throw;
            }
            return BadRequest("Something went wrong");
        }
    }
}
