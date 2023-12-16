using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineTravelDiscussionForum.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System;

namespace OnlineTravelDiscussionForum.Dtos
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }


    public class forgotPasswordDto
    {
        public string Email { get; set; }= string.Empty;
    }

    public class ResetPasswordDto
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

}

