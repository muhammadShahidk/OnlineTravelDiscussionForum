using Microsoft.EntityFrameworkCore;

namespace OnlineTravelDiscussionForum.Modals
{
    public class ForumDataContext:DbContext
    {
        public ForumDataContext(DbContextOptions<ForumDataContext> options) : base(options) { }
        
       public DbSet<User> users { get; set; }
    }
}
