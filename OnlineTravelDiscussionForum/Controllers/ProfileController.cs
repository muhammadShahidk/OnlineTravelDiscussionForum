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

        public ProfileController(UserManager<ApplicationUser> userManager, ForumDbContext context, IMapper mapper, IUserService userService)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }


        [HttpPut("User")]
        [Authorize(Roles = $"{StaticRoles.USER},{StaticRoles.ADMIN},{StaticRoles.MODERATOR}")]
        public async Task<IActionResult> UpdateUser( [FromBody] UpdateUserDetailsDto userRequestDto)
        {
            var user = await _userService.GetCurrentUser();
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.FirstName = userRequestDto.FirstName;
            user.LastName = userRequestDto.LastName;
            await _userManager.SetEmailAsync(user,userRequestDto.Email);
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
    

        [HttpGet("forgot-password")]
        public async Task<ActionResult> ForgotPasswordToken(forgotPasswordDto forgotPassword)
        {
            //var LogedinUserID = CurrentUserID();
            //if (LogedinUserID == null)
            //{
            //    return BadRequest("first login to get reset Token");
            //}
            var user = await _userManager.FindByNameAsync(forgotPassword.Username);
            if (user == null)
            {
                return BadRequest("user not found");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var link = Url.Action("ResetPassword", "Profile", new { userId = user.Id, token }, Request.Scheme);



            return Ok(new {link = link});
        }

        [HttpPut("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ResetPasswordDto resetPasswordDto)
        {

            var user = await _userManager.FindByNameAsync(resetPasswordDto.Username);
            if (user == null)
            {
                return BadRequest("user not found");
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.ResetToken, resetPasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest("password reset failed");
            }
            return Ok("password reset successfully");
        }

       
        
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromQuery] ResetPasswordDto2 model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

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
