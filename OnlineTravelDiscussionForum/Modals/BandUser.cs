using System.ComponentModel.DataAnnotations;

namespace OnlineTravelDiscussionForum.Modals
{
    public class BandUser
    {
        [Key]
        public int Id { get; set; }
        public string UserID { get; set; }
        public ApplicationUser user { get; set; }

        public DateTime startDate { get; set; } = DateTime.Now;
        public DateTime endDate { get; set; }
        public BandStatus Status { get; set; }

    }

    public enum BandStatus
    {
        Inactive=0,
        Active,

    }
}
