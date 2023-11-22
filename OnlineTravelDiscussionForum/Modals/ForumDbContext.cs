using Microsoft.EntityFrameworkCore;

namespace OnlineTravelDiscussionForum.Modals
{
    public class ForumDbContext:DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options) { }
        
       public DbSet<User> users { get; set; }
    }
}
