using System.ComponentModel.DataAnnotations;

namespace OnlineTravelDiscussionForum.Modals
{
    public class BandUser
    {
        [Key]
        public string BandId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public BandStatus Status { get; set; }
    }

 public   enum BandStatus
    {
        Active,
        Inactive,
        Pending
    }
}
