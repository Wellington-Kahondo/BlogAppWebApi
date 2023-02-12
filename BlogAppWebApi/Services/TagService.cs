using AutoMapper;
using BlogAppWebApi.Data;
using BlogAppWebApi.Helpers;
using BlogAppWebApi.Interfaces;
using BlogAppWebApi.Models;
//using BlogAppWebApi.Interfaces;
using BlogAppWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Services
{
    public class TagService : ITagService
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;
        public TagService(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<TagViewModel>> CreateTag(TagViewModel input)
        {
            var entity = _mapper.Map<Tag>(input);
            var tag = await _context.Tags.AddAsync(entity);
            _context.SaveChanges();

            return _mapper.Map<TagViewModel>(tag.Entity);

        }

        public async Task<TagViewModel> GetTagById(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
                throw new Exception("Tag not found");

            return _mapper.Map<TagViewModel>(tag);
        }

        public async Task<List<TagViewModel>> GetAllTags()
        {
            var tags = await _context.Tags.ToListAsync();
            return _mapper.Map<List<TagViewModel>>(tags);
        }

        public async Task<TagViewModel> UpdateTag(Guid id, TagViewModel input)
        {

            var currentTag = await _context.Tags.FindAsync(id);
            if (currentTag == null)
                throw new Exception("Tag not found");

            var tag = _mapper.Map<Post>(input);

            var properties = tag.GetType().GetProperties();
            foreach (var property in properties)
            {
                //validations
                if (ExtensionHelper.ValidateProperties(property.Name.ToLower(), "id"))
                    continue;

                var inputValue = property.GetValue(tag);

                if (ExtensionHelper.NullCheck(inputValue))
                    continue;

                if (inputValue is string)
                    if (string.IsNullOrEmpty(inputValue as string))
                        continue;


                var currentValue = property.GetValue(tag);
                if (!inputValue?.Equals(currentValue) ?? false)
                {
                    property.SetValue(tag, inputValue, null);
                    _context.Entry(tag).Property(property.Name).IsModified = true;
                }
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<TagViewModel>(tag);
            //var tag = await _context.Tags.FindAsync(input.Id);
            //if (tag == null)
            //    throw new Exception("Tag not found");

            //tag = _mapper.Map<Tag>(input);
            //var updatedTag = _context.Tags.Update(tag);
            //_context.SaveChanges();

            //return _mapper.Map<TagViewModel>(updatedTag.Entity);
        }

        public void DeleteTag(Guid id)
        {
            var tag = _context.Tags.Find(id);

            _context.Tags.Remove(tag);
            _context.SaveChanges();

        }
    }
}
