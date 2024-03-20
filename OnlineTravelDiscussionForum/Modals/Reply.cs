using System.ComponentModel.DataAnnotations;

namespace OnlineTravelDiscussionForum.Modals
{
    public class Reply
    {
        [Key]
        public int ReplyId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime DateCreateAt { get; set; } = DateTime.Now;
        public DateTime? DateUpdateAt { get; set; }

        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

        // Foreign key for the comment this reply belongs to
        public int CommentId { get; set; }
        public Comment Comment { get; set; }


        // Optional: if you want to support nested replies
        //public int? ParentReplyId { get; set; }
        //public Reply ParentReply { get; set; }
    }
}
