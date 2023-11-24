using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum.Data
{
    public class ForumDbContext : IdentityDbContext<ApplicationUser>
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasMany(p => p.Posts)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
               .HasMany(c => c.Comments)
               .WithOne(p => p.User)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
             .HasMany(c => c.ApprovalRequests)
             .WithOne(p => p.User)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(c => c.Comments)
                .WithOne(p => p.Post)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }


}
