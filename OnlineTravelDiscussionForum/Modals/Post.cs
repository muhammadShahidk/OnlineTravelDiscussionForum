using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineTravelDiscussionForum.Modals
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

       

        public string UserID { get; set; }
        public  ApplicationUser user { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
