using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum.Data
{
    public class ForumDbContext : IdentityDbContext<ApplicationUser>
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
    }
}
