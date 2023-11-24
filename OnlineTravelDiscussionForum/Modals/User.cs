using System.ComponentModel.DataAnnotations;

namespace OnlineTravelDiscussionForum.Modals
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<ApprovalRequest> ApprovalRequests { get; set; } = new List<ApprovalRequest>();

        // You can add additional properties as needed

    }
}
