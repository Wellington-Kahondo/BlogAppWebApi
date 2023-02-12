using Autofac;
using BlogAppWebApi.Data;
using BlogAppWebApi.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace BlogAppUnitTest
{
    public class Tests
    {
       
        //public Tests(BlogDbContext context)
        //{
        //    _context = context;
        //}
        [SetUp]
        public void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BlogDbContext>().As<IBlogDbContext>().InstancePerTest();
            var container = builder.Build();
            TestExecutionContext.CurrentContext.Test.Properties.Set("AutofacContainer", container);
        }

        [Test]
        public async Task Test1()
        {
            BlogDbContext _context = new BlogDbContext(DbContextOptions<BlogDbContext>);
            var blogPost = new Post
            {
                Title = "blog",
                Content = "my first blog",
                CreatedTimeStamp = DateTime.Now,
                UserId = Guid.Parse("F0B739FF-700C-4E40-9C0B-08DAF3B593A3"),
            };

            var post = await _context.Posts.AddAsync(blogPost);
            _context.SaveChanges();

            //if (blogPost.TagIds != null)
            //{
            //    Stopwatch stopwatch = new Stopwatch();
            //    stopwatch.Start();

            //    foreach (var tagId in input.TagIds)
            //    {
            //        Task.WaitAll(_context.PostTags.AddAsync(new PostTag() { PostId = post.Entity.Id, TagId = tagId }).AsTask());
            //        _context.SaveChanges();
            //    }
            //    stopwatch.Stop();
            //    Debug.WriteLine("stopwatch: " + stopwatch.Elapsed.TotalMilliseconds);
            //    Console.WriteLine("stopwatch con: " + stopwatch.Elapsed.TotalMilliseconds);
            //}


            var postTags = await _context.PostTags
                .Where(x => x.PostId == post.Entity.Id)
                .Include(x => x.Tag)
                .Include(x => x.Post).ToListAsync();

            var postViewModel = new PostViewModel();
            if (postTags != null)
            {
                postViewModel.Tags = postTags.Select(pt => pt.Tag.Name).ToList();

                Assert.Pass("Test passed");
            }
            else
            {
                Assert.IsNotNull(postViewModel);
            }

            
        }
    }
}