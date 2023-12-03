using System.ComponentModel.DataAnnotations;

namespace OnlineTravelDiscussionForum.Modals
{
    public class SensitiveKeyword
    {
        [Key]
        public int Id { get; set; }
        public string Keyword { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; } = null;
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
