using AutoMapper;
using BlogAppWebApi.Data;
using BlogAppWebApi.Interfaces;
using BlogAppWebApi.Models;
using BlogAppWebApi.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BlogAppWebApi.Services
{
    public class CommentService : ICommentService
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;
        public CommentService(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<CommentViewModel>> CreateComment(CommentViewModel input)
        {
            var entity = _mapper.Map<Comment>(input);
            var comment = await _context.Comments.AddAsync(entity);
            _context.SaveChanges();

            return _mapper.Map<CommentViewModel>(comment.Entity);
        }

        public async Task<CommentViewModel> GetCommentById(Guid id)
        {
            var comment = await _context.Comments
                .Include(x =>x.User)
                .SingleOrDefaultAsync(x=>x.Id == id);

            if (comment == null)
                throw new Exception("Comment not found");

            var test = _mapper.Map<CommentViewModel>(comment);
            return test;
        }

        public async Task<List<CommentViewModel>> GetAllComments()
        {
            var comments = await _context.Comments.Include(c => c.User).ToListAsync();
            return _mapper.Map<List<CommentViewModel>>(comments);
        }

        public async Task<CommentViewModel> UpdateComment(Guid id, CommentViewModel input)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                throw new Exception("Comment not found");

            var mappedComment = _mapper.Map<Comment>(input);

            var properties = mappedComment.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.Name.ToLower() == "id" || 
                    property.Name.ToLower() == "user" || 
                    property.Name.ToLower() == "post")
                    continue;


                var inputValue = property.GetValue(mappedComment);

                if (inputValue is Guid)               
                    if ((Guid)inputValue == Guid.Empty)
                        continue; 

                var currentValue = property.GetValue(comment);
                if (!inputValue.Equals(currentValue))
                {
                    property.SetValue(comment, inputValue, null);
                    _context.Entry(comment).Property(property.Name).IsModified = true;
                }
            }

            _context.Update(comment);
            await _context.SaveChangesAsync();
            return _mapper.Map<CommentViewModel>(comment);
        }

        public void DeleteComment(Guid id)
        {
            var comment = _context.Comments.Find(id);

            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
