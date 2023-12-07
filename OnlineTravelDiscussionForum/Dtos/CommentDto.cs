namespace OnlineTravelDiscussionForum.Dtos
{
    public class CommentRequestDto
    {
        public string Content { get; set; }
    }

    public class CommentResposnceDto:CommentRequestDto
    {
        public int CommentId { get; set; }
        public string UserID { get; set; }
        public DateTime DateCreateAt { get; set; } = DateTime.Now;
        public string Username { get; set; }
        public string name { get; set; }



    }

}
