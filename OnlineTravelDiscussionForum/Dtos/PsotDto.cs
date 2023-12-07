namespace OnlineTravelDiscussionForum.Dtos
{
    public class PostRequestDto
    {

        public string? Title { get; set; }
        public string? Content { get; set; }

    }

    public class PostResponseDto : PostRequestDto
    {
        public int PostID { get; set; }
        public string UserID { get; set; }
        public DateTime DateCreated { get; set; }
        public string Username { get; set; }
        public string name { get; set; }


    }
}
