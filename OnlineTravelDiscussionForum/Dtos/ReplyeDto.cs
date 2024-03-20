using System.ComponentModel.DataAnnotations;

namespace OnlineTravelDiscussionForum.Dtos
{


    public class ReplyRequestDto
    {
        [Required]
        public string Content { get; set; }

        //[Required]
        //public string UserID { get; set; }

        //[Required]
        //public int CommentId { get; set; }
    }



    public class ReplyResponseDto
    {
        public int ReplyId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreateAt { get; set; }
        public DateTime? DateUpdateAt { get; set; }
        public string UserID { get; set; }
        public int CommentId { get; set; }
    }
}
