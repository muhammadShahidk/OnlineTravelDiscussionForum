namespace OnlineTravelDiscussionForum.Dtos
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Username { get; set; } 
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
    }

    public class forgotPasswordDto
    {
        public string Username { get; set; }
    }
}
