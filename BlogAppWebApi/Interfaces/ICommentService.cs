using BlogAppWebApi.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppWebApi.Interfaces
{
    public interface ICommentService
    {
        Task<ActionResult<CommentViewModel>> CreateComment(CommentViewModel input);

        Task<CommentViewModel> GetCommentById(Guid id);

        Task<List<CommentViewModel>> GetAllComments();

        Task<CommentViewModel> UpdateComment(Guid id, CommentViewModel input);

        void DeleteComment(Guid id);
    }
}
