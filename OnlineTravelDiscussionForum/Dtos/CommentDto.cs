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
    }

}
