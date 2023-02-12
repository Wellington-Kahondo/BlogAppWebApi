using BlogAppWebApi.Interfaces;
using BlogAppWebApi.Services;
using BlogAppWebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("create-tag")]
        public async Task<IActionResult> CreateTag([FromBody] TagViewModel input)
        {
            var tag = await _tagService.CreateTag(input);
            return Ok(tag);

        }

        [HttpGet("get-tag-by-id/{id}")]
        public async Task<ActionResult<TagViewModel>> GetTagById(Guid id)
        {
            return Ok(await _tagService.GetTagById(id));
        }

        [HttpGet("get-all-tags")]
        public async Task<ActionResult<List<TagViewModel>>> GetAllTags()
        {
            var tags = await _tagService.GetAllTags();
            return Ok(tags);
        }

        [HttpPut("update-tag")]
        public async Task<ActionResult<TagViewModel>> UpdateTagById([FromBody] TagViewModel input)
        {
            var updatedTag = await _tagService.UpdateTag(input);
            return Ok(updatedTag);
        }

        [HttpDelete("delete-tag-by-id/{id}")]
        public IActionResult DeleteTagById(Guid id)
        {
            _tagService.DeleteTag(id);
            return Ok();
        }
    }
}
