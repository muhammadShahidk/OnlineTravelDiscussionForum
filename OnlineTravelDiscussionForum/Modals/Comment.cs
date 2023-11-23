using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace OnlineTravelDiscussionForum.Modals
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateCommented { get; set; }

        public int UserID { get; set; }

        public User User { get; set; }
        
        public int PostID { get; set; }

        public Post Post { get; set; }
    }

    
}
