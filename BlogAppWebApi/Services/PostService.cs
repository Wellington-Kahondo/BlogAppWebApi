using AutoMapper;
using BlogAppWebApi.Data;
using BlogAppWebApi.Helpers;
using BlogAppWebApi.Interfaces;
using BlogAppWebApi.Models;
using BlogAppWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace BlogAppWebApi.Services
{
    public class PostService : IPostService
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;
        public PostService(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostViewModel> CreatePost(PostViewModel input)
        {
            var blogPost = _mapper.Map<Post>(input);
            var post = await _context.Posts.AddAsync(blogPost);
            _context.SaveChanges();

            if (input.TagIds != null)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                foreach (var tagId in input.TagIds)
                {
                    Task.WaitAll(_context.PostTags.AddAsync(new PostTag() { PostId = post.Entity.Id, TagId = tagId }).AsTask());
                    _context.SaveChanges();
                }
                stopwatch.Stop();
                Debug.WriteLine("stopwatch: " + stopwatch.Elapsed.TotalMilliseconds);
                Console.WriteLine("stopwatch con: " + stopwatch.Elapsed.TotalMilliseconds);
            }

            var postViewModel = _mapper.Map<PostViewModel>(post.Entity);

            var postTags = await _context.PostTags
                .Where(x => x.PostId == post.Entity.Id)
                .Include(x => x.Tag)
                .Include(x => x.Post).ToListAsync();

            if (postTags != null)
                postViewModel.Tags = postTags.Select(pt => pt.Tag.Name).ToList();

            return postViewModel;
        }

        public async Task<PostViewModel> GetPostById(Guid id)
        {
            var post = await _context.Posts
                .Where(x => x.Id == id)
                .Include(x => x.User)
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync();

            if (post == null)
                throw new Exception("Post not found.");

            //map post to view model
            var postViewModel = _mapper.Map<PostViewModel>(post);

            //check if there are tags for the blog post
            var postTags = await _context.PostTags
                .Where(x => x.PostId == post.Id)
                .ToListAsync();

            if (postTags != null)
                postViewModel.Tags = postTags.Select(x => x.Tag.Name).ToList();

            //check if there are comments for the post
            var comments = await _context.Comments
                .Where(x => x.PostId == post.Id)
                .ToListAsync();

            if (comments != null)
                postViewModel.Comments = comments.Select(x => x.Content).ToList();

            return postViewModel;
        }

        public async Task<List<PostViewModel>> GetAllPosts()
        {

            var posts = await _context.Posts
                .Include(x => x.User)
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .Include(p => p.Comments)
                .ToListAsync();

            if (posts.Count == 0)
                throw new Exception("No post was found.");

            var postVMList = _mapper.Map<List<PostViewModel>>(posts);

            foreach (var postViewModel in postVMList)
            {
                var post = posts.First(p => p.Id == postViewModel.Id);
                postViewModel.Author = post.User.FirstName;
                postViewModel.Tags = post.PostTags.Select(x => x.Tag.Name).ToList();
                postViewModel.Comments = post.Comments.Select(x => x.Content).ToList();
            }

            return postVMList;
        }

        //public async Task<PostViewModel> UpdatePost(PostViewModel input)
        //{
        //    var post = await _context.Posts.FindAsync(input.Id);
        //    if (post == null)
        //        throw new Exception("Post not found.");

        //    if (input.TagIds != null)
        //    {
        //        using (var transaction = await _context.Database.BeginTransactionAsync())
        //        {
        //            try
        //            {
        //                foreach (var tagId in input.TagIds)
        //                {
        //                    var postTagAvailability = await _context.PostTags.AnyAsync(x => x.TagId == tagId && x.PostId == post.Id);

        //                    if (!postTagAvailability)
        //                    {
        //                        await _context.PostTags.AddAsync(new PostTag() { PostId = post.Id, TagId = tagId });
        //                    }
        //                }
        //                await _context.SaveChangesAsync();
        //                transaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //                throw new Exception(ex.Message);
        //            }
        //        }

        //    }

        //    post = _mapper.Map<Post>(input);
        //    var updatedPost = _context.Posts.Update(post);
        //    _context.SaveChanges();

        //    return _mapper.Map<PostViewModel>(updatedPost.Entity);
        //}

        public async Task<PostViewModel> UpdatePost(Guid id, PostViewModel input)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                throw new Exception("Post not found");

            var mappedPost = _mapper.Map<Post>(input);

            var properties = mappedPost.GetType().GetProperties();
            foreach (var property in properties)
            {
                //validations
                if (ExtensionHelper.In(property.Name.ToLower(), "id", "user", "userid", "createdtimestamp", "post"))
                    continue;

                var inputValue = property.GetValue(mappedPost);
               
                if (ExtensionHelper.NullCheck(inputValue))
                        continue;

                if (inputValue is string)
                    if (string.IsNullOrEmpty(inputValue as string))
                        continue;


                var currentValue = property.GetValue(post);
                if (!inputValue?.Equals(currentValue) ?? false)
                {
                    property.SetValue(post, inputValue, null);
                    _context.Entry(post).Property(property.Name).IsModified = true;
                }
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<PostViewModel>(post);
        }

        public async void DeletePost(Guid id)
        {
            var post =await _context.Posts.FindAsync(id);

            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
