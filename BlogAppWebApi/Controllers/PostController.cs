using BlogAppWebApi.Interfaces;
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
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("create-post")]
        public async Task<ActionResult<PostViewModel>> CreatePost([FromBody] PostViewModel input)
        {
            var postViewModel =await _postService.CreatePost(input);
            return Ok(postViewModel);
        }

        [HttpGet("get-post-by-id/{id}")]
        public async Task<ActionResult<PostViewModel>> GetPost(Guid id)
        {
            var postViewModel =await _postService.GetPostById(id);
            return Ok(postViewModel);
        }

        [HttpGet("get-all-posts")]
        public async Task<ActionResult<List<PostViewModel>>> GetAllPosts()
        {
            var posts =await _postService.GetAllPosts();
            return Ok(posts);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<PostViewModel>> UpdatePost(Guid id, [FromBody] PostViewModel input)
        {
            var postViewModel =await _postService.UpdatePost(id, input);
            return Ok(postViewModel);

        }

        [HttpDelete("delete-by-id/{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            _postService.DeletePost(id);
            return NoContent();
        }
    }
}
