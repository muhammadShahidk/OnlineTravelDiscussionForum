using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum.Data
{
    public class ForumDbContext : IdentityDbContext<ApplicationUser>
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        public DbSet<SensitiveKeyword> SensitiveKeywords { get; set; }
        public DbSet<BandUser> BandUsers { get; set; }
        public DbSet<Reply> Replies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(p => p.Posts)
                .WithOne(u => u.user)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(c => c.Comments)
               .WithOne(p => p.User)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ApplicationUser>()
         .HasMany(c => c.Replies)
         .WithOne(p => p.User)
         .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ApplicationUser>()
             .HasMany(c => c.ApprovalRequests)
             .WithOne(p => p.User)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.SensitiveKeywords)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.BandUsers)
                .WithOne(p => p.user)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(c => c.Comments)
                .WithOne(p => p.Post)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Post>()
            //  .HasMany(c => c.Comments)
            //  .WithOne(p => p.Post)
            //  .OnDelete(DeleteBehavior.Cascade);


        }
    }


}
