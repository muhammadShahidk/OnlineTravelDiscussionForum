using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum.Interfaces
{
    public interface IProfileService
    {
        Task<bool> SendForgotPasswordEmail(ApplicationUser user);
        Task<bool> SendForgotPasswordEmail(ApplicationUser user, string resetUrl);
        Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto);
        Task<bool> UpdateProfile(UpdateUserDetailsDto updateProfileDto);

    }
}
