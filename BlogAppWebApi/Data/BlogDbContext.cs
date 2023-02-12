using BlogAppWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>()
                .HasKey(bpt => new { bpt.PostId, bpt.TagId });

            modelBuilder.Entity<PostTag>()
                .HasOne(bpt => bpt.Post)
                .WithMany(b => b.PostTags)
                .HasForeignKey(bpt => bpt.PostId);

            modelBuilder.Entity<PostTag>()
                .HasOne(bpt => bpt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(bpt => bpt.TagId);

            //modelBuilder.Entity<Comment>()
            //    .HasOne(c => c.User)
            //    .WithMany(u => u.Comments)
            //    .HasForeignKey(c => c.UserId)
            //    .Will;



            modelBuilder.Entity<User>()
            .HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
    }
}
