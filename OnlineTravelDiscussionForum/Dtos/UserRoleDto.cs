namespace OnlineTravelDiscussionForum.Dtos
{
    public class UserRoleDto
    {
        public string? name { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public List<string> Rools { get; set; }
    }
}