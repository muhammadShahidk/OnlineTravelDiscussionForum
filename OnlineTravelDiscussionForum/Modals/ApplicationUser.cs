using Microsoft.AspNetCore.Identity;

namespace OnlineTravelDiscussionForum.Modals
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Reply> Replies { get; set; } = new List<Reply>();

        public ICollection<ApprovalRequest> ApprovalRequests { get; set; } = new List<ApprovalRequest>();
        public ICollection<SensitiveKeyword> SensitiveKeywords { get; set; } = new List<SensitiveKeyword>();
        public ICollection<BandUser> BandUsers { get; set; } = new List<BandUser>();

    }
}
