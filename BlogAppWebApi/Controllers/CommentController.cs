using BlogAppWebApi.Interfaces;
using BlogAppWebApi.Services;
using BlogAppWebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("create-comment")]
        public async Task<ActionResult<CommentService>> CreateComment([FromBody] CommentViewModel input)
        {
            var comment = await _commentService.CreateComment(input);
            return Ok(comment);

        }

        [HttpGet("get-comment-by-id/{id}")]
        public async Task<ActionResult<CommentService>> GetCommentById(Guid id)
        {
            return Ok(await _commentService.GetCommentById(id));
        }

        [HttpGet("get-all-comments")]
        public async Task<ActionResult<List<CommentService>>> GetAllComments()
        {
            return Ok(await _commentService.GetAllComments());
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CommentService>> UpdateCommentById(Guid id, [FromBody] CommentViewModel input)
        {
            var updatedComment = await _commentService.UpdateComment(id,input);
            return Ok(updatedComment);
        }

        [HttpDelete("delete-comment-by-id/{id}")]
        public IActionResult DeleteCommentById(Guid id)
        {
            _commentService.DeleteComment(id);
            return Ok();
        }
    }
}
