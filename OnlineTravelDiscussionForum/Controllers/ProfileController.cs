using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;
using OnlineTravelDiscussionForum.OtherObjects;
using System.Security.Claims;

namespace OnlineTravelDiscussionForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ForumDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IProfileService _ProfileService;

        public ProfileController(UserManager<ApplicationUser> userManager, ForumDbContext context, IMapper mapper, IUserService userService, IProfileService profileService)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _ProfileService = profileService;
        }


        [HttpPut("User")]
        [Authorize(Roles = $"{StaticRoles.USER},{StaticRoles.ADMIN},{StaticRoles.MODERATOR}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDetailsDto userRequestDto)
        {
            var user = await _userService.GetCurrentUser();
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.FirstName = userRequestDto.FirstName;
            user.LastName = userRequestDto.LastName;
            await _userManager.SetEmailAsync(user, userRequestDto.Email);
            //user.Email = userRequestDto.Email;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }

            return BadRequest(result.Errors);
        }


        //user profile handling

        [HttpPut("password")]
        [Authorize(Roles = $"{StaticRoles.USER},{StaticRoles.ADMIN}")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var LogedinUser = await _userService.GetCurrentUser();
            if (LogedinUser == null)
            {
                return BadRequest("first login to see posts");
            }
            var user = await _userManager.FindByIdAsync(LogedinUser.Id);
            if (user == null)
            {
                return BadRequest("user not found");
            }
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest("password change failed Wrong current password");
            }
            return Ok("password changed successfully");
        }


        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPasswordToken(forgotPasswordDto forgotPassword)
        {
//dummy token generation if we do not have email in production
            if (forgotPassword.Username != null)
            {
                var _user = await _userManager.FindByNameAsync(forgotPassword.Username);
                if (_user == null)
                {
                    return BadRequest("username is not registred ");
                }
                var _token = await _userManager.GeneratePasswordResetTokenAsync(_user);

                return Ok(new { userid = _user.Id, token = _token });
            }

//actual token generation if we have email in production
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user == null)
            {
                return BadRequest("email is not registred ");
            }
            //var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //var link = Url.Action("ResetPassword", "Profile", new { userId = user.Id, token }, Request.Scheme);
            
            var result = await _ProfileService.SendForgotPasswordEmail(user);
            if (!result)
            {
                return BadRequest("email is not registred ");
            }

            return Ok("email is sent to your email ");
        }

        
        [HttpPut("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ResetPasswordDto resetPasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(resetPasswordDto.UserId);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);

                    if (result.Succeeded)
                    {
                        return Ok(new { Message = "Password reset successful." });
                    }
                    else
                    {
                        return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
                    }
                }
                else
                {
                    return BadRequest(new { Errors = new[] { "User not found." } });
                }
            }

            return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

    }
}
