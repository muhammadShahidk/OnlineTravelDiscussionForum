namespace OnlineTravelDiscussionForum.Dtos
{
    public class UserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }

    public class UserResponseDto:UserRequestDto
    {
        public int UserID { get; set; }
    }
}
