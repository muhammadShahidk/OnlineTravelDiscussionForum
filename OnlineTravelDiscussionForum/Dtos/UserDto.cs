namespace OnlineTravelDiscussionForum.Dtos
{
    public class UserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

    }

    public class UserResponseDto:UserRequestDto
    {
        public List<string> Rools { get; set; } = new List<string>();
        public int UserID { get; set; }
    }
}
