using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineTravelDiscussionForum.Modals
{
    public class ApprovalRequest
    {

        [Key]
        public int RequestId { get; set; }

        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; } = null;

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
