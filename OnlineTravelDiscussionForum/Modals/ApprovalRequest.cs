using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineTravelDiscussionForum.Modals
{
    public class ApprovalRequest
    {
        
            [Key]
            public int RequestId { get; set; }

            public int UserID { get; set; }

            [ForeignKey("UserID")]
            public User User { get; set; }

            [Required]
            public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending;
        
    }

    public enum ApprovalStatus
    {
        Aproved,
        Rejected,
        Pending
    }
}
